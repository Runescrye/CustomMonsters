using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Linq;
using Masterplan.Data;
using Masterplan.Tools;
using System.Drawing;
using MonsterPorter.Properties;
using System.Drawing.Imaging;

namespace MonsterPorter.Renderers
{
	class FoundryVTTRenderer : BaseRenderer
	{
		private static Dictionary<Image, string> EncodedImageCache { get; set; } = new Dictionary<Image, string>();

		public const string TABLE = @"<table style=""font-size: 16pt; border-color: #BBBBBB; border-style: solid; border-width: 1px; border-collapse: collapse; table-layout: fixed; width: 99%;"">";
		public const string TBODY = @"<tbody>";
		public const string TR_CREATURE = @"<TR style=""background-color: #364F27; color: #FFFFFF;"">";
		public const string TD = @"<TD style=""padding-top: 2px; padding-bottom: 2px; vertical-align: top;"">";
		public const string TD_COLSPAN2 = @"<TD colspan=2; style=""padding-top: 2px; padding-bottom: 2px; vertical-align: top;"">";
		public const string TD_COLSPAN3 = @"<TD colspan=3; style=""padding-top: 2px; padding-bottom: 2px; vertical-align: top;"">";
		public const string TR_SHADED = @"<TR style=""padding-top: 2px; padding-bottom: 2px; vertical-align: top; background-color: #9FA48D;"">";
		public const string TR_ATWILL = @"<TR style="" background-color: #238E23; color: #FFFFFF;"">";
		public const string TR_ENCOUNTER = @"<TR style = ""background-color: #8B0000; color: #FFFFFF;"">";
		public const string TR_DAILY = @"<TR style = ""background-color: #333333; color: #FFFFFF;"">";
		public const string TR = @"<TR style=""background-color: #E1E7C5;"">";

		public Dictionary<Guid, Library> LibraryByCreature { get; private set; }


		public FoundryVTTRenderer(Dictionary<Guid, Library> libraryByCreature)
        {
			LibraryByCreature = libraryByCreature;
        }

        public override string Render(ICreature creature)
		{
			return Concatenate(CreatureAsHTML(creature));
		}

		public List<string> CreatureAsHTML(ICreature creature)
		{
			List<string> content = new List<string>();

			string title = creature.Name;

			content.Add(TABLE);
			content.Add(TBODY);
			content.Add(TR_CREATURE);

			content.Add(TD_COLSPAN2);
			content.Add("<B>" + WebUtility.HtmlEncode(title) + "</B>");
			content.Add("<BR>");
			content.Add(creature.Phenotype);
			content.Add("</TD>");

			content.Add(TD);
			content.Add("<B>" + WebUtility.HtmlEncode(CreatureLevelText(creature)) + "</B>");
			content.Add("<BR>");
			content.Add(XPByLevel(creature.Level) + " XP");
			content.Add("</TD>");

			content.Add("</TR>");

			content.Add(TR);
			ParseHP(creature, content);
			ParseInitiative(creature, content);
			content.Add("</TR>");

			content.Add(TR);
			ParseDefences(creature, content);
			ParsePerception(creature, content);
			content.Add("</TR>");

			content.Add(TR);

			ParseMovement(creature, content);

			int rows = (creature.Role.Flag == RoleFlag.Standard) ? 1 : 2;

			if (!string.IsNullOrEmpty(creature.Resist) || !string.IsNullOrEmpty(creature.Vulnerable) || !string.IsNullOrEmpty(creature.Immune) || (creature.DamageModifiers.Count != 0))
				rows += 1;

			ParseSenses(creature, content, rows);
			content.Add("</TR>");

			ParseResistances(creature, content);
			ParseActionPointsAndSaves(creature, content);
			ParsePowers(creature, content);
			ParseSkills(creature, content);
			ParseAbility(creature, content);

			var alignment = creature.Alignment;
			if (string.IsNullOrEmpty(creature.Alignment))
				alignment = "Unaligned";

			AddSection("Alignment", alignment, content);
			AddSection("Languages", creature.Languages, content);
			AddSection("Equipment", creature.Equipment, content);
			AddSection("Tactics", creature.Tactics, content);

			if (LibraryByCreature.ContainsKey(creature.ID))
			{
				content.Add(TR);
				content.Add(TD_COLSPAN3);
				content.Add("<i>");
				content.Add(WebUtility.HtmlEncode(LibraryByCreature[creature.ID].Name));
				content.Add("</i>");
				content.Add("</TD>");
				content.Add("</TR>");
			}

			content.Add("</tbody>");
			content.Add("</table>");

			return content;
		}

