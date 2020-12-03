namespace ZeMenu.Menus.Items
{
    class TextItem : MenuItem
    {
        internal TextItem(string DisplayText, params Commands.Command[] commands) : base(DisplayText, commands)
        {
            
        }

        internal override void Activate()
        {
            foreach (Commands.Command c in Actions)
            {
                c.DoAction();
            }
        }
    }
}
