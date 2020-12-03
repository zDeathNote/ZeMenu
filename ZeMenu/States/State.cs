using GTA;
using System;

namespace ZeMenu.States
{
    abstract class State
    {
        internal readonly CommandStateHandler ParentHandler;
        internal Player P { get; }
        internal State(CommandStateHandler ParentHandler, Player player)
        {
            this.ParentHandler = ParentHandler;
            P = player;
        }

        internal abstract void StateTick(object sender, EventArgs e);
    }
}
