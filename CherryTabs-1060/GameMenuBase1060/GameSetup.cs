/**
 * FILE:    Class GameSetup
 * COMPANY: POST Development LLC. (c) 2011
 * AUTHOR:  Frank Post
 *
 * PACKAGE: <GameMenuBase1063>
 *
 * PURPOSE: This Class displays the setup form describing customs settings for the game.
 *
 * USAGE:   <Invoked by btn1Activity() in Class GameMenuButtonData>
 *
 * VERSION: <see AssemblyInfo.cs>
 *
 * REVISION HISTORY:
 * -->DATE<--  -->VERSION<--  -->DESCRIPTION<--
 * 03-05-11       00.00.00     Initial construction.
 * 
 * Eugene George - Change to be used for Pull Tabs Game...
 * 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CoreUtilities;
using BreakTheBankTabs1063;

namespace GameMenuBase1063
{
    

    public partial class GameSetup : Form
    {
        public WEBService.GameSettings gameSettings = new WEBService.GameSettings();
        public WEBService.Service service = null;

        public bool modified = false;
        Settings1063 settings = null;

        class denominationItem
        {
            public decimal denomination = 0;
            public override string ToString()
            {
                return denomination.ToString("C");
            }
        }

        public GameSetup()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Upon program initialization establish a connection with the bingo web service.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameSetup_Load(object sender, EventArgs e)
        {
            // Load bingoservice
            service = new WEBService.Service();
            service.Timeout = 300000;
            string tmpErr = service.lastError();
            lbDenominations.Sorted = true;

            settings = new Settings1063();
        }


        /// <summary>
        /// Upon entering this form retrieve the current game settings record
        /// and set the correspong form variables.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameSetup_VisibleChanged(object sender, EventArgs e)
        {
            // When the form shows
            if (Visible)
            {
                

                gameSettings = GameMenuButtonData.service.getGameSettings(GameMenuButtonData.gameSystemID);

                if (gameSettings.data == "")
                {
                    settings = new Settings1063("4,0.25,0.50,0.75,1,999999");
                    MessageBox.Show("Game settings initialized for the first time, Please Modify and Save the settings!");
                }
                else
                {
                    //try to parse existing data...

                    try
                    {
                        settings = new Settings1063(gameSettings.data);
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(ex.Message);
                        MessageBox.Show(ex.Message);
                        settings = new Settings1063();
                    }
                }

                lbDenominations.Items.Clear();

                //populate the listbox...
                for (int i = 0; i < settings.noOfDenominations; i++)
                {
                    denominationItem item = new denominationItem();
                    item.denomination = settings.gameDenominations[i];
                    lbDenominations.Items.Add(item);
                }
                chkSpin.Checked = settings.IsSpin;

            }

        }

        
        /// <summary>
        /// Save the seetings record.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {

            settings.noOfDenominations = lbDenominations.Items.Count;
            settings.gameDenominations = new decimal[settings.noOfDenominations];
            for (int i = 0; i < lbDenominations.Items.Count;i++ )
            {
                denominationItem denItm = (denominationItem)lbDenominations.Items[i];
                settings.gameDenominations[i] = denItm.denomination;
            }
            settings.IsSpin = chkSpin.Checked;

            gameSettings.data = settings.ToDataString();

            gameSettings.gameSysID = GameMenuButtonData.gameSystemID;
            gameSettings.whenSaved = DateTime.Now;

            if (!GameMenuButtonData.service.saveGameSettings(gameSettings))
            {
                // error saving gameSettings
                string tmpErr = GameMenuButtonData.service.lastError();
                if (tmpErr == "")
                    tmpErr = "DID NOT SAVE CHANGES, UNKNOWN ERROR!";
                
                MessageBox.Show(tmpErr);
                return;
            }
            modified = false;
            DialogResult = DialogResult.OK;
        }


        /// <summary>
        /// Cancel save action.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            bool cancelChanges = true;
            if (modified)
            {
                // Prompt to verify losing changes
                cancelChanges = (MessageBox.Show("Cancel Changes?", "WARNING", MessageBoxButtons.YesNo) == DialogResult.Yes);
            }
            if (cancelChanges)
            {
                // Cancel changes
                DialogResult = DialogResult.Cancel;
            }
        }


        /// <summary>
        /// Enables the save button and sets the modified bool to true.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void modified_Changed(object sender, EventArgs e)
        {
            modified = true;
            btnSave.Enabled = true;
        }

        private void btnAddDenomination_Click(object sender, EventArgs e)
        {
            denomination denom = new denomination();
            if (denom.ShowDialog() == DialogResult.OK)
            {
                decimal val = denom.nudDenomination.Value;
                denominationItem d=new denominationItem();
                d.denomination=val;

                lbDenominations.Items.Add(d);
                modified_Changed(sender, e);
            }
        }

        private void lbDenominations_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbDenominations.SelectedIndex != -1)
                btnRemoveDenomination.Enabled = true;
            else
                btnRemoveDenomination.Enabled = false;
        }

        private void btnRemoveDenomination_Click(object sender, EventArgs e)
        {
            lbDenominations.Items.Remove(lbDenominations.SelectedItem);
            modified_Changed(sender, e);
        }
    }
}