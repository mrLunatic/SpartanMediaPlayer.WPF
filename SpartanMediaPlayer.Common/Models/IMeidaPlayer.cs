using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartanMediaPlayer.Models
{
    public interface IMediaPlayer 
    {
        /// <summary>
        /// Текущее положение на треке
        /// </summary>
        TimeSpan Position { get; }

        TimeSpan Duration { get; }

        /// <summary>
        /// Воспроизводимый плейлист
        /// </summary>
        PlayList PlayList { get; set; }

        /// <summary>
        /// Текущий трек
        /// </summary>
        MediaFile MedaFile { get; }

        /// <summary>
        /// Номер трека в плейлисте
        /// </summary>
        int Track { get; set; }

        /// <summary>
        /// Текущее значение громкости (0.0F - Min; 1.0F - Max)
        /// </summary>
        double Volume { get; set; }

        /// <summary>
        /// Проигрывать в случайном порядке
        /// </summary>
        bool Shuffle { get; set; }

        /// <summary>
        /// Режим повторения
        /// </summary>
        RepeatMode RepeatMode { get; set; }

        /// <summary>
        /// Воспроизводить текущий плейлист
        /// </summary>
        void Play();

        /// <summary>
        /// Приостановить воспроизведение
        /// </summary>
        void Pause();

        /// <summary>
        /// Остановить воспроизведение
        /// </summary>
        void Stop();

        /// <summary>
        /// Перейти к следующему файлу
        /// </summary>
        void PlayNext();

        /// <summary>
        /// Перейти к началу трека / перейти к предыдущему треку
        /// </summary>
        void PlayPrev();

        /// <summary>
        /// Перейти к заданному времени
        /// </summary>
        /// <param name="timeSpan">Время, на которое необходимо перейти</param>
        void Seek(TimeSpan timeSpan);   
    }
}
