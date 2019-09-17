using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

// GameCore ..includes..
using GameCore;
using GameCore.Timing;
using GameCore.MultiMedia;
using GameCore.Math;
using GameCore.Input;
using GameCore.Utility;
using GameCore.Application;
using System.Reflection;

namespace BreakTheBankTabs1063
{


    class Bingo : ApplicationBase
    {
        // global instance of the FastAction application 
        public static Bingo singleton = null;

        Alarm almMachineStatus = new Alarm(60.0f);

        // storage for the currency Symbol
        public string moneySymbol = "$";


        //public WEBService.Service service = null;
        public WEBService.PINs curPIN = new WEBService.PINs();
        public WEBService.Settings settings = null;
        public int curLevel = 0;
        //public WEBService.Games24[] curGames = new WEBService.Games24[4];
        public WEBService.EGMTerminals curEGMData = null;
        public const int gameSystemID = 1063;

        //        public WEBService.Progressive progressive = null;
        // new GameSettings
        public WEBService.GameSettings gameSettings = null;
        public Settings1063 localSettings = null;
        public int connectionAttempts = 0;

        private int tempWidth = 0, tempHeight = 0;
        private int FixWidth = 1360, FixHeight = 768;
        Point[] resArray = new Point[3]{
            new Point(1366,768),
            new Point(1360,768),
            new Point(1280,768)
        };

        public bool isXP = false;

        // Obsolete use Settings1034...

        //public int maxCardDisplayMode = 2;                  // 2 = default 15 cards display, 0= 2 cards, 1= 6 cards, 2= 15 cards
        //public int bPPatternIndex = 0;          // index into the payout set of the bonus pattern
        //public int bPPatternCount = 0;          // number of hits needed to go into the bonus round
        //public double bPPercent = 0F;        // percent of card cost contributed to the bonus fund/seed.
        //public decimal bPSeed = 0M;          // amount of $$ the seed is initialized (when the EGM is installed, not when it is awarded)
        //public LevelInfo[] levelInfo = new LevelInfo[4];

        public long terminalID = -1;
        public string serialNum = "Unknown";
        public int commandLinePIN = 0;

        public bool usingDemoPIN = false;
        public bool recovering = false;
        bool forcedSoundOff = false;

        GameStateMachine stateMachine = new GameStateMachine(); // manager/container for all Game states

        GuiManager guiManager = null;

        // state at which the buttons are activated
        public GameCore.MultiMedia.GuiButton.State buttonActionState =
            GameCore.MultiMedia.GuiButton.State.PRESSED;
        ServerStatusMessage serverMessage = new ServerStatusMessage();

        public void logError(string tErr)
        {
            Logger.Log(tErr);
            try
            {
                WEBService.LogEntry tmpEntry = new WEBService.LogEntry();
                tmpEntry.gameID = gameSystemID;
                tmpEntry.terminalID = curEGMData.terminalID;
                tmpEntry.userID = curPIN.PIN;
                tmpEntry.moduleName = "BreakTheBankTabs1063.exe";
                tmpEntry.errorStr = tErr;
                //Bingo.singleton.service.logError(tmpEntry);
            }
            catch (Exception ex)
            {
                Logger.Log("Exception while trying to save error to database, Error: " + ex.Message);
            }
        }

        public void saveRecoveryData()
        {

        }

        //public void parseGameSettingsData()
        //{

