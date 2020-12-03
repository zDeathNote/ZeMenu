using System;
using System.Collections.Generic;
using GTA;

namespace ZeMenu.States
{
    public class CommandStateHandler { 
        private readonly Player P;
        internal readonly Dictionary<string, Timer> StateTimers = new Dictionary<string, Timer>();
        internal readonly Dictionary<string, GTA.Object[]> StateObjects = new Dictionary<string, GTA.Object[]>();
        internal readonly Dictionary<string, int[]> StateHandles = new Dictionary<string, int[]>();


        private readonly State[] states;
        public CommandStateHandler(Player player)
        {
            P = player;
            states = new State[]
            {
                new StateWantedIgnored(this, P),
                new StateIsSmoking(this, P),
                new StateIsInvincible(this, P),
                new StateIsVehicleInvincible(this, P),
                new StateVehicleNeverDirty(this, P),
                new StateNoReload(this, P)
            };
        }
        
        internal void ZeMenu_TickStateHandler(object sender, EventArgs e)
        {
            //try
            //{
                foreach (State s in states)
                {
                    s.StateTick(sender, e);
                }
            //}
            //catch (Exception ex)
            //{
                //Game.Console.Print(ex.Message);
                //Game.Console.Print(ex.StackTrace);
            //}
        }
    }
}
