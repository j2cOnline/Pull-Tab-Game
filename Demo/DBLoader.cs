using System;
using System.Collections.Generic;
using System.Text;
using GameCore.MultiMedia;

namespace BreakTheBankTabs1063
{
    /// <summary>
    /// This class extends MultiMediaLoader to include the new
    /// MultiMedia objects added to Table Board Bingo
    /// </summary>
    public class BMLoader : MultiMediaLoader
    {
        public BMLoader(string name): base(name)
        {
            //loadItems.Add(new Card());
//            loadItems.Add(new Balls());
        }

        public override MultiMediaObject OnReadLine(string header)
        {
            MultiMediaObject obj = null;
            return obj;
        }

        public override void OnSave(System.IO.StreamWriter stream)
        {
            base.OnSave(stream);
        }
    }
}
