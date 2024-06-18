using DbMaintenanceWPF.Infrastructure.Commands;
using DbMaintenanceWPF.Models;
using DbMaintenanceWPF.Models.ItemModels;
using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Interface;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace DbMaintenanceWPF.ViewModel
{
    public class PostVM(PostM model, IUserDialogService userDialog, IStorageViewModel storageViewModel) : Base.ViewModelBase
    {

        #region Свойства

        readonly PostM Model = model;
        readonly IUserDialogService UserDialog = userDialog;
        public IEnumerable<Post> Posts => Model.PublicListPosts;

        public Visibility VisibleComponent
        {
            get
            {
                var user = (storageViewModel.GetViewModel(nameof(MainVM)) as MainVM)?.CurrentUser as User;
                return user?.Role == "Пользователь" ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        #endregion

        #region Команды

        public void UpdateList() => OnPropertyChanged(nameof(Posts));

        #region AddCommand - Добавление должности

        private ICommand addCommand;
        public ICommand AddCommand => addCommand ??= new RelayCommand(OnAddCommandExecuted, CanAddCommandExecute);
        private static bool CanAddCommandExecute(object p) => true;

        private void OnAddCommandExecuted(object p)
        {
            var post = new Post();
            if (UserDialog.Edit(post, "Добавление должности"))
            {
                Model.Create(post);
                OnPropertyChanged(nameof(Posts));
            }
        }

        #endregion

        #region EditCommand - Редактирование должности

        private ICommand editCommand;
        public ICommand EditCommand => editCommand ??= new RelayCommand(OnEditCommandExecuted, CanEditCommandExecute);
        private static bool CanEditCommandExecute(object p) => p is Post;

        private void OnEditCommandExecuted(object p)
        {
            Post post = (Post)p;
            if (UserDialog.Edit(post, "Редактирование должности"))
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

        #region MultiplyRemoveCommand - Множественное удаление должностей

        private ICommand multiplyRemoveCommand;
        public ICommand MultiplyRemoveCommand => multiplyRemoveCommand ??= new RelayCommand(OnMultiplyRemoveCommandExecuted, CanMultiplyRemoveCommandExecute);
        private static bool CanMultiplyRemoveCommandExecute(object p) => true;

        private void OnMultiplyRemoveCommandExecuted(object p)
        {
            if (UserDialog.ShowConfirm("Удаление должностей", "Удалить выбранные должности?"))
            {
                foreach (Post post in Posts) if (post.IsSelected) Model.Remove(post);
                OnPropertyChanged(nameof(Posts));
            }
        }

        #endregion
        #endregion
    }
}