        private void ParseAbility(ICreature creature, List<string> content)
        {
			content.Add(TR_SHADED);

			content.Add(TD);
			content.Add("<B>Str</B>: " + ability(creature.Strength, creature.Level));
			content.Add("<BR>");
			content.Add("<B>Con</B>: " + ability(creature.Constitution, creature.Level));
			content.Add("</TD>");

			content.Add(TD);
			content.Add("<B>Dex</B>: " + ability(creature.Dexterity, creature.Level));
			content.Add("<BR>");
			content.Add("<B>Int</B>: " + ability(creature.Intelligence, creature.Level));
			content.Add("</TD>");

			content.Add(TD);
			content.Add("<B>Wis</B>: " + ability(creature.Wisdom, creature.Level));
			content.Add("<BR>");
			content.Add("<B>Cha</B>: " + ability(creature.Charisma, creature.Level));
			content.Add("</TD>");

			content.Add("</TR>");
		}

        private void ParseSkills(ICreature creature, List<string> content)
        {
			string skills = creature.Skills;
			if ((skills != null) && (skills.ToLower().Contains("perception")))
			{
				// Remove the Perception skill
				string str = "";
				string[] tokens = skills.Split(new string[] { ",", ";" }, StringSplitOptions.RemoveEmptyEntries);
				foreach (string token in tokens)
				{
					if (token.ToLower().Contains("perception"))
						continue;

					if (str != "")
						str += "; ";

					str += token;
				}
				skills = str;
			}
			if (skills == null)
				skills = "";

			AddSection("Skills", skills, content);
		}

        private void AddSection(string sectionName, string sectionValue, List<string> result)
        {
			if (string.IsNullOrEmpty(sectionValue))
				return;

			result.Add(TR);
			result.Add(TD_COLSPAN3);
			result.Add($"<B>{sectionName} {WebUtility.HtmlEncode(sectionValue)}");
			result.Add("</TD>");
			result.Add("</TR>");
		}

        private void ParseActionPointsAndSaves(ICreature creature, List<string> content)
        {
			int saveMod = 0;
			int AP = 0;

			switch (creature.Role.Flag)
			{
				case RoleFlag.Elite:
					saveMod = 2;
					AP = 1;
					break;
				case RoleFlag.Solo:
					saveMod = 5;
					AP = 2;
					break;
			}

			if (AP != 0)
			{
				content.Add(TR);
				content.Add(TD_COLSPAN2);
				content.Add($"<B>Saving Throws</B> +{saveMod} <B>Action Points</B> {AP}");
				content.Add("</TD>");
				content.Add("</TR>");
			}
		}

        private void ParsePowers(ICreature creature, List<string> content)
        {
			Dictionary<CreaturePowerCategory, List<string>> parsedPowers = new Dictionary<CreaturePowerCategory, List<string>>();
			var categories = Enum.GetValues(typeof(CreaturePowerCategory));
			foreach (CreaturePowerCategory category in categories)
            {
				parsedPowers[category] = new List<string>();
            }

			var parsedTraits = parsedPowers[CreaturePowerCategory.Trait];

			creature.Auras.ForEach(x => ParseAura(x, parsedTraits));

			if (creature.Regeneration != null)
			{
				ParseRegeneration(creature.Regeneration, parsedTraits);
			}

			parsedPowers[CreaturePowerCategory.Trait] = parsedTraits;

			foreach (var power in creature.CreaturePowers)
            {
                if (!parsedPowers.ContainsKey(power.Category))
                {
                    parsedPowers[power.Category] = new List<string>();
                }

				PowerToHTML(power, parsedPowers[power.Category]);
            }

			AddPowerCategory(CreaturePowerCategory.Trait, parsedPowers[CreaturePowerCategory.Trait], content);
			AddPowerCategory(CreaturePowerCategory.Standard, parsedPowers[CreaturePowerCategory.Standard], content);
			AddPowerCategory(CreaturePowerCategory.Move, parsedPowers[CreaturePowerCategory.Move], content);
			AddPowerCategory(CreaturePowerCategory.Minor, parsedPowers[CreaturePowerCategory.Minor], content);
			AddPowerCategory(CreaturePowerCategory.Free, parsedPowers[CreaturePowerCategory.Free], content);
			AddPowerCategory(CreaturePowerCategory.Triggered, parsedPowers[CreaturePowerCategory.Triggered], content);
			AddPowerCategory(CreaturePowerCategory.Other, parsedPowers[CreaturePowerCategory.Other], content);
		}

