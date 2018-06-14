using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Song_Builder
{
    public class Song
    {
        public string FilePath { get; }

        public string FileName
        {
            get
            {
                FileInfo fileInfo = new FileInfo(FilePath);
                return fileInfo.Name;
            }
        }

        public string Title { get; set; }
        public string Artist { get; set; }
        public string Genre { get; set; }
        public string Year { get; set; }

        public Song()
        {

        }

        public Song(string filePath)
        {
            this.FilePath = filePath;
        }

        public override string ToString()
        {
            string str = string.Empty;

            if (string.IsNullOrEmpty(Title))
                str += "\nTitle: Empty";
            else
                str += "\nTitle: " + Title;

            if (string.IsNullOrEmpty(Artist))
                str += "\nArtist: Empty";
            else
                str += "\nArtist: " + Artist;

            if (string.IsNullOrEmpty(Genre))
                str += "\nGenre: Empty";
            else
                str += "\nGenre: " + Genre;

            if (string.IsNullOrEmpty(Year))
                str += "\nYear: Empty";
            else
                str += "\nYear: " + Year;

            return str;
        }
    }
}
