using GTA;
using System;
using System.Collections.Generic;
using ZeMenu.Util;

namespace ZeMenu.States
{
    internal class StateIsSmoking : State
    {
        private readonly Dictionary<VehicleWindow, VehicleSeat> DictSeatWindow = new Dictionary<VehicleWindow, VehicleSeat>()
        {
            {VehicleWindow.LeftFront, VehicleSeat.Driver },
            {VehicleWindow.RightFront, VehicleSeat.RightFront },
            {VehicleWindow.LeftRear, VehicleSeat.LeftRear },
            {VehicleWindow.RightRear, VehicleSeat.RightRear },
        };
        public StateIsSmoking(CommandStateHandler ParentHandler, Player P) : base(ParentHandler, P)
        {
        }

        internal override void StateTick(object sender, EventArgs e)
        {
            if (CommandStates.States["IsSmoking"])
            {
                Start();

                if (P.Character.Weapons.CurrentType != Weapon.Unarmed &&
                    !P.Character.isInVehicle() &&
                    !P.Character.isGettingIntoAVehicle)
                {
                    Stop();
                    return;
                }

                Timer t = ParentHandler.StateTimers["IsSmoking"];

                

                if (t.ElapsedTime >= 5750 && P.Character.isIdle)
                {
                    if (P.Character.isInVehicle())
                    {
                        StartVeh();
                    }
                    else
                    {
                        StopVeh();
                    }
                    P.Character.Task.PlayAnimation(Animations.isSmokingAnims, "partial_smoke", 8.0F, Animations.isSmokingAnimFlags);
                    t.Start();
                }
                ParentHandler.StateTimers["IsSmoking"] = t;
            }
            else
            {
                Stop();
                return;
            }
        }

        private void CreateSpliff()
        {
            GTA.Object spliff = World.CreateObject(0xD130ADEF, P.Character.Position);
            spliff.AttachToPed(P.Character, Bone.RightHand, new Vector3(0.14F, 0.03F, 0.04F), new Vector3(2.21F, -0.12F, 0.0F));

            ParentHandler.StateObjects.Add("IsSmoking", new GTA.Object[] { spliff });
        }

        private void RemoveSpliff()
        {
            if (ParentHandler.StateObjects.ContainsKey("IsSmoking"))
            {
                ParentHandler.StateObjects["IsSmoking"][0].Detach();
                ParentHandler.StateObjects["IsSmoking"][0].NoLongerNeeded();
                ParentHandler.StateObjects.Remove("IsSmoking");
            }
        }

        private void Start()
        {
            Timer t;
            if (!ParentHandler.StateTimers.ContainsKey("IsSmoking"))
            {
                t = new Timer();
                ParentHandler.StateTimers.Add("IsSmoking", t);

                CreateSpliff();
                t.Start();
            }
        }

        private void StartVeh()
        {
            VehicleWindow playerWindow = VehicleWindow.LeftFront;
            /*foreach (VehicleWindow w in Enum.GetValues(typeof(VehicleWindow)))
            {
                if (!DictSeatWindow.ContainsKey(w))
                {
                    continue;
                }
                //Game.Console.Print(w.ToString());
                Ped ped = P.Character.CurrentVehicle.GetPedOnSeat(DictSeatWindow[w]);
                if (ped != null && ped.Equals(P.Character))
                {
                    playerWindow = w;
                }
            }*/
            //Game.Console.Print(playerWindow.ToString());
            bool isWindowIntact = GTA.Native.Function.Call<bool>("IS_VEH_WINDOW_INTACT", P.Character.CurrentVehicle, (int)playerWindow);
            if (isWindowIntact)
            {
                GTA.Native.Function.Call<bool>("REMOVE_CAR_WINDOW", P.Character.CurrentVehicle, (int)VehicleWindow.LeftFront);

            }
            if (!ParentHandler.StateHandles.ContainsKey("IsSmoking"))
            {
                int handle = GTA.Native.Function.Call<int>("START_PTFX_ON_VEH", "smoke_filled_car", P.Character.CurrentVehicle,
                    0, 0, 0, 0, 0, 0, 1.7f);
                ParentHandler.StateHandles.Add("IsSmoking", new int[] { handle });
            }
        }

        private void Stop()
        {
            //ensure we set everything to stop incase it was stopped by weapon change
            CommandStates.States["IsSmoking"] = false;

            StopVeh();
            
            if (ParentHandler.StateTimers.ContainsKey("IsSmoking"))
            {
                ParentHandler.StateTimers["IsSmoking"].Stop();
                ParentHandler.StateTimers.Remove("IsSmoking");
            }
            RemoveSpliff();
        }

        private void StopVeh()
        {
            if (ParentHandler.StateHandles.ContainsKey("IsSmoking"))
            {
                int handle = ParentHandler.StateHandles["IsSmoking"][0];
                GTA.Native.Function.Call("REMOVE_PTFX_FROM_VEH", P.LastVehicle);
                GTA.Native.Function.Call("STOP_PTFX", handle);
                ParentHandler.StateHandles.Remove("IsSmoking");
            }
        }
    }
}
