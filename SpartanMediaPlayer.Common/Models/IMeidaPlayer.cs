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
        Equalizer Equalizer { get; set; }

        TimeSpan CurrentPosition { get; }

        PlayList CurrentPlayList { get; set; }
        MediaFile CurrentFile { get; set; }

        double MainLevel { get; set; }
        double Balance { get; set; }
        bool Muted { get; set; }

        void Play();
        void Pause();
        void Stop();
        void PlayNext();
        void SeekForward(TimeSpan timeSpan);
        void PlayPrev();
        void SeekBackward(TimeSpan timeSpan);
    }
}