        private void AddPowerCategory(CreaturePowerCategory powerCategory, List<string> parsedPowers, List<string> content)
        {
			if (parsedPowers.Count == 0)
				return;

			content.Add(TR_CREATURE);
			content.Add(TD_COLSPAN3);
			content.Add("<B>" + PowerCategoryToString(powerCategory) + "</B>");
			content.Add("</TD>");
			content.Add("</TR>");

			content.AddRange(parsedPowers);

		}

        private void ParseRegeneration(Regeneration regeneration, List<string> content)
        {
			content.Add(TR_SHADED);
			content.Add(TD_COLSPAN3);
			content.Add("<B>Regeneration</B>");
			content.Add("</TD>");
			content.Add("</TR>");

			content.Add(TR);
			content.Add(TD_COLSPAN3);
			content.Add("Regeneration " + WebUtility.HtmlEncode(regeneration.ToString()));
			content.Add("</TD>");
			content.Add("</TR>");
		}

        private void ParseAura(Aura aura, List<string> content)
        {
			string normalizedAura = NormalizeAura(aura.Description);
			string auraDetails = WebUtility.HtmlEncode(normalizedAura);

			var data = EncodeImage(Resources.aura);

			content.Add(TR_SHADED);
			content.Add(TD_COLSPAN3);
			content.Add("<img src=data:image/png;base64," + data + ">");
			content.Add("<B>" + WebUtility.HtmlEncode(aura.Name) + "</B>");
			if (aura.Keywords != "")
				content.Add("(" + aura.Keywords + ")");
			if (aura.Radius > 0)
				content.Add(" &diams; Aura " + aura.Radius);
			content.Add("</TD>");
			content.Add("</TR>");

			content.Add(TR);
			content.Add(TD_COLSPAN3);
			content.Add(auraDetails);
			content.Add("</TD>");
			content.Add("</TR>");
		}

        /// <summary>
        /// Returns the HTML representation of the power.
        /// </summary>
        /// <param name="cd">The CombatData to use.</param>
        /// <param name="mode">The type of HTML to generate</param>
        /// <param name="functional_template">True if this power is from a functional template; false otherwise</param>
        /// <returns>Returns the HTML source code.</returns>
        public void PowerToHTML(CreaturePower power, List<string> content)
		{
			var powerTR = TR_SHADED;
			if (power.Action != null)
				powerTR = PowerTypeToTR(power.Action.Use);

			content.Add(powerTR);
			content.Add(TD_COLSPAN3);
			content.Add(powerHeader(power));
			content.Add("</TD>");
			content.Add("</TR>");
			content.Add(TR);
			content.Add(TD_COLSPAN3);
			content.Add(PowerContent(power));
			content.Add("</TD>");
			content.Add("</TR>");
		}

		public string PowerTypeToTR(PowerUseType type)
        {
			switch (type)
            {
				case PowerUseType.AtWill:
				case PowerUseType.Basic:
					return TR_ATWILL;
				case PowerUseType.Encounter:
					return TR_ENCOUNTER;
				case PowerUseType.Daily:
					return TR_DAILY;
				default:
					return TR_ATWILL;
            }
        }

		string powerHeader(CreaturePower power)
        {
            string str = "";

            Image icon = GetRangeIcon(power);

            str += "<B>" + WebUtility.HtmlEncode(power.Name) + "</B>";

            if (icon != null)
            {
                str = "<img src=data:image/png;base64," + EncodeImage(icon) + ">" + str;
            }

            if (power.Keywords != string.Empty)
            {
                string keywords = WebUtility.HtmlEncode(power.Keywords);
                str += " (" + keywords + ")";
            }

            string info = GetPowerInfo(power);
            if (info != string.Empty)
                str += " &diams; " + info;

            return str;
        }

