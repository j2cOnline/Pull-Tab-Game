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
using System.Drawing;

namespace BreakTheBankTabs1063.MainStates
{
    class StGameOver: GameState
    {
        private Alarm almGameOver = new Alarm(Definitions.GameOverDelay);

        public StGameOver(StateMain main)
            : base("GemeOver-State", main)
        {
        }


        #region State Overrides...

        public override void OnUpdate(Keyboard keyboard, Mouse mouse, Timer timer)
        {
            almGameOver.Update(timer.DeltaTimeMS);
            if (almGameOver.Check(Alarm.CheckType.TURNOFF))
            {
                context.cashOut();
            }

            if (context.btnCashout.Released)
            {
                context.cashOut();
            }
        }

        public override void OnRender(IGraphics graphics)
        {
            
        }

        public override void OnEnter()
        {
            almGameOver.Reset();
            almGameOver.Enable();
            context.btnCashout.Enable = true;

            
        }

        public override void OnLeave()
        {
            context.picHelpOptions.Visible = context.picHelpOptions.Enable = false;
            context.btnPayouts.Visible = context.btnPayouts.Enable = false;

            context.picPayTable.Visible = context.picPayTable.Enable = false;
            context.btnBack.Visible = context.btnBack.Enable = false;
            context.btnHelp.Visible = context.btnHelp.Enable = false;
        }

        #endregion
    }
}
