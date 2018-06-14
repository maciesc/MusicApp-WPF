using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MusicApp.State_Memento;
using MusicApp.Playlist_Iterator;
using System.Collections.ObjectModel;
using MusicApp.Song_Builder;
using System.ComponentModel;
using System.Windows.Media;
using MusicApp.History_Command;
using System.IO;
using System.Windows.Forms;

namespace MusicApp.Player_Singleton
{
    public class Player : INotifyPropertyChanged
    {
        #region Singleton

        private static Player instance;
        private PrintDialog printDlgl;
        public static Player Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Player();
                }

                return instance;
            }
        }

        private Player()
        {
            mediaPlayer = new MediaPlayer();

            Songs = new ObservableCollection<Song>();
            State = new State();

            PlayList = new NormalPlaylist();
            PlayList.Songs = Songs.ToList();
            this.TypeOfPlay = TypeOfPlay.Normal;
            State.TypeOfPlay = (int)TypeOfPlay.Normal;
        }

        #endregion

        private MediaPlayer mediaPlayer;
        private bool playing = false;

        public TypeOfPlay TypeOfPlay { get; private set; }

        public ObservableCollection<Song> Songs { get; set; }

        public State State { get; private set; }

        private Song _currentSong;
        public Song CurrentSong
        {
            get { return _currentSong; }
            set
            {
                if (_currentSong != value)
                {
                    _currentSong = value;
                    RaisePropertyChanged(nameof(this.CurrentSong));

                    if (CurrentSong != null && playing)
                    {
                        Play();
                    }
                }
            }
        }
        

        public void setSongTime(int time)
        {

            TimeSpan position = new TimeSpan(time/3600,time/60,time%60);
            mediaPlayer.Position = position;
            
        }
        public double CurrentSongPositioninDouble
        {
            get
            {
                if (CurrentSong != null && mediaPlayer.Source != null && mediaPlayer.NaturalDuration.HasTimeSpan)
                {
                    return mediaPlayer.Position.Seconds;
                }
                return 0;
            }
        }

        public TimeSpan CurrentSongLength
        {
            get
            {
                if (CurrentSong != null && mediaPlayer.Source != null && mediaPlayer.NaturalDuration.HasTimeSpan)
                {
                    return mediaPlayer.NaturalDuration.TimeSpan;
                }
                return TimeSpan.Zero;
            }
        }

        public TimeSpan CurrentSongPosition
        {
            get
            {
                RaisePropertyChanged(nameof(CurrentSongLength));

                if (CurrentSong != null && mediaPlayer.Source != null)
                {
                    
                    return mediaPlayer.Position;
                }
                return TimeSpan.Zero;
            }
            
        }

        


        public IPlaylist PlayList { get; set; }

        public void LoadSongs(List<string> filesPaths, string sourceFilePath)
        {
            this.State.FolderPath = sourceFilePath;

            SongCreator songCreator = new SongCreator();
            this.Songs.Clear();

            foreach (var file in filesPaths)
            {
                Song song = songCreator.Create(new TagSongBuilder(file));

                this.Songs.Add(song);
            }
        }

        public void CreatePlaylist(TypeOfPlay typeOfPlay)
        {
            this.TypeOfPlay = typeOfPlay;
           
            switch (this.TypeOfPlay)
            {
                case TypeOfPlay.Normal:
                    PlayList = new NormalPlaylist();
                    break;
                case TypeOfPlay.Random:
                    PlayList = new RandomPlaylist();
                    break;
                case TypeOfPlay.Reversed:
                    PlayList = new ReversedPlaylist();
                    break;
                default:
                    break;
            }

            this.PlayList.Songs = this.Songs.ToList();
            PlayList.First();
            this.CurrentSong = PlayList.CurrentItem;
        }

        public Boolean getPlaying() { return playing; }

        public void PlayPause()
        {
            if (!playing)
            {
                Play();
            }
            else
            {
                Pause();
            }
        }

        private void Play()
        {
            if (mediaPlayer.Source?.OriginalString != this.CurrentSong.FilePath)
            {
                mediaPlayer.Open(new Uri(this.CurrentSong.FilePath));
                
                AddSongToHistory(CurrentSong);
            }

            mediaPlayer.Play();

            playing = true;
        }

        private void Pause()
        {
            mediaPlayer.Pause();
            playing = false;
        }

        public void Stop()
        {
            mediaPlayer.Stop();
            playing = false;
        }

        public void Next()
        {
            PlayList.Next();
            this.CurrentSong = PlayList.CurrentItem;

            if (playing)
            {
                Play();
            }
        }

        public void Prev()
        {
            PlayList.Prev();
            this.CurrentSong = PlayList.CurrentItem;

            if (playing)
            {
                Play();
            }
        }

        public IMemento CreateMemento()
        {
            this.State.TypeOfPlay = (int)this.TypeOfPlay;

            if(this.CurrentSong != null)
            {
                this.State.SongFilePath = this.CurrentSong?.FilePath;
                this.State.Time = this.CurrentSongPosition.ToString();
            }

            MementoOfState memento = new MementoOfState();
            memento.SetState(this.State);
            return memento;
        }

        public void SetMemento(IMemento memento)
        {
            this.State = memento.GetState();
            this.TypeOfPlay = (TypeOfPlay)State.TypeOfPlay;

            this.CreatePlaylist(this.TypeOfPlay);

            if(string.IsNullOrEmpty(State.SongFilePath) == false)
            {
                this.CurrentSong = Songs.FirstOrDefault(item => item.FilePath == State.SongFilePath);

                mediaPlayer.Open(new Uri(this.CurrentSong.FilePath));
                AddSongToHistory(CurrentSong);
                mediaPlayer.MediaOpened += MediaPlayer_MediaOpened;
            }
        }

        private void MediaPlayer_MediaOpened(object sender, EventArgs e)
        {
            this.mediaPlayer.MediaOpened -= MediaPlayer_MediaOpened;

            if(string.IsNullOrEmpty(State.Time) == false)
            {
                TimeSpan position = TimeSpan.Parse(State.Time);
                mediaPlayer.Position = position;
                
                
            }
        }

        #region History - Command Implementation

        public ObservableCollection<Song> SongsHistory { get; set; } = new ObservableCollection<Song>();
        private Stack<ICommand> historyUndo = new Stack<ICommand>();
        private Stack<ICommand> historyRedo = new Stack<ICommand>();

        public void ClearHistory()
        {
            historyRedo.Clear();
            historyUndo.Clear();
            SongsHistory.Clear();
        }

        class AddSongToHistoryCommand : ICommand
        {
            Player player = null;
            private Song song = null;

            public AddSongToHistoryCommand(Player player, Song song)
            {
                this.player = player;
                this.song = song;
            }

            public void Undo()
            {
                player.SongsHistory.Remove(song);
            }

            public void Redo()
            {
                player.SongsHistory.Add(song);
            }
        }

        class RemoveSongFromHistoryCommand : ICommand
        {
            Player player = null;
            private Song song = null;
            private int index = -1;

            public RemoveSongFromHistoryCommand(Player player, Song song, int index)
            {
                this.player = player;
                this.song = song;
                this.index = index;
            }

            public void Undo()
            {
                player.SongsHistory.Insert(index, song);
            }

            public void Redo()
            {
                player.SongsHistory.Remove(song);
            }
        }

        public void AddSongToHistory(Song song)
        {
            historyUndo.Push(new AddSongToHistoryCommand(this, song));
            historyRedo.Clear();
            SongsHistory.Add(song);
        }

        public void RemoveSongFromHistory(Song song)
        {
            int index = SongsHistory.IndexOf(song);
            historyUndo.Push(new RemoveSongFromHistoryCommand(this, song, index));
            historyRedo.Clear();
            SongsHistory.Remove(song);
        }

        public bool CanUndo
        {
            get
            {
                return historyUndo.Count > 0;
            }
        }

        public bool CanRedo
        {
            get
            {
                return historyRedo.Count > 0;
            }
        }

        public void Undo()
        {
            if (historyUndo.Count > 0)
            {
                ICommand command = historyUndo.Pop();
                historyRedo.Push(command);
                command.Undo();
            }
        }

        public void Redo()
        {
            if (historyRedo.Count > 0)
            {
                ICommand command = historyRedo.Pop();
                historyUndo.Push(command);
                command.Redo();
            }
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
