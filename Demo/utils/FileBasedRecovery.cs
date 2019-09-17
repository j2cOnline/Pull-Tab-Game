using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using GameCore;
using GameCore.Utility;
using System.Windows.Forms;
namespace BreakTheBankTabs1063.utils
{
    class FileBasedRecovery
    {
        public const String recoveryFile = "recovery.dat";
        public static String pathPerfix = System.Windows.Forms.Application.StartupPath + "\\";

        //public static string getDatabaseRecoveryData()
        //{
        //    //String recoveryData = Bingo.singleton.service.getGameRecoveryData(Bingo.singleton.curEGMData.terminalID);

           
        //        //return recoveryData;
        //}

        public static bool saveDatabaseRecoveryData(String data)
        {

            //if (!Bingo.singleton.service.saveGameRecoveryData(Bingo.singleton.curEGMData.terminalID, data))
            {
                //string tmpErr = Bingo.singleton.service.lastError();
                //tmpErr = "Could not update game recovery record in database, can not continue! " + tmpErr;
                //Bingo.singleton.logError(tmpErr);
                //MessageBox.Show(tmpErr);
                //Bingo.singleton.Stop();
            }

            return true;
        }


        public static bool clearDatabaseRecoveryData()
        {
            try
            {
                //bool tmpResult = Bingo.singleton.service.deleteGameRecoveryData(Bingo.singleton.curEGMData.terminalID);
            }
            catch
            {
                // do nothing
                return false;
            }

            return true;
        }
        public static String getFileBasedRecoveryData()
        {
            String recoveryData = "";

            if (!File.Exists(pathPerfix + recoveryFile))
                return recoveryData;

            try
            {
                TextReader rFile = new StreamReader(pathPerfix + recoveryFile);
                recoveryData = rFile.ReadToEnd();
                rFile.Close();
            }
            catch (Exception ex)
            {
                Logger.Log("getFileBasedRecoveryData Error: " + ex.ToString());
                recoveryData = null;
            }
            return recoveryData;
        }

        public static bool saveFileBasedRecoveryData(String data)
        {
            bool saveSuccess = true;
            try
            {
                TextWriter rFile = new StreamWriter(pathPerfix + recoveryFile);
                rFile.Write(data);
                rFile.Close();
            }
            catch (Exception ex)
            {
                Logger.Log("saveFileBasedRecoveryData Error: " + ex.ToString());
                saveSuccess = false;
            }
            return saveSuccess;
        }


        public static bool clearFileBasedRecoveryData()
        {
            try
            {
                if (File.Exists(pathPerfix + recoveryFile))
                    File.Delete(pathPerfix + recoveryFile);
            }
            catch (Exception ex)
            {
                Logger.Log("clearFileBasedRecoveryData Error: " + ex.ToString());
                return false;
            }

            return true;
        }
    }

}
