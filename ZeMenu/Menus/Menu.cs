using System;
using ZeMenu.Menus.Items;
namespace ZeMenu.Menus
{
    class Menu : MenuItem
    {
        internal Menu Parent { get; set;}
        internal MenuItem[] Items { get; } 
        internal Menu(string DisplayText, params MenuItem[] items) : base(DisplayText)
        {
            Items = items;
        }

        internal override void Activate()
        {
            throw new NotImplementedException();
        }
    }
}
