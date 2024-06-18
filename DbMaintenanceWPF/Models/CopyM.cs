
using DbMaintenanceWPF.Service;
using DbMaintenanceWPF.Service.Interface;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace DbMaintenanceWPF.Models
{
    public class CopyM
    {
        public CopyM(IINIManager iNIManager)
        {
            INIManager = iNIManager;

            listFiles = GetListFiles(INIManager.GetPrivateString("main", "PathCopy"));
            PublicListFiles = new ReadOnlyObservableCollection<FileInfo>(listFiles);
        }

        #region Свойства

        readonly IINIManager INIManager;

        public readonly ReadOnlyObservableCollection<FileInfo> PublicListFiles;
        ObservableCollection<FileInfo> listFiles;

        #endregion

        #region Команды

        ObservableCollection<FileInfo> GetListFiles(string pathCopy) => new([.. new DirectoryInfo(pathCopy).GetFiles("*.sql")]);

        public void UpdateList()
        {
            listFiles.Clear();
            ObservableCollection<FileInfo> tempList = GetListFiles(INIManager.GetPrivateString("main", "PathCopy"));
            foreach (var file in tempList) listFiles.Add(file);
        }
        public void Add(string path) => listFiles.Add(new FileInfo(path));
        public void Remove(FileInfo file)
        {
            listFiles.Remove(file);
            File.Delete(file.FullName);
        }

        #endregion
    }
}
