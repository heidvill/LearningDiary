using System;
using System.Collections.Generic;
using System.Text;

namespace Oppimispaivakirja
{
    class UI
    {
        public TopicDBHandler dbHandler { get; set; } = new TopicDBHandler();
        public void Start()
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
                if (key == ConsoleKey.A) PrintTopicList(dbHandler.GetAllTopics());
                if (key == ConsoleKey.N) AddNewTopic();
                if (key == ConsoleKey.F) FindTopics();
                //if (key == ConsoleKey.R) RemoveTopic(tp);
            }
        }

        public void AddNewTopic()
        {
            Topic t = new Topic();
            AddTitle(t);
            AddDescription(t);
            AddEstimatedTime(t);
            AddTimeSpent(t);
            AddSource(t);
            AddStartTime(t);
            AddInProgress(t);
            AddCompletionDate(t);

            dbHandler.AddTopic(t);
        }

        private void AddTitle(Topic t)
        {
            while (true)
            {
                Console.Write("Anna otsikko: ");
                string title = Console.ReadLine().Trim();
                if (title != "")
                {
                    t.Title = title;
                    break;
                }
                else
                {
                    Console.WriteLine("Otsikko on pakollinen tieto.");
                }
            }
        }

        private void AddDescription(Topic t)
        {

            Console.WriteLine("Anna kuvaus:");
            string description = Console.ReadLine().Trim();
            t.Description = description;
        }

        private void AddEstimatedTime(Topic t)
        {
            while (true)
            {
                Console.Write("Anna arvio aiheen oppimiseen kuluvasta ajasta tunteina, esim. 1,5: ");
                string input = Console.ReadLine().Trim();
                if (input == "") break;
                else if (!double.TryParse(input, out _))
                {
                    Console.WriteLine("Et antanut aikaa oikeassa muodossa, esim. 2 tai 3,25");
                }
                else
                {
                    double estimatedTime = double.Parse(input);
                    t.EstimatedTimeToMaster = estimatedTime;
                    break;
                }
            }
        }

        private void AddTimeSpent(Topic t)
        {
            while (true)
            {
                Console.Write("Anna oppimiseen käytetty aika, esim. 4 tai 2,5: ");
                string input = Console.ReadLine().Trim();
                if (input == "") break;
                else if (!double.TryParse(input, out _))
                {
                    Console.WriteLine("Et antanut aikaa oikeassa muodossa.");
                }
                else
                {
                    double timeSpent = double.Parse(input);
                    t.TimeSpent = timeSpent;
                    break;
                }

            }
        }

        private void AddSource(Topic t)
        {
            Console.Write("Anna opiskelumateriaalin lähde: ");
            string source = Console.ReadLine().Trim();
            if (source != "")
            {
                t.Source = source;
            }

        }

        private void AddStartTime(Topic t)
        {
            while (true)
            {
                Console.Write("Anna aloitusaika pp.kk.vvvv: ");
                string input = Console.ReadLine().Trim();
                if (input == "") break;
                else if (!DateTime.TryParse(input, out _))
                {
                    Console.WriteLine("Et antanut päivää oikeassa muodossa");
                }
                else
                {
                    DateTime start = DateTime.Parse(input);
                    t.StartLearningDate = start;
                    break;
                }
            }
        }

        private void AddInProgress(Topic t)
        {
            Console.Write("Onko aiheen opiskelu kesken k/e: ");
            string input = Console.ReadLine().Trim().ToLower();
            bool inProgress;
            if (input == "k") inProgress = true;
            else if (input == "e") inProgress = false;
            else inProgress = true;

            t.InProgress = inProgress;
        }

        private void AddCompletionDate(Topic t)
        {
            while (true)
            {
                Console.Write("Anna opiskelun päättymisaika pp.kk.vvvv: ");
                string input = Console.ReadLine().Trim();
                if (input == "") break;
                else if (!DateTime.TryParse(input, out _))
                {
                    Console.WriteLine("Et antanut päivää oikeassa muodossa.");
                }
                else
                {
                    DateTime completionDate = DateTime.Parse(input);
                    t.CompletionDate = completionDate;
                    break;
                }
            }
        }

        private void FindTopics()
        {
            Console.WriteLine("Tee haku. Kirjoita hakusana tai id tai poistu painamalla Enter");

            string input = Console.ReadLine().Trim();
            if (input != "") PrintTopicList(FindTopics(input));
            Console.WriteLine("Jatka painamalla mitä tahansa");
            Console.ReadKey();
        }

        public List<Topic> FindTopics(string input)
        {
            int id;
            List<Topic> found = new List<Topic>();
            if (int.TryParse(input, out id))
            {
                found.Add(dbHandler.GetTopicWithId(id));
            }
            else
            {
                found = dbHandler.GetTopicsWithSearchWord(input);
            }

            return found;
        }

        public void PrintTopicList(List<Topic> list)
        {
            if (list.Count == 0)
            {
                Console.WriteLine("Ei löytynyt yhtään");
            }
            else
            {
                foreach (Topic topic in list)
                {
                    Console.WriteLine(topic);
                }
            }
        }
    }
}