        //    // Commented temporarly by EG
        //    try
        //    {
        //        if (gameSettings.data != "")
        //        {
        //            // parse the nData into variables
        //            string[] parts = gameSettings.data.Split(',');
        //            if (parts.Length != 17)
        //            {
        //                throw new Exception("Invalid number of parameters in settings.data string("
        //                    + parts.Length.ToString() + ", should be 17)");
        //            }
        //            maxCardDisplayMode = int.Parse(parts[0]);
        //            bPPatternIndex = int.Parse(parts[1]);
        //            bPPatternCount = int.Parse(parts[2]);
        //            bPPercent = double.Parse(parts[3]);
        //            bPSeed = decimal.Parse(parts[4]);
        //            levelInfo[0].POSetNum = int.Parse(parts[5]);
        //            levelInfo[1].POSetNum = int.Parse(parts[6]);
        //            levelInfo[2].POSetNum = int.Parse(parts[7]);
        //            levelInfo[3].POSetNum = int.Parse(parts[8]);
        //            levelInfo[0].costMulti = int.Parse(parts[9]);
        //            levelInfo[1].costMulti = int.Parse(parts[10]);
        //            levelInfo[2].costMulti = int.Parse(parts[11]);
        //            levelInfo[3].costMulti = int.Parse(parts[12]);
        //            levelInfo[0].prizeMulti = int.Parse(parts[13]);
        //            levelInfo[1].prizeMulti = int.Parse(parts[14]);
        //            levelInfo[2].prizeMulti = int.Parse(parts[15]);
        //            levelInfo[3].prizeMulti = int.Parse(parts[16]);
        //        }
        //        else
        //        {
        //            string tmpErr = "This Game has not been configured by the POS, can not continue!";
        //            logError(tmpErr);
        //            MessageBox.Show(tmpErr);
        //            Application.Exit();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string tmpErr = "Invalid Data String in GameSettings, " + ex.Message;
        //        logError(tmpErr);
        //        MessageBox.Show(tmpErr);
        //        Application.Exit();
        //    }
        //}

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="standAlone">run in standalone mode.. no db connection required</param>
        public Bingo(string[] args)
        {
            singleton = this;
            // hide the cursor
            //Cursor.Hide();
            // new generic gameSettings data
            this.FormClosing += new FormClosingEventHandler(Bingo_FormClosing);

            Screen Srn = Screen.PrimaryScreen;
            tempWidth = Srn.Bounds.Width;
            tempHeight = Srn.Bounds.Height;
            StartScreen stScr = new StartScreen();
            System.Console.WriteLine("OSVersion: {0}", Environment.OSVersion.ToString());


            isXP = (Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor <= 1);
            if (isXP)
                FixWidth = 1366;
            stScr.Show();
            
                //try to change resolution...
                bool success = false;
                int i = 0;
                do
                {
                    if (i >= resArray.Length)
                    {
                        MessageBox.Show("None of resolutions worked for this hardware...");
                        Application.Exit();
                        return;
                    }
                    Resolution.CResolution ChangeRes = new Resolution.CResolution(resArray[i].X, resArray[i].Y, ref success);

                    i++;

                }
                while (!success);
            

            moneySymbol = String.Format("{0:C}", 1.00f);
            moneySymbol = moneySymbol.Substring(0, moneySymbol.IndexOf("1"));
            terminalID = GameCore.Utility.Functions.GetMachineID();

            string tmpStartupPath = Application.StartupPath;
            FileSearch.SetStartingSearchDirectory(tmpStartupPath + "\\Media\\");

            string tmpErr = "";
            //PARSE COMMAND LINE ARGS
            int commandLinePIN = 0;
            string webServiceAddress = "";
            //if (args.Length >= 3)
            //{
            //    if (args[1].Contains("http"))
            //    {
            //        webServiceAddress = args[1].ToString();
            //    }
            //    else
            //    {
            //        tmpErr = "Invalid command line WEB Service Address: " + args[1].Trim() + ", terminating Program!";
            //        Logger.Log(tmpErr);
            //        Bingo.singleton.CloseApplication(tmpErr);
            //        return;
            //    }
            //    // get the installed Game ID from the command line
            //    try
            //    {
            //        commandLinePIN = int.Parse(args[0].Trim());
            //    }
            //    catch
            //    {
            //        tmpErr = "Invalid command line PIN: " + args[0].Trim() + ", terminating Program!";
            //        Logger.Log(tmpErr);
            //        commandLinePIN = 0;
            //        Bingo.singleton.CloseApplication(tmpErr);
            //        return;
            //    }
            //}
            //else
            //{
            //    tmpErr = "Invalid command line Params, terminating Program!";
            //    Logger.Log(tmpErr);
            //    Bingo.singleton.CloseApplication(tmpErr);
            //    return;
            //}
            //===============================
            //global::BreakTheBankTabs1063.Properties.Settings.Default.MagicTouchTabs1034_WEBService_Service = webServiceAddress;
            //// ******* CONNECT TO THE DATABASE... ***********************
            //// CONNECT TO DATABASE SERVICE
            //if (service == null)
            //{
            //    int tries = 0;

            //    while (tries < 5)
            //    {
            //        service = new WEBService.Service();
            //        if (service != null)
            //        {
            //            service.Timeout = 10000;
            //            break;
            //        }
            //        Logger.Log("Attempt to connect to WEB Service #" + tries.ToString() + " failed!");
            //        tries++;
            //    }
            //    if (service == null)
            //    {
            //        Logger.Log("ALL Attempts to connect to WEB Service failed, Closing Application!");
            //        Bingo.singleton.CloseApplication("Error Connecting to WEB Service");
            //    }
            //}
            //try
            //{
            //    // sync with server date and time
            //    setLocalTime.SYSTEMTIME tmpTime = new setLocalTime.SYSTEMTIME();
            //    tmpTime.FromDateTime(service.getServerDateTime());
            //    setLocalTime.SetLocalTime(ref tmpTime);
            //}
            //catch (Exception ex)
            //{
            //    tmpErr = "Exception connecting to WEB Service calling 'service.getServerDateTime()' "
            //           + "Possibly Launcher passed incorrect WEB Service Address, CANNOT CONTINUE!\n\r" + ex.Message;
            //    Logger.Log(tmpErr);
            //    Bingo.singleton.CloseApplication(tmpErr);
            //    return;
            //}
            // get the EGM status
            curEGMData = new WEBService.EGMTerminals();
            // get serial number from command line
            //serialNum = args[2];
            //curEGMData.serialNum = serialNum;
            //if (curEGMData.terminalID == "0")
            //{
            //    // could not get just created EGMTerminals record
            //    tmpErr = service.lastError();
            //    if (tmpErr == "")
            //    {
            //        tmpErr = "Could not get EGMTerminals record for this Unit (TerminalID = " + terminalID.ToString() + "), terminating Program!";
            //    }
            //    logError(tmpErr);
            //    Bingo.singleton.CloseApplication(tmpErr);
            //    return;
            //}
            // now get the system settings
            settings = new WEBService.Settings();
            settings.verifyWinAmt = 1000;

            //gameSettings = service.getGameSettings("");



            //parseGameSettingsData();
            // now get command line PIN data
            //if (commandLinePIN == settings.demoPIN)
            //{
            //    usingDemoPIN = true;
            //    curPIN.PIN = settings.demoPIN;
            //    curPIN.balance = settings.demoAmt;
            //    curPIN.playing = true;
            //    curPIN.playingAt = curEGMData.terminalID;
            //    curPIN.transactionID = 0;
            //    curPIN.netSeconds = (int)Math.Round((settings.demoAmt * 100) / settings.penniesPerMinute);
            //    curPIN.winBalance = 0M;
            //}
            //else
            {
                curPIN = new WEBService.PINs();
                curPIN.balance = 200;

                //////////////////////////////////////////////////////////////////////
                //                curPIN.netSeconds = 0;
                //              curPIN.winBalance = 0M;
                ////////////////////////////////////////////////////////////////////
                //if (curPIN.PIN == 0)
                //{
                //    // command line PIN not found
                //    tmpErr = service.lastError();
                //    if (tmpErr == "")
                //    {
                //        tmpErr = "Could not get PINs record for PIN " + commandLinePIN.ToString() + ", can not continue!";
                //    }
                //    logError(tmpErr);
                //    Bingo.singleton.CloseApplication(tmpErr);
                //    return;
                //}
            }
            guiManager = new GuiManager();
            //            frmWinVerification = new WinVerification();
            //            Logger.Log("Finished 'Bingo(string[] args)'");

        }

