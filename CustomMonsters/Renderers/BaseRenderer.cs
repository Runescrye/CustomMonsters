using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MonsterPorter.Renderers
{
    abstract class BaseRenderer : IRenderer
    {
        public abstract string Render(ICreature creature);
        protected static Regex dmgRegex     = new Regex(@"\d*d\d+\s*(?:[+-]\s*\d+)?");
        protected static Regex attackRegex  = new Regex(@"\+(\d+)\s*vs\.?\s*(Fortitude|Reflex|Will|AC)");
        protected static Regex skillValue = new Regex(@"\d+");

        protected string StringToStringSubstring(string haystack, string first, string last)
        {
            var start = haystack.IndexOf(first);
            if (start == -1)
                return null;

            start = start + first.Length;

            var end = haystack.IndexOf(last);
            end = (end == -1 || end < start) ? haystack.Length - start : end - start;

            return haystack.Substring(start, end);
        }

        protected int GetCreatureSkill(ICreature creature, string skillName)
        {
            int result = 0;
            if (!string.IsNullOrEmpty(creature.Skills))
            {
                string[] skills = creature.Skills.Split(new string[] { ";", "," }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string skill in skills)
                {
                    string sk = skill.Trim();
                    if (sk.ToLower().Contains(skillName))
                        result = int.Parse(skillValue.Match(sk).Value);
                }
            }

            return result;
        }

        protected int CalculateCreatureXP(ICreature creature)
        {
            var xp = 0;
            
            xp = XPByLevel(creature.Level);
            switch (creature.Role.Flag)
            {
                case RoleFlag.Minion:
                    float minionXP = xp / 4;
                    xp = (int)Math.Round(minionXP, MidpointRounding.AwayFromZero);
                    break;
                case RoleFlag.Elite:
                    xp *= 2;
                    break;
                case RoleFlag.Solo:
                    xp *= 5;
                    break;
            }

            return xp;
        }

        public static int XPByLevel(int level)
        {
            if (level < 1)
                level = 1;

            switch (level)
            {
                case 1: return 100;
                case 2: return 125;
                case 3: return 150;
                case 4: return 175;
                case 5: return 200;
                case 6: return 250;
                case 7: return 300;
                case 8: return 350;
                case 9: return 400;
                case 10: return 500;
                case 11: return 600;
                case 12: return 700;
                case 13: return 800;
                case 14: return 1000;
                case 15: return 1200;
                case 16: return 1400;
                case 17: return 1600;
                case 18: return 2000;
                case 19: return 2400;
                case 20: return 2800;
                case 21: return 3200;
                case 22: return 4150;
                case 23: return 5100;
                case 24: return 6050;
                case 25: return 7000;
                case 26: return 9000;
                case 27: return 11000;
                case 28: return 13000;
                case 29: return 15000;
                case 30: return 19000;
                case 31: return 23000;
                case 32: return 27000;
                case 33: return 31000;
                case 34: return 39000;
                case 35: return 47000;
                case 36: return 55000;
                case 37: return 63000;
                case 38: return 79000;
                case 39: return 95000;
                case 40: return 111000;
                default: return 111000;
            }
        }

        protected string PowerUseTypeToHeaderString(PowerUseType useType)
        {
            switch (useType)
            {
                case PowerUseType.AtWill:
                case PowerUseType.Basic:
                    return "atwill";
                case PowerUseType.Encounter:
                    return "encounter";
                case PowerUseType.Daily:
                    return "daily";
                default:
                    return "other";
            }
        }

        protected string PowerUseTypeToString(PowerUseType useType)
        {
            switch (useType)
            {
                case PowerUseType.AtWill:
                case PowerUseType.Basic:
                    return "At-Will";
                case PowerUseType.Encounter:
                    return "Encounter";
                case PowerUseType.Daily:
                    return "Daily";
                default:
                    return "Other";
            }
        }

        protected static string PowerCategoryToString(CreaturePowerCategory powerCategory)
        {
            switch (powerCategory)
            {
                case CreaturePowerCategory.Trait:
                    return "Traits";
                case CreaturePowerCategory.Standard:
                case CreaturePowerCategory.Move:
                case CreaturePowerCategory.Minor:
                case CreaturePowerCategory.Free:
                    return powerCategory + " Actions";
                case CreaturePowerCategory.Triggered:
                    return "Triggered Actions";
                case CreaturePowerCategory.Other:
                default:
                    return "Other Actions";
            }

        }

        protected string RoleFlagToString(RoleFlag roleFlag)
        {
            switch (roleFlag)
            {
                case RoleFlag.Minion:
                    return "Minion ";
                case RoleFlag.Elite:
                    return  "Elite ";
                case RoleFlag.Solo:
                    return "Solo ";
                default:
                    return "Standard";
            }
        }

        protected string RoleToString(IRole role)
        {
            switch (role.Type)
            {
                case RoleType.Artillery:
                    return "artillery";
                case RoleType.Blaster:
                    return "blaster";
                case RoleType.Brute:
                    return "brute";
                case RoleType.Controller:
                    return "controller";
                case RoleType.Lurker:
                    return "lurker";
                case RoleType.Obstacle:
                    return "obstacle";
                case RoleType.Skirmisher:
                    return "skirmisher";
                case RoleType.Soldier:
                    return "soldier";
                case RoleType.Warder:
                    return "warder";
                default:
                    return "other";
            }
        }

        protected string ExtractHit(string details)
        {
            return StringToStringSubstring(details, "Hit: ", "Effect:")?.Trim();
        }

        protected string ExtractEffect(string details)
        {
            return StringToStringSubstring(details, "Effect: ", "Hit:")?.Trim();
        }
    }
}
