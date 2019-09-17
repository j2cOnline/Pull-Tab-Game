using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using BreakTheBankTabs1063;
using BreakTheBankTabs1063.utils;
using System.Reflection;
//using MagicTouchTabs1063.utils;
//using MagicTouchTabs1063.utils;


namespace GameMenuBase1063
{
    public partial class TabsDeck : Form
    {
        public static Random RND = new Random((int)DateTime.Now.Ticks);
        public TabsDeck()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(runCreateDeck);

            th.Start();
        }


        void runCreateDeck()
        {
            GameMenuButtonData.service.ClearPullTabDeck(GameMenuButtonData.gameSystemID, 0);
            int c=0;
            for (int deal = 0; deal < 250; deal++)
            {
                SetControlPropertyThreadSafe(lblStatus, "Text", "Creating Deal " + deal.ToString() + " of 250");
                List<WEBService.PullTab> tabs = new List<WEBService.PullTab>();
                for (int tabID = 0; tabID < 4000; tabID++)
                {
                    WEBService.PullTab tab = new WEBService.PullTab();
                    tab.TabID = c++;
                    tab.DealID = deal;
                    tab.TabData = createRandomTab();
                    tabs.Add(tab);
                }
                GameMenuButtonData.service.CreatePullTabs(tabs.ToArray());
            }

            SetControlPropertyThreadSafe(lblStatus, "Text", "Creating Deal 250 of 250\r\nDone!");
        }

        public static int[] createRandomTab()
        {

            int[] FINALreelStops = new int[Definitions.reelWidth];

            int[] tab = new int[9]; 

            for (int i = 0; i < Definitions.reelWidth; i++)
            {
                int FINALreelStop = RND.Next(Definitions.ReelLength);
                for (int j = 0; j < Definitions.iconsViewRows; j++)
                {
                    int ind = (FINALreelStop + j) % 256;
                    tab[(i * 3) + j] = Definitions.VirtualReels[ind, i];
                }
            }

            return tab;
           
        }


        private delegate void SetControlPropertyThreadSafeDelegate(Control control, string propertyName, object propertyValue);

        public void SetControlPropertyThreadSafe(Control control, string propertyName, object propertyValue)
        {
            try
            {
                System.Console.WriteLine("Text=" + propertyValue.ToString());
                if (control.InvokeRequired)
                {
                    control.Invoke(new SetControlPropertyThreadSafeDelegate(SetControlPropertyThreadSafe), new object[] { control, propertyName, propertyValue });
                }
                else
                {
                    control.GetType().InvokeMember(propertyName, BindingFlags.SetProperty, null, control, new object[] { propertyValue });
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
        }
    }
}
