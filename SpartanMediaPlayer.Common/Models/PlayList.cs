using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartanMediaPlayer.Models
{
    public class PlayList
    {
        public string Title { get; set; }

        public IList<MediaFile> Tracks { get; set; }

        public PlayList()
        {
            Title = string.Empty;
            Tracks = new ObservableCollection<MediaFile>();
        }
    }
}
