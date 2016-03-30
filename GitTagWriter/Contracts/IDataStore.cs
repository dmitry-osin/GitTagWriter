namespace GitTagWriter.Contracts
{
    public interface IDataStore
    {
        void SaveOnDisk(string path, string data);
    }
}