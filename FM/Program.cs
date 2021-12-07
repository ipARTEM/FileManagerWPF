using System;
using System.Collections.Generic;
using System.IO;

namespace FM
{
    internal class Program
    {
        private static bool _exit=true;
        private static string _getPath = null;

        static void Main(string[] args)
        {
            do
            {
                Console.WriteLine(_allCommand[0]);
                Console.Write(PathLine.GetPath(_getPath));
                Console.Write(">>");



                Input(Console.ReadLine());
                Console.WriteLine();
            } while (_exit);
        }

        private static void Input(string str)
        {
            str = str.ToLower();
            switch (str)
            {

                case "help":
                    foreach (var i in _allCommand)
                    {
                        Console.WriteLine(i);
                    }
                    break;

                case "q":
                    _exit = false;
                    break;

                case "d":
                    foreach (var i in PathLine.GetDrives())
                    {
                        Console.WriteLine(i);
                    }
                    break;

                case "show":
                    Console.WriteLine(@"Введите адрес каталога в формате 'd:\folder\subfolder'");
                    string str2 = Console.ReadLine();
                    PathLine.GetShow(@$"{str2}");
                    break;

                case "cd":
                    Console.WriteLine("Введите путь к переходу в новую директорию");
                    _getPath = PathLine.GetPath(Console.ReadLine());
                    break;

                case "null":
                    _getPath = null;
                    break;

                case "nc":
                    if (_getPath == null)
                    {
                        Console.WriteLine("Не указана директория, где сохранить новую папку");
                    }
                    else
                    {

                        Console.WriteLine("Введите название папки");
                        _getPath = PathLine.CreateCatalog(_getPath, Console.ReadLine());
                    }
                    break;



                default:
                    break;
            }
        }

        private static List<string> _allCommand = new List<string>()
        {
            "help - выводит справочную информацию о командах",
            "q- выход из программы",
            "d- показать диски",
            "show -показать содержимое диска или папки",
            "'null' - выйти из каталога",
            @"'cd' затем 'C:\Windows'- переход в данный каталог",
            "'nc'- создать новый каталог в данной директории"

        };

        



    }
}
