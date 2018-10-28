using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryToJSON
{
    class Folder
    {
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public List<FileInFolder> Files { get; set; } = new List<FileInFolder>();
        public List<Folder> Children { get; set; } = new List<Folder>();

        public Folder(string path)
        {
            this.Name = Path.GetFileName(path);
            this.DateCreated = File.GetCreationTime(path);

            string[] filesArr = Directory.GetFiles(path);
            foreach (var way in filesArr)
                this.Files.Add(new FileInFolder(way));

            string[] childArr = Directory.GetDirectories(path);
            foreach (var way in childArr)
                this.Children.Add(new Folder(way));
        }
    }
    
    class FileInFolder
    {
        public string Name { get; set; }
        public string Size { get; set; }
        public string PathOfFile { get; set; }

        public FileInFolder(string route)
        {
            this.Name = Path.GetFileName(route);
            this.Size = string.Format(new FileInfo(route).Length + " B");
            this.PathOfFile = Path.GetFullPath(route);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string route = @"E:\Projects";

            Console.WriteLine(JsonConvert.SerializeObject(new Folder(route) , Formatting.Indented , new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" }));
        }
    }
}
