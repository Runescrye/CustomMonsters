using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Masterplan.Data;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text.Encodings.Web;

namespace MonsterPorter.Renderers
{
    class JsonRenderer : BaseRenderer
    {
        public override string Render(ICreature creature)
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            options.WriteIndented = true;
            return JsonSerializer.Serialize(creature, options);
            /*
            var result = new Dictionary<string,object>();
            result.Add("ac", creature.AC);
            result.Add("alignment", creature.Alignment);
            result.Add("category", creature.Category);
            result.Add("charisma", creature.Charisma);
            result.Add("constitution", creature.Constitution);
            result.Add("strength", creature.Strength);
            result.Add("dexterity", creature.Dexterity);
            result.Add("intelligence", creature.Intelligence);
            result.Add("initiative", creature.Initiative);
            result.Add("movement", creature.Movement);
            result.Add("origin", creature.Origin);
            result.Add("name", creature.Name);
            result.Add("phenotype", creature.Phenotype);
            result.Add("resist", creature.Resist);
            result.Add("size", base.SizeToString(creature.Size));
            result.Add("role", base.RoleToString((ComplexRole)creature.Role));
            result.Add("equipment", creature.Equipment);
            result.Add("auras", ParseAuras(creature));
            result.Add("powers", ParsePowers(creature));

            return JsonSerializer.Serialize(result);*/
        }

        private List<Dictionary<string, object>> ParseAuras(ICreature creature)
        {
            var result = new List<Dictionary<string, object>>();
            foreach (var aura in creature.Auras)
            {
                var auraResult = new Dictionary<string, object>();

                auraResult.Add("name", aura.Name);
                auraResult.Add("keywords", aura.Keywords);
                auraResult.Add("radius", aura.Radius);
                auraResult.Add("details", aura.Details);

                result.Add(auraResult);
            }

            return result;
        }

        private List<Dictionary<string, object>> ParsePowers(ICreature creature)
        {
            var result = new List<Dictionary<string, object>>();
            foreach (var power in creature.CreaturePowers)
            {
                var powerResult = new Dictionary<string, object>();

                powerResult.Add("name", power.Name);
                powerResult.Add("keywords", power.Keywords);
                powerResult.Add("radius", power.Range);

                result.Add(powerResult);
            }

            return result;
        }
    }
}
