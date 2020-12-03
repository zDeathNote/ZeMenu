namespace ZeMenu.Menus.Items
{
    abstract class MenuItem
    {
        internal Commands.Command[] Actions { get; }
        internal string DisplayText { get; set; } 
        internal MenuItem(string DisplayText, params Commands.Command[] Actions)
        {
            this.DisplayText = DisplayText;
            this.Actions = Actions;
        }

        internal abstract void Activate();
    }
}
