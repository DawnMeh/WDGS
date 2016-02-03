using SQLite;

namespace WDGS.Database
{
    class Trivia
    {
        [PrimaryKey, AutoIncrement]
        public int activityID { get; set; }
        public string triviaOne { get; set; }
        public string triviaTwo { get; set; }
    }
}
