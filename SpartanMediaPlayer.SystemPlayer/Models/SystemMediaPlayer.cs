using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using SpartanMediaPlayer.Annotations;

namespace SpartanMediaPlayer.Models
{
    public class SystemMediaPlayer : IMediaPlayer, INotifyPropertyChanged
    {
        private readonly MediaElement _player = new MediaElement();
        private readonly DispatcherTimer _timer = new DispatcherTimer();
        private readonly Random _random = new Random();

        private readonly IList<int> _playedTrackIndexes = new List<int>(); 

       

        #region IMediaPlayer

        public PlayerState PlayerState
        {
            get { return _playerState; }
            private set
            {
                if (value == _playerState) return;
                _playerState = value;
                OnPropertyChanged();
            }
        }
        private PlayerState _playerState;

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
        private TimeSpan _position;

        public TimeSpan Duration
        {
            get { return _duration; }
            private set
            {
                if (value.Equals(_duration)) return;
                _duration = value;
                OnPropertyChanged();
            }
        }
        private TimeSpan _duration;

        public PlayList PlayList
        {
            get { return _playList; }
            set
            {
                if (Equals(value, _playList)) return;
                _playList = value;
                Track = 0;
            }
        }
        private PlayList _playList;


        public MediaFile MediaFile
        {
            get { return _mediaFile; }
            private set
            {
                if (Equals(value, _mediaFile)) return;
                _mediaFile = value;
                OnPropertyChanged();
                _player.Source = _mediaFile.Uri;
                Duration = _player.NaturalDuration.TimeSpan;
            }
        }
        private MediaFile _mediaFile;
        
        public int Track
        {
            get { return _track; }
            set
            {
                if (_track == value) return;

                _track = value;
                OnPropertyChanged();

                _playedTrackIndexes.Add(_track);

                if (PlayList == null) return;

                MediaFile = PlayList.Tracks[_track];
            }
        }
        private int _track;

        public double Volume
        {
            get { return _volume; }
            set
            {
                if (Equals(_volume, value)) return;

                _volume = value;
                _player.Volume = value;
                OnPropertyChanged();
            }
        }
        private double _volume;

        public bool Shuffle
        {
            get { return _shuffle; }
            set
            {
                if (value.Equals(_shuffle)) return;
                _shuffle = value;
            }
        }
        private bool _shuffle;
        
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
        private RepeatMode _repeatMode;
       
        public void Play()
        {
            _player.Play();
            PlayerState = PlayerState.Playing;
            if (!_timer.IsEnabled)
                _timer.Start();
        }

        public void Pause()
        {
            if (!_player.CanPause) return;

            _player.Pause();
            PlayerState = PlayerState.Paused;

            if (_timer.IsEnabled)
                _timer.Stop();
        }

        public void Stop()
        {
            _player.Stop();
            PlayerState = PlayerState.Stopped;

            if (_timer.IsEnabled)
                _timer.Stop();
        }

        public void PlayNext()
        {
            Stop();

            if (PlayList == null) return;

            var index = GetNextTrackIndex();

            if (index < 0) return;

            Track = index;
            Play();
        }

        public void PlayPrev()
        {
            Stop();

            if (PlayList == null) return;

            var index = GetPrevTrackIndex();

            if (index < 0) return;

            Track = index;
            Play();
        }

        public void Seek(TimeSpan timeSpan)
        {
            _player.Position = timeSpan;
        }

        #endregion

        public SystemMediaPlayer()
        {
            _timer.Interval = TimeSpan.FromMilliseconds(100);
            _timer.Tick += (sender, args) => Position = _player.Position;
            _player.MediaEnded += (sender, args) => PlayNext();
        }

        private int GetNextTrackIndex()
        {

            if (RepeatMode == RepeatMode.One)
                return Track;

            var allIndexes = Enumerable.Range(0, PlayList.Tracks.Count);

            IList<int> avaliableIndexes = allIndexes.Except(_playedTrackIndexes).ToList();



            if (avaliableIndexes.Any())
            {
                var index = Shuffle? _random.Next(0, avaliableIndexes.Count()) : 0;

                return avaliableIndexes.ElementAt(index);
            }

            if (RepeatMode == RepeatMode.None)
                return -1;
            
            _playedTrackIndexes.Clear();

            return Shuffle ? _random.Next(0, PlayList.Tracks.Count - 1) : 0;
        }

        private int GetPrevTrackIndex()
        {
            if (_playedTrackIndexes.Count == 0) return -1;

            var index = _playedTrackIndexes.Last();

            _playedTrackIndexes.RemoveAt(_playedTrackIndexes.Count - 1);

            return index;
        }

        #region INPC





        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
