using System;
using System.Collections.Generic;
using System.Text;
using BreakTheBankTabs1063.GameObjects;
using BreakTheBankTabs1063.utils;

using BreakTheBankTabs1063.MainStates;

namespace BreakTheBankTabs1063
{
    public class DataFunctions
    {
        // Data string content for Super Bonus
        // gameSysID, bonusPlay, cardCount, card serial numbers
        // example
        // 2002,false,4,123456,234567,345678,456789,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0

        public static int parseCardCount(string pStr)
        {
            string[] parts = null;
            int tmpCount = 0;
            parts = pStr.Split(',');
            try
            {
                tmpCount = int.Parse(parts[2]);
                return tmpCount;
            }
            catch
            {
                return tmpCount;
            }
        }


        public static string convertGameDataToString(int gID, bool isFreeTabsMode,  Icon[] tmpIcons)
        {

            string tmpStr = gID.ToString() + "," + isFreeTabsMode;
            tmpStr += "," + StateMain.singleton.tabNo.ToString();
            tmpStr += "," + StateMain.singleton.wonAmount;
            tmpStr += "," + StateMain.singleton.TabPrices[StateMain.singleton.tabPricesIdx].ToString();

            for (int idx = 0; idx < Definitions.IconsPerTab; ++idx)
                tmpStr = tmpStr + "," + tmpIcons[idx].iconNo.ToString();

            for (int PatternIdx = 0; PatternIdx < Definitions.TotalPaylines; ++PatternIdx)
            {
                decimal patAmt=((StPlay)StateMain.singleton.stPlay).finalLineAmounts[PatternIdx];                
                tmpStr = tmpStr + "," + patAmt.ToString();
            }

            return tmpStr;
        }
    }
}