        void Bingo_FormClosing(object sender, FormClosingEventArgs e)
        {
            //switch back to original res.
            bool tmpSuccess = false;
            Resolution.CResolution ChangeRes = new Resolution.CResolution(tempWidth, tempHeight, ref tmpSuccess);
            
        }

        /// <summary>
        /// Deconstructor....
        /// </summary>
        ~Bingo()
        {
            //empty
        }

        /// <summary>
        /// Updates the player's PIN record to the database.
        /// </summary>
        public void updatePinRecord()
        {
            //if (curPIN.PIN != settings.demoPIN)
            //{
            //    // Update PINs record in database
            //    if (!service.updatePINRec(curPIN))
            //    {
            //        string tmpErr = service.lastError();
            //        tmpErr = "Could NOT update PINs record, can not continue! " + tmpErr;
            //        logError(tmpErr);
            //        CloseApplication(tmpErr);
            //    }
            //}
            // <HERE handle if using DEMO PIN>
        }


        /// <summary>
        /// First loading sequence.. only called once from ApplicationBase:Load()
        /// </summary>
        /// <returns>returns true if load is succesful</returns>
        public override bool OnFirstLoad()
        {

            Logger.Log("\n*** -- BEGIN LOADING -- ***\n");

            // SETS THE DEFAULT STARTING SEARCH DIRECTORY 
            // BASED ON THE GAME BEING PLAYED....
            //            FileSearch.SetStartingSearchDirectory(FileSearch.GetApplicationDirectory() + "\\Media\\");

            almMachineStatus.Enable();
            try
            {
                MultiMediaLoader ml = new MultiMediaLoader("Bingo");
                ml.LoadFile("Bingo.txt");

                guiManager.Add(ml);
                ////////////////////////////////////////  un-Comment out for testing
                //                lblFps = (GuiLabel)guiManager.Get("lbl_fps");
                //                lblMessage = (GuiLabel)guiManager.Get("lbl_message");
                // add the 'StateLoading' state top the Bingo.stateMachine
                stateMachine.Add(new StateLoading(stateMachine));
                // change to (display) the 'Loading' state
                stateMachine.ChangeActiveState("Loading");
            }
            catch (Exception e)
            {
                Logger.Log("Exception Loading Loading State \n" + e.ToString());
                MessageBox.Show("Error Loading");
                return false;
            }
            //            Logger.Log("Finished 'OnFirstLoad()' \n");
            return true;
        }

