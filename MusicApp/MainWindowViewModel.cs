using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

using MusicApp.Song_Builder;
using MusicApp.Player_Singleton;
using MusicApp.State_Memento;
using System.Xml.Serialization;
using System.Xml;
using System.Runtime.Serialization.Formatters.Binary;

namespace MusicApp
{
   
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string folerPath = string.Empty;

        public Player Player
        {
            get
            {
                return Player.Instance;
            }
        }

        public bool IsFilesLoaded
        {
            get { return Player.Instance.Songs.Count > 0; }
        }

        private bool _playRandom = false;
        public bool PlayRandom
        {
            get { return _playRandom; }
            set
            {
                if (_playRandom != value)
                {
                    _playRandom = value;
                    RaisePropertyChanged(nameof(this.PlayRandom));

                    if (PlayRandom)
                    {
                        Player.Instance.CreatePlaylist(TypeOfPlay.Random);
                    }
                }
            }
        }

        private bool _playNormal = true;
        public bool PlayNormal
        {
            get { return _playNormal; }
            set
            {
                if (_playNormal != value)
                {
                    _playNormal = value;
                    RaisePropertyChanged(nameof(this.PlayNormal));

                    if (PlayNormal)
                    {
                        Player.Instance.CreatePlaylist(TypeOfPlay.Normal);
                    }
                }
            }
        }

        private bool _playReversed = false;
        public bool PlayReversed
        {
            get { return _playReversed; }
            set
            {
                if (_playReversed != value)
                {
                    _playReversed = value;
                    RaisePropertyChanged(nameof(this.PlayReversed));

                    if (PlayReversed)
                    {
                        Player.Instance.CreatePlaylist(TypeOfPlay.Reversed);
                    }
                }
            }
        }

        public ICommand LoadFilesCommand { get; set; }
        public ICommand PrevCommand { get; set; }
        public ICommand NextCommand { get; set; }
        public ICommand PlayPauseCommand { get; set; }
        public ICommand StopCommand { get; set; }

        public ICommand UndoHistoryCommand { get; set; }
        public ICommand RedoHistoryCommand { get; set; }
        public ICommand ClearHistoryCommand { get; set; }
        public ICommand RemoveSongFromHistoryCommand { get; set; }

        public ICommand CloseWindowCommand { get; set; }

        public MainWindowViewModel()
        {
            LoadFilesCommand = new RelayCommand<object>(LoadFiles);
            PrevCommand = new RelayCommand<object>(Prev);
            NextCommand = new RelayCommand<object>(Next, CanNext);
            PlayPauseCommand = new RelayCommand<object>(PlayPause);
            StopCommand = new RelayCommand<object>(Stop);

            UndoHistoryCommand = new RelayCommand<object>(UndoHistory, CanUndoHistory);
            RedoHistoryCommand = new RelayCommand<object>(RedoHistory, CanRedoHistory);
            ClearHistoryCommand = new RelayCommand<object>(ClearHistory);
            RemoveSongFromHistoryCommand = new RelayCommand<object>(RemoveSongFromHistory);

            CloseWindowCommand = new RelayCommand<object>(CloseWindow);

            if (Application.Current is App)
            {
                Read();
            }
        }
        
        private void LoadFiles(object par)
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.folerPath = dialog.SelectedPath;

                List<string> files = Directory.GetFiles(this.folerPath, "*.mp3").ToList();

                try
                {
                    Player.Instance.LoadSongs(files, this.folerPath);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            RaisePropertyChanged(nameof(this.IsFilesLoaded));

            if (PlayNormal)
            {
                Player.Instance.CreatePlaylist(TypeOfPlay.Normal);
            }
            else if (PlayRandom)
            {
                Player.Instance.CreatePlaylist(TypeOfPlay.Random);
            }
            else if (PlayReversed)
            {
                Player.Instance.CreatePlaylist(TypeOfPlay.Reversed);
            }
        }

        private void Prev(object par)
        { 
            Player.Instance.Prev();
        }

        private bool CanNext(object par)
        {
            return !Player.Instance.PlayList.IsDone;
        }

        private void Next(object par)
        {
            Player.Instance.Next();
        }

        private void PlayPause(object par)
        {
            Player.Instance.PlayPause();
        }

        private void Stop(object par)
        {
            Player.Instance.Stop();
        }

        private void ClearHistory(object par)
        {
            Player.Instance.ClearHistory();
        }

        private bool CanUndoHistory(object par)
        {
            return Player.Instance.CanUndo;
        }

        private void UndoHistory(object par)
        {
            Player.Instance.Undo();
        }

        private bool CanRedoHistory(object par)
        {
            return Player.Instance.CanRedo;
        }

        private void RedoHistory(object par)
        {
            Player.Instance.Redo();
        }

        public void RemoveSongFromHistory(object par)
        {
            Song song = par as Song;
            if (song != null)
            {
                Player.Instance.RemoveSongFromHistory(song);
            }
        }

        public void CloseWindow(object par)
        {
            Save();
        }

        private void Read()
        {
            FileInfo fileInfo = new FileInfo("data.xml");
            if (!fileInfo.Exists)
            {
                return;
            }

            FileStream fileStream = null;

            IMemento memento = null;

            try
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                fileStream = new FileStream("data.xml", FileMode.OpenOrCreate);
                memento = (MementoOfState)binaryFormatter.Deserialize(fileStream);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                }
            }

            if (memento != null)
            {
                this.folerPath = memento.GetState().FolderPath;

                if (string.IsNullOrEmpty(this.folerPath))
                {
                    return;
                }

                try
                {
                    List<string> files = Directory.GetFiles(this.folerPath, "*.mp3").ToList();
                    Player.Instance.LoadSongs(files, this.folerPath);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                RaisePropertyChanged(nameof(this.IsFilesLoaded));

                Player.Instance.SetMemento(memento);

                switch (Player.Instance.TypeOfPlay)
                {
                    case TypeOfPlay.Normal:
                        this._playNormal = true;
                        this._playRandom = false;
                        this._playReversed = false;
                        break;
                    case TypeOfPlay.Random:
                        this._playNormal = false;
                        this._playRandom = true;
                        this._playReversed = false;
                        break;
                    case TypeOfPlay.Reversed:
                        this._playNormal = false;
                        this._playRandom = false;
                        this._playReversed = true;
                        break;
                    default:
                        break;
                }

                RaisePropertyChanged(nameof(this.PlayNormal));
                RaisePropertyChanged(nameof(this.PlayRandom));
                RaisePropertyChanged(nameof(this.PlayReversed));
            }
        }

        private void Save()
        {
            MementoOfState memento = (MementoOfState)Player.Instance.CreateMemento();

            FileStream fileStream = null;

            try
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                fileStream = new FileStream("data.xml", FileMode.OpenOrCreate);
                binaryFormatter.Serialize(fileStream, memento);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                }
            }
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
