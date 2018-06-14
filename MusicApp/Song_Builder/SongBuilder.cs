using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Song_Builder
{
    public abstract class SongBuilder
    {
        protected Song song = null;

        public SongBuilder(string songFilePath)
        {
            this.song = new Song(songFilePath);
        }

        public Song GetSong()
        {
            return song;
        }

        public abstract void SetArtist();
        public abstract void SetTitle();
        public abstract void SetGenre();
        public abstract void SetYear();
    }
}
