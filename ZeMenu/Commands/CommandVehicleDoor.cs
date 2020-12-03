using GTA;

namespace ZeMenu.Commands
{
    internal class CommandVehicleDoor : Command
    {
        private VehicleDoor Door { get; }
        public CommandVehicleDoor(Player p, VehicleDoor door) : base(p)
        {
            Door = door;
        }

        internal override void DoAction(params object[] args)
        {
            Vehicle v;
            if (Target.Character.isInVehicle()) v = Target.Character.CurrentVehicle;
            else v = Target.LastVehicle;

            if (v != null && v.Exists())
            {
                if (v.Door(Door).isOpen)
                {
                    v.Door(Door).Close();
                }
                else
                {
                    v.Door(Door).Open();
                }
            }
            
        }
    }
}
