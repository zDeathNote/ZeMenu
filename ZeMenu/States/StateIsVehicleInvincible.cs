using GTA;
using System;

namespace ZeMenu.States
{
    internal class StateIsVehicleInvincible : State
    {
        public StateIsVehicleInvincible(CommandStateHandler ParentHandler, Player player) : base(ParentHandler, player)
        {
        }

        internal override void StateTick(object sender, EventArgs e)
        {
            if (CommandStates.States["IsVehicleInvincible"])
            {
                if (P.Character.isInVehicle())
                {
                    P.Character.CurrentVehicle.Repair();
                }
            }
        }
    }
}
