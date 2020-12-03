using GTA;
using System;

namespace ZeMenu.Commands
{
    internal class CommandPedDensity : Command
    {
        public CommandPedDensity(Player p) : base(p)
        {
        }

        internal override void DoAction(params object[] args)
        {
            if (args.Length > 0)
            {
                Game.Console.Print(args[0].ToString());
                World.PedDensity = (float)Convert.ToDouble(args[0]);
                //World.PedDensity = float.Parse(args[0].ToString());
            }
        }
    }
}
