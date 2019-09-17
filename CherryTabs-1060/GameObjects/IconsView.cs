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
using System.Diagnostics;

namespace BreakTheBankTabs1063.GameObjects
{
    public class IconsView: GuiItem
    {
        public static IconsView singleton = null;
        public GuiPictureBox picTabsOverlay=null;
        public GuiPictureBox picTabsOverlayFreeTabs = null;

        public Icon[] icons = new Icon[Definitions.IconsPerTab];
        public int iconsRevealed = 0;
        int[,] payLines = null;

        //line blinkers...

        Alarm almFlashLines = new Alarm(0.5f);
        //Alarm almChangeFlashingLine = new Alarm(2.0f);
        int lineFlashesSoFar = 0;
        bool lineShowing = true;
        int curLineFlashing = 0;
        public bool firstWinFlag = false;
        //Under Overlay Layer Animations
        


        //Icons Animations 
        AnimatedLabel[] iconAnimations = new AnimatedLabel[Definitions.iconIndexMax + 1]; //+1 because it's zero based...

        private GuiPictureBox[] picWinBoxes = null;
        private GuiPictureBox[] picWinLines = null;
        private GuiPictureBox LineLights = null;
        private IFont fntLineBets = null;

        public bool[,] HideCol { get; set; } = new bool[3,3];

        public IconsView(MultiMediaLoader ml)
            : base("IconsView", 0, 0)
        {
            singleton = this;
             //pay lines init
            payLines = new int[Definitions.TotalPaylines * 3, Definitions.reelWidth];
            StateMain.getPaylines(ref payLines);

            // initialize static data for icons
            FramedTexture ftx = (FramedTexture)ml.GetObject("ftx_ovr_SlotIcons");
            Icon.iconImages = new GuiPictureBox(ftx, 0, 0, 0);

            ftx = (FramedTexture)ml.GetObject("ftx_ovr_SlotIcons_Win");
            Icon.winIconImages = new GuiPictureBox(ftx, 0, 0, 0);

            ftx = (FramedTexture)ml.GetObject("ftx_anim_Reveal");
            Icon.coverAnim = new GuiPictureBox(ftx, 0, 0, 0);

            ftx = (FramedTexture)ml.GetObject("ftx_ovr_LineLights");
            LineLights = new GuiPictureBox(ftx, 0, 0, 0);
            fntLineBets = (IFont)ml.GetObject("lineBet_font");

             // Load Boxes and Lines...
            picWinBoxes = new GuiPictureBox[Definitions.TotalPaylines];
            picWinLines = new GuiPictureBox[Definitions.TotalPaylines];

            for (int i = 1; i <= Definitions.TotalPaylines; i++)
            {
                ftx = (FramedTexture)ml.GetObject("ftx_ovr_Line_" + i.ToString() + "_Box");
                picWinBoxes[i-1] = new GuiPictureBox(ftx, 0, 0, 0);

                ftx = (FramedTexture)ml.GetObject("ftx_ovr_Line_" + i.ToString());
                picWinLines[i-1] = new GuiPictureBox(ftx, 0, 0, 0);
            }


            //load the overlay...
            ftx = (FramedTexture)ml.GetObject("ftx_ovr_MainScreen");
            picTabsOverlay = new GuiPictureBox(ftx, 0, 0, 0);

            ftx = (FramedTexture)ml.GetObject("ftx_ovr_MainScreen_ExtraSpins");
            picTabsOverlayFreeTabs = new GuiPictureBox(ftx, 0, 0, 0);
            StateMain.singleton.picMainOverlay.Alpha = 0;
            
            //loading icon animations ...
            Animation anim = (Animation)ml.GetObject("anim_Win_anim");
            for (int i = 0; i < Definitions.iconIndexMax; i++)
            {
                ftx = (FramedTexture)ml.GetObject("ftx_anim_Win_"+(i+1).ToString());
                iconAnimations[i] = new AnimatedLabel(anim, ftx, 0, 0);
            }

            // setting icons locations...

            for (int i = 0; i < Definitions.IconsPerTab; i++)
            {
                icons[i] = new Icon();

                icons[i].X = Definitions.CardLocations[i, 0];
                icons[i].Y = Definitions.CardLocations[i, 1];
                icons[i].iconNo = (i+1) % Definitions.iconIndexMax; //for test TBR..
            }

            //almChangeFlashingLine.Enable();
            almFlashLines.Enable();
        }

        
        #region Lines and Boxes

