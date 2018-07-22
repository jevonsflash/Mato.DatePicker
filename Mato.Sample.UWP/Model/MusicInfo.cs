using System.Collections.Generic;
using Mato.AutoComplete.UWP;

namespace Mato.Sample.UWP.Model
{
    public class MusicInfo : IClueObject
    {
        public string Title
        {
            get;
            set;
        }
        public string Url
        {
            get;
            set;
        }
        public string AlbumTitle
        {
            get;
            set;
        }
        public string Artist
        {
            get;
            set;
        }
        public List<string> ClueStrings
        {
            get
            {
                var result = new List<string>();


                result.Add(this.Title);
                result.Add(this.Artist);
                result.Add(this.AlbumTitle);
                return result;
            }
        }
    }
}
