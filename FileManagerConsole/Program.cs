
using FileManagerConsole;
using NLog.Internal;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.InteropServices;

public delegate void OnKey(ConsoleKeyInfo key);
class Program
{
    //const int STD_OUTPUT_HANDLE = -11;

    //[DllImport("kernel32.dll")]
    //static extern IntPtr GetStdHandle(int handle);

    //[DllImport("kernel32.dll", SetLastError = true)]
    //static extern bool SetConsoleDisplayMode(IntPtr ConsoleHandle, uint Flags, IntPtr NewScreenBufferDimensions);

    //[DllImport("user32.dll")]
    //public static extern bool ShowWindow(System.IntPtr hWnd, int cmdShow);


    static void Main(string[] args)
    {
        //var hConsole = GetStdHandle(STD_OUTPUT_HANDLE);
        //SetConsoleDisplayMode(hConsole, 1, IntPtr.Zero);
        //Console.SetBufferSize(1920, 1080);

        Console.SetBufferSize(3840, 2160);

        //Process p = Process.GetCurrentProcess();
        //ShowWindow(p.MainWindowHandle, 3);



        //Console.SetWindowSize(Console.WindowWidth, Console.WindowHeight);


        Console.CursorVisible = false;

        ReadAllSettings();


        FileManager manager = new FileManager();

        UIElements.FirstLine();
        //UIElements.SecondLine();


        manager.Explore();

        if (FileManager.NumLineStr != "18")
        {
            AddUpdateAppSettings(FileManager.l, FileManager.NumLineStr);
        }


    }

    static void NewSettings()
    {
        ReadAllSettings();

        //Console.WriteLine("Имя: ");
        AddUpdateAppSettings(FileManager.l, FileManager.NumLineStr);
        ReadSetting(FileManager.l);
    }

    public static int startLine;

    static void ReadAllSettings()
    {
        try
        {
            var appSettings = ConfigurationManager.AppSettings;

            if (appSettings.Count == 0)
            {
                startLine = 20;
            }
            else
            {
                appSettings.GetValues(0);

                foreach (var key in appSettings.AllKeys)
                {
                    if (key == "l")
                    {
                        try
                        {
                            startLine = Convert.ToInt32(appSettings[key]);

                        }
                        catch (Exception)
                        {

                            throw;
                        }

                    }
                }
            }
        }
        catch (ConfigurationErrorsException)
        {
            Console.WriteLine("Error reading app settings");
        }
    }

    static void ReadSetting(string key)
    {
        try
        {
            var appSettings = ConfigurationManager.AppSettings;
            string result = appSettings[key] ?? "Not Found";

            if (key == "l")
            {
                try
                {
                    startLine = Convert.ToInt32(appSettings[key]);

                }
                catch (Exception)
                {

                    throw;
                }

            }


        }
        catch (ConfigurationErrorsException)
        {
            Console.WriteLine("Error reading app settings");
        }
    }

    static void AddUpdateAppSettings(string key, string value)
    {
        try
        {
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = configFile.AppSettings.Settings;
            if (settings[key] == null)
            {
                settings.Add(key, value);
            }
            else
            {
                settings[key].Value = value;
            }
            configFile.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
        }
        catch (ConfigurationErrorsException)
        {
            Console.WriteLine("Error writing app settings");
        }
    }
}