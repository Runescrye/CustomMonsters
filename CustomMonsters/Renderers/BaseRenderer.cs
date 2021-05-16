using Masterplan.Data;

namespace MonsterPorter.Renderers
{
    abstract class BaseRenderer
    {
        public abstract string Render(ICreature creature);
    }
}
