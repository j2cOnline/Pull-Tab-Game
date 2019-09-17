using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using GameCore;
using GameCore.Timing;
using GameCore.MultiMedia;
using GameCore.Math;
using GameCore.Input;
using GameCore.Application;
using GameCore.Utility;

namespace BreakTheBankTabs1063
{
    class StateLoading : IGameState
    {
        static public StateLoading singleton = null;

        int loadingPart = 0;
        GameStateMachine stateMachine = null;
        public MultiMediaLoader mediaObjs = null;
        GuiLabel lblAction = null;

        public StateLoading( GameStateMachine stateMachine ): base ( "Loading" )
        {
            singleton = this;
            this.stateMachine = stateMachine;
            mediaObjs = new MultiMediaLoader("SharedMedia");
        }

        #region  IGameState

        public override bool OnLoad()
        {
            // STANDS ALONE.. DOES NOT REF "SharedMedia" Loader
            BMLoader ml = new BMLoader("LoadingLoader");
            ml.LoadFile(Name + ".txt");
            guiManager.Add(ml);
            lblAction = (GuiLabel)guiManager.Get("lbl_action");

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
        }

        public override string OnUpdate(Keyboard keyboard, Mouse mouse, GameCore.Timing.Timer timer)
        {
            if (!Bingo.instance.TopMost)
            {
                Bingo.instance.TopMost = true; 
            }
            switch (loadingPart)  // "...Loading Sounds..." 
            {
                case 0: lblAction.Text = "...Loading Sounds...";
                    break;
                case 1: mediaObjs.LoadFile("Sounds.txt"); lblAction.Text = "...Loading Fonts...";
                    break;
                case 2: mediaObjs.LoadFile("Fonts.txt"); lblAction.Text = "...Loading Animations...";
                    break;
                case 3: mediaObjs.LoadFile("Animations.txt"); lblAction.Text = "...Loading Bitmaps...";
                    break;
                case 4: mediaObjs.LoadFile("Bitmaps.txt"); lblAction.Text = "...Loading Main State...";
                    break;
                case 5: stateMachine.Add(new StateMain());
                    break;
/*
                case 6: stateMachine.Add(new StateTitle()); lblAction.Text = "...Loading PIN State...";
                    break;
                case 7: stateMachine.Add(new StatePIN()); lblAction.Text = "...Loading Main State...";
                    break;
                case 8: stateMachine.Add(new StateDemo()); lblAction.Text = "...Loading Attraction State...";
                    break;
                case 9: stateMachine.Add(new StateAttraction()); lblAction.Text = "...Loading Game Menu State...";
                    break;
                case 10: stateMachine.Add(new StateGameMenu());
                    break;
*/
                default:
                    {
                        return "Main";
                    }
            }

            loadingPart++;

            guiManager.Update(keyboard, mouse, timer);
            return "NoChange";
        }

        public override void OnRender(IGraphics graphics)
        {
            guiManager.Draw(graphics);
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


        #endregion
    }
}
