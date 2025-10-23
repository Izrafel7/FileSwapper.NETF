using System;
using System.IO;
using System.Collections.Generic;

namespace FileSwapper
{
    internal class SwapManager
    {
        private string _backupName;
        private const string _oldDir = "old";
        private const string _newDir = "new";
        public SwapManager() { }
        public bool init()
        {
            _backupName = getNameForBackup();
            try
            {
                buildBackupDirectory();
            }
            catch
            {
                return false;
            }

            return true;
        }
        public bool swapFiles(IEnumerable<string> files)
        {
            try
            {
                foreach (var file in files)
                {
                    File.Copy(file, Path.Combine(_backupName, _newDir, file));
                    try
                    {
                        File.Copy(Path.Combine("..", file), Path.Combine(_backupName, _oldDir, file));
                        File.Move(Path.Combine("..", file), Path.Combine("..", String.Concat(file, ".", _backupName)));
                    }
                    catch (FileNotFoundException) { }
                    File.Move(file, Path.Combine("..", file));
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        private string getNameForBackup()
        {
            return DateTime.Now.ToString().Replace(".", "").Replace(" ", "").Replace(":", "");
        }
        void buildBackupDirectory()
        {
            Directory.CreateDirectory(_backupName);

            Directory.CreateDirectory(Path.Combine(_backupName, _oldDir));

            Directory.CreateDirectory(Path.Combine(_backupName, _newDir));

        }
    }
}
