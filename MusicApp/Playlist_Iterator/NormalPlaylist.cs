﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MusicApp.Song_Builder;

namespace MusicApp.Playlist_Iterator
{
    public class NormalPlaylist : IPlaylist
    {
        public List<Song> Songs { get; set; } = new List<Song>();
        private int index = -1;

        public bool IsDone
        {
            get
            {
                if ((index + 1) == Songs.Count)
                    return true;
                else
                    return false;
            }
        }

        public Song CurrentItem
        {
            get
            {
                if (index >= 0 && index < Songs.Count && Songs.Count > 0 )
                {
                    return Songs[index];
                }

                return null;
            }
        }

        public void First()
        {
            index = 0;
        }

        public void Prev()
        {
            if(index > 0)
            {
                index--;
            }
        }

        public void Next()
        {
            if ((index + 1) < Songs.Count)
            {
                index++;
            }
        }
    }
}
