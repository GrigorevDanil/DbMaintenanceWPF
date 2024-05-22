using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Base;
using DbMaintenanceWPF.Service.Interface;

namespace DbMaintenanceWPF.Service.ManageDatabase
{
    class EmployeeRepository : RepositoryInMemory<Employee>
    {
        readonly IEditorDatabase EditorDatabase;
        readonly IReaderDatabase ReaderDatabase;
        readonly ICreaterListItems<Employee> CreatorList;

        public EmployeeRepository(IEditorDatabase editorDatabase, IReaderDatabase readerDatabase, ICreaterListItems<Employee> createrListItems) : base(createrListItems.GetList())
        {
            EditorDatabase = editorDatabase;
            ReaderDatabase = readerDatabase;
            CreatorList = createrListItems;
        }
        protected override long? AddToDatabase(Employee item) => EditorDatabase.OperationOnRecord("INSERT employee VALUES ('NULL', @_idDepartment, @_idPost, @_surname, @_name, @_lastname, @_email, @_numPhone, @_adrs)", [item.Department.Id.ToString(), item.Post.Id.ToString(), item.Surname, item.Name, item.Lastname, item.Email, item.NumPhone, item.Address]);
        protected override void UpdateInDatabase(int id, Employee item) => EditorDatabase.OperationOnRecord("UPDATE employee SET IdDepartment = @_idDepartment, IdPost = @_idPost, Surname = @_surname, Name = @_name, Lastname = @_lastname, Email = @_email, NumPhone = @_numPhone, Address = @_adrs  WHERE IdEmployee = @_idEmployee", [item.Department.Id.ToString(), item.Post.Id.ToString(), item.Surname, item.Name, item.Lastname, item.Email, item.NumPhone, item.Address]);
        protected override void RemoveFromDatabase(Employee item) => EditorDatabase.OperationOnRecord("DELETE FROM employee WHERE IdEmployee = @id", [item.Id.ToString()]);
        protected override void Update(Employee Source, Employee Destination)
        {
            Destination.Department = Source.Department;
            Destination.Post = Source.Post;
            Destination.Surname = Source.Surname;
            Destination.Name = Source.Name;
            Destination.Lastname = Source.Lastname;
            Destination.Email = Source.Email;
            Destination.NumPhone = Source.NumPhone;
            Destination.Address = Source.Address;
        }


    }
}
