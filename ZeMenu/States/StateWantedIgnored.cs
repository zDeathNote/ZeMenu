using System;
using GTA;

namespace ZeMenu.States
{
    internal class StateWantedIgnored : State
    {
        private bool reset = true;
        private float PreviousMultiplier { get; set; }
        public StateWantedIgnored(CommandStateHandler ParentHandler, Player P) : base(ParentHandler, P)
        {
        }

        internal override void StateTick(object sender, EventArgs e)
        {
            if (CommandStates.States["WantedIgnored"])
            {
                reset = false;
                P.WantedLevel = 0;
                //Game.WantedMultiplier = 0.0F;
            }
            else
            {
                if (!reset)
                {
                    reset = true;
                    //Game.WantedMultiplier = 0.0F;
                }
            }
        }
    }
}
