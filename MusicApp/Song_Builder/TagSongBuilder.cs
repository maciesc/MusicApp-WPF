using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Song_Builder
{
    public class TagSongBuilder : SongBuilder
    {
        TagLib.File file = null;

        public TagSongBuilder(string songFileName)
            : base(songFileName)
        {
            file = TagLib.File.Create(songFileName);
        }

        public override void SetTitle()
        {
            song.Title = file.Tag.Title;
        }

        public override void SetArtist()
        {
            string[] artists = file.Tag.Artists;
            if (artists.Length > 0)
            {
                song.Artist = string.Join(", ", artists);
                return;
            }

            string[] albumArtists = file.Tag.AlbumArtists;
            if (albumArtists.Length > 0)
            {
                song.Artist = string.Join(", ", albumArtists);
                return;
            }

            string joinedArtists = file.Tag.JoinedAlbumArtists;
            if (!string.IsNullOrEmpty(joinedArtists))
            {
                song.Artist = joinedArtists;
                return;
            }

            return;
        }

        public override void SetGenre()
        {
            string[] genres = file.Tag.Genres;
            if (genres.Length > 0)
            {
                song.Genre = string.Join(", ", genres);
            }

            return;
        }

        public override void SetYear()
        {
            song.Year = file.Tag.Year.ToString();
            return;
        }
    }
}
