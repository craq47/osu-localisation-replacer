using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;

class Paths
{
    public static string ApplicationFolder = AppDomain.CurrentDomain.BaseDirectory;
    public static string LocalisationFolder = Path.Combine(ApplicationFolder, "Localisation");
    public static string CustomLocalisationFolder = Path.Combine(LocalisationFolder, "custom");
    public static string OsuApplicationFile = Path.Combine(ApplicationFolder, "osu!.exe");
}

class Program
{
    static void Main()
    {
        if (!Directory.Exists(Paths.CustomLocalisationFolder))
        {
            Directory.CreateDirectory(Paths.CustomLocalisationFolder);
            MessageBox.Show($"Custom localisation folder has been created in:\n '{Paths.CustomLocalisationFolder}'\n\nPlease, add replacement files...");
            return;
        }

        if (!File.Exists(Paths.OsuApplicationFile))
        {
            MessageBox.Show("Executable file not found.");
            return;
        }

        // Check the size of all txt files in the 'Localisation' folder when running the code
        // Перевірка розміру усіх txt файлів у теці "Localisation" під час запуску коду
        var localisationFiles = Directory.GetFiles(Paths.LocalisationFolder, "*.txt");
        var initialTxtFileSizes = Directories.GetFileSizesInFolder(localisationFiles);
        //
        // Start a third-party program
        // Запуск сторонньої програми
        _ = Files.Osu.LaunchApplicationAsync(Paths.OsuApplicationFile);
        //
        // Checking the file sizes in the 'Localization\custom' folder and updating them every 2 seconds
        // Перевірка розмірів файлів у теці "Localization\custom" і їх оновлення кожні 2 секунди
        Files.Localisation.ReplaceWithUpdates(Paths.LocalisationFolder, Paths.CustomLocalisationFolder,
            initialTxtFileSizes, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(10));
    }

    class Files
    {
        public class Osu
        {
            public static string ApplicationFile = "osu!.exe";

            public static void LaunchApplication(string appPath)
            {
                Process.Start(appPath);
            }

            public static async Task LaunchApplicationAsync(string appPath)
            {
                await Task.Run(() => Process.Start(appPath));
            }
        }

        public class Localisation
        {
            public static void Replace(string localizationFolder, string customLocalizationFolder, long[] originalFileSizes)
            {
                string[] customLocalizationFiles = Directory.GetFiles(customLocalizationFolder, "*.txt");

                for (int i = 0; i < customLocalizationFiles.Length; i++)
                {
                    long customFileSize = GetFileSize(customLocalizationFiles[i]);
                    //
                    // Check if the file in the 'Localization' folder is larger
                    // Перевірка, чи файл у текі "Localization" має більший розмір
                    if (customFileSize > originalFileSizes[i])
                    {
                        // Replace file in 'Localization' with file from 'Localization\\custom'
                        // Заміна файлу у "Localization" файлом із "Localization\\custom"
                        string originalFilePath = Path.Combine(localizationFolder, Path.GetFileName(customLocalizationFiles[i]));
                        //
                        if (File.Exists(originalFilePath))
                        {
                            File.Copy(customLocalizationFiles[i], originalFilePath, true);
                            //Console.WriteLine($"File '{Path.GetFileName(customLocalizationFiles[i])}' replaced in 'Localization' folder.");
                        }
                    }
                }
            }

            public static void ReplaceWithUpdates(string localizationFolder, string customLocalizationFolder, long[] originalFileSizes, TimeSpan dueTime, TimeSpan period)
            {
                // Start the timer to call the method every 2 seconds
                // Запускаємо таймер для виклику методу кожні 2 секунди
                System.Threading.Timer timer = new System.Threading.Timer(Replace, null, TimeSpan.Zero, dueTime);
                //
                // Wait 1 minute before ending the program
                // Чекаємо 1 хвилину перед завершенням програми
                Thread.Sleep(period);
                //
                // Stop the timer before the program ends
                // Зупиняємо таймер перед завершенням програми
                timer.Dispose();
                //
                void Replace(object state)
                {
                    // Checking the file sizes in the 'Localization\custom' folder and updating them
                    // Перевірка розмірів файлів у теці "Localization\custom" і їх оновлення
                    Files.Localisation.Replace(localizationFolder, customLocalizationFolder, originalFileSizes);
                    //
                    // Якщо потрібно зупинити таймер після 1 хвилини
                    //timer.Dispose();
                }
            }
        }

        static bool IsChanged(Dictionary<string, long> initialSizes, Dictionary<string, long> newSizes)
        {
            return initialSizes.Count == newSizes.Count &&
                   initialSizes.All(entry => newSizes.ContainsKey(entry.Key) && newSizes[entry.Key] != entry.Value);
        }

        public static long GetFileSize(string filePath)
        {
            if (File.Exists(filePath))
            {
                FileInfo fileInfo = new FileInfo(filePath);
                return fileInfo.Length;
            }

            return -1;
        }
    }

    class Directories
    {
        public static long[] GetFileSizesInFolder(string[] filePaths)
        {
            long[] fileSizes = new long[filePaths.Length];

            for (int i = 0; i < filePaths.Length; i++)
            {
                fileSizes[i] = Files.GetFileSize(filePaths[i]);
            }

            return fileSizes;
        }
    }
}
