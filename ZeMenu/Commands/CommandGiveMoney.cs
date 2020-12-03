using System;

namespace ZeMenu.Commands
{
    class CommandGiveMoney : Command
    { 
        internal CommandGiveMoney(GTA.Player P) : base(P)
        {
        }

        internal override void DoAction(params object[] args)
        {
            if (args.Length > 0)
            {
                Target.Money += Convert.ToInt32(args[0]);
            }
        }
    }
}
