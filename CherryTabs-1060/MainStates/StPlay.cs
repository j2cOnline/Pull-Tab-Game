using System;
using System.Collections.Generic;
using System.Text;
using GameCore;
using GameCore.Timing;
using GameCore.MultiMedia;
using GameCore.Math;
using GameCore.Input;
using GameCore.Application;
using GameCore.Utility;
using BreakTheBankTabs1063;
//using BreakTheBankTabs1063.GameObjects;
using BreakTheBankTabs1063.utils;
using BreakTheBankTabs1063.GameObjects;

namespace BreakTheBankTabs1063.MainStates
{
    class StPlay: GameState
    {
        public bool autoReveal = false;
        public int[] randomIcon = null;
        Alarm almAutoRevealDelay = new Alarm(0.05f);
        int rCounter = 0;
        
        public decimal winnings = 0;
        public int freeTabsWon=0;

        //public int[] finalReelStops = null;
        public decimal[] finalLineAmounts = null;
        public int[] finalNoOfIconsWon = null;
        public decimal[,] payTable = null;
        int[,] ReadTabIcons = null;

        Alarm almSetIcons = new Alarm(1.5f);
        bool iconsSet = false;

        public StPlay(StateMain main)
            : base("Play-State", main)
        {
            //ReadTabIcons = new int[Definitions.ReelLength, Definitions.reelWidth];
            //StateMain.getVirtualReels(ref ReadTabIcons);

            
            
            randomIcon = new int[Definitions.IconsPerTab];
            for (int i = 0; i < Definitions.IconsPerTab; i++)
            {
                randomIcon[i] = i;
            }

            payTable = new decimal[10 + 1, Definitions.reelWidth];
            StateMain.getPaytable(ref payTable, 1);

            almAutoRevealDelay.Enable();
        }

        public String getGameStateData()
        {
            String data = "";
            for (int i = 0; i < Definitions.reelWidth*Definitions.iconsViewRows; i++)
            {
                data += ReadTabIcons[i % Definitions.reelWidth, i / Definitions.reelWidth] + ";";
            }

            for (int i = 0; i < Definitions.TotalPaylines; i++)
            {
                data += finalLineAmounts[i] + ";";
            }

            for (int i = 0; i < Definitions.TotalPaylines; i++)
            {
                data += finalNoOfIconsWon[i] + ";";
            }
            data += freeTabsWon+";"+winnings;

            return data;
        }

        public void setGameStateData(String data)
        {
            
            String[] parts=data.Split(';');
            int total = (Definitions.reelWidth * Definitions.iconsViewRows) + (Definitions.TotalPaylines * 2) + 2;
            if (parts.Length != total)
                throw new Exception("setGameStateData: Wrong no. of values");
            int shift = 0;



            for (int i = 0; i < Definitions.reelWidth * Definitions.iconsViewRows; i++)
            {
                ReadTabIcons[i % Definitions.reelWidth, i / Definitions.reelWidth]=int.Parse(parts[i]);
            }

            shift += Definitions.reelWidth * Definitions.iconsViewRows;

            for (int i = 0; i < Definitions.TotalPaylines; i++)
            {
                finalLineAmounts[i] = decimal.Parse(parts[i + shift]);
            }
            
            shift += Definitions.TotalPaylines;

            for (int i = 0; i < Definitions.TotalPaylines; i++)
            {
                finalNoOfIconsWon[i] = int.Parse(parts[i+shift]);
            }
            
            shift += Definitions.TotalPaylines;

            freeTabsWon = int.Parse(parts[shift]);
            winnings = decimal.Parse(parts[shift + 1]);
        }

        public void randomizeAutoRevealSequence()
        {
            for (int i = 0; i < 10; i++)
            {
                int a = context.RND.Next(0, Definitions.IconsPerTab-1);
                int b = context.RND.Next(0, Definitions.IconsPerTab - 1);
                swap(ref randomIcon[a], ref randomIcon[b]);
            }
        }

        void swap(ref int a, ref int b)
        {
            int c = a;
            a = b;
            b = c;
        }


        

