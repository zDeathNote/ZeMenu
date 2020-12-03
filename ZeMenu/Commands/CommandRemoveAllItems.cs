using GTA;

namespace ZeMenu.Commands
{
    class CommandRemoveAllItems : Command
    {
        internal CommandRemoveAllItems(Player p) : base(p)
        {
        }
        internal override void DoAction(params object[] args)
        {
            Target.Character.Weapons.RemoveAll();
        }
    }
}
