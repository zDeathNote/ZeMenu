using GTA;

namespace ZeMenu.Commands
{
    internal class CommandVehicleColor : Command
    {
        private ColorType Type {get;}
        private ColorIndex Index { get; }
        internal enum ColorType { Current, Color, Feature1, Feature2, Specular };
        internal CommandVehicleColor(Player p, ColorType colorType, ColorIndex color) : base(p)
        {
            Type = colorType;
            Index = color;
        }

        internal override void DoAction(params object[] args)
        {
            Vehicle v;
            if (Target.Character.isInVehicle()) v = Target.Character.CurrentVehicle;
            else v = Target.LastVehicle;
            if (v != null && v.Exists())
            {
                switch (Type)
                {
                    case ColorType.Color:
                        Target.Character.CurrentVehicle.Color = Index;
                        break;
                    case ColorType.Feature1:
                        Target.Character.CurrentVehicle.FeatureColor1 = Index;
                        break;
                    case ColorType.Feature2:
                        Target.Character.CurrentVehicle.FeatureColor2 = Index;
                        break;
                    case ColorType.Specular:
                        Target.Character.CurrentVehicle.SpecularColor = Index;
                        break;
                    case ColorType.Current:
                        Game.DisplayText("\n                         Name:     " + v.Name.ToUpper() + 
                                         "\n                         Color1:   " + v.Color.Name +
                                         "\n                         Feature1: " + v.FeatureColor1.Name +
                                         "\n                         Feature2: " + v.FeatureColor2.Name + 
                                         "\n                         Specular: " + v.SpecularColor.Name, 7500);
                        break;
                }
            }
        }
    }
}
