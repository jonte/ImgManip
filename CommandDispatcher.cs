using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImgResize
{
    public class CommandDispatcher
    {
        private LinkedList<Func<bool>> commandList = new LinkedList<Func<bool>>();

        public void addCommand(Func<bool> cmd) {
            commandList.AddLast(cmd);
        }

        public bool hasCommand() {
            return commandList.Count > 0;
        }

        public void executeNext() {
            if (hasCommand()) {
                Func<bool> f = commandList.First.Value;
                commandList.RemoveFirst();
                f();
            }
        }

        public void executeAll() {
            while (hasCommand())
                executeNext();
        }
    }
}