        public void drawLine(int lineNo,int finalNoOfIconsWon, decimal amountWon, IGraphics graphics)
         {

             if ( amountWon>0M)  // if there are winners on this line...
                 {
                     picWinLines[lineNo].ForceDraw(graphics, 323, 44);
                 }

                 
         }


        public void drawBox(int lineNo, int finalNoOfIconsWon, decimal amountWon, IGraphics graphics)
        {
             
            if (finalNoOfIconsWon > 1)  // if there are winners on this line...
            {

                bool[,] winIcons = getWinnerIcons(lineNo, finalNoOfIconsWon, amountWon);

                for (int row = 0; row < Definitions.iconsViewRows; row++)
                {
                    for (int col = 0; col < Definitions.reelWidth; col++)
                    {
                        if (winIcons[col, row])
                        {
                            picWinBoxes[lineNo].ForceDraw(graphics, icons[col + (row * Definitions.reelWidth)].X - 9, icons[col + (row * Definitions.reelWidth)].Y - 8);
                        }
                    }
                }
            }
             
        }

         public bool[,] getWinnerIcons(int lineNo, int finalNoOfIconsWon,decimal amountWon)
         {
             

             bool[,] winner = new bool[Definitions.reelWidth, 3];

             if (amountWon>0M)
             {
                 for (int j = 0; j < Definitions.reelWidth; j++)
                 {
                     if (lineNo < Definitions.VLinesStartIdx && j >= finalNoOfIconsWon)
                         continue;
                     for (int i = 0; i < 3; i++)
                     {
                         if (lineNo >= Definitions.VLinesStartIdx && i >= finalNoOfIconsWon)
                             continue;
                         winner[j, i] |= (payLines[(lineNo * Definitions.iconsViewRows) + i, j] == 1) ? true : false;
                         //winner[j, 1] |= (payLines[(lineNo * Definitions.iconsViewRows) + 1, j] == 1) ? true : false;
                         //winner[j, 2] |= (payLines[(lineNo * Definitions.iconsViewRows) + 2, j] == 1) ? true : false;
                     }

                 }
             }

             
             String txt = "";

             System.Console.WriteLine("Line No. " + lineNo);
             for (int y = 0; y < 3; y++)
             {
                 for (int x = 0; x < Definitions.reelWidth; x++)    
                 {
                     txt += (winner[x, y]) ? "1" : "0";
                 }
                 txt += "\n";
             }
             System.Console.WriteLine(txt);
              // */

             return winner;
         }

         int getFirstWinLine()
         {
             int winLine = 0;
             decimal[] finalLineAmounts = ((StPlay)StateMain.singleton.stPlay).finalLineAmounts;
             for (int i = 0; i < Definitions.TotalPaylines; i++)
             {
                 if (finalLineAmounts[winLine] < finalLineAmounts[i]) winLine = i;
             }

             return winLine;
         }

         int getNextWinLine(int start)
         {
             
             int winLine = start;
             int errCo = 0;
             //read finalNoOfIconsWon from stPlay
             //int[] finalNoOfIconsWon = ((StPlay)StateMain.singleton.stPlay).finalNoOfIconsWon;
             decimal[] finalAmts = ((StPlay)StateMain.singleton.stPlay).finalLineAmounts;

             do
             {
                 winLine++;
                 if (winLine >= StateMain.singleton.activeLines)
                     winLine = 0;
                 errCo++;
                 if (errCo > StateMain.singleton.activeLines)
                     throw new Exception("infinite loop in getNextWinLine");
             }
             while (finalAmts[winLine] == 0);

             
             return winLine;
         }


#endregion

         #region icons Management
         public void resetIcons()
         {
             iconsRevealed = 0;
             foreach (Icon icon in icons)
             {
                 icon.hideIcon = false;
                 icon.status = Icon.iconStatus.Concealed;
                 //icon.iconNo = 0;
             }
             Icon.playStarted = false;  //reset tab peal indicator!

             //clear animations...
             StateMain.singleton.Animations.Clear();
         }

