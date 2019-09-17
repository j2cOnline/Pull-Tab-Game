/**
 * FILE:    Class PlayData
 * COMPANY: POST Development LLC. (c) 2011
 * AUTHOR:  Frank Post
 *
 * PACKAGE: <PlayerAudit1013>
 *
 * PURPOSE: This Class displays and prints the game specific information according to the custom
 *          game requirements.
 *
 * USAGE:   <Created in PlayerAudit and used by the GameMenu function(s) of the ZSystem - refer
 *          to the ZSystem Documentation>
 *
 * VERSION: <see AssemblyInfo.cs>
 *
 * REVISION HISTORY:
 * -->DATE<--  -->VERSION<--  -->DESCRIPTION<--
 * 01-25-11       00.00.00     Initial construction.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CoreUtilities;
using System.Reflection;


namespace PlayerAudit1063
{
    public partial class PlayData : Form
    {
        public static WEBService.Service service           = null;
        public static WEBService.PINs curPIN               = null;
        public static WEBService.Sales curSale             = null;
        public static WEBService.Redemptions curRedemption = null;
        public static WEBService.Plays curPlay             = null;
        public static WEBService.InstalledGames instGame   = null;

        Font fntLg = new Font("Arial", 20, FontStyle.Bold);
        Font fntMd = new Font("Arial", 12, FontStyle.Regular);
        Font fntSm = new Font("Arial", 8, FontStyle.Regular);
        SolidBrush brushDark = new SolidBrush(Color.Black);
        SolidBrush brushMed = new SolidBrush(Color.Gray);

        const int iconsPerTab = 9;
        decimal[] winAmt = null;
        String tabNo = "";
        decimal winnings, tabPrice;

        public void logError(string tErr)
        {
            try
            {
                WEBService.LogEntry tmpEntry = new WEBService.LogEntry();
                tmpEntry.gameID = curPlay.gameSysID;
                tmpEntry.terminalID = curPlay.stationID;
                tmpEntry.userID = curPIN.PIN;
                tmpEntry.moduleName = "PlayerAudit1031.dll";
                service.logError(tmpEntry);
            }
            catch
            {
                // don't do anything but catch exception
            }
        }
                

        /// <summary>
        /// Constructor.
        /// </summary>
        public PlayData()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Dynamically create the ball and card labels.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlayData_Load(object sender, EventArgs e)
        {
            
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlayData_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                // Load Bingo Service
                service         = new WEBService.Service();
                service.Timeout = 300000;
                string tmpErr   = service.lastError();

                // Must get installed game data
                instGame = service.getInstalledGameData(curPlay.gameSysID);
                tmpErr   = service.lastError();

                // must get installedGame data
                instGame = service.getInstalledGameData(curPlay.gameSysID);
                tmpErr = service.lastError();
                try
                {
                    lblGameTitle.Text = "Break The Bank Tabs 1063";
                    string[] parts = curPlay.data.Split(',');

                    if (parts[1].ToLower() == "false")
                        lblGameType.Text = "Normal Reveal";
                    else
                        lblGameType.Text = "Free Tab Reveal";

                    tabNo = parts[2];
                    winnings = decimal.Parse(parts[3]);
                    tabPrice = decimal.Parse(parts[4]);
                    
                    lblTabNo.Text = "Tab#: " + parts[2];
                    lblWinnings.Text = "Winnings: " + winnings.ToString("C");
                    lblTabPrice.Text = "Tab Price: " + tabPrice.ToString("C") ;

                    pnlTab.Controls.Clear();

                    String msg = "Icons: \r\n";

                    for (int i = 0; i < iconsPerTab; i++)
                    {
                        
                        int iconNo = int.Parse(parts[i + 5]) + 1;
                        msg += iconNo.ToString() + ",";

                        String picFile = "ovr_AuditSlot_" + iconNo.ToString() + ".png";
                        PictureBox pic = new PictureBox();
                            
                        pic.Image = Bitmap.FromStream(Assembly.GetExecutingAssembly()
                               .GetManifestResourceStream("PlayerAudit1063.images." + picFile));

                        pnlTab.Controls.Add(pic);
                        pic.Left = (i % 3)*70;
                        pic.Top = (i/3) * 65;
                        pic.Width = 60;
                        pic.Height = 55;
                    }
                    msg+= "\r\nwin lines: \r\n";

                    int shift = 5 + iconsPerTab;
                    int remaining = 8;
                    

                    pnlWonPatterns.Controls.Clear();
                    
                    int curPat = 1;
                    int posIdx = 0;
                    winAmt = new decimal[remaining];

                    for (int i = 0; i < remaining; i++)
                    {

                        decimal patAmt = decimal.Parse(parts[shift + i]);
                        winAmt[i] = patAmt;
                        msg += "loc[" + (shift + i).ToString() + "]=" + patAmt.ToString() + ",";

                        if (patAmt > 0)
                        {
                            String picFile = "ovr_AuditPattern_" + curPat.ToString() + ".png";
                            PictureBox pic = new PictureBox();

                            pic.Image = Bitmap.FromStream(Assembly.GetExecutingAssembly()
                                   .GetManifestResourceStream("PlayerAudit1063.images." + picFile));

                            pnlWonPatterns.Controls.Add(pic);
                            pic.Left = (posIdx % 5) * 110;
                            pic.Top = (posIdx / 5) * 67;
                            pic.Width = 55;
                            pic.Height = 67;

                            Label lblAmt = new Label();

                            lblAmt.Text = patAmt.ToString("C");
                            lblAmt.AutoSize = true;
                            
                            pnlWonPatterns.Controls.Add(lblAmt);
                            lblAmt.Left = ((posIdx % 5) * 110) + 55;
                            lblAmt.Top = ((posIdx / 5) * 67) + 20;
                            
                            posIdx++;
                        }
                        curPat++;
                    }
                    //MessageBox.Show(msg);

                    lblBalance.Text = "Balance at end: " + curPlay.balance.ToString("C");
                    int ftWon = int.Parse(parts[remaining + shift]);
                    decimal bonusWon = decimal.Parse(parts[remaining + shift + 1]);
                    lblBonus.Text = "";

                    if (bonusWon > 0) 
                        lblBonus.Text = "Any Same Symbol Win: " + bonusWon.ToString("C");
                    if (ftWon > 0)
                        lblBonus.Text += " Free Tabs Won: "+ftWon.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("error " + ex.ToString());
                }
            }
        }


        private void PlayData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt)
                e.Handled = true;
        }


        /// <summary>
        /// Assign the components from the data parsed from the data string. Components
        /// dynamically create PlayData_Load are ball and card labels. All other componets
        /// are created in the corresponding design class.
        /// </summary>
        private void assignClassComponents()
        {
            
        }


        /// <summary>
        /// Parse the dataStr in the play record and load into class
        /// data variables.
        /// </summary>
        private void parseDataString(string data)
        {

        }

        


        #region Printing
        
        private void prtPlayDoc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            if (prtPlayDoc.PrinterSettings.IsValid == false)
            {
                string tmpErr = "No Printer connection detected!";
                MessageBox.Show(tmpErr);
                return;
            }
            float x = 0;    // e.PageSettings.PrintableArea.Left;
            float y = 0;    // start offset
            float width = e.PageSettings.PrintableArea.Width - x;

            y = DrawTextHorizontalCentered(lblGameTitle.Text, y, fntMd, brushDark, width, x, e.Graphics);
            y = DrawTextHorizontalCentered("Player Audit - Play Data", y, fntMd, brushDark, width, x, e.Graphics);
            y = DrawTextHorizontalCentered("PIN# " + curPIN.PIN.ToString(), y, fntMd, brushDark, width, x, e.Graphics);


            y += 10;
            // PIN Data (played or not)
            y = Draw2ItemRightAligned("Started:", curPIN.when.ToString(), y, fntSm, brushDark, width, x, e.Graphics);
            y = Draw2ItemRightAligned("Starting Balance:", curSale.saleAmt.ToString("C"), y, fntSm, brushDark, width, x, e.Graphics);
            if (curRedemption.transactionID > 0)
            {
                y = Draw2ItemRightAligned("Redeemed:", curRedemption.whenRedeemed.ToString(), y, fntSm, brushDark, width, x, e.Graphics);
                y = Draw2ItemRightAligned("Redeemed Balance:", curRedemption.amt.ToString("C"), y, fntSm, brushDark, width, x, e.Graphics);
                y = Draw2ItemRightAligned("Redeemed Winnings:", curRedemption.winnings.ToString("C"), y, fntSm, brushDark, width, x, e.Graphics);
            }
            else
            {
                y = Draw2ItemRightAligned("Redeemed:", "NOT REDEEMED", y, fntSm, brushDark, width, x, e.Graphics);
                y = Draw2ItemRightAligned("Redeemed Balance:", "N/A", y, fntSm, brushDark, width, x, e.Graphics);
                y = Draw2ItemRightAligned("Redeemed Winnings:", "N/A", y, fntSm, brushDark, width, x, e.Graphics);
            }
            y += 15;
            y = DrawTextHorizontalCentered("PlayID: " + curPlay.playID.ToString(), y, fntMd, brushDark, width, x, e.Graphics);
            y = DrawTextHorizontalCentered(curPlay.whenPlayed.ToString(), y, fntMd, brushDark, width, x, e.Graphics);
            y += 10;


            y = Draw2ItemRightAligned("Winnings:", winnings.ToString("C"), y, fntSm, brushDark, width, x, e.Graphics);
            y = Draw2ItemRightAligned("Tab No. ", tabNo, y, fntSm, brushDark, width, x, e.Graphics);
            y = Draw2ItemRightAligned("Tab Price:", tabPrice.ToString("C"), y, fntSm, brushDark, width, x, e.Graphics);
            y = Draw2ItemRightAligned("Play End Balance", curPlay.balance.ToString("C"), y, fntSm, brushDark, width, x, e.Graphics);
            
            y += 15;
            y = DrawTextHorizontalCentered("Patterns Won", y, fntMd, brushDark, width, x, e.Graphics);
            y += 10;

            for (int i = 0; i < winAmt.Length; i++)
            {
                if(winAmt[i]>0)
                    y = Draw2ItemRightAligned("Pattern " + (i+1).ToString(), winAmt[i].ToString("C"), y, fntSm, brushDark, width, x, e.Graphics);
            }
            
        }


        static public float Draw2ItemRightAligned(string text1, string text2, float y, Font font, Brush brush, float width, float margin, Graphics g)
        {
            SizeF size = g.MeasureString(text1, font);
            g.DrawString(text1, font, brush, new PointF(margin + 150 - size.Width, y));  // 150
            size = g.MeasureString(text2, font);
            g.DrawString(text2, font, brush, new PointF(margin + 275 - size.Width, y)); // 275
            return y + (size.Height * 1.1f);
        }

        static public float DrawTextHorizontalCentered(string text, float y, Font font, Brush brush, float width, float margin, Graphics g)
        {
            if (text == "")
                return y;

            SizeF size = g.MeasureString(text, font);
            g.DrawString(text, font, brush, new PointF(AlignCenter(margin, width, size.Width), y));

            return y + (size.Height * 1.1f);
        }

        static public float AlignCenter(float offset, float placementExtents, float extents)
        {
            return ((placementExtents - extents) * 0.5f) + offset;
        }
        #endregion Printing

        private void btnPrint_Click(object sender, EventArgs e)
        {
            curPIN = service.getPINData(curPlay.PIN);
            string tmpErr = service.lastError();
            curSale = service.getSalesData(curPIN.transactionID);
            tmpErr = service.lastError();
            curRedemption = service.getRedemptionData(curPIN.transactionID);
            tmpErr = service.lastError();
            prtPlayDoc.Print();
        }
    }
}