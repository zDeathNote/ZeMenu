using GTA;

namespace ZeMenu.Commands
{
    class CommandAbout : Command
    {
        internal CommandAbout(Player p) : base(p) { }

        private string info = "beta-1.0 by zDeathNote";
        internal override void DoAction(params object[] args)
        {
            Game.Console.Print(info);
            Game.DisplayText("\n                                     " + info);
        }
    }
}
