/**
 * FILE:    Class PlayerAudit
 * COMPANY: POST Development LLC. (c) 2011
 * AUTHOR:  Frank Post
 *
 * PACKAGE: <PlayerAudit1013>
 *
 * PURPOSE: This Class <program> is a Dynamically Linked Library (DLL) that displays game specific
 *          information. The protocol is defined in the "Player Audit DLL Requirements" document.
 *
 * USAGE:   <Used by the GameMenu function(s) of the ZSystem - refer to the ZSystem Documentation>
 *
 * VERSION: <see AssemblyInfo.cs>
 *
 * REVISION HISTORY:
 * -->DATE<--  -->VERSION<--  -->DESCRIPTION<--
 * 01-25-11       00.00.00     Initial construction.
 */

using System;
using System.Collections.Generic;
using System.Text;


namespace PlayerAudit1063
{
    public class PlayerAudit
    {
        public static PlayData frmPlayData = new PlayData();

        public static bool getPlayData(int playID, DateTime wPlayed, int gID, int gSysID, int PIN,
            string sID, decimal bal, decimal winBal, int nSecs, string data, string servAddr)
        {
            global::PlayerAudit1063.Properties.Settings.Default.PlayerAudit1013_WEBService_Service = servAddr;

            PlayData.curPlay = new WEBService.Plays();
            PlayData.curPlay.playID = playID;
            PlayData.curPlay.whenPlayed = wPlayed;
            PlayData.curPlay.gameID = gID;
            PlayData.curPlay.gameSysID = gSysID;
            PlayData.curPlay.PIN = PIN;
            PlayData.curPlay.stationID = sID;
            PlayData.curPlay.balance = bal;
            PlayData.curPlay.winBalance = winBal;
            PlayData.curPlay.netSeconds = nSecs;
            PlayData.curPlay.data = data;

            frmPlayData.Show();

            return true;
        }

        public static bool closePlayData()
        {
            if (frmPlayData.Visible)
                frmPlayData.Hide();
            return true;
        }
    }
}