         public void setIcons(int[,] ReadTabIcons)
         {
             for (int i = 0; i < Definitions.reelWidth * Definitions.iconsViewRows; i++)
             {
                 icons[i].iconNo = ReadTabIcons[i % Definitions.reelWidth, i / Definitions.reelWidth];
                 //int icon = ReadTabIcons[iconsStops[i], i];
                 //int before = ReadTabIcons[(iconsStops[i] + Definitions.ReelLength - 1) % Definitions.ReelLength, i];
                 //int after = ReadTabIcons[(iconsStops[i] + 1) % Definitions.ReelLength, i];
                 //icons[i].iconNo = before;
                 //icons[i + Definitions.reelWidth].iconNo = icon;
                 //icons[i + (Definitions.reelWidth * 2)].iconNo = after;
             }
         }
#endregion

         #region On Icon Animations


         public void startWinnerIconAnims(int[] finalNoOfIconsWon, int line, decimal[] amountWon)
         {
             // update winAnimationPlaying flag...
             foreach (Icon icon in icons)
             {
                 icon.hideIcon = false;
             }

             if (finalNoOfIconsWon[line] > 1 || amountWon[line]>0)  // if there are winners on this line...
                 {
                     bool[,] winIcons = getWinnerIcons(line, finalNoOfIconsWon[line], amountWon[line]);

                     //scanning array for winners...
                     
                     for (int row = 0; row < Definitions.iconsViewRows; row++)
                     {
                         for (int col = 0; col < Definitions.reelWidth; col++)
                         {
                             if (winIcons[col, row])
                             {
                                 //start animation for this icon...

                                 addIconAnimation(col + (row * Definitions.reelWidth));
                                 icons[col + (row * Definitions.reelWidth)].status = Icon.iconStatus.Winner;
                                 icons[col + (row * Definitions.reelWidth)].hideIcon = true;
                                 if (firstWinFlag && StateMain.singleton.soundsOn)
                                 {
                                     StateMain.singleton.Sounds.iconWinSounds[icons[col + (row * Definitions.reelWidth)].iconNo].Play(false, true);
                                 }
                             }
                         }
                     }
                 }
             
         }

         public void addAnimation(int icon, AnimatedLabel anim)
         {
             AnimatedLabel animCopy = new AnimatedLabel();
             animCopy.SetFrom(anim);

             animCopy.userData = icon;   // reference the icon no. 
             animCopy.X = icons[icon].X;
             animCopy.Y = icons[icon].Y;
            StateMain.singleton.Animations.Add(animCopy);
             anim.Play();
         }

         public void addIconAnimation(int icon)
         {
             // add the right animation for this icon...
             addAnimation(icon, iconAnimations[icons[icon].iconNo]);
         }

         public void animateFreeTabsIcon()      //for animating all scatters
         {
             if (firstWinFlag && StateMain.singleton.soundsOn)
             {
                 StateMain.singleton.Sounds.iconWinSounds[Definitions.freeTabsIcon].Play(false, true);
             }

             for (int i=0;i<Definitions.IconsPerTab;i++)
             {
                 if (icons[i].iconNo == Definitions.freeTabsIcon)
                     addIconAnimation(i);
             }
         }

        #endregion


         #region Abstracts...

         /// <summary>
         /// Sets the card to equal another
         /// </summary>
         /// <param name="item"></param>
         protected override void OnSet(GuiItem item)
        {
            //cast.. could throw an exception if item is not a Card
            bounds.Width = item.Bounds.Width;
            bounds.Height = item.Bounds.Height;
        }

         int lastFrm = 0;