        /// <summary>
        /// Starts a new Game
        /// </summary>
        public void StartNewGame()
        {
            //Logger.Log("\n*** -- New Game Starting -- ***\n");
            //Logger.Log("\n*** -- BEGIN PLAY -- ***\n");
        }

        /// <summary>
        /// unloads all memory
        /// </summary>
        protected override void OnUnload()
        {
            stateMachine.FreeResources();

            singleton = null;
            instance = null;
        }

        /// <summary>
        ///  called once per frame.. updates Logic
        /// </summary>
        /// <returns>returns false if app needs to close</returns>
        public override bool OnFrame()
        {

            try
            {
                // update statemachine
                if (stateMachine.Update(keyboard, mouse, timer) == false)
                    Stop();

                guiManager.Update(keyboard, mouse, timer);
                almMachineStatus.Update(timer.DeltaTimeMS);
                if (almMachineStatus.Check(Alarm.CheckType.RESET))
                {
                    // 60 seconds has past, update the machine status
                    if (curPIN.PIN != settings.demoPIN)
                    {
                        // now update machine status to playing this pin
                        //if (!service.updateMachineStatus(curEGMData.terminalID, curPIN.PIN, gameSystemID))
                        //{
                        //    string tmpErr = service.lastError();
                        //    if (tmpErr == "")
                        //    {
                        //        tmpErr = "Could not update machine status to playing PIN " + curPIN.PIN.ToString() + ", can not continue!";
                        //    }
                        //    logError(tmpErr);
                        //    MessageBox.Show(tmpErr);
                        //    Stop();
                        //    return false;
                        //}
                    }
                    // check sound on
                    // check sound on
                    //if (service.areEGMSoundsOff())
                    //{
                    //    forcedSoundOff = true;
                    //    if (StateMain.singleton != null)
                    //    {
                    //        StateMain.singleton.btnAudio.Enable = StateMain.singleton.soundsOn = false;
                    //    }

                    //}
                    else if (forcedSoundOff)
                    {
                        forcedSoundOff = false;
                        if (StateMain.singleton != null)
                        {
                            StateMain.singleton.btnAudio.Enable = StateMain.singleton.soundsOn = true;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                dumpLastFatalError();
                logError("\n\r***** Frame Error *****\n\r" + e.ToString());
                //                Logger.Log("\n\r***** Active State *****\n\r" + stateMachine.ActiveState.Name);
                if (stateMachine.ActiveState.Name == "LOADING")
                {
                    logError("\n\r***** Bypassing NULL Reference Exception while Loading *****\n\r");
                }
                else
                {
                    // TODO: connection Error Code, added 2 Lines below ************************************************************
                    Bingo.singleton.connectionAttempts = 0;
                    if (!connectToService())
                    {
                        if (!serverMessage.Visible)
                        {
                            serverMessage.lblMessage.Text = e.ToString();
                            serverMessage.ShowDialog();
                        }
                    }
                }
            }

            return true;
        }

        void dumpLastFatalError()
        {
            Logger.globalLogger = new Logger("LastFatalError_" + DateTime.Now.Year.ToString() + "-"
                                                    + DateTime.Now.Month.ToString() + "-"
                                                    + DateTime.Now.Day.ToString() + "-"
                                                    + DateTime.Now.Hour.ToString() + "-"
                                                    + DateTime.Now.Minute.ToString() + ".txt");
            dumpObject(StateMain.singleton, true);
            dumpObject(StateMain.singleton.iconsView, false);
            dumpObject(StateMain.singleton.stPlay, false);

        }

        void dumpObject(object obj, bool recursive)
        {
            Logger.Log("\r\n\r\nDumping: " + obj.ToString() + "\r\n\r\n");
            Type objType = obj.GetType();

            Logger.Log("Properties:");
            Logger.Log("------------------------");

            PropertyInfo[] props = objType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in props)
            {

                Logger.Log(prop.Name + "=" + prop.GetValue(obj, null) + "\n");

            }
            Logger.Log("Fields:");
            Logger.Log("------------------------");

            FieldInfo[] fields = objType.GetFields(BindingFlags.Public | BindingFlags.Instance);
            foreach (FieldInfo fld in fields)
            {

                Logger.Log(fld.Name + "=" + fld.GetValue(obj) + "\n");

            }
        }

        /// <summary>
        /// The render loop
        /// </summary>
        public override void OnRender()
        {
            try
            {
                // in directx we redraw everything every frame
                if (graphics.GType == IGraphics.Type.DirectX)
                {
                    stateMachine.ActiveState.InvalidateAll();
                    guiManager.InvalidateAll();
                }


                // draw the state
                stateMachine.Draw(graphics);

                /////////////////////////////////////////  un-Comment out for testing
                //                lblFps.Visible = false;
                //                lblFps.Text = "FPS " + timer.FramesPerSeconds.ToString();

                guiManager.Draw(graphics);

            }
            catch (Exception e)
            {
                MessageBox.Show("Unknown Render Error" + e.ToString());
                logError("\n***** Render Error *****\n" + e.ToString());
            }
        }


        public bool connectToService()
        {
            // CONNECT TO DATABASE SERVICE
            string tmpErr = "";
            //while (Bingo.singleton.connectionAttempts < 5)
            //{
            //    tmpErr = "";
            //    Bingo.singleton.connectionAttempts++;
            //    service = new WEBService.Service();
            //    if (service != null)
            //    {
            //        service.Timeout = 10000;
            //        try
            //        {
            //            // make simple web service call to verify connection
            //            DateTime tmpVal = service.getServerDateTime();
            //            break;
            //        }
            //        catch (Exception ex)
            //        {
            //            tmpErr = "Attempt to connect to WEB Service #" + Bingo.singleton.connectionAttempts.ToString() + " failed! ERROR: " + ex.Message;
            //            logError(tmpErr);
            //        }
            //    }
            //    else
            //    {
            //        tmpErr = "Attempt to instanciate WEB Service #" + Bingo.singleton.connectionAttempts.ToString() + " failed!";
            //        logError(tmpErr);
            //    }
            //}
            //if ((service == null) || (tmpErr != ""))
            //{
            //    logError("ALL Attempts to connect to WEB Service failed, Closing Application! ERROR = " + tmpErr);
            //    serverMessage.lblMessage.Text = "Error Connecting to WEB Service, ERROR " + tmpErr;
            //    serverMessage.ShowDialog();
            //    Bingo.singleton.CloseApplication("");
            //    return false;
            //}
            return true;
        }
        /// <summary>
        /// Stops the application
        /// </summary>
        public void Stop()
        {
            running = false;
        }

        /// <summary>
        /// Frees the loaded data for the stateMachine
        /// </summary>
        public void FreeStateResources()
        {
            stateMachine.FreeResources();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public string FormatCurrency(decimal amount)
        {

            return String.Format("{0:C}", amount);
        }
    }
}

