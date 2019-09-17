/**
 * FILE:    Class GameMenuButtonData
 * COMPANY: POST Development LLC. (c) 2011
 * AUTHOR:  Frank Post
 *
 * PACKAGE: <GameMenuBase1063>
 *
 * PURPOSE: This Class <program> is a Dynamically Linked Library (DLL) that displays custom
 *          buttons that describes game specific information and settings. The protocol is
 *          defined in the "Game Menu DLL Requirements" document.
 *
 * USAGE:   <Used by the Setup function(s) of the ZSystem - refer to the ZSystem Documentation>
 *
 * VERSION: <see AssemblyInfo.cs>
 *
 * REVISION HISTORY:
 * -->DATE<--  -->VERSION<--  -->DESCRIPTION<--
 * 03-05-11       00.00.00     Initial construction.
 */

using System;
using System.Collections.Generic;
using System.Text;


namespace GameMenuBase1063
{
    public class GameMenuButtonData
    {
        public enum permissionBits
        {
            pAdmin = 1, pCaller = 2, pEditUser = 4, pEditGames = 8, pEditPayouts = 16,
            pPlayerAudit = 32, pSales = 64, pSalesMgr = 128, pSalesRpt = 256, pSetup = 512, pSpecial = 1024, pVerifyPrize = 2048
        };

        public const int gameSystemID = 1063;

        public static WEBService.Service service    = null;
        public static WEBService.Settings settings  = null;

        public static GameSetup frmGameSetup            = new GameSetup();
        public static TabsDeck frmTabsDeck = new TabsDeck();
        

        public GameMenuButtonData()
        {
            // Empty
        }


        public static string[] getGameMenuButtonTitles(string servAddress)
        {
            global::GameMenuBase1063.Properties.Settings.Default.GameMenuBase1034_WEBService_Service = servAddress;
            service = new WEBService.Service();
            service.Timeout = 300000;
            string tmpErr = service.lastError();
            settings = service.getSettings();
            tmpErr = service.lastError();
            string[] tmpStr = new string[6];
            tmpStr[0] = "Setup";
            tmpStr[1] = "Tabs Deck";
            tmpStr[2] = ""; // Reserved
            tmpStr[3] = ""; // Reserved
            tmpStr[4] = ""; // Reserved
            tmpStr[5] = ""; // Reserved

            return tmpStr;
        }


        public static int[] getGameMenuButtonPermissions()
        {
            int[] tmpPerms = new int[6];
            tmpPerms[0] = (int)permissionBits.pSetup;
            tmpPerms[1] = (int)permissionBits.pAdmin;
            tmpPerms[2] = 0;
            tmpPerms[3] = 0;
            tmpPerms[4] = 0;
            tmpPerms[5] = 0;

            return tmpPerms;
        }


        #region Buttons

        public static bool btn1Activity()       // returns true if successful
        {
            frmGameSetup.ShowDialog();
            return true;
        }

        public static bool btn2Activity()       // returns true if successful
        {
            frmTabsDeck.ShowDialog();
            return true;
        }
        

        #endregion Buttons
    }
}
