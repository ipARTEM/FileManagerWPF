using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public static void CopyFile(string toCopy, string newFile)
        {
            File.Copy(toCopy, newFile);
        }

        public static void CopyCatalog(string toCopyCatalog, string newCopyCatalog)
        {
            DirectoryInfo diSource = new DirectoryInfo(toCopyCatalog);
            DirectoryInfo diTarget = new DirectoryInfo(newCopyCatalog);

            if (Directory.Exists(diTarget.FullName) == false)
            {
                Directory.CreateDirectory(diTarget.FullName);
            }

            // Копируем все файлы в новую директорию
            foreach (FileInfo fi in diSource.GetFiles())
            {
                fi.CopyTo(Path.Combine(diTarget.ToString(), fi.Name), true);
            }

            // Копируем рекурсивно все поддиректории
            foreach (DirectoryInfo diSourceSubDir in diSource.GetDirectories())
            {
                  diTarget.CreateSubdirectory(diSourceSubDir.Name);
            }
        }

        /// <summary>
        /// Поиск файлов по маске
        /// </summary>
        /// <param name="getCatalog"></param>
        /// <param name="getFiles"></param>
        public static void GetFullFiles(string getCatalog, string getFiles)
        {
            string[] files= Directory.GetFiles(getCatalog, getFiles);

            Console.WriteLine("Всего файлов: {0}.", files.Length);
            foreach (string f in files)
            {
                Console.WriteLine(f);
            }
        }

        /// <summary>
        /// Поиск папок по маске
        /// </summary>
        public static void GetFullDirectories(string getPath, string pathCatalog)
        {
            string[] dirs = Directory.GetDirectories(getPath, pathCatalog);
            Console.WriteLine("Всего каталогов: {0}", dirs.Length);

            foreach (string d in dirs)
            {
                Console.WriteLine(d);
            }
        }

        /// <summary>
        /// Вывод данных о папке
        /// </summary>
        /// <param name="infoCatalog"></param>
        public static void InfoCatalog(string infoCatalog)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(infoCatalog);

            Console.WriteLine($"Название каталога: {dirInfo.Name}");
            Console.WriteLine($"Полное название каталога: {dirInfo.FullName}");
            Console.WriteLine($"Время создания каталога: {dirInfo.CreationTime}");
            Console.WriteLine($"Корневой каталог: {dirInfo.Root}");
        }

        /// <summary>
        /// Вывод данных о файле
        /// </summary>
        /// <param name="infoFile"></param>
        public static void InfoFile(string infoFile)
        {
            FileInfo fileInf = new FileInfo(infoFile);
            if (fileInf.Exists)
            {
                Console.WriteLine("Имя файла: {0}", fileInf.Name);
                Console.WriteLine("Время создания: {0}", fileInf.CreationTime);
                Console.WriteLine("Размер: {0}", fileInf.Length);
            }

        }

        public static void TextFileStatistics(string fileStatict)
        {
            try
            {
               
                
                using (StreamReader sr = new StreamReader(fileStatict))
                {
                    Console.WriteLine( sr.ReadToEnd());     //весь текст
                    string word="";
                    string[] words;

                    while (sr.EndOfStream != true)
                    {
                        word += sr.ReadLine();
                    }
                     words = word.Split(' ');
                    Console.WriteLine("Количество слов: "+ words.Length);
                }

                using (StreamReader sr = new StreamReader(fileStatict))
                {
                    string line;
                    int i = 0;
                    while ((line = sr.ReadLine()) != null) //читаем по одной линии(строке) пока не вычитаем все из потока (пока не достигнем конца файла)
                    {
                        i++;
                        //Console.WriteLine(line);
                    }
                    Console.WriteLine("Количество строк: " + i.ToString());
                }

                using (StreamReader sr = new StreamReader(fileStatict))
                {
                    string line;
                    int i = 0;
                    while ((line = sr.ReadLine()) != null) 
                    {
                        if(line[0]=='\t')
                        {
                            i++;
                        }
                    }
                    Console.WriteLine("Количество абзацев: " + i.ToString());
                }

                using (StreamReader sr = new StreamReader(fileStatict))
                {
                    string s = sr.ReadToEnd();
                    char[] ch = s.ToCharArray();
                    
                    Console.WriteLine("Количество символов с пробелами: " + ch.Length.ToString());
                }

                using (StreamReader sr = new StreamReader(fileStatict))
                {
                    string st=sr.ReadToEnd();
                    string[] SMass;
                    int sumStr=0;
                    
                    SMass = st.Split(' ','\n');

                    foreach (var i in SMass)
                    {
                        if (i==""||i=="\r")
                        {
                            continue;
                        }
                        sumStr++;
                    }
                    Console.WriteLine("Количество слов без пробелов: " + sumStr);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void AttributeFile(string pathFile, string attrib)
        {
            FileInfo fileInfo = new FileInfo(pathFile);
            
            bool at=false;
            if (attrib == "t")
            {
                at=true;
            }
            
            fileInfo.IsReadOnly = at;

            Console.WriteLine("Атрибут IsReadOnly: "+ fileInfo.IsReadOnly);
        }


    }
}
