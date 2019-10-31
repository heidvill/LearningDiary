using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Oppimispaivakirja
{
    class TopicDBHandler
    {
        LearningDiaryDBContext db = new LearningDiaryDBContext();

        public List<Topic> GetAllTopics()
        {
            var q = from t in db.Topic
                    select t;
            if (q.Count() == 0)
            {
                return null;
            }
            //return new List<Topic>(q.ToArray());
            return q.ToList();
        }

        public Topic GetTopicWithId(int id)
        {
            var q = (from t in db.Topic
                     where t.Id == id
                     select t).FirstOrDefault();

            return q;
        }

        public List<Topic> GetTopicsWithSearchWord(string input)
        {
            var q = from t in db.Topic
                    where t.Title.Contains(input) || t.Description.Contains(input)
                    select t;
            return q.ToList();
        }

        public void AddTopic(Topic t)
        {
            db.Topic.Add(t);
            db.SaveChanges();
        }

        
    }
}
