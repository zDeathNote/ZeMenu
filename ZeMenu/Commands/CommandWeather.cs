using GTA;

namespace ZeMenu.Commands
{
    class CommandWeather : Command
    {
        private Weather NewWeather { get; }
        public CommandWeather(Player P, Weather w) : base(P)
        {
            NewWeather = w;
        }

        internal override void DoAction(params object[] args)
        {
            GTA.Native.Function.Call("FORCE_WEATHER_NOW", new GTA.Native.Parameter((int)NewWeather));
        }
    }
}
