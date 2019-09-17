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

namespace BreakTheBankTabs1063.GameObjects
{
    public class SoundSystem
    {
        public Sound[] lineSounds = new Sound[Definitions.TotalPaylines];
        public Sound[] betSounds = new Sound[Definitions.TotalPaylines];
        public Sound[] iconWinSounds = new Sound[Definitions.iconIndexMax + 1];
        public Sound[] magicianDances = new Sound[Definitions.magicianDances];

        public Sound buttonPress = null;
        public Sound odoTick = null;
        public Sound wrongButton = null;
        public Sound freeTabsLoop = null;
        public Sound buttonRevealAll = null;
        public Sound iconReveal = null;
        public Sound enterFreeTabs = null;


        public SoundSystem(MultiMediaLoader ml)
        {
            for (int i = 0; i < Definitions.TotalPaylines; i++)
            {
                lineSounds[i] = (Sound)ml.GetObject("btn_Line_" + (i + 1).ToString());           
            }

            for (int i = 0; i <= Definitions.iconIndexMax ; i++)
            {
                iconWinSounds[i] = (Sound)ml.GetObject("anims_Icon_" + (i + 1).ToString());
            }

            for (int i = 0; i < Definitions.magicianDances; i++)
            {
                magicianDances[i] = (Sound)ml.GetObject("anims_Dance_" + (i + 1).ToString());
            }

            buttonPress = (Sound)ml.GetObject("btn_Press");
            odoTick = (Sound)ml.GetObject("anims_CreditIncrease");
            wrongButton = (Sound)ml.GetObject("alrt_WrongPress");
            freeTabsLoop = (Sound)ml.GetObject("anim_BonusLoop");
            buttonRevealAll = (Sound)ml.GetObject("btn_RevealAllSound");
            iconReveal = (Sound)ml.GetObject("btn_IconReveal");
            enterFreeTabs = (Sound)ml.GetObject("alrt_FreeTabs");
            
        }
    }
}
