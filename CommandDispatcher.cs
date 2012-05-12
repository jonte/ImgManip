using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImgResize
{
    /// <summary>
    /// This is basically a linked list of command to apply. addCommand adds a new command and
    /// executeNext executes the next command in line. executeAll calls executeNext for all available
    /// commands.
    /// </summary>
    public class CommandDispatcher
    {
        private LinkedList<Func<bool>> commandList = new LinkedList<Func<bool>>();
        
        /// <summary>
        /// Add a function to the list
        /// </summary>
        /// <param name="cmd"></param>
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
        /// <summary>
        /// Execute all the functions available
        /// </summary>
        public void executeAll() {
            while (hasCommand())
                executeNext();
        }
    }
}
