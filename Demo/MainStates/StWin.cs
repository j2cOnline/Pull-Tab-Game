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
using GameCore.AnimationCore;
using BreakTheBankTabs1063;
//using BreakTheBankTabs1063.GameObjects;
using BreakTheBankTabs1063.utils;
using System.Drawing;


namespace BreakTheBankTabs1063.MainStates
{
    class StWin : GameState
    {
        Alarm almWinDelay = new Alarm(5.0f);
        StPlay play = null;
        KeyedAnimation ka = null;
        public static AnimationKey[] animComing = new AnimationKey[1];
        public static AnimationKey[] animLeaving = new AnimationKey[1];
        Alarm almFreeTabsWinSignDelay = new Alarm(2.0f);
        int linesWon = 0;
        public StWin(StateMain main)
            : base("Win-State", main)
        {
            animComing[0] = new AnimationKey(261, 230, 1f, Tweening.TweenType.QuadradicEaseOut);
            animLeaving[0] = new AnimationKey(261, 800, 1f, Tweening.TweenType.QuadradicEaseOut);
        }


        decimal getWinAmount()
        {
            linesWon = 0;
            decimal amount = 0;
            for (int i = 0; i < context.activeLines; i++)
            {
                if(play.finalLineAmounts[i]>0)
                    linesWon++;

                amount += play.finalLineAmounts[i];
            }
            if (amount < context.totalWon)
                amount = context.totalWon;

            return amount;
        }

        #region State Overrides...

        public override void OnUpdate(Keyboard keyboard, Mouse mouse, Timer timer)
        {
            
            if (context.btnContinue.Released)
            {
                context.odoBalance.setValueInstant(context.pinBalance);                
                context.changeStateTo(context.stCredit);
            }

            if (ka != null) //there is an animation to play
            {
                ka.Update(keyboard, mouse, timer);
                if (ka.isDone())
                {
                    if (((int)ka.tag) == 1)
                    {
                        //start 2nd animation...
                        almFreeTabsWinSignDelay.Update(timer.DeltaTimeMS);
                        if (almFreeTabsWinSignDelay.Check(Alarm.CheckType.RESET))
                        {
                            ka = new KeyedAnimation(context.picFreeTabsWinSign, animLeaving);
                            ka.tag = (int)2; //2nd anim
                            ka.Start();
                        }
                    }
                    else if (((int)ka.tag) == 2)
                    {
                        context.picFreeTabsWinSign.MoveAbsolute(261, -537);
                        ka = null;
                        context.odoBalance.setValueInstant(context.pinBalance);
                        context.changeStateTo(context.stCredit);
                    }
                }
            }
            else if (context.freeTabsMode)  //animation done...
            {
                almWinDelay.Update(timer.DeltaTimeMS);
                if (almWinDelay.Check(Alarm.CheckType.RESET))
                {
                    context.odoBalance.setValueInstant(context.pinBalance);
                    context.changeStateTo(context.stCredit);
                }
            }
        }

        public override void OnRender(IGraphics graphics)
        {
            if (ka != null)
            {
                if (ka.isDone() && ((int)ka.tag) == 1)
                {
                    graphics.DrawText(650, 420, play.freeTabsWon.ToString(), Color.White, context.fntFreeTabsSign, GuiItem.Align.XCenter);
                }
            }

            
        }



        public override void OnEnter()
        {
            almFreeTabsWinSignDelay.Enable();
            almFreeTabsWinSignDelay.Reset();
            //Trigger animations for winning icons...
            play = (StPlay)context.stPlay;

            context.wonAmount = getWinAmount();
            context.pinBalance += context.wonAmount;
            
            if ((context.wonAmount >= Bingo.singleton.settings.verifyWinAmt) && (Bingo.singleton.settings.verifyWinAmt > 0M))
            {
                WinVerification frmWinVerificaiton = new WinVerification();
                frmWinVerificaiton.SetWinValue = context.wonAmount;
                frmWinVerificaiton.ShowDialog(Bingo.singleton);
            }


            context.iconsView.firstWinFlag = true;

            almWinDelay.Reset();
            almWinDelay.Enable();

            if (play.freeTabsWon > 0)
            {
                context.freeTabsBalance += play.freeTabsWon;
                context.startFreeTabsMode();
                ka = new KeyedAnimation(context.picFreeTabsWinSign, animComing);
                ka.tag = (int)1; //1st anim
                ka.Start();
            }
            else
                context.btnContinue.Enable = context.btnContinue.Visible = true;


            

            if (context.freeTabsMode)
            {
                context.freeTabsTotalWon += context.wonAmount;
            }

            context.saveRecoveryRecord();
        }

        public override void OnLeave()
        {
            //update Data...
            context.updatePINRecord();
            context.savePlay();

            context.wonAmount = 0;
            context.lblLineAmount.Text = "";

            
        }

        #endregion
    }
}
