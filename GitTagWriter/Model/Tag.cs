namespace GitTagWriter.Model
{
    public class Tag
    {
        public string Id { get; set; }
        public Commit Commit { get; set; }

        public override string ToString()
        {
            return Id;
        }
    }
}