using System.Collections.Generic;
using GitTagWriter.Model;

namespace GitTagWriter.Contracts
{
    public interface IGitCommands
    {
        IEnumerable<Tag> GetTags(string repoPath);
        Commit GetCommit(string commit, string repoPath);
        void Clone(string remotePath, string localPath);
    }
}