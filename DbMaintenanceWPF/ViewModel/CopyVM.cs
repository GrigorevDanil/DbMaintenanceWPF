using DbMaintenanceWPF.Infrastructure.Commands;
using DbMaintenanceWPF.Infrastructure.Commands.Factories;
using DbMaintenanceWPF.Infrastructure.Commands.Interface;
using DbMaintenanceWPF.Models;
using DbMaintenanceWPF.Service.Interface;
using DbMaintenanceWPF.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DbMaintenanceWPF.ViewModel
{
    public class CopyVM : ViewModelBase
    {
        public CopyVM(CopyM model, IINIManager iNIManager, IBackupManagerDatabase backupManager, IUserDialogService dialogService, ICommandFactory commandFactory)
        {
            Model = model;
            INIManager = iNIManager;
            BackupManager = backupManager;
            DialogService = dialogService;
            CommandFactory = commandFactory;

            PathCopy = INIManager.GetPrivateString("main", "PathCopy");
        }

        #region Свойства

        readonly CopyM Model;
        readonly IINIManager INIManager;
        readonly IBackupManagerDatabase BackupManager;
        readonly IUserDialogService DialogService;
        readonly ICommandFactory CommandFactory;

        public IEnumerable<FileInfo> Files => Model.PublicListFiles;

        FileInfo selectedFile;
        public FileInfo SelectedFile { get => selectedFile; set => Set(ref selectedFile, value); }

        string pathCopy;
        public string PathCopy { get => pathCopy; set => Set(ref pathCopy, value); }

        #endregion

        #region Команды

        #region AddCommand - Добавление копии

        private ICommand addCommand;
        public ICommand AddCommand => addCommand ??= new RelayCommand(OnAddCommandExecuted, CanAddCommandExecute);
        private static bool CanAddCommandExecute(object p) => true;

        private void OnAddCommandExecuted(object p)
        {
            string fileName = DialogService.ShowInput("Введите название копии");
            if (string.IsNullOrEmpty(fileName)) return;

            for(int i =0; i < Files.Count(); i++)
            {
                if (Files.ElementAt(i).Name == fileName + ".sql")
                {
                    if (DialogService.ShowConfirm("Предупреждение", "Файл с таким именем уже существует. Продолжить?")) Model.Remove(Files.ElementAt(i));
                    else return;
                }
            }

            BackupManager.CreateBackup(fileName, PathCopy);
            Model.Add(PathCopy + '\\' + fileName + ".sql");
            OnPropertyChanged(nameof(Files));
        }

        #endregion

        #region SetCommand - Установка копии

        private ICommand setCommand;
        public ICommand SetCommand => setCommand ??= new RelayCommand(OnSetCommandExecuted, CanSetCommandExecute);
        private static bool CanSetCommandExecute(object p) => p is FileInfo;

        private void OnSetCommandExecuted(object p)
        {
            BackupManager.SetBackup(((FileInfo)p).Name,PathCopy);
            CommandFactory.CreateRestartApplicationCommand().Execute(null);
        }

        #endregion

        #region RemoveCommand - Удаление копии

        private ICommand removeCommand;
        public ICommand RemoveCommand => removeCommand ??= new RelayCommand(OnRemoveCommandExecuted, CanRemoveCommandExecute);
        private static bool CanRemoveCommandExecute(object p) => p is FileInfo;

        private void OnRemoveCommandExecuted(object p)
        {
            if (DialogService.ShowConfirm("Удаление копии","Удалить выбранную копию?"))
            {
                Model.Remove((FileInfo)p);
                OnPropertyChanged(nameof(Files));
            }
        }

        #endregion

        #region SetPathCommand - Установка папки копий

        private ICommand setPathCommand;
        public ICommand SetPathCommand => setPathCommand ??= new RelayCommand(OnSetPathCommandExecuted, CanSetPathCommandExecute);
        private static bool CanSetPathCommandExecute(object p) => true;

        private void OnSetPathCommandExecuted(object p)
        {
            string pathCopy = DialogService.ShowBrowserCatalog();
            if (string.IsNullOrEmpty(pathCopy)) return;
            PathCopy = pathCopy;
            INIManager.WritePrivateString("main", "PathCopy", PathCopy);
            Model.UpdateList();
        }

        #endregion

        #endregion
    }
}
