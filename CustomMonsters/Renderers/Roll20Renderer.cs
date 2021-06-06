using System;
using System.Text;
using System.Text.RegularExpressions;
using Masterplan.Data;

namespace MonsterPorter.Renderers
{
    class Roll20Renderer : BaseRenderer
    {
        const string END = "$T4E";
        const string MACRO_START = "!c @{selected|token_id}$$";
        const string CREATURE_STAT_BLOCK = @"${0}${1}${2}${3}${4}${5}${6}${7}" + END;
        const string CREATURE_AURA = @"{{{{other=yes}}}}{{{{name={0}}}}}{{{{effect={1}}}}}";
        const string CREATURE_POWER_HEADER = @"{{{{{0}=yes }}}}{{{{name={1}}}}}{{{{emote={2}}}}}{{{{requirement={3}}}}}";
        const string CREATURE_POWER_ATTACK = @"{{{{attack=XXd20+{0}ZZ vs {1}}}}}";
        const string POWER_HIT = @"{{{{hiteffect={0}}}}}";
        const string POWER_EFFECT = @"{{{{effect={0}}}}}";
        const string AURA_NAME_FORMAT = @"{0} {1}";
        const string ACTION_FORMAT = @"{{{{trigger={0}}}}}{{{{keywords={1}}}}}{{{{type={2}}}}}{{{{action={3}}}}}{{{{range={4}}}}}";

        public override string Render(ICreature creature)
        {
            var perception = creature.Wisdom.Modifier + creature.Level / 2;

            StringBuilder creatureMacro = new StringBuilder();

            creatureMacro.Append(MACRO_START + creature.Name);
            creatureMacro.AppendFormat(CREATURE_STAT_BLOCK, creature.Level, perception, creature.HP, creature.Initiative, creature.AC, creature.Fortitude, creature.Reflex, creature.Will);

            var auras = ParseAuras(creature);
            if (!string.IsNullOrEmpty(auras))
            {
                creatureMacro.Append(auras);
                creatureMacro.Append(END);
            }

            creatureMacro.Append(ParsePowers(creature));
            return creatureMacro.ToString();
        }

        private string ParseAuras(ICreature creature)
        {
            StringBuilder result = new StringBuilder();

            bool addEnd = false;
            foreach (Aura aura in creature.Auras)
            {
                if (addEnd)
                {
                    result.Append(END);
                }

                string auraText = string.Format(AURA_NAME_FORMAT, aura.Name + " Aura", aura.Radius);
                result.AppendFormat(CREATURE_AURA, auraText, aura.Details);
                addEnd = true;
            }

            return result.ToString();
        }

        private string ParsePowers(ICreature creature)
        {
            StringBuilder result = new StringBuilder();

            bool addEnd = false;
            foreach (CreaturePower power in creature.CreaturePowers)
            {
                if (addEnd)
                {
                    result.Append(END);
                }
                var actionHeadersUseType = "other";

                if (power.Category != CreaturePowerCategory.Trait)
                {
                    actionHeadersUseType = PowerUseTypeToHeaderString(power.Action.Use);

                    var actionType = ActionTypeToString(power.Action.Action);
                    actionType += !string.IsNullOrEmpty(power.Keywords) ? " ♦" : string.Empty;

                    var actionBodyUseType = PowerUseTypeToString(power.Action.Use) + " ♦";

                    var range = (power.Attack == null) ? "Personal" : "Melee";
                    range = string.IsNullOrEmpty(power.Range) ? range : power.Range;

                    result.AppendFormat(ACTION_FORMAT, power.Action.Trigger, power.Keywords, actionType, actionBodyUseType, range);
                }

                result.AppendFormat(CREATURE_POWER_HEADER, actionHeadersUseType, power.Name, power.Description, power.Condition);
                ParseAttack(power, result);

                addEnd = true;
            }

            return result.ToString();
        }

        private void ParseAttack(CreaturePower power, StringBuilder result)
        {
            if (power.Attack == null)
            {
                result.Append(string.Format(POWER_EFFECT, AnnotateDamage(power.Details)));
                return;
            }

            result.AppendFormat(CREATURE_POWER_ATTACK, power.Attack.Bonus, power.Attack.Defence);
            var powerHit = ExtractHit(power.Details);
            if (!string.IsNullOrEmpty(powerHit))
            {
                result.Append(string.Format(POWER_HIT, AnnotateDamage(powerHit)));
            }

            var powerEffect = ExtractEffect(power.Details);
            if (!string.IsNullOrEmpty(powerEffect))
            {
                result.Append(string.Format(POWER_EFFECT, AnnotateDamage(powerEffect)));
            }

            if (string.IsNullOrEmpty(powerHit) && string.IsNullOrEmpty(powerEffect))
            {
                result.Append(string.Format(POWER_HIT, AnnotateDamage(power.Details)));
            }
        }

        private static Regex dmgRegex = new Regex("\\d?d\\d+\\s*([+-]\\s*\\d+)?");
        private string AnnotateDamage(string source)
        {
            return dmgRegex.Replace(source, x => "XX" + x.Value + "ZZ");
        }

        private string ActionTypeToString(ActionType action)
        {
            switch (action)
            {
                case ActionType.None:
                    return "No Action";
                case ActionType.Interrupt:
                case ActionType.Reaction:
                    return "Immediate " + action.ToString();
                case ActionType.Opportunity:
                    return "Opportunity Action";
                default:
                    return action.ToString();
            }
        }
    }
}
