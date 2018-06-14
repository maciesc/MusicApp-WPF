using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.State_Memento
{
    [Serializable]
    public class MementoOfState : IMemento
    {
        private State state;

        public void SetState(State state)
        {
            this.state = state;
        }

        public State GetState()
        {
            return state;
        }

        public MementoOfState() { }

        protected MementoOfState(SerializationInfo info, StreamingContext context)
        {
            state = new State();
            state.TypeOfPlay = (int)info.GetValue("TypeOfPlay", typeof(int));
            state.SongFilePath = (string)info.GetValue("SongFilePath", typeof(string));
            state.FolderPath = (string)info.GetValue("FolderPath", typeof(string));
            state.Time = (string)info.GetValue("Time", typeof(string));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("TypeOfPlay", state.TypeOfPlay);
            info.AddValue("SongFilePath", state.SongFilePath);
            info.AddValue("FolderPath", state.FolderPath);
            info.AddValue("Time", state.Time);
        }
    }
}
