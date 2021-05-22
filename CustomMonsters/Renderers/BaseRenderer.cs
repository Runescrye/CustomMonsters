using Masterplan.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterPorter.Renderers
{
    abstract class BaseRenderer : IRenderer
    {
        public abstract string Render(ICreature creature);

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

        protected string PowerUseTypeToString(PowerUseType useType)
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

        protected string SizeToString(CreatureSize size)
        {
            switch (size)
            {
                case CreatureSize.Tiny:
                    return "tiny";
                case CreatureSize.Small:
                    return "small";
                case CreatureSize.Medium:
                    return "medium";
                case CreatureSize.Large:
                    return "large";
                case CreatureSize.Huge:
                    return "huge";
                case CreatureSize.Gargantuan:
                    return "gargantuan";
                default:
                    return "medium";
            }
        }

        protected string RoleToString(ComplexRole role)
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
            return StringToStringSubstring(details, "Hit: ", "Effect:");
        }

        protected string ExtractEffect(string details)
        {
            return StringToStringSubstring(details, "Effect: ", "Hit:");
        }
    }
}
