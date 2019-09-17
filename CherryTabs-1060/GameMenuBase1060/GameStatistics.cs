/**
 * FILE:    Class GameStatistics
 * COMPANY: POST Development LLC. (c) 2011
 * AUTHOR:  Frank Post
 *
 * PACKAGE: <GameMenuBase1013>
 *
 * PURPOSE: This Class displays the game statistics form describing game performance data.
 *
 * USAGE:   <Invoked by btn2Activity() in Class GameMenuButtonData>
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
using System.Text;
using System.Windows.Forms;


namespace GameMenuBase1063
{
    public partial class GameStatistics : Form
    {
        public GameStatistics()
        {
            InitializeComponent();
        }


        //
        // Reserved to display custom game statistics
        //


        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}