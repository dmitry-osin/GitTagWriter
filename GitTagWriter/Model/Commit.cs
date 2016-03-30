using System;

namespace GitTagWriter.Model
{
    public class Commit
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public override string ToString()
        {
            return string.Format("{0} - {1}", Id, Date.ToShortDateString());
        }
    }
}