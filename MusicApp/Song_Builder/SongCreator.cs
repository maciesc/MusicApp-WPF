using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Song_Builder
{
    public class SongCreator
    {
        public Song Create(SongBuilder songBuilder)
        {
            songBuilder.SetTitle();
            songBuilder.SetArtist();
            songBuilder.SetGenre();
            songBuilder.SetYear();
            return songBuilder.GetSong();
        }
    }
}
