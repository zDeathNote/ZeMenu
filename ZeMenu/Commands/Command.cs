using GTA;

namespace ZeMenu.Commands
{
    abstract class Command
    {
        internal Player Target { get; }

        internal Command(Player p)
        {
            Target = p;
        }
        internal abstract void DoAction(params object[] args);
    }
}
