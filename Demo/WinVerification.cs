/**
 * FILE:    Class WinVerification.cs
 * COMPANY: POST Development LLC. (c) 2011
 * AUTHOR:  Frank Post
 *
 * PACKAGE: <DoubleBonus_1034>
 *
 * PURPOSE: This class displays and describes the form that verifies win amounts.
 *
 * USAGE:
 *
 * VERSION: <see AssemblyInfo.cs>
 *
 * REVISION HISTORY:
 * -->DATE<--  -->VERSION<--  -->DESCRIPTION<--
 * 01-12-10       00.00.00     Initial construction.
 * 03-04-11                    Implemented new call and check of the user record and credentials.
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
    public partial class WinVerification : Form
    {
        enum RESPONSE { VERIFIED, USERNAME_UNKNOWN, PASSWORD_UNKNOWN, PERMISSION_DENIED };

        WEBService.Users userRecord = null;

        public WinVerification()
        {
            InitializeComponent();
        }

        decimal winAmount = 0;
        public decimal SetWinValue
        {
            set 
            {
                winAmount = value;
                lblWinAmount.Text = value.ToString("c");
            }
        }

        private void controlSelect(object sender, EventArgs e)
        {
            onScreenKeyboard1.activeControl = (Control)sender;
        }

        private void btnVerify_Click(object sender, EventArgs e)
        {
            // Prepare input text
            lblResultInfo.Text = "";
            txbLogin.Text.Trim();
            txbLogin.Text.ToUpper();
            txbPassword.Text.Trim();
            txbPassword.Text.ToUpper();

            RESPONSE response = checkCredentials(txbLogin.Text, txbPassword.Text);

            switch (response)
            {
                case RESPONSE.VERIFIED:
                    DialogResult = DialogResult.OK;
                    return;
                case RESPONSE.USERNAME_UNKNOWN:
                    lblResultInfo.Text = "User unknown, please try again!";
                    break;
                case RESPONSE.PASSWORD_UNKNOWN:
                    lblResultInfo.Text = "User password incorrect, please try again!";
                    break;
                case RESPONSE.PERMISSION_DENIED:
                    lblResultInfo.Text = "Permissions are not granted to this user for win verification";
                    break;
                default:
                    lblResultInfo.Text = "User unknown, please try again!";
                    break;
            }

            txbPassword.Text = "";
            txbLogin.Text    = "";
        }


        /// <summary>
        /// Return the credential reponse pertaining to the username, password input.
        /// The responses are as follows: 
        /// Username unknown, password unknown, permissions denied, verified (success).
        /// </summary>
        /// <param name="loginName">user login name</param>
        /// <param name="password">user password</param>
        /// <returns></returns>
        private RESPONSE checkCredentials(string loginName, string password)
        {
            //userRecord = Bingo.singleton.service.getUserDataByLoginName(loginName);

            if (userRecord.userID == 0)
                return RESPONSE.USERNAME_UNKNOWN;
            if (!userRecord.password.Equals(password))
                return RESPONSE.PASSWORD_UNKNOWN;
            if (!userRecord.verifyPrize)
                return RESPONSE.PERMISSION_DENIED;

            return RESPONSE.VERIFIED;
        }


        private void WinVerification_Load(object sender, EventArgs e)
        {
            txbLogin.Text    = "";
            txbPassword.Text = "";
        }

         protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
        }

        private void WinVerification_Activated(object sender, EventArgs e)
        {
            txbLogin.Focus();
        }

        private void WinVerification_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                lblResultInfo.Text = "";
                txbLogin.Focus();
            }
        }
    }
}