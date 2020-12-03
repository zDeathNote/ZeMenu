using GTA;

namespace ZeMenu.Commands
{
    class CommandWantedLevel : Command
    {
        internal enum WantedLevelType{
            UP,DOWN
        }

        private WantedLevelType Type { get; }
        internal CommandWantedLevel(Player p, WantedLevelType type) : base(p)
        {
            Type = type;
        }
        internal override void DoAction(params object[] args)
        {
            switch (Type)
            {
                case WantedLevelType.UP:
                    Target.WantedLevel++;
                    break;
                case WantedLevelType.DOWN:
                    Target.WantedLevel--;
                    break;
            }
        }
    }
}