        private static Image GetRangeIcon(CreaturePower power)
        {
            Image icon = null;
            string rng = power.Range.ToLower();
            if (rng.Contains("melee"))
            {
                if ((power.Action != null) && (power.Action.Use == PowerUseType.Basic))
                    icon = Resources.MeleeBasic;
                else
                    icon = Resources.Melee;
            }
            if (rng.Contains("ranged"))
            {
                if ((power.Action != null) && (power.Action.Use == PowerUseType.Basic))
                    icon = Resources.RangedBasic;
                else
                    icon = Resources.Ranged;
            }
            if (rng.Contains("area"))
            {
                icon = Resources.Area;
            }
            if (rng.Contains("close"))
            {
                icon = Resources.Close;
            }
            if ((icon == null) && (power.Action != null) && (power.Action != null))
            {
                if (power.Action.Use == PowerUseType.Basic)
                    icon = Resources.MeleeBasic;
                else
                    icon = Resources.Melee;
            }

            return icon;
        }

        private string EncodeImage(Image icon)
        {
			if (!EncodedImageCache.ContainsKey(icon))
            {
				MemoryStream ms = new MemoryStream();
				icon.Save(ms, ImageFormat.Png);
				EncodedImageCache[icon] = Convert.ToBase64String(ms.ToArray());
			}	

			return EncodedImageCache[icon];
		}

        string GetPowerInfo(CreaturePower power)
		{
			if ((power.Condition == string.Empty) && (power.Action == null))
				return string.Empty;

			if (power.Action != null)
            {
				return power.Action.ToString();
			}

			return string.Empty;
		}

		string PowerContent(CreaturePower power)
		{
			List<string> lines = new List<string>();

			string desc = WebUtility.HtmlEncode(power.Description) ?? "";
			
			if (desc != "")
				lines.Add("<I>" + desc + "</I>");

			if ((power.Action != null) && (power.Action.Trigger != ""))
			{
				string action;
				switch (power.Action.Action)
				{
					case ActionType.Interrupt:
						action = "immediate interrupt";
						break;
					case ActionType.None:
						action = "no action";
						break;
					case ActionType.Reaction:
						action = "immediate reaction";
						break;
					default:
						action = power.Action.ToString().ToLower() + " action";
						break;
				}

				lines.Add("Trigger (" + action + "): " + power.Action.Trigger);
			}

			string condition = WebUtility.HtmlEncode(power.Condition);
			
			if (condition != "")
			{
				condition = "Prerequisite: " + condition;

				lines.Add(condition);
			}

			string range = power.Range ?? "";
			string attack = power.Attack?.ToString() ?? "";
			
			if (range != "")
				lines.Add("Range: " + range);
			if (attack != "")
				lines.Add("Attack: " + AttackToString(power.Attack));

			string details = power.Details ?? "";
			
			if (details != "")
            {
				details = dmgRegex.Replace(power.Details, x => "[[/roll " + x + "]]");
				lines.Add(details);
			}

			if ((power.Action != null) && (power.Action.SustainAction != ActionType.None))
			{
				string sustain = power.Action.SustainAction.ToString();

				lines.Add("Sustain: " + sustain);
			}

			string str = "";
			foreach (string line in lines)
			{
				if (str != "")
					str += "<BR>";

				str += line;
			}

			return str;
		}

		private string AttackToString(PowerAttack attack)
        {
			string sign = (attack.Bonus >= 0) ? "+" : string.Empty;
			return "[[/roll 1d20" + sign + attack.Bonus + "[vs. " + attack.Defence + "]" + "]]";
	}

		private void ParseResistances(ICreature creature, List<string> content)
        {
			var immune = string.Join(", ", creature.DamageModifiers.Where(x => x.Value == 0).Select(x => x.Type.ToString().ToLower()));
			var vuln = string.Join(", ", creature.DamageModifiers.Where(x => x.Value > 0).Select(x => Math.Abs(x.Value) + " " + x.Type.ToString().ToLower()));
			var resist = string.Join(", ", creature.DamageModifiers.Where(x => x.Value < 0).Select(x => Math.Abs(x.Value) + " " + x.Type.ToString().ToLower()));

			if (!string.IsNullOrEmpty(creature.Resist))
				resist += ", " + WebUtility.HtmlEncode(creature.Resist);

			if (!string.IsNullOrEmpty(creature.Vulnerable))
				vuln += ", " + WebUtility.HtmlEncode(creature.Vulnerable);

			if (!string.IsNullOrEmpty(creature.Immune))
				immune += ", " + WebUtility.HtmlEncode(creature.Immune);
			
			string damage_mods = string.Empty;
			if (immune != string.Empty)
			{
				damage_mods += "<B>Immune</B> " + immune;
			}
			if (resist != string.Empty)
			{
				if (damage_mods != string.Empty)
					damage_mods += "; ";

				damage_mods += "<B>Resist</B> " + resist;
			}
			if (vuln != string.Empty)
			{
				if (damage_mods != string.Empty)
					damage_mods += "; ";

				damage_mods += "<B>Vulnerable</B> " + vuln;
			}

			if (damage_mods != string.Empty)
			{
				content.Add(TR);
				content.Add(TD_COLSPAN2);
				content.Add(damage_mods);
				content.Add("</TD>");
				content.Add("</TR>");
			}
		}

