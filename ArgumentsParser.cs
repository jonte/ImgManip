using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Test.CommandLineParsing;


namespace ImgResize
{
    public class ArgumentsParser
    {
        public static void parseArguments(String[] args, CommandDispatcher cd){
            try
            {
                CommandLineDictionary dict = CommandLineDictionary.FromArguments(args, '-', '=');
                // Shorthand
                Func<string, bool> hasPar = (string key) => hasParameter(dict, key);

                if (hasPar("install"))
                    cd.addCommand(() => RegistryManipulator.install());

                else if (hasPar("uninstall"))
                    cd.addCommand(() => RegistryManipulator.uninstall());

                else if(hasFileParameter(dict)){
                    var filename = dict["file"];
                    if(hasPar("resizePercentage"))
                        cd.addCommand(() => ImageManipulators.setNewSize(filename, Byte.Parse(dict["resizePercentage"])));

                    if (hasPar("resizeSlider"))
                        cd.addCommand(() => {
                            var p = new PercentageSlider();
                            var perc = p.getSelection();
                            return ImageManipulators.setNewSize(filename, perc);
                        });

                    if (hasPar("sepiaPercentage"))
                        cd.addCommand(() => ImageManipulators.setSepiaTone(filename, Byte.Parse(dict["sepiaPercentage"])));

                    if(hasPar("grayScale"))
                        cd.addCommand(() => ImageManipulators.setColorspace(filename, "gray"));

                    if (hasPar("more-contrast"))
                        cd.addCommand(() => ImageManipulators.setContrast(filename, "-contrast"));

                    if (hasPar("less-contrast"))
                        cd.addCommand(() => ImageManipulators.setContrast(filename, "+contrast"));

                    if (hasPar("addBorder") && hasPar("borderColor"))
                        cd.addCommand(() => ImageManipulators.setBorder(filename, dict["borderColor"]));

                    if (hasPar("oilPaint"))
                        cd.addCommand(() => ImageManipulators.oilPaint(filename, dict["oilPaint"]));

                    if (hasPar("signature"))
                        cd.addCommand(() => ImageManipulators.signature(filename, dict["signature"]));

                    if (hasPar("toFileType"))
                        cd.addCommand(() => ImageManipulators.toFileType(filename, dict["toFileType"]));
                }              
            }
            catch (System.ArgumentException) { printUsageHelp(); }
            catch (KeyNotFoundException) { /* This is not neccessarily bad */ }
        }

        /*
         * Ensure a filename is present
         */
        private static bool hasFileParameter(Dictionary<String, String> dict) {
            return hasParameter(dict, "file", new System.ArgumentException());
        }
        private static bool hasParameter(Dictionary<String, String> dict, String parameter, Exception ex) { 
            if (!hasParameter(dict, parameter))
                throw ex;
            else
                return true;
        }
        private static bool hasParameter(Dictionary<String, String> dict, String parameter) {
            return dict.Keys.Contains(parameter);
        }
        /*
         * Print the usage help for the application, and then exit with a code 0 (which does not have
         * a specific meaning..)
         * */
        public static void printUsageHelp() {
            log4net.LogManager.GetLogger("main").Debug("Called with incorrect parameters, showing usage help");
            Console.Out.WriteLine("Oops.. It seems some commandline parameter is missing or is not parsable." + 
                                  "Here is the correct syntax for supplying commandline parameters:\n" +
                                  "-resizePercentage=[percentage of change]\n" +
                                  "-file=[Path to file] (MUST be present when supplying a modifying parameter such as -resizePercentage" +
                                  "EXAMPLES:\n"+
                                  "-resizePercentage=25 -file=c:\\myfile.png");
            System.Environment.Exit(0);
        }
    }
}
