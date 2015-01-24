using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartanMediaPlayer.Models
{
    public class MediaFile
    {
        public string Path { get; set; }
        public int Rating { get; set; }

        public TimeSpan Duration { get; set; }

        public string Author { get; set; }
        public string Title { get; set; }
        public string Album { get; set; }
        public string Genre { get; set; }
    }
}
