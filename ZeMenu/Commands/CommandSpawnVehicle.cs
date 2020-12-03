namespace ZeMenu.Commands
{
    class CommandSpawnVehicle : Command
    {
        private string VehName { get; }
        internal CommandSpawnVehicle(GTA.Player P, string v) : base(P)
        {
            VehName = v;
        }

        internal override void DoAction(params object[] args)
        {
            GTA.Vehicle v = GTA.World.CreateVehicle(VehName, Target.Character.Position.Around(5.0F));
            if (v == null) GTA.Game.Console.Print("Could not spawn " + VehName);
            else v.NoLongerNeeded();
        }
    }
}
