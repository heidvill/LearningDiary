using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Oppimispaivakirja
{
    class Program
    {
        private static string filePath;

        static void Main(string[] args)
        {
            string path = ".";
            string fileName = "Topicfile.txt";
            DirectoryInfo d = new DirectoryInfo(path);
            filePath = d.FullName + "\\" + fileName;

            if (!File.Exists(filePath))
            {
                File.Create(filePath).Dispose();
            }

            TopicHandler topicHandler = new TopicHandler();
            ReadCSV(topicHandler);
            Start(topicHandler);
        }

        private static void ReadCSV(TopicHandler topicHandler)
        {
            StreamReader r = new StreamReader(filePath);
            string row;
            while ((row = r.ReadLine()) != null)
            {
                topicHandler.Topics.Add(StringToTopic(row, ';'));
            }
            r.Dispose();
        }

        private static void Start(TopicHandler tp)
        {
            while (true)
            {
                Console.WriteLine("Tulosta kaikki aiheet, paina a");
                Console.WriteLine("Lisää uusi aihe, paina n");
                Console.WriteLine("Etsi aihetta, paina f");
                Console.WriteLine("Poista aihe, paina r");
                Console.WriteLine("Poistu, paina Enter");

                var key = Console.ReadKey().Key;
                Console.Clear();

                if (key == ConsoleKey.Enter) break;
                if (key == ConsoleKey.A) tp.PrintAllTopics();
                if (key == ConsoleKey.N) AddNewTopic(tp);
                if (key == ConsoleKey.F) FindTopic(tp);
                if (key == ConsoleKey.R) RemoveTopic(tp);
            }
        }

        private static void RemoveTopic(TopicHandler tp)
        {
            while (true)
            {
                Console.WriteLine("Kirjoita poistettavan aiheen otsikko tai id: ");
                Console.WriteLine("Poistu painamalla Enter");
                string input = Console.ReadLine().Trim();
                if (input == "") break;
                tp.RemoveTopic(input);
            }
        }

        private static void AddNewTopic(TopicHandler tp)
        {
            Topic t = tp.AddNewTopic();
            StreamWriter w = new StreamWriter(filePath, true);
            w.WriteLine(t.ToFileForm());
            w.Dispose();

        }

        private static void FindTopic(TopicHandler tp)
        {
            while (true)
            {
                Console.WriteLine("Tee haku. Kirjoita hakusanaksi otsikon osa tai id eli luku");
                Console.WriteLine("Poistu painamalla Enter");
                string input = Console.ReadLine().Trim();
                if (input == "") break;
                tp.FindTopic(input);
            }
            Console.Clear();
        }

        static Topic StringToTopic(string row, char separator)
        {
            string[] parts = row.Split(separator);
            Topic t = null;
            if (parts.Length > 8)
            {
                t = new Topic(int.Parse(parts[0]), parts[1], parts[2], double.Parse(parts[3]), double.Parse(parts[4]), parts[5], DateTime.Parse(parts[6]), bool.Parse(parts[7]), DateTime.Parse(parts[8]));
            }
            else
            {
                t = new Topic(parts[0], parts[1], double.Parse(parts[2]), double.Parse(parts[3]), parts[4], DateTime.Parse(parts[5]), bool.Parse(parts[6]), DateTime.Parse(parts[7]));
            }
            return t;
        }


    }
}
