using GTA;
using System;

namespace ZeMenu.States
{
    internal class StateVehicleNeverDirty : State
    {
        public StateVehicleNeverDirty(CommandStateHandler ParentHandler, Player player) : base(ParentHandler, player)
        {
        }

        internal override void StateTick(object sender, EventArgs e)
        {
            if (CommandStates.States["VehicleNeverDirty"])
            {
                if (P.Character.isInVehicle())
                {
                    P.Character.CurrentVehicle.Dirtyness = 0;
                }
            }
        }
    }
}