        #region State Overrides...

        public override void OnUpdate(Keyboard keyboard, Mouse mouse, Timer timer)
        {

            if (context.btnRevealAll.Released)
            {
                context.btnRevealAll.Enable = false;
                autoReveal = true;
            }

            almSetIcons.Update(timer.DeltaTimeMS);
            if (almSetIcons.Check(Alarm.CheckType.TURNOFF) && !iconsSet)
            {
                iconsSet = true;
                context.iconsView.setIcons(ReadTabIcons);
            }


            if (autoReveal)
            {
                almAutoRevealDelay.Update(timer.DeltaTimeMS);
                if (almAutoRevealDelay.Check(Alarm.CheckType.RESET))
                {
                    if (context.soundsOn)
                    {
                        //context.Sounds.iconReveal.Stop(true);
                        //context.Sounds.iconReveal.Play(false, true);
                    }

                    int iconNo = randomIcon[rCounter];
                    if(context.iconsView.icons[iconNo].status==Icon.iconStatus.Concealed)
                        context.iconsView.icons[iconNo].Reveal();
                    rCounter++;
                    if (rCounter >= Definitions.IconsPerTab)
                    {
                        autoReveal = false;
                        rCounter = 0;
                    }
                }
            }

            if (context.iconsView.iconsRevealed >= Definitions.IconsPerTab) 
            {
                //done either win or lose...
                var pass = true;
                if (Bingo.singleton.localSettings.IsSpin)
                {
                    if (!context.spinDone())
                    {
                        pass = false;
                    }
                }
                if (pass)
                {
                    if (winnings > 0 || freeTabsWon > 0)
                        context.changeStateTo(context.stWin);
                    else
                        context.changeStateTo(context.stLose);
                }
            }


            //if (keyboard.KeyReleased('S'))
            //{
            //    Bingo.singleton.logError("test log");
            //    context.iconsView.icons[0].iconNo = 10;
            //    context.iconsView.icons[2].iconNo = 10;
            //    context.iconsView.icons[7].iconNo = 10;
            //    freeTabsWon = 4;
            //    autoReveal = true;
            //}
        }

        public override void OnRender(IGraphics graphics)
        {
            
        }

        public override void OnEnter()
        {

            //Reading Game icons...

            //PullTabSlots.setGameInfo(Definitions.TotalPaylines, (int)(context.TabPrices[context.tabPricesIdx] / context.TabPrices[0]), 0.01M);

            //finalReelStops = new int[Definitions.reelWidth];
            finalLineAmounts = new decimal[Definitions.TotalPaylines];
            finalNoOfIconsWon = new int[Definitions.TotalPaylines];
            ReadTabIcons = new int[Definitions.reelWidth, Definitions.iconsViewRows];

            winnings = StateMain.getGameInfo(ref ReadTabIcons, ref finalLineAmounts, ref finalNoOfIconsWon, ref freeTabsWon);

            if (!Bingo.singleton.localSettings.IsSpin)
            {
                context.iconsView.setIcons(ReadTabIcons);
            }
            else
            {
                iconsSet = false;
                almSetIcons.Enable();
                almSetIcons.Reset();

            }
            //Randomize icons just in case auto reveal was clicked...
            randomizeAutoRevealSequence();
            rCounter = 0;

            // Balance calculations...

            if (!context.freeTabsMode)
            {
                context.pinBalance -= context.costForReveal;
                //change odo instantly to prevent odo effect...
                context.odoBalance.setValueInstant(context.pinBalance);
            }

            

            //decrease the no. if free tabs mode
            if (context.freeTabsMode)
                context.freeTabsBalance--;


            context.saveRecoveryRecord();
            //context.tabNo=context.RND.Next(Bingo.singleton.localSettings.maxTabNo);
            //context.lblTabNo.Text =  context.tabNo.ToString();
        }

        public override void OnLeave()
        {
            autoReveal = false;

            context.btnRevealAll.Enable = context.btnRevealAll.Visible = false;

            // Balance calculations...

            context.pinBalance += context.wonAmount;
            
        }

        #endregion
    }
}
