using MusicApp.Song_Builder;

namespace MusicApp.State_Memento
{
    public enum TypeOfPlay
    {
        Normal = 0,
        Random = 1,
        Reversed = 2
    };

    public class State
    {
        public State() { }

        public int TypeOfPlay { get; set; }
        public string SongFilePath { get; set; }
        public string FolderPath { get; set; }
        public string Time { get; set; }
    }
}
