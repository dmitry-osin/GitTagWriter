using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GitTagWriter.Common;
using GitTagWriter.Contracts;
using GitTagWriter.Model;
using GitTagWriter.Utils;

namespace GitTagWriter.Service
{
    /// <summary>
    /// Класс обертка VCS Git
    /// </summary>
    public class GitService : IGitCommands
    {
        private const string ShowCommand = "show";
        private const string LsRemote = "ls-remote";
        private const string TagsCommand = "--tags";
        private const string DateFormatCommand = "-s --format=%ci";
        private const string CloneCommand = "clone";
        private static string _gitExePath;

        private GitService(string gitExePath)
        {
            _gitExePath = gitExePath;
        }


        /// <summary>
        /// Метод возвращает инициализованный инстанс GitService
        /// </summary>
        /// <param name="gitExePath">Файл git.exe</param>
        /// <returns>GitService</returns>
        public static GitService NewInstance(string gitExePath = "C:\\Program Files\\Git\\bin\\git.exe")
        {
            return new GitService(gitExePath);
        }

        /// <summary>
        /// Метод получает список тегов репозитория
        /// </summary>
        /// <param name="repoPath">Расположение репозитория</param>
        /// <returns>Коллекцию TAG</returns>
        public IEnumerable<Tag> GetTags(string repoPath)
        {
            var output = GitUtils.ExecuteGitCommand(_gitExePath, repoPath, string.Format("{0} {1}", LsRemote, TagsCommand));
            var tags = output.Split("\n".ToCharArray()).Select(s => new Tag()
            {
                Id = s.Split("\t".ToCharArray()).Last().ToString().Replace("refs/tags/", string.Empty),
                Commit = GetCommit(s.Split("\t".ToCharArray()).First().ToString(), repoPath)

            }).Where(tag => tag.Commit.Id != string.Empty && tag.Id != string.Empty).ToList();
            return tags;
        }

        /// <summary>
        /// Метод получает информацию о комите репозитория
        /// </summary>
        /// <param name="commit">Коммит id</param>
        /// <param name="repoPath">Расположение репозитория</param>
        /// <returns>Commit</returns>
        public Commit GetCommit(string commit, string repoPath)
        {
            var output = GitUtils.ExecuteGitCommand(_gitExePath, repoPath,
                string.Format("{0} {1} {2}", ShowCommand, DateFormatCommand, commit));

            return new Commit()
            {
                Id = commit,
                Date = DateTime.Parse(output),
            };
        }

        /// <summary>
        ///  Метод клонирует репозиторий согласно указанному пути
        /// </summary>
        /// <param name="basePath">Ссылка на клонируемый репозиторий</param>
        /// <param name="localPath">Новое расположение репозитория</param>
        public void Clone(string basePath, string localPath)
        {
            if (!Directory.Exists(localPath))
            {
                Directory.CreateDirectory(localPath);
                GitUtils.ExecuteGitCommand(_gitExePath, localPath, string.Format("{0} {1}", CloneCommand, basePath));
            }
            else
            {
                throw new GitException("Cloning error. Directory already exists");
            }
        }


    }
}