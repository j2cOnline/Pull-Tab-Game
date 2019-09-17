using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

using GameCore;
using GameCore.Timing;
using GameCore.MultiMedia;
using GameCore.Math;
using GameCore.Input;
using GameCore.Application;
using GameCore.Utility;
using BreakTheBankTabs1063.MainStates;
using BreakTheBankTabs1063.GameObjects;
using BreakTheBankTabs1063.utils;

namespace BreakTheBankTabs1063
{


    public class StateMain : IGameState
    {
        public static StateMain singleton = null;
        // game constants
        public double sfX = 1.0F;                   // 1.0 = scale value for 800x
        public double sfY = 1.0F;                   // 1.0 = scale value for 600y

        public bool soundsOn=true;

        public AnimatedLabel animWholePull = null; 

        public GuiPictureBox picMainBackGround = null;
        public GuiPictureBox picMainOverlay = null;
        public GuiPictureBox picFreeTabsWinSign = null;
        public GuiPictureBox picFreeTabsTotalWinSign = null;
        public Sign totalWinSign = null;
        //labels

        public GuiLabel lblBalance = null;
        public GuiLabel lblWon = null;
        public GuiLabel lblCost = null;
        public GuiLabel lblFreeTabs = null;
        public GuiLabel lblFreeTabsNo = null;
        public GuiLabel lblLineAmount = null;
        public GuiLabel lblTabNo = null;
        public int tabNo = 0;

        //buttons

        public GuiButton btnRevealAll = null;
        public GuiButton btnContinue = null;

        public GuiButton btnDenomination = null;
        

        
        public GuiButton btnCashout = null;
        public GuiButton btnHelpOptions = null;

        public IconsView iconsView = null;
        public IFont fntDenomination = null;
        public IFont fntFreeTabsSign = null;
        public IFont fntPaytablePrizes = null;
        public IFont fntGameOver = null;
        
        //animations...

        

        public AnimatedLabel animMiniLogo = null;


        // ----------------------------- The Game States --------------------------------

        public GameState currentState = null;  // the current game state

        //game states...

        public GameState stCredit = null;
        public GameState stPlay = null;
        public GameState stWin = null;
        public GameState stLose = null;
        public GameState stHelpOptions = null;
        public GameState stGameOver = null;

        //Top Layer Animations
        List<AnimatedLabel> Animaions = new List<AnimatedLabel>();

        //Game Meters

        public int activeLines = 25;
        public int betPerLine = 1;
        public int tabPricesIdx =0;

        public decimal pinBalance = 0;
        public decimal wonAmount = 0;
        public decimal costForReveal = 0;
        public TextOdo odoBalance = null;
        public TextOdo odoFreeTabs = null;

        //Free Tabs Mode
        public struct freeTabsPerScatters
        {
            public int freeTabs;
            public int scatters;
        }
        public freeTabsPerScatters []scatters=new freeTabsPerScatters[3];
        public bool freeTabsMode = false;
        bool transitionDone = false;
        Alarm almTransDelay = new Alarm(0.05f);
        Alarm almMeterCycle = new Alarm(2.0f);
        int curFreeTabsCycle = 0;
        public int freeTabsBalance = 0;
        public decimal freeTabsTotalWon = 0;
        public Alarm almTotalWinSign = new Alarm(2.0f);
        // Help / Options / Pay Table
        
        public GuiPictureBox picHelpOptions = null;
        public GuiPictureBox picPayTable = null;
        public GuiPictureBox picWinPatterns = null;

        public GuiButton btnPayouts = null;
        public GuiButton btnHelp = null;
        public GuiButton btnWinPatterns = null;
        public GuiButton btnBack = null;
        public GuiButton btnAudio = null;

        public decimal[] TabPrices = new decimal []{ 0.05M, 0.10M, 0.25M, 0.50M, 1M };    //TBR... setting
        public Random RND = new Random((int)DateTime.Now.Ticks);

