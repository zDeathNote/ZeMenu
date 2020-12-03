using GTA;
using System;

namespace ZeMenu.Commands
{
    internal class CommandVehicleDensity : Command
    {
        public CommandVehicleDensity(Player p) : base(p)
        {
        }

        internal override void DoAction(params object[] args)
        {
            if (args.Length > 0)
            {
                Game.Console.Print(args[0].ToString());
                World.CarDensity = (float)Convert.ToDouble(args[0]);
                //World.CarDensity = float.Parse(args[0].ToString());
            }
        }
    }
}
