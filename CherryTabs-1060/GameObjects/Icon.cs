using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;

using GameCore.MultiMedia;
using GameCore.Timing;
using GameCore.Utility;
using BreakTheBankTabs1063.utils;
using BreakTheBankTabs1063.MainStates;

namespace BreakTheBankTabs1063.GameObjects
{
    public class Icon: GuiItem
    {
        public int iconNo = 0;
        public enum iconStatus
        {
            Concealed=0,
            Revealing,
            Revealed,
            Winner          // it's part of a winning line...
        }

        public static bool playStarted = false;

        public bool hideIcon = false; //used by iconsView to manage blinking lines anims...
        public iconStatus status;
        public static GuiPictureBox iconImages = null;
        public static GuiPictureBox winIconImages = null;

        public static GuiPictureBox coverAnim = null;
        private int coverAnimFrame = 0;
        Alarm almCoverAnim = new Alarm(0.05f);
        
        public Icon()
            : base("Card", 0, 0)
        {
            Interactive = true;
            status = iconStatus.Concealed;
            bounds.Width = coverAnim.Width;
            bounds.Height= coverAnim.Height;
            almCoverAnim.Enable();

        }


#region Abstracts...

        /// <summary>
        /// Sets the card to equal another
        /// </summary>
        /// <param name="item"></param>
        protected override void OnSet(GuiItem item)
        {
            //cast.. could throw an exception if item is not a Card
            Icon icon = (Icon)item;

            status = icon.status;
            iconNo = icon.iconNo;
            
        }

         
        /// <summary>
        /// Updates the card test for card Presses if state is set to selecting
        /// </summary>
        /// <param name="mouse">current Mouse object</param>
        /// <param name="timer">current Timer Object</param>
        protected override void OnUpdate(GameCore.Input.Keyboard keyboard, GameCore.Input.Mouse mouse, GameCore.Timing.Timer timer)
        {

            
            if (status == iconStatus.Concealed)
            {
                if (Released && (StateMain.singleton.currentState == StateMain.singleton.stPlay || StateMain.singleton.currentState == StateMain.singleton.stCredit))
                {

                    //if (StateMain.singleton.soundsOn)
                    //{
                    //    StateMain.singleton.Sounds.iconReveal.Stop(true);
                    //    StateMain.singleton.Sounds.iconReveal.Play(false, true);
                    //}
                    //Reveal();
                    //if(!((StPlay)StateMain.singleton.stPlay).autoReveal)
                    //StateMain.singleton.saveRecoveryRecord();   //every icon should be updated...
                }
            }
            else if (status == iconStatus.Revealing)
            {
                almCoverAnim.Update(timer.DeltaTimeMS);
                if(almCoverAnim.Check(Alarm.CheckType.RESET))
                {
                    coverAnimFrame++;
                    if (coverAnimFrame >= Definitions.CoverAnimationFrames)
                    {
                        IconsView.singleton.iconsRevealed++;
                        status = iconStatus.Revealed; 
                    }
                }
            }

        }

        public void Reveal()
        {
            playStarted = true;
            status = iconStatus.Revealing;
            coverAnimFrame = 0;
        }

        /// <summary>
        /// Redraws the card and cardSlots if needs to
        /// </summary>
        /// <param name="graphics">Current IGraphics object</param>
        protected override void OnDraw(IGraphics graphics)
        {
            if (status != iconStatus.Winner)
            {
                iconImages.Frame = iconNo;
                iconImages.ForceDraw(graphics, X, Y);
            }
            else
            {
                winIconImages.Frame = iconNo;
                winIconImages.ForceDraw(graphics, X, Y);
            }

            if (status == iconStatus.Concealed || status==iconStatus.Revealing)
            {
                //coverAnim.Frame = coverAnimFrame;
                //coverAnim.ForceDraw(graphics, X, Y);
            }

        }


        /// <summary>
        /// 
        /// </summary>
        public override void OnMoved(int xDiff, int yDiff)
        {
            //MoveRelative(xDiff, yDiff);
        }

        /// <summary>
        /// Used to release the reference count of desired variables
        /// </summary>
        public override void FreeResources()
        {

        }

        /// <summary>
        /// tells all the object they must draw on next onDraw()
        /// </summary>
        protected override void OnInvalidate()
        {
            
        }


        /// <summary>
        /// reads the card data from a stream
        /// </summary>
        /// <param name="layout"></param>
        /// <param name="stream"></param>
        /// <returns></returns>
        protected override GuiItem OnFromStream(MultiMediaLoader layout, System.IO.StreamReader stream)
        {
            return null;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        protected override void OnToStream(System.IO.StreamWriter stream)
        {
            throw new Exception("Card::OnToStream() not implemented");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override List<MultiMediaObject> GetLinkedMultiMediaObjects()
        {
            return new List<MultiMediaObject>();
        }

    #endregion
    }
}
