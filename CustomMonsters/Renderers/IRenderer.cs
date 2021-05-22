using Masterplan.Data;

namespace MonsterPorter.Renderers
{
    interface IRenderer
    {
        public abstract string Render(ICreature creature);
    }
}
