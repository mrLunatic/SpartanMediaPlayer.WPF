using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using SpartanMediaPlayer.Annotations;

namespace SpartanMediaPlayer.Models
{
    public class SystemMediaPlayer : IMediaPlayer
    {
        private readonly MediaElement _player = new MediaElement();
        private readonly DispatcherTimer _timer = new DispatcherTimer();

        private int currentTrackIndex = 0;
        private IList<int> playedTrackIndexes = new List<int>(); 

       

        #region IMediaPlayer

        public TimeSpan Position
        {
            get { return _player.Position; }
        }

        public TimeSpan Duration
        {
            get { return _player.NaturalDuration.TimeSpan; }
        }

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

        
        public int Track
        {
            get { return _track; }
            set
            {
                _track = value;

                if (PlayList == null) return;

                _player.Source = PlayList.Tracks[_track].Uri;
            }
        }

        public double Volume
        {
            get { return _player.Volume; }
            set { _player.Volume = value; }
        }

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
        private int _track;

        public RepeatMode RepeatMode { get; set; }

        public void Play()
        {
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
        }

        public void PlayNext()
        {
            Stop();

            if (PlayList == null) return;




            
            Play();
        }

        public void PlayPrev()
        {
            Stop();
            Track = GetPrevFile();
            Play();
        }

        public void Seek(TimeSpan timeSpan)
        {
            _player.Position = timeSpan;
        }

        #endregion

        public SystemMediaPlayer()
        {
            _player.MediaEnded += OnMediaEnded;
            _player.MediaOpened += OnMediaOpened;
        }


        private void OnMediaEnded(object sender, System.Windows.RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private MediaFile GetPrevFile()
        {
            return null;
        }
    }
}
