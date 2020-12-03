using System.Drawing;
using System.Windows.Forms;
using GTA;
using ZeMenu.Menus;
using ZeMenu.Menus.Items;
using ZeMenu.States;

namespace ZeMenu
{
    class ZeMenu : Script
    {
        private bool MenuShowing { get; set; } = false;
        private int VERTICAL_INC { get; } = 30;

        private readonly MenuController MenuManager;
        private readonly CommandStateHandler StateHandler;


        RectangleF RectMenu = new RectangleF(20.0f, 30.0f, 300.0f, 400.0f);
        RectangleF RectSprite = new RectangleF(330.0f, 30.0f, 160.0f, 120.0f);
        internal readonly GTA.Font MenuFont = new GTA.Font("Arial", 0.6f, FontScaling.FontSize);
        internal readonly GTA.Font TickerFont = new GTA.Font("Arial", 0.4f, FontScaling.FontSize);

        internal Color MenuBgColor { get; set; } = Color.FromArgb(100, 0, 0, 0);
        internal Color ItemColor { get; set; } = Color.FromArgb(255, 255, 255, 255);
        internal Color ItemSelectColor { get; set; } = Color.FromArgb(255, 0, 128, 255);
        internal Color ItemEnabledColor { get; set; } = Color.FromArgb(255, 0, 200, 255);
        
        public ZeMenu()
        {
            int itemVis = (int)(RectMenu.Height / VERTICAL_INC) - 1;
            MenuManager = new MenuController(itemVis, Player);
            StateHandler = new CommandStateHandler(Player);
            //Player.Character.Weapons.

            this.KeyDown += ZeMenu_KeyDown;
            this.Tick += StateHandler.ZeMenu_TickStateHandler;
            this.PerFrameDrawing += ZeMenu_PerFrameDrawing;
        }

        private void ZeMenu_PerFrameDrawing(object sender, GraphicsEventArgs e)
        {
            if (MenuShowing)
            {
                //e.Graphics.Scaling = FontScaling.FontSize;
                e.Graphics.DrawRectangle(RectMenu, MenuBgColor);
                int count = 1;
                for (int i = MenuManager.ViewRangeStart; i <= MenuManager.ViewRangeEnd; i++)
                {
                    MenuFont.Color = i.Equals(MenuManager.SelectedIndex)
                            ? ItemSelectColor : ItemColor;

                    if (i.Equals(MenuManager.SelectedIndex) && MenuManager.ActiveMenu.Items[i] is SpriteMenuItem smi)
                    {
                        if (smi.GetTexture != null)
                        {
                            e.Graphics.DrawSprite(smi.GetTexture, RectSprite);
                        }
                    }

                    if (MenuManager.ActiveMenu.Items[i] is ToggleItem ti)
                    {
                        if (ti.State)
                        {
                            MenuFont.Color = ItemEnabledColor;
                        }
                    }

                   

                    e.Graphics.DrawText(MenuManager.ActiveMenu.Items[i].DisplayText, 30, count * VERTICAL_INC, MenuFont);
                    count++;
                }
                
                e.Graphics.DrawText((MenuManager.SelectedIndex + 1) 
                        + "/" + MenuManager.ActiveMenu.Items.Length, 20.0f, RectMenu.Height + VERTICAL_INC, TickerFont);
            }
            
        }

        private int KeyWait { get; } = 35;

        //perhaps switch to bindkey for single call actions
        private void ZeMenu_KeyDown(object sender, GTA.KeyEventArgs e)
        {
            if (e.Key == Keys.F7)
            {
                MenuShowing = !MenuShowing;
                if (CommandStates.States["LockControls"]) Player.CanControlCharacter = !MenuShowing;
                else Player.CanControlCharacter = true;
                Wait(KeyWait);
            }
            if (MenuShowing)
            {
                switch (e.Key)
                {
                    case Keys.NumPad0:
                        MenuManager.Previous();
                        Wait(KeyWait);
                        break;
                    case Keys.NumPad5:
                        MenuManager.Activate();
                        Wait(KeyWait);
                        break;
                    case Keys.NumPad8:
                        MenuManager.Up();
                        Wait(KeyWait);
                        break;
                    case Keys.NumPad2:
                        MenuManager.Down();
                        Wait(KeyWait);
                        break;
                    case Keys.NumPad4:
                        MenuManager.Left();
                        Wait(KeyWait);
                        break;
                    case Keys.NumPad6:
                        MenuManager.Right();
                        Wait(KeyWait);
                        break;
                }
            }
        }
    }
}
