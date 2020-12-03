using System;

namespace ZeMenu.Menus.Items
{
    class ToggleItem : MenuItem
    {
        internal bool State { get => CommandStates.States[StateKey]; }
        internal String StateKey { get; private set; }
        internal ToggleItem(string DisplayText, string StateKey) : base(DisplayText)
        {
            this.StateKey = StateKey;
        }

        internal void SetState()
        {
            if (CommandStates.States.ContainsKey(StateKey)) {
                CommandStates.States[StateKey] = !CommandStates.States[StateKey];
            }
            //GTA.Game.Console.Print(CommandStates.ToDebugString());
        }

        internal override void Activate()
        {
            SetState();
        }
    }
}
