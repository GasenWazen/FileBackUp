using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;


namespace BackUper
{
    public class Paths
    {
        public string[] PathsOfDirs { get; set; }
        public string[] PathOfBackUp { get; set; }
        public string[] DirForTempFiles { get; set; }
    }
    
    class Program
    {

        public static string DirectoryNameCreator(string inputDir)
        {
            return inputDir + "(Copy)";
        }
        public static string NameCreator(string inputName)
        {
            
            return inputName + ".zip";
        }
        
        static async Task Main(string[] args)
        {
            
            




            for (; ; )
            {
                
                Console.WriteLine("Выббирите действие: ");
                Console.WriteLine("1 - архивация");
                string choise = Console.ReadLine();
                switch (choise)
                {
                    case "1":

                        
                            FileStream fs = new FileStream("D://C#//FileBackUp//jsconfig1.json", FileMode.Open);
                        
                        
                        Paths dirs = await JsonSerializer.DeserializeAsync<Paths>(fs);
                        foreach(string stringDir in dirs.PathsOfDirs)
                        {

                            using (StreamWriter logFiles = new StreamWriter(stringDir + "//" + "log.txt", true))
                            {
                                DirectoryInfo dir = new DirectoryInfo(stringDir);
                                FileInfo[] files = dir.GetFiles();
                                string newDirectory = dirs.PathOfBackUp[0];
                                try
                                {
                                    Directory.CreateDirectory(newDirectory);
                                    logFiles.WriteLine("Создана директория для копирования");
                                }
                                catch
                                {
                                    logFiles.WriteLine($"Обнаружена директория для копирования {newDirectory}");
                                }
                                Directory.CreateDirectory(dirs.DirForTempFiles[0]);

                                foreach (FileInfo file in files)
                                {

                                    File.Copy(file.FullName, dirs.DirForTempFiles[0] + $"//{file.Name}", true);
                                    logFiles.WriteLine($"{file.Name} записан");
                                }
                                try
                                {
                                    ZipFile.CreateFromDirectory(dirs.DirForTempFiles[0], dirs.PathOfBackUp[0] +"//"+ $"({dir.Name})BackUp({DateTime.Now.ToShortDateString()}).zip");
                                    logFiles.WriteLine($"Архив записан: {newDirectory + $"BackUp({DateTime.Now}).zip"}");
                                    continue;
                                }
                                catch
                                {
                                    Console.WriteLine("Директория уже есть");
                                    logFiles.WriteLine($"Обнаружен архив: {dirs.PathOfBackUp[0]}" + "//" + $"BackUp({DateTime.Now.ToShortDateString()})");
                                    File.Delete(dirs.PathOfBackUp[0] + "//" + $"({dir.Name})BackUp({DateTime.Now.ToShortDateString()}).zip");
                                    logFiles.WriteLine($"Обнаруженный архив удалён");
                                    ZipFile.CreateFromDirectory(dirs.DirForTempFiles[0], dirs.PathOfBackUp[0]+ "//" + $"({dir.Name})BackUp({DateTime.Now.ToShortDateString()}).zip");
                                    logFiles.WriteLine($"Создан новый архив с именем: {dirs.PathOfBackUp[0] + $".zip"}");
                                }
                                
                                Directory.Delete(dirs.DirForTempFiles[0], true);
                            }
                            


                        }
                        
                        break;
                    default:
                        break;
                }
                
                    
                
            }
           
        }
    }
}


