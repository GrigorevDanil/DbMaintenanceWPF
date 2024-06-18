using DbMaintenanceWPF.Service.Base;
using DbMaintenanceWPF.Service.Interface;
using System.Windows.Media.Imaging;
using System;
using DbMaintenanceWPF.Models.Items;
using System.Linq;

namespace DbMaintenanceWPF.Service.Repositories
{
    public class UserRepository : RepositoryInMemory<User>
    {
        readonly IImageHelper ImageHelper;
        readonly IEditorDatabase EditorDatabase;
        readonly IReaderDatabase ReaderDatabase;
        readonly ICreaterEntity<User> CreatorList;

        public UserRepository(IEditorDatabase editorDatabase, IReaderDatabase readerDatabase, ICreaterEntity<User> createrListItems, IImageHelper imageHelper) : base(createrListItems.GetList())
        {
            EditorDatabase = editorDatabase;
            ReaderDatabase = readerDatabase;
            CreatorList = createrListItems;
            ImageHelper = imageHelper;
        }

        protected override long? AddToDatabase(User item) => EditorDatabase.OperationOnRecord("INSERT INTO user VALUES (NULL,@img,@_name,@_surn,@log, @passHash, @_salt, @_role, 3, @_lock,@date)", [
            ImageHelper.ImageSourceToBytes(new PngBitmapEncoder(), item.Image),
            item.Name,
            item.Surname,
            item.Login,
            item.PasswordHash,
            item.Salt,
            item.Role,
            item.IsLock ? "1" : "0",
            item.DateLock?.ToString("yyyy-MM-dd HH:mm:ss")]);

        protected override void UpdateInDatabase(User item) => EditorDatabase.OperationOnRecord("UPDATE user SET Image = @img, Name = @_name, Surname = @_surn, Login = @log,PasswordHash = @passHash,Salt = @_salt, Role = @_role, IsLock = @lock, DateLock = @date WHERE IdUser = @id", [
            ImageHelper.ImageSourceToBytes(new PngBitmapEncoder(), item.Image),
            item.Name,
            item.Surname,
            item.Login,
            item.PasswordHash,
            item.Salt,
            item.Role,
            item.IsLock ? "1" : "0",
            item.DateLock?.ToString("yyyy-MM-dd HH:mm:ss"),
            item.Id]);

        protected override void RemoveFromDatabase(User item) => EditorDatabase.OperationOnRecord("DELETE FROM user WHERE IdUser = @id", [item.Id]);
        protected override void Update(User Source, User Destination)
        {
            Destination.Name = Source.Name;
            Destination.Surname = Source.Surname;
            Destination.Login = Source.Login;
            Destination.Role = Source.Role;
            Destination.PasswordHash = Source.PasswordHash;
            Destination.Salt = Source.Salt;
            Destination.IsLock = Source.IsLock;
            Destination.DateLock = Source.DateLock;
        }

        public User GetUserByLogin(string login) => GetAll().FirstOrDefault(user => user.Login == login);





    }
}
