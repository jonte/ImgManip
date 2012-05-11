using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ImgResize
{
    [TestClass]
    public class CommandDispatcherTest
    {
        CommandDispatcher cd = new CommandDispatcher();
        [TestMethod]
        public void canAdd() {
            Assert.IsFalse(cd.hasCommand());
            cd.addCommand(() => {return true;});
            Assert.IsTrue(cd.hasCommand());
        }

        [TestMethod]
        public void canExecute() {
            cd.addCommand(() => { return true; });
            cd.addCommand(() => { return true; });
            cd.executeAll();
            Assert.IsFalse(cd.hasCommand());
        }

        [TestMethod]
        public void internalFunctionExecutesProperly() {
            int x = 0;
            cd.addCommand(() => { x++; return true; });
            cd.addCommand(() => { x++; return true; });
            cd.executeAll();
            Assert.AreEqual(x, 2);
        }
    }
}
