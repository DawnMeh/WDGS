using SQLite;

namespace WDGS.Database
{
    class Answers
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int activityID { get; set; }
        public int answerType { get; set; }
        public int answerID { get; set; }
        public string answer { get; set; }
    }
}
