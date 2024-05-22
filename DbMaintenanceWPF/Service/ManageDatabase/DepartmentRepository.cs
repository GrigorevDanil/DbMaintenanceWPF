using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Base;
using DbMaintenanceWPF.Service.Interface;

namespace DbMaintenanceWPF.Service.ManageDatabase
{
    class DepartmentRepository : RepositoryInMemory<Department>
    {
        readonly IEditorDatabase EditorDatabase;
        readonly IReaderDatabase ReaderDatabase;
        readonly ICreaterListItems<Department> CreatorList;

        public DepartmentRepository(IEditorDatabase editorDatabase, IReaderDatabase readerDatabase, ICreaterListItems<Department> createrListItems) : base(createrListItems.GetList())
        {
            EditorDatabase = editorDatabase;
            ReaderDatabase = readerDatabase;
            CreatorList = createrListItems;
        }
        protected override long? AddToDatabase(Department item) => EditorDatabase.OperationOnRecord("INSERT department VALUES ('NULL', @_title)", [item.Title]);
        protected override void UpdateInDatabase(int id, Department item) => EditorDatabase.OperationOnRecord("UPDATE department SET Title = @_title WHERE IdDepartment = @_idDepartment", [item.Title, id.ToString()]);
        protected override void RemoveFromDatabase(Department item) => EditorDatabase.OperationOnRecord("DELETE FROM department WHERE IdDepartment = @id", [item.Id.ToString()]);
        protected override void Update(Department Source, Department Destination) => Destination.Title = Source.Title;

    }
}
