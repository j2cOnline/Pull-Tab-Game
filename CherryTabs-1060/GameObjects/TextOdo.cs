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


namespace BreakTheBankTabs1063.GameObjects
{
    public class TextOdo: GuiItem
    {
        GuiLabel txtCounter = null;
        private decimal objValue;   //goal value
        private decimal curValue;   //current value
        private decimal delta;
        private decimal step;
        private String format=null;
        private decimal minSteps = 0.10M;
        public bool Active = true;
        public Alarm almDelay = new Alarm(0.05f);
        public TextOdo(GuiLabel txtCounter, decimal curValue, String format, decimal minSteps)
            : base("Text-Odo", 0, 0)
        {
            this.txtCounter = txtCounter;
            this.curValue = curValue;
            this.format = format;
            this.minSteps = minSteps;
            almDelay.Enable();
        }

        public decimal Value
        {
            set
            {
                objValue=value;
                delta = objValue - curValue;
                step = (delta > 0) ? minSteps : -minSteps;
                while (Math.Abs(delta / step) > 100)
                {
                    step *= 2;
                }
            }

            get
            {
                return objValue;
            }
        }

        public void setValueInstant(decimal value)
        {
            objValue = curValue = value;
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
            if (!Active)
                return;
            almDelay.Update(timer.DeltaTimeMS);

            if (almDelay.Check(Alarm.CheckType.RESET))
            {
                if (objValue != curValue)
                {
                    if (StateMain.singleton.soundsOn)
                        StateMain.singleton.Sounds.odoTick.Play(false, true);

                    if (Math.Abs(objValue - curValue) < Math.Abs(step))
                    {
                        curValue = objValue;
                    }
                    else
                        curValue += step;
                }

                txtCounter.Text = curValue.ToString(format);
                if (txtCounter.Text == "")
                    txtCounter.Text = "0";
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
