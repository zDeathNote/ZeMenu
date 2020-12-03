using System;
using ZeMenu.Commands;

namespace ZeMenu.Menus.Items
{
    internal class SpinnerMenuItem : MenuItem
    {

        private string UnformattedText { get; }
        private decimal LowerLimit { get; }
        private decimal UpperLimit { get; }
        private decimal SpinnerStep { get; }
        internal decimal SpinnerValue { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DisplayText">Needs to be a String.format type string where {0} will be substituted by SpinnerValue</param>
        /// <param name="Actions"></param>
        public SpinnerMenuItem(string DisplayText, decimal defaultValue, decimal step,
                                decimal lower, decimal upper, params Command[] Actions) : base(DisplayText, Actions)
        {
            UnformattedText = DisplayText;
            SpinnerValue = defaultValue;
            SpinnerStep = step;
            LowerLimit = lower;
            UpperLimit = upper;
            UpdateText();
        }

        internal override void Activate()
        {
            foreach(Command c in Actions)
            {
                c.DoAction(new object[] { SpinnerValue });
            }
        }

        internal void UpdateText()
        {
            DisplayText = String.Format(UnformattedText, SpinnerValue);
        }

        internal void StepUp()
        {
            if ((SpinnerValue + SpinnerStep) > UpperLimit)
            {
                return;
            }
            //SpinnerValue = Complex.Add(SpinnerValue, SpinnerStep).Real;
            SpinnerValue += SpinnerStep;
            UpdateText();
        }

        internal void StepDown()
        {
            if ((SpinnerValue - SpinnerStep) < LowerLimit)
            {
                return;
            }
            //SpinnerValue = Complex.Subtract(SpinnerValue, SpinnerStep).Real;
            SpinnerValue -= SpinnerStep;
            UpdateText();
        }
    }
}
