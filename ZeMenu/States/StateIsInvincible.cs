using GTA;
using System;

namespace ZeMenu.States
{
    internal class StateIsInvincible : State
    {
        private bool reset = true;
        private static int NewMax { get; } = 1000;
        private static int Max { get; } = 100;
        public StateIsInvincible(CommandStateHandler ParentHandler, Player player) : base(ParentHandler, player)
        {
        }

        internal override void StateTick(object sender, EventArgs e)
        {
            if (CommandStates.States["IsInvincible"] == true)
            {
                P.MaxHealth = NewMax;
                P.MaxArmor = NewMax;
                if (P.Character.Health < NewMax)
                {
                    P.Character.Health = NewMax;
                }
                if (P.Character.Armor < NewMax)
                {
                    P.Character.Armor = NewMax;
                }
                reset = false;
            }
            else
            {
                if (!reset)
                {
                    reset = true;
                    P.MaxHealth = Max;
                    P.MaxArmor = Max;
                    P.Character.Armor = Max;
                    P.Character.Health = Max;
                }
            }
        }
    }
}