        /// <summary>
        /// Updates the card test for card Presses if state is set to selecting
        /// </summary>
        /// <param name="mouse">current Mouse object</param>
        /// <param name="timer">current Timer Object</param>
        protected override void OnUpdate(GameCore.Input.Keyboard keyboard, GameCore.Input.Mouse mouse, GameCore.Timing.Timer timer)
        {
            foreach (Icon icon in icons)
            {
                icon.Update(keyboard,mouse,timer);
            }

            foreach (AnimatedLabel anim in StateMain.singleton.Animations)
            {
                anim.Update(keyboard, mouse, timer);
            }

            if (StateMain.singleton.currentState == StateMain.singleton.stWin && (((StPlay)StateMain.singleton.stPlay).winnings > 0 || ((StPlay)StateMain.singleton.stPlay).freeTabsWon>0))
            {
                
                //almChangeFlashingLine.Update(timer.DeltaTimeMS);
                //almFlashLines.Update(timer.DeltaTimeMS);
                int tempFrm=0;
                if (StateMain.singleton.Animations.Count > 0)
                {
                    tempFrm = StateMain.singleton.Animations[0].GetFrame() ;

                    if (tempFrm % 7 == 0 && lastFrm!=tempFrm && tempFrm!=28)
                    {
                        lastFrm = tempFrm;
                            lineShowing = !lineShowing;
                            if (lineShowing)
                                lineFlashesSoFar++;
                    }

                    //Debug.WriteLine(lineShowing + " - " + tempFrm + " - " + lastFrm + " - " + curLineFlashing);
                }
                

                if ((tempFrm>=28) || firstWinFlag)
                {
                    
                    lineFlashesSoFar = 0;
                    curLineFlashing = (firstWinFlag)?getFirstWinLine():getNextWinLine(curLineFlashing);

                    StateMain.singleton.lblLineAmount.Text = "Pattern "+(curLineFlashing+1).ToString()+" pays "+((StPlay)StateMain.singleton.stPlay).finalLineAmounts[curLineFlashing].ToString("C");

                    StateMain.singleton.Animations.Clear();
                    startWinnerIconAnims(((StPlay)StateMain.singleton.stPlay).finalNoOfIconsWon, curLineFlashing, ((StPlay)StateMain.singleton.stPlay).finalLineAmounts);
                    if (((StPlay)StateMain.singleton.stPlay).freeTabsWon > 0)
                        animateFreeTabsIcon();
                    
                    firstWinFlag = false;
                }
            }
        }


        /// <summary>
        /// Redraws the card and cardSlots if needs to
        /// </summary>
        /// <param name="graphics">Current IGraphics object</param>
        protected override void OnDraw(IGraphics graphics)
        {

            for (int row = 0; row < Definitions.iconsViewRows; row++)
            {
                for (int col = 0; col < Definitions.reelWidth; col++)
                {
                    if (!HideCol[col,row])
                    {
                        icons[col + (row * Definitions.iconsViewRows)].Draw(graphics);
                    }
                }
            }

            
 
            //Draw Animations...
            foreach (AnimatedLabel anim in StateMain.singleton.Animations)
            {
                anim.Draw(graphics);
            }

            //Draw winning Line behind the icons
            if (StateMain.singleton.currentState == StateMain.singleton.stWin && lineShowing)
            {
                drawLine(curLineFlashing, ((StPlay)StateMain.singleton.stPlay).finalNoOfIconsWon[curLineFlashing], ((StPlay)StateMain.singleton.stPlay).finalLineAmounts[curLineFlashing], graphics);
            }

            //Draw overlay

            //picTabsOverlay.ForceDraw(graphics, 0, 0);
            //picTabsOverlayFreeTabs.ForceDraw(graphics, 0, 0);


            //Draw win boxes

            //if (StateMain.singleton.currentState == StateMain.singleton.stWin && lineShowing)
            //{
            //    drawBox(curLineFlashing, ((StPlay)StateMain.singleton.stPlay).finalNoOfIconsWon[curLineFlashing], ((StPlay)StateMain.singleton.stPlay).finalLineAmounts[curLineFlashing], graphics);
            //}

            //if (StateMain.singleton.currentState == StateMain.singleton.stCredit)
            //{
            //    for (int i = 0; i < StateMain.singleton.activeLines; i++)
            //    {
            //        drawLine(i, 2, 1.0M, graphics);
            //    }
            //}

            //for (int i = 0; i < StateMain.singleton.activeLines; i++)
            //{
            //    LineLights.Frame = i;
            //    if (StateMain.singleton.currentState == StateMain.singleton.stWin && (((StPlay)StateMain.singleton.stPlay).finalLineAmounts[i]>0 && lineShowing && curLineFlashing==i))
            //    {
            //        LineLights.ForceDraw(graphics, Definitions.LeftLights.X, Definitions.LeftLights.Y);
            //        LineLights.ForceDraw(graphics, Definitions.RightLights.X, Definitions.RightLights.Y);

            //    }
            //}
        }


        /// <summary>
        /// 
        /// </summary>
        public override void OnMoved(int xDiff, int yDiff)
        {
            foreach (Icon icon in icons)
                icon.MoveRelative(xDiff, yDiff);

            foreach (AnimatedLabel anim in StateMain.singleton.Animations)
            {
                anim.MoveRelative(xDiff, yDiff);
            }
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
            foreach (Icon icon in icons)
                icon.Invalidate();

            foreach (AnimatedLabel anim in StateMain.singleton.Animations)
            {
                anim.Invalidate();
            }
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
