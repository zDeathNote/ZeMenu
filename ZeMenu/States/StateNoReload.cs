using GTA;
using System;

namespace ZeMenu.States
{
    internal class StateNoReload : State
    {
        public StateNoReload(CommandStateHandler ParentHandler, Player player) : base(ParentHandler, player)
        {
        }

        internal override void StateTick(object sender, EventArgs e)
        {
            if (CommandStates.States["NoReload"])
            {
                GTA.value.Weapon w = P.Character.Weapons.Current;
                P.Character.Weapons.Current.AmmoInClip = w.MaxAmmoInClip;
            }
        }
    }
}
