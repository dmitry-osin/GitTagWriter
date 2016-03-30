using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitTagWriter.Service;

namespace GitTagWriter
{
    class Program
    {
        static void Main(string[] args)
        {
            var gitService = GitService.NewInstance();
            var tags = gitService.GetTags(@"C:\Users\dmitr\git\preact");
            var service = new TagSerializeService();
            var xml = service.Serialize(tags);
            service.SaveOnDisk("test.xml", xml);
        }
    }
}
