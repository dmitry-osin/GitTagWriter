using System.Linq;
using GitTagWriter.Common;
using GitTagWriter.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GitTagWriter.Tests
{
    [TestClass]
    public class GitServiceTests
    {
        [TestMethod]
        public void GetTagsTest()
        {
            var gitService = GitService.NewInstance();
            var tags = gitService.GetTags(@"C:\Users\dmitr\git\preact");

            Assert.IsTrue(tags.Any());
        }

        [TestMethod]
        public void GetCommitTest()
        {
            var gitService = GitService.NewInstance();
            var commit = gitService.GetCommit("4.5.1", @"C:\Users\dmitr\git\preact");

            Assert.IsNotNull(commit);
        }

        [TestMethod]
        [ExpectedException(typeof(GitException))]
        public void CloneTest()
        {
            var gitService = GitService.NewInstance();
            gitService.Clone("https://github.com/dmitry-osin/MyShows", @"preact");
            gitService.Clone("https://github.com/dmitry-osin/MyShows", @"preact");
        }
    }
}
