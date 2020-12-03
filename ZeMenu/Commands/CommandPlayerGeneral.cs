using GTA;

namespace ZeMenu.Commands
{
    class CommandPlayerGeneral : Command
    {
        internal enum Type { HEAL, ARMOR }

        private Type CommandType { get;  }
        internal CommandPlayerGeneral(Player p, Type t) : base(p)
        {
            CommandType = t;
        }

        internal override void DoAction(params object[] args)
        {
            switch (CommandType) {
                case Type.HEAL:
                    Target.MaxHealth = 100;
                    Target.Character.Health = 100;
                    break;
                case Type.ARMOR:
                    Target.MaxArmor = 100;
                    Target.Character.Armor = 100;
                    break;
            }
        }
    }
}
