using GTA;
using ZeMenu.Menus.Items;

namespace ZeMenu.Menus
{
    class MenuController
    {
        private readonly MenuBuilder menuBuilder;
        internal Menu ActiveMenu { get; set; }
        internal Menu PreviousMenu { get; set; } = null;
        internal MenuItem SelectedItem { get; set; }
        internal int SelectedIndex { get; set; } = 0;

        internal int DefaultItemsVisible { get; }
        internal int ItemsVisible { get; private set; }
        internal int ViewRangeStart { get; set; } = 0;
        internal int ViewRangeEnd { get; set; }

        internal MenuController(int itemVis, Player P)
        {
            ItemsVisible = itemVis;
            DefaultItemsVisible = itemVis;
            ViewRangeEnd = ViewRangeStart + ItemsVisible;

            menuBuilder = new MenuBuilder(P);
            ActiveMenu = menuBuilder.MainMenu;
            SelectedItem = menuBuilder.MainMenu.Items[0];
            CheckWindowBounds();
        }

        internal void Activate()
        {
            if (SelectedItem is Menu m)
            {
                m.Parent = ActiveMenu;
                ActiveMenu = m;

                SelectedIndex = 0;
                SelectedItem = ActiveMenu.Items[SelectedIndex];

                ViewRangeStart = 0;
                ViewRangeEnd = ViewRangeStart + ItemsVisible;
                CheckWindowBounds();
            } 
            else
            {
                SelectedItem.Activate();
            }
            
        }

        internal void Previous()
        {
            if (ActiveMenu.Parent != null)
            {
                ActiveMenu = ActiveMenu.Parent;

                SelectedIndex = 0;
                SelectedItem = ActiveMenu.Items[SelectedIndex];

                ViewRangeStart = 0;
                ViewRangeEnd = ViewRangeStart + ItemsVisible;
                CheckWindowBounds();
            }
        }

        internal void Left() //Go a whole page 'up'
        {
            if (SelectedItem is SpinnerMenuItem spinner)
            {
                spinner.StepDown();
                return;
            }
            for (int i = 0; i < ItemsVisible; i++)
            {
                Up(false);
            }
        }

        internal void Right() //Go a whole page 'down'
        {
            if (SelectedItem is SpinnerMenuItem spinner)
            {
                spinner.StepUp();
                return;
            }
            for (int i = 0; i < ItemsVisible; i++)
            {
                Down(false);
            }
        }

        internal void Up(bool WrapAround = true)
        {
            if (SelectedIndex <= 0) //when we scroll up at at the top item
            {
                if (WrapAround) //scroll to bottom
                {
                    SelectedIndex = ActiveMenu.Items.Length - 1;
                    SelectedItem = ActiveMenu.Items[SelectedIndex];

                    //make sure the view window reflects our wrap around
                    ViewRangeEnd = SelectedIndex;
                    ViewRangeStart = ViewRangeEnd - ItemsVisible;
                }
                //do nothing at the top and WrapAround==false
            }
            else // normal up operation
            {
                SelectedIndex--;
                SelectedItem = ActiveMenu.Items[SelectedIndex];
            }

            if (SelectedIndex <= ViewRangeStart) //if cursor is at the top of the viewing range move the range up
            {                                    //unless it's already at the top
                ViewRangeStart--;
           
                if (ViewRangeStart < 0) ViewRangeStart = 0;
                else ViewRangeEnd--;
            }
            CheckWindowBounds();
        }

        internal void Down(bool WrapAround = true)
        {
            if (SelectedIndex >= (ActiveMenu.Items.Length - 1)) // when we scroll down on the bottom item
            {
                if (WrapAround) //scroll to top
                {
                    SelectedIndex = 0;
                    SelectedItem = ActiveMenu.Items[SelectedIndex];

                    //make sure the view window reflects our wrap around
                    ViewRangeStart = 0;
                    ViewRangeEnd = ViewRangeStart + ItemsVisible;
                }
                //do nothing at the bottom and WrapAround==false
            }
            else //normal down operation
            {
                SelectedIndex++;
                SelectedItem = ActiveMenu.Items[SelectedIndex];
            }

            if (SelectedIndex >= ViewRangeEnd) //if cursor is at the bottom of viewing range move the range down
            {                                  //unless it is already at the bottom
                ViewRangeEnd++;

                if (ViewRangeEnd > ActiveMenu.Items.Length - 1) ViewRangeEnd = ActiveMenu.Items.Length - 1;
                else ViewRangeStart++;
            }
            CheckWindowBounds();
        }

        //Make sure the ViewRange hasn't gone out of bounds and adjusts if necessary
        private void CheckWindowBounds()
        {   
            if (ViewRangeStart < 0) ViewRangeStart = 0;
            if (ViewRangeEnd > ActiveMenu.Items.Length - 1) ViewRangeEnd = ActiveMenu.Items.Length - 1;
        }
    }
}
