using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartanMediaPlayer.Models
{
    public interface IMediaPlayer : INotifyPropertyChanged
    {
        TimeSpan CurrentPosition { get; }

        PlayList CurrentPlayList { get; set; }
        MediaFile CurrentFile { get; set; }

        double Volume { get; set; }
        bool Shuffle { get; set; }
        RepeatMode RepeatMode { get; set; }

        void Play();
        void Pause();
        void Stop();

        void PlayNext();
        void PlayPrev();

        void Seek(TimeSpan timeSpan);   
    }
}
