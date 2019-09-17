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
    class StCredit: GameState
    {

        public StCredit(StateMain main)
            : base("Credit-State", main)
        {
        }


        #region State Overrides...

        public override void OnUpdate(Keyboard keyboard, Mouse mouse, Timer timer)
        {
            

            if (context.btnDenomination.Released)
            {
                if (context.soundsOn)
                    context.Sounds.buttonPress.Play(false, true);

                context.tabPricesIdx++;
                if (context.tabPricesIdx >= Bingo.singleton.localSettings.noOfDenominations)    //TBR... setting..
                    context.tabPricesIdx = 0;
            }

            

            

            if (context.btnHelpOptions.Released)
            {
                context.changeStateTo(context.stHelpOptions);
            }

            if (context.btnCashout.Released)
            {
                context.cashOut();
            }

            if (context.pinBalance >= context.TabPrices[context.tabPricesIdx])
            {
                if (context.btnRevealAll.Released || context.animWholePull.Released)
                {
                    if (context.soundsOn)
                        context.Sounds.buttonRevealAll.Play(false, true);

                    context.btnRevealAll.Enable = false;
                    ((StPlay)context.stPlay).autoReveal = true;
                    context.changeStateTo(context.stPlay);
                    context.animWholePull.Resest();		               
    	            context.animWholePull.Play();
                }

                if (Icon.playStarted)
                {
                    context.changeStateTo(context.stPlay);
                }
            }


            
        }

        public override void OnRender(IGraphics graphics)
        {
            
        }

        public override void OnEnter()
        {
            context.totalWon = 0;
            context.animWholePull.Resest(); 	 	
        	context.animWholePull.Stop(); 

            if (context.pinBalance < context.TabPrices[0] && !(context.freeTabsBalance > 0))  //exhausted money cashout!
                context.changeStateTo(context.stGameOver);
            else
            {
                //Adjust TabPrice for Money

                while (context.pinBalance < context.TabPrices[context.tabPricesIdx] && !(context.freeTabsBalance > 0))
                {
                    context.tabPricesIdx++;
                    if (context.tabPricesIdx >= Bingo.singleton.localSettings.noOfDenominations)    //TBR... setting..
                        context.tabPricesIdx = 0;
                }


                context.btnContinue.Enable = context.btnContinue.Visible = false;
                context.btnCashout.Enable = true;
                context.btnDenomination.Enable = true;
                context.btnHelpOptions.Enable = true;
                context.btnRevealAll.Enable = context.btnRevealAll.Visible = true;
                //context.btnSelectLines.Enable = true;
                //context.btnBetDown.Enable = context.btnBetUp.Enable = true;

                
                context.iconsView.resetIcons();

                if (context.freeTabsMode)
                {
                    if (context.freeTabsBalance > 0)
                    {
                        context.animWholePull.Resest(); 	 	
    	                context.animWholePull.Play();

                        context.btnRevealAll.Enable = false;
                        context.changeStateTo(context.stPlay);
                        ((StPlay)context.stPlay).autoReveal = true;
                    }
                    else
                    {
                        context.endFreeTabsMode();
                    }
                }

                context.saveRecoveryRecord();
            }
        }

        public override void OnLeave()
        {
            context.btnCashout.Enable = false;
            context.btnDenomination.Enable = false;
            context.btnHelpOptions.Enable = false;
            //context.btnSelectLines.Enable = false;
            //context.btnBetDown.Enable = context.btnBetUp.Enable = false;
        }

        #endregion
    }
}
