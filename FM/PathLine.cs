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
        /// <summary>
        /// Показать все диски
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Показать все файлы
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Показать все папки и файлы в заданной директории
        /// </summary>
        /// <param name="dirName"></param>
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

        /// <summary>
        /// Название директории
        /// </summary>
        /// <param name="dirName"></param>
        /// <returns></returns>
        public static string GetPath(string dirName)
        {
            if (Directory.Exists(dirName))
            {
                return dirName;
            }
            else
                return null;
        }

        /// <summary>
        /// Создать папку
        /// </summary>
        /// <param name="oldPath"></param>
        /// <param name="newCatalog"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Создать файл
        /// </summary>
        /// <param name="path"></param>
        /// <param name="file"></param>
        public static void CreateFile(string path, string file)
        {
            using (FileStream fstream = new FileStream($@"{path}\{file}", FileMode.OpenOrCreate))
            {

            }

        }

        /// <summary>
        /// Удалить папку
        /// </summary>
        /// <param name="path"></param>
        /// <param name="folderDil"></param>
        public static void DeleteCatalog(string path, string folderDil)
        {
            try
            {
                path = @$"{path}\{folderDil}";
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                dirInfo.Delete(true);
                Console.WriteLine("Каталог удалён");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Удалить файл
        /// </summary>
        /// <param name="path"></param>
        /// <param name="file"></param>
        public static void DeleteFile(string path, string file)
        {
            path = @$"{path}\{file}";
            FileInfo fileInf = new FileInfo(path);
            if (fileInf.Exists)
            {
                fileInf.Delete();
                
            }
        }

        /// <summary>
        /// Переместить папку
        /// </summary>
        /// <param name="pathOld"></param>
        /// <param name="pathNew"></param>
        public static void MoveToCatalog(string pathOld, string pathNew)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(pathOld);
            if (dirInfo.Exists && Directory.Exists(pathNew) == false)
            {
                dirInfo.MoveTo(pathNew);
            }

        }

        /// <summary>
        /// Переместить файл
        /// </summary>
        /// <param name="pathOld"></param>
        /// <param name="pathNew"></param>
        public static void MoveToFile(string pathOld, string pathNew)
        {
            FileInfo fileInf = new FileInfo(pathOld);
            if (fileInf.Exists)
            {
                fileInf.MoveTo(pathNew);
            }
        }

        /// <summary>
        /// Переименовать папку
        /// </summary>
        /// <param name="pathOld"></param>
        /// <param name="pathNew"></param>
        public static void RenameCatalog(string pathOld, string pathNew)
        {
            Directory.Move(pathOld, pathNew);
        }

        /// <summary>
        /// Переименовать файл
        /// </summary>
        /// <param name="pathOld"></param>
        /// <param name="pathNew"></param>
        public static void RenameFile(string pathOld, string pathNew)
        {
            File.Move(pathOld, pathNew);
        }
    }
}
