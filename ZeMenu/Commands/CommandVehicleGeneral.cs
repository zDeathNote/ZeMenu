using GTA;

namespace ZeMenu.Commands
{
    internal class CommandVehicleGeneral : Command { 
    
        internal enum GeneralActions { REPAIR, DIRTY, CLEAN}
        private GeneralActions Action { get; }
        public CommandVehicleGeneral(Player p, GeneralActions act) : base(p)
        {
            Action = act;
        }

        internal override void DoAction(params object[] args)
        {
            Vehicle v;
            if (Target.Character.isInVehicle()) v = Target.Character.CurrentVehicle;
            else v = Target.LastVehicle;

            if (v != null && v.Exists())
            {
                switch (Action)
                {
                    case GeneralActions.REPAIR:
                        v.Repair();
                        break;
                    case GeneralActions.CLEAN:
                        v.Dirtyness = 0;
                        break;
                    case GeneralActions.DIRTY:
                        v.Dirtyness = 40;
                        break;
                }
            }
        }
    }
}