        public SoundSystem Sounds = null;
        public decimal totalWon = 0;
        public StateMain()
            : base("Main")
        {
            // set onLoad
            // singleton = this;
        }

        public override bool OnLoad()
        {
            singleton = this;
            sfX = ((double)IGraphics.Singleton.ScreenWidth / (double)1024);
            sfY = ((double)IGraphics.Singleton.ScreenHeight / (double)768);


            try
            {
                Bingo.singleton.localSettings = new Settings1063("4,0.25,0.50,0.75,1.00,");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error parsing local settings: " + ex.Message);
                Application.Exit();
            }


            BMLoader ml = new BMLoader("MainLoader");
            try
            {
                ml.Add(StateLoading.singleton.mediaObjs);
                ml.LoadFile("Main.txt");
                guiManager.Add(ml);


                Sounds = new SoundSystem(ml);   //load sounds...

                fntDenomination = (IFont)ml.GetObject("denomination_font");
                fntFreeTabsSign = (IFont)ml.GetObject("FreeTabsSign_font");
                fntPaytablePrizes = (IFont)ml.GetObject("Paytable_font");
                fntGameOver = (IFont)ml.GetObject("gameover_font");

                picMainBackGround = (GuiPictureBox)ml.GetObject("bg_MainScreen_pic");
                picMainOverlay = (GuiPictureBox)ml.GetObject("bg_MainScreen_ExtraSpins_pic");

                picFreeTabsWinSign = (GuiPictureBox)ml.GetObject("ovr_FreeTabsSign_pic");
                picFreeTabsTotalWinSign = (GuiPictureBox)ml.GetObject("ovr_TotalWon_pic");
                picHelpOptions = (GuiPictureBox)ml.GetObject("bg_Help_pic");
                picPayTable = (GuiPictureBox)ml.GetObject("bg_Pays_pic");
                picWinPatterns = (GuiPictureBox)ml.GetObject("bg_WinningPatterns_pic");


                btnRevealAll = (GuiButton)ml.GetObject("btn_RevealAll_button");
                btnContinue = (GuiButton)ml.GetObject("btn_btn_Continue");
                btnDenomination = (GuiButton)ml.GetObject("btn_Denomination_button");
                animWholePull = (AnimatedLabel)ml.GetObject("anim_WholeTabPull_Small_animation"); 

                //btnSelectLines = (GuiButton)ml.GetObject("btn_SelectLines_button");
                btnCashout = (GuiButton)ml.GetObject("btn_Cashout_button");
                btnHelpOptions = (GuiButton)ml.GetObject("btn_HelpOptions_button");
                //btnBetUp = (GuiButton)ml.GetObject("btn_BetUp_button");
                //btnBetDown = (GuiButton)ml.GetObject("btn_BetDown_button");

                btnBack = (GuiButton)ml.GetObject("btn_Back_button");
                btnHelp = (GuiButton)ml.GetObject("btn_Help_button");
                btnPayouts = (GuiButton)ml.GetObject("btn_Payouts_button");
                btnWinPatterns = (GuiButton)ml.GetObject("btn_WinningPatterns_button");
                btnAudio = (GuiButton)ml.GetObject("btn_Audio_button");

                
                //Labels

                lblBalance = (GuiLabel)ml.GetObject("lbl_balance");
                lblWon = (GuiLabel)ml.GetObject("lbl_won");
                lblCost = (GuiLabel)ml.GetObject("lbl_cost");
                lblFreeTabs = (GuiLabel)ml.GetObject("lbl_freeTabsLabel");
                lblFreeTabsNo = (GuiLabel)ml.GetObject("lbl_TabsNo");
                lblLineAmount = (GuiLabel)ml.GetObject("lbl_LineAmount");
                lblTabNo = (GuiLabel)ml.GetObject("lbl_TabNumber");

                // updating bounds so that buttons will not interfere...
                
                btnCashout.Bounds=new Rectangle(btnCashout.X,btnCashout.Y,btnCashout.Width-70,btnCashout.Height);
                btnDenomination.Bounds = new Rectangle(btnDenomination.X, btnDenomination.Y, btnDenomination.Width - 10, btnDenomination.Height-35); ;

                
                animMiniLogo = (AnimatedLabel)ml.GetObject("anim_MiniLogo_animation");

                
                

                //Game States...

                stCredit = new StCredit(this);
                stPlay = new StPlay(this);
                stWin = new StWin(this);
                stLose = new StLose(this);
                stHelpOptions = new StHelpOptions(this);
                stGameOver = new StGameOver(this);


                // Creating Complex Objects

                
                iconsView = new IconsView(ml);
                guiManager.Add(iconsView);

                odoBalance = new TextOdo(lblBalance, Bingo.singleton.curPIN.balance, "C",0.05M);
                guiManager.Add(odoBalance);

                odoFreeTabs = new TextOdo(lblFreeTabs, 0M, "#",1M);
                odoFreeTabs.Active = false;
                guiManager.Add(odoFreeTabs);


                totalWinSign = new Sign(picFreeTabsTotalWinSign, StWin.animComing, StWin.animLeaving);
                guiManager.Add(totalWinSign);

                almTotalWinSign.Enable();
                almTotalWinSign.Reset();
                

                //order items...

                picMainOverlay.Visible = false;
                guiManager.MoveToFront(picMainBackGround);
                guiManager.MoveToFront(picMainOverlay);  
                guiManager.MoveToFront(iconsView);
                guiManager.MoveToFront(btnContinue);
                guiManager.MoveToFront(btnRevealAll);

                guiManager.MoveToFront(animWholePull);
                animWholePull.Resest(); 	 	
                animWholePull.Stop(); 	  
                animWholePull.Interactive = true; 

                //guiManager.MoveToFront(btnBetUp);
                //guiManager.MoveToFront(btnBetDown);

                //guiManager.MoveToFront(btnSelectLines);
                guiManager.MoveToFront(btnCashout);
                guiManager.MoveToFront(btnHelpOptions);

                guiManager.MoveToFront(animMiniLogo);
                guiManager.MoveToFront(lblBalance);
                guiManager.MoveToFront(lblWon);
                guiManager.MoveToFront(lblCost);
                guiManager.MoveToFront(lblFreeTabs);
                guiManager.MoveToFront(lblFreeTabsNo);

                
                guiManager.MoveToFront(lblLineAmount);
                guiManager.MoveToFront(lblTabNo);
                guiManager.MoveToFront(picFreeTabsWinSign);
                guiManager.MoveToFront(picFreeTabsTotalWinSign);

                guiManager.MoveToFront(picWinPatterns);
                guiManager.MoveToFront(picPayTable);
                guiManager.MoveToFront(picHelpOptions);
                guiManager.MoveToFront(btnDenomination);
                guiManager.MoveToFront(btnWinPatterns);
                guiManager.MoveToFront(btnPayouts);
                guiManager.MoveToFront(btnHelp);
                guiManager.MoveToFront(btnBack);
                guiManager.MoveToFront(btnAudio);

                picWinPatterns.Visible = false;
                picPayTable.Visible = false;
                picHelpOptions.Visible = false;
                btnWinPatterns.Visible = false;
                btnPayouts.Visible = false;
                btnHelp.Visible = false;
                btnBack.Visible = false;
                btnAudio.Visible = false;
                btnAudio.ToggleMode = true;

                // Meters & Service...

                activeLines = Definitions.TotalPaylines;

                pinBalance = Bingo.singleton.curPIN.balance;

                TabPrices = new decimal[Bingo.singleton.localSettings.noOfDenominations];

                for (int i = 0; i < Bingo.singleton.localSettings.noOfDenominations; i++)
                {
                    TabPrices[i] = Bingo.singleton.localSettings.gameDenominations[i];
                }

                // TBR...

                for (int i = 0; i < scatters.Length; i++)
                {
                    scatters[i] = new freeTabsPerScatters();
                    scatters[i].freeTabs = Definitions.Scatters[i + 2];
                    scatters[i].scatters = (i + 3);
                }

                //Bingo.singleton.service.deleteGameRecoveryData(Bingo.singleton.terminalID.ToString());

                #region Game Recovery

                try
                {
                    
                    
                    // change current state...
                    changeStateTo(stCredit);
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Recovery Data Error: " + ex.Message);
                }

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show("StateMain - OnLoad: " + ex.ToString());
            }
            return true;
        }


