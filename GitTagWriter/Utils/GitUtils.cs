using System.Diagnostics;

namespace GitTagWriter.Utils
{
    public static class GitUtils
    {
        /// <summary>
        /// Метод выполняет команду через git
        /// </summary>
        /// <param name="gitExePath">Путь до git.exe</param>
        /// <param name="gitRepositoryPath">Путь к репозиторию</param>
        /// <param name="gitCommand">Команды git</param>
        /// <returns></returns>
        public static string ExecuteGitCommand(string gitExePath, string gitRepositoryPath, string gitCommand)
        {
            var git = new Process
            {
                StartInfo =
                {
                    FileName = gitExePath,
                    Arguments = string.Format(gitCommand),
                    UseShellExecute = false,
                    RedirectStandardError = true
                }
            };
            git.StartInfo.WorkingDirectory = gitRepositoryPath;
            git.StartInfo.UseShellExecute = false;
            git.StartInfo.CreateNoWindow = true;
            git.StartInfo.RedirectStandardOutput = true;
            git.StartInfo.RedirectStandardError = true;
            git.Start();

            var response = git.StandardOutput.ReadToEnd();
            var err = git.StandardError.ReadToEnd();

            return response;
        }

    }
}