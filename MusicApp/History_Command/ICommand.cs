using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.History_Command
{
    public interface ICommand
    {
        void Undo();
        void Redo();
    }
}