        private void ParseSenses(ICreature creature, List<string> content, int rows)
        {
			string senses = WebUtility.HtmlEncode(creature.Senses ?? string.Empty);
			senses = RemoveSense(senses, "perception");

			content.Add("<TD rowspan=" + rows + @"; style = ""padding-top: 2px; padding-bottom: 2px; vertical-align: top;"">" + senses + "</TD>");
		}

        private string RemoveSense(string senses, string senseToRemove)
        {
			if (senses.ToLower().Contains(senseToRemove))
			{
				string[] clauses = senses.Split(new string[] { ";", "," }, StringSplitOptions.RemoveEmptyEntries);

				senses = "";
				foreach (string clause in clauses)
				{
					if (clause.ToLower().Contains(senseToRemove))
						continue;

					if (senses != "")
						senses += "; ";

					senses += clause;
				}
			}

			return senses;
		}

        private void ParseMovement(ICreature creature, List<string> content)
        {
            if (!string.IsNullOrEmpty(creature.Movement))
			{
				content.Add(TD_COLSPAN2);
				content.Add("<B>Speed</B> " + WebUtility.HtmlEncode(creature.Movement));
				content.Add("</TD>");
			}
        }

        private void ParsePerception(ICreature creature, IList<string> result)
        {
			var perception = GetCreatureSkill(creature, "perception");

			// Get the wisdom mod + 1/2 level
			perception += creature.Wisdom.Modifier + (creature.Level / 2);
			var perception_str = "Perception +" + perception;

			result.Add(TD);
			result.Add(perception_str);
			result.Add("</TD>");
		}

        private void ParseDefences(ICreature creature, IList<string> result)
        {
			string ac_str = $"<B>AC</B> {creature.AC}";
			string fort_str = $"<B>Fort</B> {creature.Fortitude}";
			string ref_str = $"<B>Ref</B> {creature.Reflex}";
			string will_str = $"<B>Will</B> {creature.Will}";

			result.Add(TD_COLSPAN2);
			result.Add(ac_str + "; " + fort_str + "; " + ref_str + "; " + will_str);
			result.Add("</TD>");
		}

        private void ParseInitiative(ICreature creature, IList<string> result)
        {
			int init_bonus = creature.Initiative;
			string init_str = init_bonus.ToString();

			if (init_bonus >= 0)
				init_str = "+" + init_str;

			result.Add(TD);
			result.Add($"<B>Initiative</B> {init_str}");
			result.Add("</TD>");
		}

        private void ParseHP(ICreature creature, IList<string> result)
        {
			string hp_str = $"<B>HP</B> {creature.HP}";

			if (creature.Role.Flag != RoleFlag.Minion)
			{
				string bloodied_str = $"<B>Bloodied</B> {creature.HP / 2}";
				hp_str += "; " + bloodied_str;
			}

			result.Add(TD_COLSPAN2);
			result.Add(hp_str);
			result.Add("</TD>");
		}

        /// <summary>
        /// Level N [Elite / Solo] Role
        /// </summary>
        public string CreatureLevelText(ICreature creature)
		{
			int level = creature.Level;

			string str = "";
			if (creature.Role.Flag != RoleFlag.Standard)
            {
				str += RoleFlagToString(creature.Role.Flag);
            }

			str += " " + RoleToString(creature.Role);

			if (creature.Role is ComplexRole complexRole && complexRole.Leader)
			{
				str += " (L)";
			}

			return "Level " + level + " " + str;
		}

		public string Concatenate(List<string> lines)
		{
			string text = "";
			foreach (string line in lines)
			{
				if (text != "")
					text += Environment.NewLine;

				text += line;
			}

			return text;
		}

		string ability(Ability ab, int level)
		{
			if (ab == null)
				return "-";

			int mod = ab.Modifier + (level / 2);

			string str = "";

			str += ab.Score.ToString();
			str += " ";

			string mod_str = mod.ToString();
			if (mod >= 0)
				mod_str = "+" + mod_str;
			str += "(" + mod_str + ")";

			return str;
		}
	}
}
