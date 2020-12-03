using GTA;
using System;

namespace ZeMenu.States
{
    internal class StateInfiniteAmmo : State
    {
        public StateInfiniteAmmo(CommandStateHandler ParentHandler, Player player) : base(ParentHandler, player)
        {
        }

        internal override void StateTick(object sender, EventArgs e)
        {
            if (CommandStates.States["InfiniteAmmo"])
            {
                GTA.value.Weapon w = P.Character.Weapons.Current;
                P.Character.Weapons.Current.Ammo = w.MaxAmmo;
            }
        }
    }
}
