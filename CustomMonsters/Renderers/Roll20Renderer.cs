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
        const string CREATURE_POWER = @"{{{{{0}=yes }}}}{{{{name={1}}}}}";
        const string CREATURE_POWER_ATTACK = @"{{{{attack={0} XXd20+{1}ZZ vs {2}}}}}";
        const string POWER_HIT = @"{{{{hiteffect={0}}}}}";
        const string POWER_EFFECT = @"{{{{effect={0}}}}}";
        const string AURA_NAME_FORMAT = @"{0} {1}";

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
                var actionText = "other";
                if (power.Action != null)
                {
                    actionText = base.PowerUseTypeToString(power.Action.Use);
                }

                result.AppendFormat(CREATURE_POWER, actionText, power.Name);

                if (power.Attack != null)
                {
                    result.AppendFormat(CREATURE_POWER_ATTACK, power.Range, power.Attack.Bonus, power.Attack.Defence);
                }

                var powerHit = base.ExtractHit(power.Details);
                if (!string.IsNullOrEmpty(powerHit))
                {
                    result.Append(string.Format(POWER_HIT, AnnotateDamage(powerHit.Trim())));
                }

                var powerEffect = base.ExtractEffect(power.Details);
                if (!string.IsNullOrEmpty(powerEffect))
                {
                    result.Append(string.Format(POWER_EFFECT, AnnotateDamage(powerEffect.Trim())));
                }

                if (string.IsNullOrEmpty(powerHit) && string.IsNullOrEmpty(powerEffect))
                {
                    result.Append(string.Format(POWER_HIT, AnnotateDamage(power.Details.Trim())));
                }

                addEnd = true;
            }

            return result.ToString();
        }

        private static Regex dmgRegex = new Regex("\\d?d\\d+\\s*([+-]\\s*\\d)");
        private string AnnotateDamage(string source)
        {
            return dmgRegex.Replace(source, ReplaceDamage);
        }

        private string ReplaceDamage(Match match)
        {
            return "XX" + match.Value + "ZZ";
        }
    }
}
