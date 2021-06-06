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
