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


namespace BreakTheBankTabs1063.MainStates
{
    class StLose : GameState
    {
        Alarm almLoseDelay = new Alarm(2.0f);

        public StLose(StateMain main)
            : base("Lose-State", main)
        {
        }


        #region State Overrides...

        public override void OnUpdate(Keyboard keyboard, Mouse mouse, Timer timer)
        {
            if (context.freeTabsMode)
            {
                almLoseDelay.Update(timer.DeltaTimeMS);
                if (almLoseDelay.Check(Alarm.CheckType.RESET))
                {
                    
                    context.changeStateTo(context.stCredit);
                }

            }
            if (context.btnContinue.Released)
            {
                context.changeStateTo(context.stCredit);
            }
        }

        public override void OnRender(IGraphics graphics)
        {

        }

        public override void OnEnter()
        {
            almLoseDelay.Reset();
            almLoseDelay.Enable();
            
            context.btnContinue.Enable = context.btnContinue.Visible = true;

            context.saveRecoveryRecord();
        }

        public override void OnLeave()
        {
            //update Data...
            context.updatePINRecord();
            context.savePlay();

            context.wonAmount = 0;
        }

        #endregion
    }
}
