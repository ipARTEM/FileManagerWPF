using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM
{
    public static class PathLine
    {
        public static List<DriveInfo> GetDrives()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            List<DriveInfo> newAllDrives = new List<DriveInfo>();
            foreach (var driveInfo in allDrives)
            {
                if (driveInfo.IsReady)
                {
                    newAllDrives.Add(driveInfo);
                }
            }
            return newAllDrives;
        }

        public static List<FileInfo> GetFiles(String path)
        {
            if (path == "" || path == null)
            {
                return null;
            }
            if (Path.HasExtension(path))
            {
                path = Path.GetDirectoryName(path);
            }
            return new DirectoryInfo(path).GetFiles().ToList();
        }

        public static void GetShow(string dirName)
        {
            Console.WriteLine();
            if (Directory.Exists(dirName))
            {
                Console.WriteLine("Подкаталоги:");
                string[] dirs = Directory.GetDirectories(dirName);
                foreach (string s in dirs)
                {
                    Console.WriteLine(s);
                }
                Console.WriteLine();
                Console.WriteLine("Файлы:");
                string[] files = Directory.GetFiles(dirName);
                foreach (string s in files)
                {
                    Console.WriteLine(s);
                }
            }
        }

        public static string GetPath(string dirName)
        {
            if (Directory.Exists(dirName))
            {
                return dirName;
            }
            else
                return null;
        }

        public static string CreateCatalog(string oldPath, string newCatalog)
        {
            string oldPath2 = @$"{oldPath}\{newCatalog}";
            DirectoryInfo dirInfo2 = new DirectoryInfo(oldPath2);
            DirectoryInfo dirInfo = new DirectoryInfo(oldPath);

            if (dirInfo2.Exists)
            {
                //dirInfo.Create();
                Console.WriteLine("такой каталог уже есть");
            }
            else
            {

                dirInfo.CreateSubdirectory(newCatalog);
                Console.WriteLine("папка успешно создана");
            }

            return dirInfo.FullName;
        }

        public static void CreateFile(string path, string file)
        {
            using (FileStream fstream = new FileStream($@"{path}\{file}", FileMode.OpenOrCreate))
            {

            }

        }

        public static void DeleteCatalog(string path, string folderDil)
        {
            try
            {
                path = @$"{path}\{folderDil}";
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                dirInfo.Delete(true);
                Console.WriteLine("Каталог удален");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void DeleteFile(string path, string file)
        {
            path = @$"{path}\{file}";
            FileInfo fileInf = new FileInfo(path);
            if (fileInf.Exists)
            {
                fileInf.Delete();
                
            }
        }
    }
}
