using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WDGS.Database
{
    public class WDGSDatabase
    {
        static object locker = new object();

        SQLiteConnection database;

        public WDGSDatabase()
        {
            database = DependencyService.Get<DBConnect>().GetConnection();

            database.CreateTable<Answers>();
        }

        public void insertAnswerForActivity(int activity, int questionType, int answerID, string answer)
        {
            lock (locker)
            {
                if (database.Query<Answers>("SELECT * FROM Answers WHERE activityID=? AND answerType=? AND answerID=?", activity, questionType, answerID).Count() == 0)
                {
                    Answers newAnswer = new Answers();
                    newAnswer.activityID = activity;
                    newAnswer.answerType = questionType;
                    newAnswer.answerID = answerID;
                    newAnswer.answer = answer;

                    database.Insert(newAnswer);
                    return;
                }

                database.Query<Answers>("UPDATE Answers SET answer=? WHERE activityID=? AND answerType=? AND answerID=?", answer, activity, questionType, answerID);
            }
        }

        public String getAnswerForActivity(int activity, int questionType, int answerID)
        {
            try
            {
                lock (locker)
                {
                    return database.Query<Answers>("SELECT answer FROM Answers WHERE activityID=? AND answerType=? AND answerID=?", activity, questionType, answerID).First().answer;
                }
            }
            catch
            {
                return "Click to Answer";
            }
        }
    }
}
