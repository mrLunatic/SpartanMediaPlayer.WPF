using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using SaprtanMediaPlayer.NAudioPlayer.Annotations;
using SpartanMediaPlayer.Models;

namespace SaprtanMediaPlayer.NAudioPlayer
{
    public class Class1
    {
    }

    public sealed class Player : IMediaPlayer
    {
        private IWavePlayer waveOut = new WaveOut();
        private ISampleProvider sampleProvider;

        private PlayList _playList;
        private TimeSpan _position;
        private MediaFile _mediaFile;
        private double _volume;
        private bool _shuffle;
        private RepeatMode _repeatMode;


        public TimeSpan Position
        {
            get { return _position; }
            private set
            {
                if (value.Equals(_position)) return;
                _position = value;
                OnPropertyChanged();
            }
        }

        public PlayList PlayList
        {
            get { return _playList; }
            set
            {
                if (Equals(value, _playList)) return;
                _playList = value;
                OnPropertyChanged();
            }
        }

        public MediaFile MediaFile
        {
            get { return _mediaFile; }
            set
            {
                if (Equals(value, _mediaFile)) return;
                _mediaFile = value;
                OnPropertyChanged();



            }
        }

        public double Volume
        {
            get { return _volume; }
            set
            {
                if (value.Equals(_volume)) return;
                _volume = value;
                OnPropertyChanged();
            }
        }

        public bool Shuffle
        {
            get { return _shuffle; }
            set
            {
                if (value.Equals(_shuffle)) return;
                _shuffle = value;
                OnPropertyChanged();
            }
        }

        public RepeatMode RepeatMode
        {
            get { return _repeatMode; }
            set
            {
                if (value == _repeatMode) return;
                _repeatMode = value;
                OnPropertyChanged();
            }
        }


        public void Play()
        {
            throw new NotImplementedException();
        }

        public void Pause()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public void PlayNext()
        {
            throw new NotImplementedException();
        }

        public void PlayPrev()
        {
            throw new NotImplementedException();
        }

        public void Seek(TimeSpan timeSpan)
        {
            throw new NotImplementedException();
        }


        #region INPC

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = ((INotifyPropertyChanged) this).PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}
