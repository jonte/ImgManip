using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;

namespace ImgResize
{
    class Program
    {
        private static readonly ILog log = LogManager.GetLogger("main");
        static void Main(string[] args)
        {
            // Set up logging
            log4net.Config.XmlConfigurator.Configure(); // use app.config
            
            log.Info("Starting run..");
            log.Info("Parsing arguments..");
            CommandDispatcher commandDispatcher = new CommandDispatcher();
            ArgumentsParser.parseArguments(args, commandDispatcher);

            log.Info("Executing commands..");
            commandDispatcher.executeAll();
        }
    }
}
