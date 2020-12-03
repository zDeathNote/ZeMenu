using GTA;

namespace ZeMenu.Commands
{
    internal class CommandTeleport : Command
    {
        public CommandTeleport(Player p) : base(p)
        {
        }

        internal override void DoAction(params object[] args)
        {
            Blip waypoint = Game.GetWaypoint();
            if (waypoint != null && waypoint.Exists())
            {
                Target.TeleportTo(waypoint.Position);
            }
        }
    }
}
