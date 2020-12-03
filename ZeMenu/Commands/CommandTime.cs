using System;
using GTA;

namespace ZeMenu.Commands
{
    class CommandTime : Command
    {
        internal enum TimeType
        {
            HOUR,DAY
        }

        private TimeType AdjustType { get; }
        public CommandTime(Player p, TimeType t) : base(p)
        {
            AdjustType = t;
        }

        internal override void DoAction(params object[] args)
        {
            switch(AdjustType)
            {
                case TimeType.HOUR:
                    World.CurrentDayTime = World.CurrentDayTime.Add(TimeSpan.FromHours(1));
                    break;
                case TimeType.DAY:
                    World.CurrentDayTime = World.CurrentDayTime.Add(TimeSpan.FromDays(1));
                    break;
            }
        }
    }
}
