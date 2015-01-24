using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using SpartanMediaPlayer.Annotations;

namespace SpartanMediaPlayer.Models
{
    public class SystemMediaPlayer : IMediaPlayer
    {


        private static TimeSpan SeekStep = TimeSpan.FromSeconds(2.0);
        private MediaPlayer _player = new MediaPlayer();
        private PlayList _currentPlayList;
        private MediaFile _currentFile;
        private int _currentFileIndex;


        public Equalizer Equalizer
        {
            get { return _equalizer; }
            set { _equalizer = value; }
        }

        public TimeSpan CurrentPosition
        {
            get { return _player.Position; }
            private set
            {
                _player.Position = value;
                OnPropertyChanged();
            }
        }

        public PlayList CurrentPlayList
        {
            get { return _currentPlayList; }
            set
            {
                if (Equals(value, _currentPlayList)) return;
                _currentPlayList = value;
                OnPropertyChanged();
            }
        }

        public MediaFile CurrentFile
        {
            get { return _currentFile; }
            set
            {
                if (Equals(value, _currentFile)) return;
                _currentFile = value;
                OnPropertyChanged();

                if (value != null)
                {
                    _player.Open(new Uri(value.Path));
                    Play();
                }
            }
        }

        public double MainLevel
        {
            get { return _player.Volume; }
            set
            {
                if (value.Equals(_player.Volume)) return;
                _player.Volume = value;
                OnPropertyChanged();
            }
        }

        public double Balance
        {
            get { return _player.Balance; }
            set
            {
                if (value.Equals(_player.Balance)) return;
                _player.Balance = value;
                OnPropertyChanged();
            }
        }

        public bool Muted
        {
            get { return _player.IsMuted; }
            set
            {
                if (value.Equals(_player.IsMuted)) return;
                _player.IsMuted = value;
                OnPropertyChanged();
            }
        }


        public void Play()
        {
            if (CurrentFile != null)
                _player.Play();
        }

        public void Pause()
        {
            if (_player.CanPause)
                _player.Pause();
        }

        public void Stop()
        {
            _player.Stop();
            CurrentPosition = TimeSpan.FromSeconds(0.0);
        }

        public void PlayNext()
        {
            if (CurrentPlayList == null) return;

            if (CurrentPlayList.Tracks.GetEnumerator().MoveNext())
                CurrentFile = CurrentPlayList.Tracks.GetEnumerator().Current;
        }

        public void SeekForward(TimeSpan timeSpan)
        {
            if (CurrentFile == null) return;

            if ((CurrentFile.Duration - CurrentPosition) < SeekStep)
                CurrentPosition += SeekStep;
        }

        public void PlayPrev()
        {
            if (CurrentPlayList == null) return;

            if (CurrentPlayList.Tracks.GetEnumerator().)
                CurrentFile = CurrentPlayList.Tracks.GetEnumerator().Current;
        }

        public void SeekBackward(TimeSpan timeSpan)
        {
            throw new NotImplementedException();
        }

        #region INPC

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = ((INotifyPropertyChanged) this).PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}
