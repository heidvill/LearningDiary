using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections;
using System.Data.SqlClient;

namespace Oppimispaivakirja
{
    class TopicHandler
    {
        public List<Topic> Topics { get; set; }

        public TopicHandler()
        {
            Topics = new List<Topic>();
        }

        public TopicHandler(List<Topic> topics)
        {
            Topics = topics;
        }

        public Topic AddNewTopic()
        {
            Topic t = new Topic();
            AddTitle(t);
            AddDescription(t);
            AddEstimatedTime(t);
            AddTimeSpent(t);
            AddSource(t);
            AddStartTime(t);
            AddInProgress(t);
            Add(t);

            Topics.Add(t);
            return t;
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

        public Topic RemoveTopic(string input)
        {
            List<Topic> found = FindTopic(input);
            Topic t = null;
            if (found.Count > 1)
            {
                Console.WriteLine("Löytyi useita.");
                Console.WriteLine("Anna aiheen id:");
                input = Console.ReadLine().Trim();
            }

            return t;
        }

        private Topic RemoveTopic(Topic t)
        {
            Topics.Remove(t);
            return t;
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

        public List<Topic> FindTopic(string input)
        {
            int id;
            List<Topic> found;
            if (int.TryParse(input, out id))
            {
                found = FindWithId(id);
            }
            else
            {
                found = FindByTitle(input);
            }

            PrintQuery(found);
            return found;
        }

        private List<Topic> FindWithId(int id)
        {
            var q = from t in Topics
                    where t.Id == id
                    select t;

            return q.ToList();
        }

        private List<Topic> FindByTitle(string s)
        {
            s = s.ToLower();
            var q = from t in Topics
                    where t.Title.ToLower().Contains(s)
                    select t;

            return q.ToList();
        }

        private void PrintQuery(List<Topic> list)
        {
            if (list.Count == 0)
            {
                Console.WriteLine("Ei löytynyt yhtään aihetta ");
                return;
            }

            foreach (var item in list)
            {
                Console.WriteLine(item);
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

        private void Add(Topic t)
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

        public void PrintAllTopics()
        {
            foreach (Topic topic in Topics)
            {
                Console.WriteLine(topic);
            }
            Console.WriteLine();

            Console.WriteLine("Poistu painamalla mitä tahansa näppäintä");
            Console.ReadKey();

            Console.Clear();
        }
    }
}
