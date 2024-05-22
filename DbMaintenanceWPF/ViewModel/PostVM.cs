using DbMaintenanceWPF.Infrastructure.Commands;
using DbMaintenanceWPF.Models;
using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Interface;
using System.Collections.Generic;
using System.Windows.Input;

namespace DbMaintenanceWPF.ViewModel
{
    class PostVM : Base.ViewModelBase
    {

        #region Свойства

        readonly PostM Model;
        readonly IUserDialogService UserDialog;
        public IEnumerable<Post> Posts => Model.PublicListPosts;

        #endregion

        #region Команды

        #region AddCommand - Добавление должности

        private ICommand addCommand;
        public ICommand AddCommand => addCommand ??= new RelayCommand(OnAddCommandExecuted, CanAddCommandExecute);
        private static bool CanAddCommandExecute(object p) => p is Post;

        private void OnAddCommandExecuted(object p)
        {
            var post = (Post)p;
            if (!UserDialog.Edit(post, "Добавление должности")) OnPropertyChanged(nameof(Posts));
        }

        #endregion

        #region EditCommand - Редактирование должности

        private ICommand editCommand;
        public ICommand EditCommand => editCommand ??= new RelayCommand(OnEditCommandExecuted, CanEditCommandExecute);
        private static bool CanEditCommandExecute(object p) => p is Post;

        private void OnEditCommandExecuted(object p)
        {
            Post post = (Post)p;
            if (!UserDialog.Edit(post, "Редактирование должности"))
            {
                Model.Update(post);
                OnPropertyChanged(nameof(Posts));
            }
        }

        #endregion

        #region RemoveCommand - Удаление должности

        private ICommand removeCommand;
        public ICommand RemoveCommand => removeCommand ??= new RelayCommand(OnRemoveCommandExecuted, CanRemoveCommandExecute);
        private static bool CanRemoveCommandExecute(object p) => p is Post;

        private void OnRemoveCommandExecuted(object p)
        {
            Post post = (Post)p;
            if (UserDialog.ShowConfirm("Удаление должности", "Удалить выбранную должность?"))
            {
                Model.Remove(post);
                OnPropertyChanged(nameof(Posts));
            }
        }

        #endregion

        #endregion

        public PostVM(PostM model, IUserDialogService userDialog)
        {
            Model = model;
            UserDialog = userDialog;
        }
    }
}
