using GTA;

namespace ZeMenu.Commands
{
    class CommandGiveItem : Command
    {
        internal Weapon Item;
        internal int Amount { get; }
        internal CommandGiveItem(Player p, Weapon weap, int amt) : base(p)
        {
            Item = weap;
            Amount = amt;
        }
        internal override void DoAction(params object[] args)
        {
            Ped p = Target.Character;
            p.Weapons[Item].Ammo = p.Weapons[Item].MaxAmmo;
        }
    }
}