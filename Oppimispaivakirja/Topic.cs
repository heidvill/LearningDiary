using System;
using System.Collections.Generic;
using System.Text;

namespace Oppimispaivakirja
{
    class Topic
    {
        private readonly int id;
        private static int nextFreeId;
        public int Id { get; private set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double EstimatedTimeToMaster { get; set; }
        public double TimeSpent { get; set; } = 0;
        public string Source { get; set; }
        public DateTime StartLearningDate { get; set; }
        public bool InProgress { get; set; } = false;
        public DateTime CompletionDate { get; set; }

        public Topic()
        {
            Id = nextFreeId++;
        }

        public Topic(string title) : base()
        {
            Title = title;
        }

        public Topic(string title, string description, double estimatedTimeToMaster, double timeSpent, string source, DateTime startLearningDate, bool inProgress, DateTime completionDate) : this(title)
        {
            EstimatedTimeToMaster = estimatedTimeToMaster;
            TimeSpent = timeSpent;
            Source = source;
            StartLearningDate = startLearningDate;
            InProgress = inProgress;
            CompletionDate = completionDate;

        }

        public Topic(int id, string title, string description, double estimatedTimeToMaster, double timeSpent, string source, DateTime startLearningDate, bool inProgress, DateTime completionDate)
        {
            Id = id;
            nextFreeId = id + 1;
            Title = title;
            Description = description;
            EstimatedTimeToMaster = estimatedTimeToMaster;
            TimeSpent = timeSpent;
            Source = source;
            StartLearningDate = startLearningDate;
            InProgress = inProgress;
            CompletionDate = completionDate;
        }

        public string ToFileForm()
        {
            return $"{Id};{Title};{Description};{EstimatedTimeToMaster};{TimeSpent};{Source};{StartLearningDate};{InProgress};{CompletionDate}";
        }

        public override string ToString()
        {
            string nl = Environment.NewLine;
            StringBuilder s = new StringBuilder();
            s.Append($"{Id} {Title}{nl}");
            s.Append($"\t{Description}");

            return s.ToString();
        }
    }
}
