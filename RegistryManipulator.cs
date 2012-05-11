using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using log4net;

namespace ImgResize
{
    /* Contains functions for installing and uninstalling the application in the system
     * registry. This is done to place the items in the context menu
     */
    public class RegistryManipulator
    {
        private static bool debug = true;
        // This should probably be customizable..
        private static string commandPath = "%USERPROFILE%\\imgResize\\ImageResize.NET.exe";
        public static Dictionary<String, String[]> commandsMap = new Dictionary<String, String[]> {
                    {"Scale25", new string[]{"-resizePercentage=25", "Skala om till 25% storlek"}},
                    {"Scale50", new string[]{"-resizePercentage=50 ", "Skala om till 50% storlek"}},
                    {"Scale75", new string[]{"-resizePercentage=75", "Skala om till 75% storlek"}},
                    {"ScaleX", new string[]{"-resizeSlider", "Skala om till valbar storlek"}},
                    {"ToSepia", new string[]{"-sepiaPercentage=80", "Ändra sepiaton"}},
                    {"ToGrayscale", new string[]{"-grayscale", "Konvertera till gråskala"}},
                    {"MoreContrast", new string[]{"-more-contrast", "Öka kontrast"}},
                    {"LessContrast", new string[]{"-less-contrast", "Sänk kontrast"}},
                    {"AddBorderBlack", new string[]{"-addBorder -borderColor=black", "Lägg till svart ram (10px)"}},
                    {"AddBorderWhite", new string[]{"-addBorder -borderColor=white", "Lägg till vit ram (10px)"}},
                    {"PaintSmall", new string[]{"-oilPaint=3", "Gör om till oljemålning (liten pensel)"}},
                    {"PaintBig", new string[]{"-oilPaint=7", "Gör om till oljemålning (stor pensel)"}},
                    {"SignatureSE", new string[]{"-signature=southeast", "Placera signatur i sydöstra hörnet"}},
                    {"ToPNG", new string[]{"-toFileType=PNG", "Konvertera till PNG-format"}},
                };
        private static ILog log = log4net.LogManager.GetLogger("main");
        public static bool install() {
            log.Debug("Running installation");
            try
            {
                
                RegistryKey rk = Registry.ClassesRoot.CreateSubKey("*\\shell\\Scale down");
                rk.SetValue("icon", "imageres.dll,-168", RegistryValueKind.String);
                rk.SetValue("MUIVerb", "Bildverktyg", RegistryValueKind.String);

                // This will contain the submenu items
                LinkedList<String> choices = new LinkedList<String>();

                // Create the menu items
                rk.SetValue("SubCommands", String.Join(";", commandsMap.Keys), RegistryValueKind.String);

                // Create the commands for the menu items
                foreach (var item in commandsMap){
                    installCommand(item);
                }
	        }
            catch (UnauthorizedAccessException e) {
                privilegeError(e.ToString());
            }
            return true;
        }
        private static void installCommand(KeyValuePair<String, String[]> item) {
            /**
             * BIGASS WARNING: This section of the code will fail horribly if the build is not set to Any CPU.
             * Windows will not put the values where specified, but rather somewhere else if x86 is spec'd.
             */
            if (debug)
            {
                log.Warn("Using debug paths.. This is really bad if this is not a dev machine!");
                commandPath = System.Reflection.Assembly.GetExecutingAssembly().Location;

            }
            log.Debug("Registering menu item: " + item.Key);
            string completeCommand = commandPath + " " + item.Value[0] + " -file=\"%1\"";

            RegistryKey rk = Registry.LocalMachine.
                 CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\CommandStore\\shell\\" + item.Key, RegistryKeyPermissionCheck.ReadWriteSubTree);
            rk.SetValue("", item.Value[1]);
            rk.Close();
            rk = Registry.LocalMachine.
                CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\CommandStore\\shell\\" + item.Key + "\\command", RegistryKeyPermissionCheck.ReadWriteSubTree);
            log.Debug("Setting Key: " + item.Key + " Value: " + completeCommand);
            rk.SetValue("", completeCommand, RegistryValueKind.ExpandString);
            rk.Close();
            log.Debug("Finished registering: " + item.Key);
        }
        public static bool uninstall() {
            log.Debug("Uninstalling!");
            throw new Exception("This isn't really working yet..");
            return true;
        }

        private static void privilegeError(String reason = "unknown") {
            var username = Environment.UserName;
            log.Error("Caught privilegeError as user '"+username+
                "', and this was passed as the reason: " + reason);
        }
    }

    
}
