using System;
using System.Collections.Generic;
using System.Text;

namespace BreakTheBankTabs1063
{

    

    public class Settings1063
    {
        public int noOfDenominations = 0;
        public decimal[] gameDenominations = null;
        

        public Settings1063()
        {
            
        }

        /// <summary>
        /// constructor, parses data string for the settings
        /// </summary>
        /// <param name="data">string to parse for the settings</param>
        /// 
        public Settings1063(string data)
        {
            // parse the data string
            parseSettings(data);
        }

        public bool IsSpin { get; internal set; }

        public void parseSettings(string nData)
        {
            try
            {
                if (nData == "")
                {
                    // empty string indicates that no existing gameSettings record
                    throw new Exception("Invalid Setup, please correct and save the settings at the POS to complete setup!");
                }
                else
                {
                    // parse the nData into variables
                    string[] parts = nData.Split(',');

                    if (parts.Length != (int.Parse(parts[0])+2))
                    {
                        throw new Exception("Invalid number of parameters (" + parts.Length.ToString() + ", should be " + (int.Parse(parts[0]) + 2) + ")");
                    }
                    int shift = 0;

                    noOfDenominations = int.Parse(parts[0]);
                    shift++;

                    gameDenominations = new decimal[noOfDenominations];
                    
                    for (int i = 0; i < noOfDenominations; i++)
                    {
                        gameDenominations[i] = decimal.Parse(parts[1 + i]);
                    }
                    shift += noOfDenominations;

                    
                }
            }
            catch (Exception ex)
            {
                string tmpErr = "Invalid Data String in GameSettings, " + ex.Message;
                throw new Exception(tmpErr);
            }
        }

        public string ToDataString()
        {
            // get settings from the variables and format the values into a string
            // maxCardDisplayMode,POSetNum,maxCardPrice,startNextGameDelay,nextBallDelay,endPays[0].startBallCnt,.......

            string tmpStr = noOfDenominations.ToString() +",";
            for (int i = 0; i < noOfDenominations; i++)
                tmpStr += gameDenominations[i].ToString() + ",";

            

            return tmpStr;
        }
    }
}
