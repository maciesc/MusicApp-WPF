using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MusicApp.Song_Builder;

namespace MusicApp.Playlist_Iterator
{
    public interface IPlaylist
    {
        List<Song> Songs { get; set; }

        void First();
        void Next();
        void Prev();
        bool IsDone { get; }
        Song CurrentItem { get; }
    }
}
