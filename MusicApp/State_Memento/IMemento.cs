using System.Runtime.Serialization;

namespace MusicApp.State_Memento
{
    public interface IMemento : ISerializable
    {
        void SetState(State state);
        State GetState();
    }
}
