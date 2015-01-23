using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartanMediaPlayer.Models
{
    public class PlayList
    {
        public string Title { get; set; }

        public IEnumerable<MediaFile> Tracks { get; set; }
    }
}
