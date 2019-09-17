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
    class StHelpOptions: GameState
    {
        public StHelpOptions(StateMain main)
            : base("HelpOptions-State", main)
        {
        }
        int oldTabPriceIdx = 0;

        void toHelpOptions()
        {
            context.btnHelp.Visible = context.btnHelp.Enable= false;
            context.btnWinPatterns.Visible = context.btnWinPatterns.Enable = false;
            context.picPayTable.Visible = context.picPayTable.Enable = false;
            context.picWinPatterns.Visible = context.picWinPatterns.Enable = false;

            context.picHelpOptions.Visible = context.picHelpOptions.Enable = true;
            context.btnPayouts.Visible = context.btnPayouts.Enable= true;
            context.btnBack.Visible = context.btnBack.Enable = true;
            context.btnDenomination.Enable=context.btnDenomination.Visible = false;
            context.btnAudio.Visible = true;
        }

        void toPayTable()
        {
            context.picHelpOptions.Visible = context.picHelpOptions.Enable = false;
            context.btnPayouts.Visible = context.btnPayouts.Enable = false;
            context.btnHelp.Visible = context.btnHelp.Enable = false;
            context.picWinPatterns.Visible = context.picWinPatterns.Enable = false;

            context.picPayTable.Visible = context.picPayTable.Enable= true;
            context.btnBack.Visible = context.btnBack.Enable = true;
            
            context.btnWinPatterns.Visible = context.btnWinPatterns.Enable = true;
            context.btnDenomination.Enable = context.btnDenomination.Visible = true;
            context.btnAudio.Visible = false;
        }

        void toWinPatterns()
        {
            context.picHelpOptions.Visible = context.picHelpOptions.Enable = false;
            context.btnPayouts.Visible = context.btnPayouts.Enable = false;
            context.btnWinPatterns.Visible = context.btnWinPatterns.Enable = false;

            context.picPayTable.Visible = context.picPayTable.Enable = false;
            context.btnHelp.Visible = context.btnHelp.Enable = true;

            context.picWinPatterns.Visible = context.picWinPatterns.Enable = true;
            
            context.btnBack.Visible = context.btnBack.Enable = true;
            
            context.btnDenomination.Enable = context.btnDenomination.Visible = true;
            context.btnDenomination.Enable = context.btnDenomination.Visible = false;
             context.btnAudio.Visible = false;
        }

        #region State Overrides...

        public override void OnUpdate(Keyboard keyboard, Mouse mouse, Timer timer)
        {
            if (context.btnPayouts.Released)
            {
                toPayTable();
            }

            if (context.btnHelp.Released)
            {
                toHelpOptions();
            }

            if (context.btnWinPatterns.Released)
            {
                toWinPatterns();
            }

            if (context.btnBack.Released)
            {
                context.changeStateTo(context.stCredit);
            }

            if (context.btnDenomination.Released)
            {
                if (context.soundsOn)
                    context.Sounds.buttonPress.Play(false, true);

                context.tabPricesIdx++;
                if (context.tabPricesIdx >= Bingo.singleton.localSettings.noOfDenominations)    //TBR... setting..
                    context.tabPricesIdx = 0;
            }

            if (context.btnAudio.Released)
            {
                context.soundsOn = !context.btnAudio.Toggled;
            }
        }

        public override void OnRender(IGraphics graphics)
        {
            decimal[,] payTable = ((StPlay)context.stPlay).payTable;
            
            decimal factor=(context.TabPrices[context.tabPricesIdx] / context.TabPrices[0]) / 100;

            if (context.picPayTable.Visible)
            {
                for (int i = 0; i < (Definitions.iconIndexMax-2); i++)
                {
                    decimal amount = Definitions.PAYTABLE[i, 2] * (StateMain.singleton.TabPrices[StateMain.singleton.tabPricesIdx] / 10);
                    graphics.DrawText(680 + i * 200, 300, amount.ToString("C"), Color.White, context.fntPaytablePrizes, GuiItem.Align.XCenter);
                }

                decimal amount2 = Definitions.PAYTABLE[Definitions.iconIndexMax - 2, 2] * (StateMain.singleton.TabPrices[StateMain.singleton.tabPricesIdx] / 10);
                graphics.DrawText(94, 522, amount2.ToString("C"), Color.White, context.fntPaytablePrizes, GuiItem.Align.XCenter);

                //2 more for cherries
                {
                    decimal amount = Definitions.PAYTABLE[Definitions.iconIndexMax - 1, 2] * (StateMain.singleton.TabPrices[StateMain.singleton.tabPricesIdx] / 10);
                    graphics.DrawText(286, 522, amount.ToString("C"), Color.White, context.fntPaytablePrizes, GuiItem.Align.XCenter);

                    amount = Definitions.PAYTABLE[Definitions.iconIndexMax - 1, 1] * (StateMain.singleton.TabPrices[StateMain.singleton.tabPricesIdx] / 10);
                    graphics.DrawText(486, 522, amount.ToString("C"), Color.White, context.fntPaytablePrizes, GuiItem.Align.XCenter);

                    amount = Definitions.PAYTABLE[Definitions.iconIndexMax - 1, 0] * (StateMain.singleton.TabPrices[StateMain.singleton.tabPricesIdx] / 10);
                    graphics.DrawText(686, 522, amount.ToString("C"), Color.White, context.fntPaytablePrizes, GuiItem.Align.XCenter);
                }

                for (int i = 0; i < Definitions.freeTabsPayTable.Length; i++)
                {
                    if (i < 5)
                    {
                        int ft = Definitions.freeTabsPayTable[i].FreeTabs;
                        graphics.DrawText(840 + (i * 116), 520, ft.ToString(), Color.White, context.fntPaytablePrizes, GuiItem.Align.XCenter);
                    }
                    else
                    {
                        decimal ft = Definitions.freeTabsPayTable[i].FreeTabs * (StateMain.singleton.TabPrices[StateMain.singleton.tabPricesIdx] / 10);
                        graphics.DrawText(494 - ((i-5) * 200), 300, ft.ToString("C"), Color.White, context.fntPaytablePrizes, GuiItem.Align.XCenter);
                    
                    }
                }
            }
        }

        public override void OnEnter()
        {
            oldTabPriceIdx = context.tabPricesIdx;

            toHelpOptions();
            context.btnAudio.Toggled = !context.soundsOn;
            context.btnDenomination.MoveAbsolute(604, 590);
        }

        public override void OnLeave()
        {
            context.picHelpOptions.Visible = context.picHelpOptions.Enable = false;
            context.btnPayouts.Visible = context.btnPayouts.Enable = false;
            context.btnWinPatterns.Visible = context.btnWinPatterns.Enable = false;
            context.picWinPatterns.Visible = context.picWinPatterns.Enable = false;
            
            context.picPayTable.Visible = context.picPayTable.Enable = false;
            context.btnBack.Visible = context.btnBack.Enable = false;
            context.btnHelp.Visible = context.btnHelp.Enable = false;
            context.btnAudio.Visible = false;
            context.btnDenomination.Visible = true;
            context.tabPricesIdx = oldTabPriceIdx;
            context.btnDenomination.MoveAbsolute(1027, 434);
        }

        #endregion
    }
}
