using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ImgResize
{
    public class ImageManipulators
    {
        public static bool setSepiaTone(String fileName, int tonePercentage) {
            Debug.WriteLine("Setting sepia tone of {0} to {1}", 
                fileName,
                tonePercentage);
            return doConvert(fileName, String.Format("-sepia-tone {0}%", tonePercentage),
                "sepia_" + tonePercentage);  
        }

        public static bool setNewSize(String fileName, int newSizePercentage)
        {
            Console.WriteLine("Setting new size of {0} to {1}",
                fileName,
                newSizePercentage);
            return doConvert(fileName, String.Format("-resize {0}%", newSizePercentage),
                "resized_" + newSizePercentage);
        }

        public static bool setColorspace(String filename, String colorspace) {
            return doConvert(filename, "-colorspace " + colorspace, colorspace);
        }

        /// <summary>
        /// Increase or decrease contrast
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="param">+contrast decreases contrast, -contrast increases contrast</param>
        /// <returns></returns>
        internal static bool setContrast(string filename, string param)
        {
            return doConvert(filename, param, param);
        }

        /// <summary>
        /// Set a 10px border, calling this several times will increase the border size
        /// </summary>
        /// <param name="filename">file to manipulate</param>
        /// <param name="color">color of border (websafe colors, probably.. deps on imagemagick)</param>
        /// <returns></returns>
        internal static bool setBorder(string filename, string color)
        {
            return doConvert(filename, "-bordercolor " + color + " -border 10", color+"_border");
        }

        internal static bool oilPaint(string filename, string size)
        {
            return doConvert(filename, "-paint " + size, "oilpaint" + size);
        }

        internal static bool signature(string filename, string position)
        {
            var signatureFile = getReource("Graphics", "signature.png");
            var convertTool = getReource("ImageMagick", "convert.exe");
            var newFilename = 
                System.IO.Path.GetDirectoryName(filename) + 
                System.IO.Path.DirectorySeparatorChar +
                System.IO.Path.GetFileNameWithoutExtension(filename) + "_sign_"+ position + "_" + 
                System.IO.Path.GetExtension(filename);
            var commandParam = " -composite -gravity " + position + " " + filename + " \"" + signatureFile + "\"" + " \"" + newFilename + "\"";
            var completeTool = convertTool + commandParam;
            return runCommand(convertTool, commandParam);
        }

        internal static bool toFileType(string filename, string newExtension)
        {
            return doConvert(filename, "", "to_" +newExtension+"."+newExtension, false);
        }


        /// <summary>
        /// Retrieve a resource from the specified subdirectory. This can be used to retrieve
        /// imagemagick tools, or graphics for signstures, for example.
        /// </summary>
        /// <param name="subDir">The directory in which to look for the resource</param>
        /// <param name="resource">The resource filename</param>
        /// <returns>A string containing the full path to the resource (guaranteed to exist)</returns>
        private static String getReource(String subDir, String resource)
        {
            char sep = System.IO.Path.DirectorySeparatorChar;
            String fileLocation = System.Reflection.Assembly.GetExecutingAssembly().Location;
            String fullToolPath =  System.IO.Path.GetDirectoryName(fileLocation) +
                sep + subDir + sep + resource;
            if (System.IO.File.Exists(fullToolPath))
                return fullToolPath;
            else
                throw new System.IO.FileNotFoundException();
        }

        /*
         * This is the interface to the convert.exe tool. All conversions should go through here
         */
        private static bool doConvert(String filename, String command, String postfix, bool preserveExtension = true)
        {
            String tool = getReource("ImageMagick", "convert.exe");
            String commandArguments = String.Format("\"{0}\" {1} \"{2}{3}{4}_{5}{6}\"",
                filename,
                command,
                System.IO.Path.GetDirectoryName(filename),
                System.IO.Path.DirectorySeparatorChar,
                System.IO.Path.GetFileNameWithoutExtension(filename),
                postfix,
                preserveExtension ? System.IO.Path.GetExtension(filename) : "");
            return runCommand(tool, commandArguments);
        }

        private static bool runCommand(string command, string args) {
            var x = (command + " " + args);
            var p = Process.Start(command, args);
            return true;
        
        }

    }
}
