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


namespace BreakTheBankTabs1063.MainStates
{
    public abstract class GameState
    {
        public String stateName;
        protected StateMain context = null;

        public GameState(String name, StateMain main)
        {
            stateName = name;
            context = main;
        }

        // Game Functions ...

        public void callBalls()
        {
            throw new Exception("Call to undefined method within state " + stateName);
        }

        public void acknowledgeWin()
        {
            throw new Exception("Call to undefined method within state " + stateName);
        }

        public void continueGame()
        {
            throw new Exception("Call to undefined method within state " + stateName);
        }

        public void playGame()
        {
            throw new Exception("Call to undefined method within state " + stateName);
        }

        public void doGameOver()
        {
            throw new Exception("Call to undefined method within state " + stateName);
        }

        public void acceptCashOut()
        {
            throw new Exception("Call to undefined method within state " + stateName);
        }

        public void back()
        {
            throw new Exception("Call to undefined method within state " + stateName);
        }

        public void pause()
        {
            throw new Exception("Call to undefined method within state " + stateName);
        }

        public void unPause()
        {
            throw new Exception("Call to undefined method within state " + stateName);
        }


        //Abstract methods each state must implement...
        public abstract void OnUpdate(Keyboard keyboard, Mouse mouse, Timer timer);
        public abstract void OnRender(IGraphics graphics);

        public abstract void OnEnter();
        public abstract void OnLeave();


    }
}
