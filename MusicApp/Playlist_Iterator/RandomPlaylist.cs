using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MusicApp.Song_Builder;

namespace MusicApp.Playlist_Iterator
{
    public class RandomPlaylist : IPlaylist
    {
        Random random = new Random();

        public List<Song> Songs { get; set; } = new List<Song>();

        public Stack<int> SongsIndexHistory { get; set; } = new Stack<int>();
        private int index = -1;

        public bool IsDone
        {
            get
            {
                return false;
            }
        }

        public Song CurrentItem
        {
            get
            {
                if (index >= 0 && index < Songs.Count && Songs.Count > 0)
                {
                    return Songs[index];
                }

                return null;
            }
        }

        public void First()
        {
            index = 0;
            SongsIndexHistory.Push(index);
        }

        public void Prev()
        {
            if(SongsIndexHistory.Count > 0)
            {
                this.index = SongsIndexHistory.Pop();
            }
        }

        public void Next()
        {
            if(Songs.Count > 1)
            {
                int tmpIndex = -1;
                while(true)
                {
                    tmpIndex = random.Next(0, Songs.Count);
                    if(tmpIndex != index)
                    {
                        break;
                    }
                }
                
                if (tmpIndex > 0 && tmpIndex < Songs.Count)
                {
                    SongsIndexHistory.Push(index);
                    index = tmpIndex;
                }
            }
            else
            {
                First();
            }
        }
    }
}
