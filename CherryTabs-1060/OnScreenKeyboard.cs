using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;


namespace BreakTheBankTabs1063
{
    public partial class OnScreenKeyboard : UserControl
    {

        /// <summary>
        /// Should handle char values.. 
        /// '\r' for Enter / Return
        /// '\b' for backspace
        /// .. possible additions ..
        /// '\t' tab
        /// </summary>
        /// <param name="val"></param>
//        public delegate void OnKeyPressed( char val );
        //public event OnKeyPressed onKeyPressed = null; 

//        bool upper = false;
        public Control activeControl = null;

        public OnScreenKeyboard()
        {
            InitializeComponent();
        }

        private void Key_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            char val = btn.Text.ToCharArray()[0];
            if (btn.Text == "Space")
                val = ' ';

            if (activeControl != null)
            {
                activeControl.Text += val;
            }
            activeControl.Focus();            

//            if (onKeyPressed != null)
  //              onKeyPressed(val);
        }

        private void key_backspace_Click(object sender, EventArgs e)
        {
            if (activeControl != null)
                if (activeControl.Text.Length > 0)
                {
                    activeControl.Text = activeControl.Text.Substring(0, activeControl.Text.Length - 1);
                }
                activeControl.Focus();
        }

   }
}
