using System.IO;
using GitTagWriter.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GitTagWriter.Tests
{
    [TestClass]
    public class TagSerializeServiceTests
    {
        [TestMethod]
        public void SerializeTest()
        {
            var gitService = GitService.NewInstance();
            var tags = gitService.GetTags(@"C:\Users\dmitr\git\preact");

            var service = new TagSerializeService();
            var xml = service.Serialize(tags);

            Assert.IsTrue(!string.IsNullOrWhiteSpace(xml) && xml.Contains("root") && xml.Contains("name") && xml.Contains("date"));
        }

        [TestMethod]
        public void SaveOnDiskTest()
        {
            var gitService = GitService.NewInstance();
            var tags = gitService.GetTags(@"C:\Users\dmitr\git\preact");

            var service = new TagSerializeService();
            var xml = service.Serialize(tags);
            

            var fileName = "test.xml";

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
                service.SaveOnDisk(fileName, xml);
            }
            else
            {
                service.SaveOnDisk(fileName, xml);
            }

            Assert.IsTrue(File.Exists(fileName));
        }

    }
}