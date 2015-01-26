using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartanMediaPlayer.Models
{
    public class MediaFile
    {
        /// <summary>
        /// Путь к файлу на локальной машине
        /// </summary>
        public Uri Uri { get; set; }

        public byte[] Image { get; set; }

        /// <summary>
        /// Продолжительность трека
        /// </summary>
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// Исполнитель трека
        /// </summary>
        public string Artist { get; set; }

        /// <summary>
        /// Название трека
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Название альбома
        /// </summary>
        public string Album { get; set; }

        /// <summary>
        /// Жанр трека
        /// </summary>
        public string Genre { get; set; }

    }
}
