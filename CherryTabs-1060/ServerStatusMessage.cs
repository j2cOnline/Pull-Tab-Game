/**
 * FILE:    Class ServerStatusMessage.cs
 * COMPANY: POST Development LLC. (c) 2011
 * AUTHOR:  Frank Post
 *
 * PACKAGE: <_90_BreakTheBank_3004>
 *
 * PURPOSE: This class displays messages to the console describing the current
 *          status of the internet connection.
 *
 * USAGE:
 *
 * VERSION: <see AssemblyInfo.cs>
 *
 * REVISION HISTORY:
 * -->DATE<--  -->VERSION<--  -->DESCRIPTION<--
 * 01-12-10       00.00.00     Initial construction.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace BreakTheBankTabs1063
{
    public partial class ServerStatusMessage : Form
    {
        const string defaultMessage = "Lost connection to the server.\n" +
                              "Please contact the attendant.\n\n" +
                              "Check Error Log";

        #region Variables
        
        #endregion

        public ServerStatusMessage()
        {
            InitializeComponent();
            lblMessage.Text = defaultMessage;
        }

        private void ServerStatusMessage_Load(object sender, EventArgs e)
        {
           // StatusMessage(1);
        }


        /*
         * Parse the address string
         */
        string ParseServerAddress(string address)
        {
            string[] parse = address.Split('/');
            return parse[2];
        }

        
        /**
         * Status messages to display.
         */
        private void StatusMessage(int message)
        {
            switch (message)
            {
                case 1:
                    lblMessage.Text = "Lost connection to the server.\n" +
                                      "Please contact the attendant.\n\n" +
                                      "Check Error Log";
                    break;

                default:
                    lblMessage.Text = "Lost connection to the server.\n" +
                                      "Please have attendant check the error log";
                    break;
            }
        }

        private void ServerStatusMessage_Leave(object sender, EventArgs e)
        {
            lblMessage.Text = defaultMessage;
        }

        private void ServerStatusMessage_VisibleChanged(object sender, EventArgs e)
        {
            if (lblMessage.Text.Length > defaultMessage.Length)
            {
                // increase size of form
                this.Size = new Size(900, 600);
                this.lblMessage.Font = new Font("Arial", 12);
            }
            else
            {
                // increase size of form
                this.Size = new Size(800, 150);
                this.lblMessage.Font = new Font("Arial", 20);
            }
        }
    }
}