        public override void OnUnload()
        {
        }

        public override void OnEnter()
        {
        }

        public override void OnExit()
        {
            //            btnAudio.Toggled = false;
            Sound.soundsEnabled = true;
        }

        public static void getVirtualReels(ref int[,] virtualReels)
        {
            int i, j;

            for (i = 0; i < Definitions.ReelLength; i++)
            {
                for (j = 0; j < Definitions.reelWidth; j++)
                {
                    //ReadTabIcons[i, j] = StateMain.singleton.RND.Next(Definitions.iconIndexMax);
                    virtualReels[i, j] = Definitions.VirtualReels[i, j];
                }
            }
        }

        public static void getPaytable(ref decimal[,] paytable, decimal denomination)
        {
            int i, j;

            for (i = 0; i < Definitions.iconIndexMax; i++)
            {
                for (j = 0; j < Definitions.reelWidth; j++)
                {
                    paytable[i, j] = Definitions.PAYTABLE[i, j] * denomination;
                }
            }
        }

        public static void getPaylines(ref int[,] paylines)
        {
            int i, j;

            for (i = 0; i < Definitions.TotalPaylines * 3; i++)
            {
                for (j = 0; j < Definitions.reelWidth; j++)
                {
                    paylines[i, j] = Definitions.PAYLINES[i, j];
                }
            }
        }

