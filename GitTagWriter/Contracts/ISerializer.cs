using System.Collections.Generic;

namespace GitTagWriter.Contracts
{
    public interface ISerializer<T>
    {
        string Serialize(IEnumerable<T> list);
    }
}