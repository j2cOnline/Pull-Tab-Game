using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using GameCore.AnimationCore;
using GameCore.MultiMedia;
using GameCore.Timing;
using GameCore.Utility;
using BreakTheBankTabs1063.utils;

namespace BreakTheBankTabs1063.GameObjects
{
    public class Sign : GuiItem
    {

        KeyedAnimation ka = null;

        AnimationKey[] coming = null;
        AnimationKey[] leaving = null;
        GuiItem movingObject = null;
        public int phase = 0;

        public bool active=false;

        public Sign(GuiItem movingObject,AnimationKey[] coming, AnimationKey[] leaving)
            : base("Sign", 0, 0)
        {
            this.coming = coming;
            this.leaving = leaving;
            this.movingObject = movingObject;
        }

        public void startAnimation()
        {
            active = true;
            ka = new KeyedAnimation(movingObject, coming);
            ka.Start();
            phase = 1;
        }

        public void endAnimation()
        {
            active = true;
            ka = new KeyedAnimation(movingObject, leaving);
            ka.Start();
            phase = 2;
        }

        #region Abstracts...

        /// <summary>
        /// Sets the card to equal another
        /// </summary>
        /// <param name="item"></param>
        protected override void OnSet(GuiItem item)
        {
            //cast.. could throw an exception if item is not a Card
            

        }


        /// <summary>
        /// Updates the card test for card Presses if state is set to selecting
        /// </summary>
        /// <param name="mouse">current Mouse object</param>
        /// <param name="timer">current Timer Object</param>
        protected override void OnUpdate(GameCore.Input.Keyboard keyboard, GameCore.Input.Mouse mouse, GameCore.Timing.Timer timer)
        {
            if (active && ka!=null)
            {
                ka.Update(keyboard, mouse, timer);
                if (ka.isDone())
                {
                    active = false;
                }
            }

        }

        /// <summary>
        /// Redraws the card and cardSlots if needs to
        /// </summary>
        /// <param name="graphics">Current IGraphics object</param>
        protected override void OnDraw(IGraphics graphics)
        {
            
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