        //public static Random RND = new Random((int)DateTime.Now.Ticks);

        public static int[] createRandomTab()
        {

            int[] FINALreelStops = new int[Definitions.reelWidth];

            int[] tab = new int[9];

            for (int i = 0; i < Definitions.reelWidth; i++)
            {
                int FINALreelStop = StateMain.singleton.RND.Next(Definitions.ReelLength);
                for (int j = 0; j < Definitions.iconsViewRows; j++)
                {
                    int ind = (FINALreelStop + j) % 256;
                    tab[(i * 3) + j] = Definitions.VirtualReels[ind, i];
                }
            }

            return tab;

        }
        static int ctid = 0;

        public static decimal getGameInfo(ref int[,] ReadTabIcons, ref decimal[] FINALlineAmountWonDecimal, ref int[] FINALnumberOfIconsOnLine, ref int FINALfreeTabs)
        {
            

            System.Console.WriteLine("----------- Get Info -----------");
            decimal totalWin = 0;


            WEBService.PullTab tab = new WEBService.PullTab();
            tab.TabID = ctid++;
            tab.DealID = ctid%4000;
            tab.TabData = createRandomTab();

            //WEBService.PullTab tab = Bingo.singleton.service.GetPullTab(Bingo.gameSystemID, 0);


            StateMain.singleton.lblTabNo.Text = tab.DealID.ToString("000") + "-" + tab.TabID.ToString("0000");
            for (int i = 0; i < 9; i++)
                ReadTabIcons[i % Definitions.reelWidth, i / Definitions.reelWidth] = tab.TabData[i];

            for (int i = 0; i < Definitions.TotalPaylines; i++)             // current payline
            {
                int repCount = 0;
                int firstIcon = -1;
                bool cut = false;
                
                
                for (int j = 0; j < Definitions.reelWidth; j++)         //current col
                {

                    for (int k = 0; k < Definitions.iconsViewRows; k++) //current row
                    {
                        bool payIcon = (Definitions.PAYLINES[(i * 3) + k, j] > 0);

                        
                        int icon = ReadTabIcons[j, k];
                        if (payIcon)
                        {
                            if (firstIcon == -1)
                            {
                                repCount = 1;
                                firstIcon = icon;
                            }
                            else if (icon == firstIcon && !cut)
                                repCount++;
                            else
                                cut = true;
                        }
                    }
                }
                
               
                if (firstIcon >= 0)
                {
                    decimal amount = Definitions.PAYTABLE[firstIcon, repCount - 1] * (StateMain.singleton.TabPrices[StateMain.singleton.tabPricesIdx]/10);
                    System.Console.WriteLine("payline: " + i + " Icon=" + firstIcon + "  reps=" + repCount+" amt: "+amount.ToString("C"));
                    
                    FINALnumberOfIconsOnLine[i] = repCount;
                    FINALlineAmountWonDecimal[i] = amount;
                    totalWin += amount;
                }
                else
                    System.Console.WriteLine("Icon=X");
            }
            int[] sym = analyzeTab(ReadTabIcons);
            int AnySymNo = 0, CherryNo = sym[Definitions.iconIndexMax - 1];
            
            for (int i = 0; i < (Definitions.iconIndexMax - 1); i++)
            {
                if (AnySymNo < sym[i])
                    AnySymNo = sym[i];
            }

            //find best bonus...
            FINALfreeTabs = 0;
            for (int i = (Definitions.freeTabsPayTable.Length - 1); i >= 0; i--)
            {
                if (Definitions.freeTabsPayTable[i].isCherry)
                {
                    if (CherryNo >= Definitions.freeTabsPayTable[i].repeatitions && FINALfreeTabs < Definitions.freeTabsPayTable[i].FreeTabs)
                        FINALfreeTabs = Definitions.freeTabsPayTable[i].FreeTabs;
                }
                else
                {
                    if (AnySymNo >= Definitions.freeTabsPayTable[i].repeatitions)
                    {
                        //FINALfreeTabs = Definitions.freeTabsPayTable[i].FreeTabs;

                        decimal amt = Definitions.freeTabsPayTable[i].FreeTabs * (StateMain.singleton.TabPrices[StateMain.singleton.tabPricesIdx] / 10);
                        totalWin += amt;
                    }
                }
            }
            
            StateMain.singleton.totalWon = totalWin;
            return totalWin;
        }

        
        static int[] analyzeTab(int[,] tab)
        {
            int[] result = new int[Definitions.iconIndexMax];

            for (int sym = 0; sym < result.Length; sym++)
            {
                int co = 0;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (tab[i, j] == sym)
                            co++;
                    }
                }
                result[sym] = co;
            }

