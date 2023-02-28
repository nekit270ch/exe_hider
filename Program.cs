using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Ressy;
using Ressy.HighLevel.Icons;

namespace exe_hider
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine(@"
Использование: exe_hider путь_к_exe расширение
        путь_к_exe     Путь к EXE-файлу.
        расширение     Расширение, под которое необходимо замаскировать файл.
");
                return;
            }

            string name = args[0];
            string ext = args[1];
            string newName = name.Replace(".exe", "_\u202e"+Reverse(ext)+".scr");

            if (!File.Exists(name))
            {
                Console.WriteLine("Ошибка: Файл \""+name+"\" не найден.");
                return;
            }
            if (!File.Exists("ico/"+ext+".ico"))
            {
                Console.WriteLine($"Ошибка: иконка для расширения \"{ext}\" не найдена. Поместите нужную иконку в папку ico и переименуйте ее в {ext}.ico.");
                return;
            }

            File.Move(name, newName);

            SetIcon(newName, "ico/"+ext+".ico");
        }
        static void SetIcon(string exe, string icon)
        {
            var exec = new PortableExecutable(exe);
            try { exec.RemoveIcon(); } catch (Exception ex) { }
            exec.SetIcon(icon);
        }
        static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
