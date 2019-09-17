/**
 * FILE:    Class GamesMenu
 * COMPANY: POST Development LLC. (c) 2011
 * AUTHOR:  Frank Post
 *
 * PACKAGE: <TestGameMenuBaseDLL>
 *
 * PURPOSE: This Class <program> tests the GameMenuButtonData DLL. This <program> allows to test
 *          the GameMenuButtonData DLL without the need of integrating into the ZSystem.
 *
 * USAGE:   <Invoke Executable>
 *
 * VERSION: <see AssemblyInfo.cs>
 *
 * REVISION HISTORY:
 * -->DATE<--  -->VERSION<--  -->DESCRIPTION<--
 * 03-05-11       00.00.00     Initial construction.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using CoreUtilities;


namespace TestGameMenuBaseDLL
{
    public partial class GamesMenu : Form
    {
        public WEBService.InstalledGames[] gameList = null;

        public string fullPathToDLL = null;     // path and file name of DLL to load
        Assembly GameMenuBaseDLL    = null;     // reference to DLL
        Type GameMenuButtonDataType = null;     // reference to namespace.class
        
        public GamesMenu()
        {
            InitializeComponent();
        }

        private void clearBtns()
        {
            btn1.Text = "N/A";
            btn1.Enabled = false;
            btn2.Text = "N/A";
            btn2.Enabled = false;
            btn3.Text = "N/A";
            btn3.Enabled = false;
            btn4.Text = "N/A";
            btn4.Enabled = false;
            btn5.Text = "N/A";
            btn5.Enabled = false;
            btn6.Text = "N/A";
            btn6.Enabled = false;
        }
        

        private void GamesMenu_VisibleChanged(object sender, EventArgs e)
        {
            clearBtns();

            // Check for GameMenuBase.DLL in game directory
            fullPathToDLL = @"C:\Users\Eugene George\Documents\Visual Studio 2010\Projects\Z Systems2\CherryTabs\CherryTabs-1063\GameMenuBase1063\bin\Debug\GameMenuBase1063.dll";

            if (File.Exists(fullPathToDLL))
            {
                try
                {

                    GameMenuBaseDLL = Assembly.LoadFrom(fullPathToDLL);

                    GameMenuButtonDataType = GameMenuBaseDLL.GetType("GameMenuBase1063.GameMenuButtonData");
                    MethodInfo buttonTitleMethod = GameMenuButtonDataType.GetMethod("getGameMenuButtonTitles");
                    MethodInfo buttonPermissionMethod = GameMenuButtonDataType.GetMethod("getGameMenuButtonPermissions");

                    string tmpAddress = global::TestGameMenuBaseDLL.Properties.Settings.Default.TestGameMenuBaseDLL_WEBService_Service;
                    object[] tmpParams = new object[1];
                    tmpParams[0] = tmpAddress;
                    
                    string[] tmpTitles = (String[])buttonTitleMethod.Invoke(null, tmpParams);
                    int[] tmpPermissions = (int[])buttonPermissionMethod.Invoke(null, null);
                    
                    // Configure buttons
                    if (tmpTitles[0] != string.Empty)
                    {
                        btn1.Text = tmpTitles[0];
                        btn1.Enabled = true;
                    }
                    if (tmpTitles[1] != string.Empty)
                    {
                        btn2.Text = tmpTitles[1];
                        btn2.Enabled = true;
                    }
                    if (tmpTitles[2] != string.Empty)
                    {
                        btn3.Text = tmpTitles[2];
                        btn3.Enabled = true;
                    }
                    if (tmpTitles[3] != string.Empty)
                    {
                        btn4.Text = tmpTitles[3];
                        btn4.Enabled = true;
                    }
                    if (tmpTitles[4] != string.Empty)
                    {
                        btn5.Text = tmpTitles[4];
                        btn5.Enabled = true;
                    }
                    if (tmpTitles[5] != string.Empty)
                    {
                        btn6.Text = tmpTitles[5];
                        btn6.Enabled = true;
                    }

                    
                }
                catch (Exception ex)
                {
                    string tmpErr = "Exception attempting to Invoke DLL Method, ERROR: " + ex.Message;
                    Logger.Log(tmpErr);
                    MessageBox.Show(tmpErr);
                }
            }
            else
            {
                string tmpErr = fullPathToDLL + " not found!";
                Logger.Log(tmpErr);
                MessageBox.Show(tmpErr);
            }
        }

        
        #region Buttons

        private void btn1_Click(object sender, EventArgs e)
        {
            MethodInfo btnActivity = GameMenuButtonDataType.GetMethod("btn1Activity");
            bool tmpResult = (bool)btnActivity.Invoke(null, null);
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            MethodInfo btnActivity = GameMenuButtonDataType.GetMethod("btn2Activity");
            bool tmpResult = (bool)btnActivity.Invoke(null, null);
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            MethodInfo btnActivity = GameMenuButtonDataType.GetMethod("btn3Activity");
            bool tmpResult = (bool)btnActivity.Invoke(null, null);
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            MethodInfo btnActivity = GameMenuButtonDataType.GetMethod("btn4Activity");
            bool tmpResult = (bool)btnActivity.Invoke(null, null);
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            MethodInfo btnActivity = GameMenuButtonDataType.GetMethod("btn5Activity");
            bool tmpResult = (bool)btnActivity.Invoke(null, null);
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            MethodInfo btnActivity = GameMenuButtonDataType.GetMethod("btn6Activity");
            bool tmpResult = (bool)btnActivity.Invoke(null, null);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion Buttons

        private void button1_Click(object sender, EventArgs e)
        {
            

        }
    }

    
}