            return result;
        }

        public override string OnUpdate(Keyboard keyboard, Mouse mouse, GameCore.Timing.Timer timer)
        {
            guiManager.Update(keyboard,mouse,timer);

            currentState.OnUpdate(keyboard, mouse, timer);

            //calculate cost from controls...
            costForReveal = TabPrices[tabPricesIdx];
            if (odoBalance.Value != pinBalance)
                odoBalance.Value = pinBalance;


            lblWon.Text = wonAmount.ToString("C");
            lblCost.Text = (freeTabsMode)?"Bonus":costForReveal.ToString("C");

            updateTransitions(keyboard, mouse, timer);

            if (totalWinSign.active == false && totalWinSign.phase == 1)
            {
                almTotalWinSign.Update(timer.DeltaTimeMS);
                if(almTotalWinSign.Check(Alarm.CheckType.RESET))
                {
                    totalWinSign.endAnimation();
                }
            }

            //update free tabs meters...

            if (freeTabsMode)
            {
                odoFreeTabs.Value = freeTabsBalance;

                btnRevealAll.Enable = btnContinue.Enable = false;
            }
            else
            {
                almMeterCycle.Update(timer.DeltaTimeMS);
                if(almMeterCycle.Check(Alarm.CheckType.RESET))
                {
                    do
                    {
                        curFreeTabsCycle++;
                        if (curFreeTabsCycle >= Definitions.freeTabsPayTable.Length)
                            curFreeTabsCycle = 0;
                    } while (!Definitions.freeTabsPayTable[curFreeTabsCycle].isCherry);
                    //lblFreeTabsNo.Text = scatters[curFreeTabsCycle].scatters.ToString();
                    lblFreeTabs.Text = "   X "+Definitions.freeTabsPayTable[curFreeTabsCycle].repeatitions.ToString()+"\r\n"+
                        Definitions.freeTabsPayTable[curFreeTabsCycle].FreeTabs.ToString()+" bonus tabs";
                }
            }

            //if (freeTabsMode && !Sounds.enterFreeTabs.Playing() && !Sounds.freeTabsLoop.Playing())
            //{
                
            //}

            return "";
        }


        public override void OnRender(IGraphics graphics)
        {

            guiManager.Draw(graphics);
            currentState.OnRender(graphics);


            //Draw Denomination...
            String strDenom = "";
            if (TabPrices[tabPricesIdx] >= 1M)
                strDenom = "$" + TabPrices[tabPricesIdx].ToString("#.##");
            else
                strDenom = ((int)(TabPrices[tabPricesIdx] * 100)).ToString() + "¢";
            if (btnDenomination.Visible)
            {
                int x = btnDenomination.X+90;
                int y = btnDenomination.Y+64;

                if (btnDenomination.Enable)
                {
                    graphics.DrawText(x - 1, y - 1, strDenom, Color.FromArgb(0x0, 0x0, 0x0), fntDenomination, GuiItem.Align.XCenter);
                    graphics.DrawText(x + 1, y + 1, strDenom, Color.FromArgb(0x0, 0x0, 0x0), fntDenomination, GuiItem.Align.XCenter);
                }
                graphics.DrawText(x, y, strDenom, Color.FromArgb(0xff, 0xff, 0xff), fntDenomination, GuiItem.Align.XCenter);
            }



            if (totalWinSign.phase == 1 && totalWinSign.active == false)
            {
                graphics.DrawText(650, 420, freeTabsTotalWon.ToString("C"), Color.White, fntFreeTabsSign, GuiItem.Align.XCenter);
            }

            if (currentState != stHelpOptions)
            {
                for (int i = 5; i < Definitions.freeTabsPayTable.Length; i++)
                {
                    decimal amount = Definitions.freeTabsPayTable[i].FreeTabs * (StateMain.singleton.TabPrices[StateMain.singleton.tabPricesIdx] / 10);
                    graphics.DrawText(240, 180 - (i - 5) * 37, amount.ToString("C"), Color.White, fntPaytablePrizes, GuiItem.Align.XCenter);
                }

                for (int i = 0; i < Definitions.iconIndexMax; i++)
                {
                    decimal amount = Definitions.PAYTABLE[i, 2] * (StateMain.singleton.TabPrices[StateMain.singleton.tabPricesIdx] / 10);
                    graphics.DrawText(240, 220 + i * 53, amount.ToString("C"), Color.White, fntPaytablePrizes, GuiItem.Align.XCenter);
                }
                //2 more for cherries
                {
                    decimal amount = Definitions.PAYTABLE[Definitions.iconIndexMax - 1, 1] * (StateMain.singleton.TabPrices[StateMain.singleton.tabPricesIdx] / 10);
                    graphics.DrawText(240, 538, amount.ToString("C"), Color.White, fntPaytablePrizes, GuiItem.Align.XCenter);

                    amount = Definitions.PAYTABLE[Definitions.iconIndexMax - 1, 0] * (StateMain.singleton.TabPrices[StateMain.singleton.tabPricesIdx] / 10);
                    graphics.DrawText(240, 591, amount.ToString("C"), Color.White, fntPaytablePrizes, GuiItem.Align.XCenter);
                }
            }
        }


        /// <summary>
        /// Causes the entire screen to be redrawn
        /// used when rendering with directX
        /// </summary>
        public override void InvalidateAll()
        {
            guiManager.InvalidateAll();
        }

        /// <summary>
        /// Used to release the reference count of desired variables
        /// </summary>
        public override void _FreeResources()
        {
            guiManager.FreeResources();
        }


        public void changeStateTo(GameState newState)
        {
            if (currentState != null)
            {
                currentState.OnLeave();
            }
            currentState = newState;
            currentState.OnEnter();
        }


      

        #region Free Tabs Mode Management

        public void startFreeTabsMode()
        {

            lblFreeTabs.MoveAbsolute(1204, 59);

            if (freeTabsMode == false)
            {
                freeTabsTotalWon = 0;
                freeTabsMode = true;
            }            

            transitionDone = false;
            lblFreeTabsNo.Visible = false;
            animMiniLogo.Visible = false;
            odoFreeTabs.Active = true;

            if (soundsOn)
            {
                Sounds.freeTabsLoop.Play(true, true);
                Sounds.enterFreeTabs.Play(false, true);
            }
            picMainOverlay.Visible = true;

        }

        public void endFreeTabsMode()
        {
            lblFreeTabs.MoveAbsolute(1236, 65);

            freeTabsMode = false;
            transitionDone = false;

            lblFreeTabsNo.Visible = true;
            animMiniLogo.Visible = true;
            odoFreeTabs.Active = false;
            Sounds.freeTabsLoop.Stop(true);
            totalWinSign.startAnimation();
            picMainBackGround.Visible = true;

            picMainBackGround.Alpha = 0;
            picMainOverlay.Alpha = 1f;
        }

        void updateTransitions(Keyboard keyboard, Mouse mouse, GameCore.Timing.Timer timer)
        {
            if (!transitionDone)
            {
                almTransDelay.Update(timer.DeltaTimeMS);

                if (freeTabsMode)
                {
                    if(almTransDelay.Check(Alarm.CheckType.RESET))
                    {

                        picMainBackGround.Alpha -= 0.04f;
                        picMainOverlay.Alpha += 0.04f;
                        if (picMainBackGround.Alpha <= 0f && picMainOverlay.Alpha >= 1f)
                        {
                            picMainBackGround.Visible = false;
                            picMainOverlay.Visible = true;
                            transitionDone = true;
                        }
                    }
                }
                else
                {
                    if (almTransDelay.Check(Alarm.CheckType.RESET))
                    {
                        picMainBackGround.Alpha += 0.04f;
                        picMainOverlay.Alpha -= 0.04f;
                        if (picMainBackGround.Alpha >= 1f && picMainOverlay.Alpha <= 0f)
                        {
                            picMainBackGround.Visible = true;
                            picMainOverlay.Visible = false;
                            transitionDone = true;
                        }
                    }
                }
            }
        }

        #endregion

        #region Service Calls

        public bool updatePINRecord()
        {
            //if (Bingo.singleton.curPIN.PIN != Bingo.singleton.settings.demoPIN)
            //{
            //    Bingo.singleton.curPIN.balance = pinBalance;    //update variable...

            //    // update PINs record in database
            //    if (!Bingo.singleton.service.updatePINRec(Bingo.singleton.curPIN))
            //    {
            //        string tmpErr = Bingo.singleton.service.lastError();
            //        tmpErr = "Could NOT update balance in PINs record, can not continue! " + tmpErr;
            //        Bingo.singleton.logError(tmpErr);
            //        Bingo.singleton.CloseApplication(tmpErr);
            //        return false;
            //    }
            //    else
            //        return true;
            //}
            //else
                return true;
        }

        public void savePlay()
        {
            //if (Bingo.singleton.curPIN.PIN != Bingo.singleton.settings.demoPIN)
            //{

            //    Bingo.singleton.curPIN.balance = pinBalance;    //update variable...

            //    // create play record
            //    WEBService.Plays curPlay = null;

            //    curPlay = new WEBService.Plays();
            //    curPlay.whenPlayed = DateTime.Now;
            //    curPlay.balance = Bingo.singleton.curPIN.balance;
            //    curPlay.winBalance = 0.0M;
            //    curPlay.stationID = Bingo.singleton.curEGMData.terminalID;
            //    curPlay.gameSysID = Bingo.gameSystemID;
            //    curPlay.PIN = Bingo.singleton.curPIN.PIN;
            //    curPlay.data = DataFunctions.convertGameDataToString(Bingo.gameSystemID, freeTabsMode, iconsView.icons);
                
            //    curPlay.playID = Bingo.singleton.service.savePlayData(curPlay);
            //    if (curPlay.playID == 0)
            //    {
            //        string tmpErr = Bingo.singleton.service.lastError();
            //        tmpErr = "Could NOT create PlayData record, can not continue! " + tmpErr;
            //        Bingo.singleton.logError(tmpErr);
            //        Bingo.singleton.CloseApplication(tmpErr);
            //    }


            //    if (!Bingo.singleton.service.addToGameSysPlay(Bingo.singleton.gameSettings.gameSysID, costForReveal, wonAmount))
            //    {
            //        string tmpErr = Bingo.singleton.service.lastError();
            //        tmpErr = "addToGameSysPlay Error! " + tmpErr;
            //        Bingo.singleton.logError(tmpErr);
            //        Bingo.singleton.CloseApplication(tmpErr);
            //    }
            //}
        }

        public void saveRecoveryRecord()
        {
            return;
            if (currentState == stGameOver)
                return;

            if (Bingo.singleton.curPIN.PIN != Bingo.singleton.settings.demoPIN)
            {
                 // Save data... 0,1
                string tmpData = Bingo.gameSystemID.ToString() + ","
                    + currentState.stateName + ",";
                
                //save the 3 inputs...2,3,4
                tmpData += betPerLine.ToString() + "," + tabPricesIdx.ToString() + "," + activeLines.ToString() + ",";

                // save icons...5-22
                for (int i = 0; i < Definitions.IconsPerTab; i++)
                {
                    tmpData += iconsView.icons[i].iconNo + ",";
                    tmpData += (int)iconsView.icons[i].status + ",";
                }
                //23
                tmpData += ((StPlay)stPlay).autoReveal.ToString() + ",";
                //24
                if(currentState!=stCredit)
                    tmpData += ((StPlay)stPlay).getGameStateData();
                //25,26,27,28
                tmpData += "," + freeTabsMode + "," + freeTabsBalance + "," + lblTabNo.Text;
                tmpData += "," + ((StPlay)stPlay).winnings;

                if (!utils.FileBasedRecovery.saveDatabaseRecoveryData(tmpData))
                {
                    string tmpErr ="Could not update game recovery record in database, can not continue! ";
                    Bingo.singleton.logError(tmpErr);
                    MessageBox.Show(tmpErr);
                    Application.Exit();
                }
                
            }
        }

        public void deleteRecoveryRecord()
        {
            try
            {
                bool tmpResult = utils.FileBasedRecovery.clearDatabaseRecoveryData();
            }
            catch
            {
                // do nothing
            }
        }

        public void cashOut()
        {
            deleteRecoveryRecord();

            //if (!Bingo.singleton.service.updateMachineStatus(Bingo.singleton.curEGMData.terminalID, 0, 0))
            //{
            //    string tmpErr = "Could not UPDATE EGM STATUS! " + Bingo.singleton.service.lastError();
            //    Bingo.singleton.logError(tmpErr);
            //}
            if (updatePINRecord())
            {
                string tmpErr = "Successfull CASHOUT PIN, terminating Program!";
                Logger.Log(tmpErr);
                Application.Exit();
            }
        }

        #endregion
    }
}
