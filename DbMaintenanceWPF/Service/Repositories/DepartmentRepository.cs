using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Base;
using DbMaintenanceWPF.Service.Interface;

namespace DbMaintenanceWPF.Service.Repositories
{
    public class DepartmentRepository : RepositoryInMemory<Department>
    {
        readonly IEditorDatabase EditorDatabase;
        readonly IReaderDatabase ReaderDatabase;
        readonly ICreaterEntity<Department> CreatorList;

        public DepartmentRepository(IEditorDatabase editorDatabase, IReaderDatabase readerDatabase, ICreaterEntity<Department> createrListItems) : base(createrListItems.GetList())
        {
            EditorDatabase = editorDatabase;
            ReaderDatabase = readerDatabase;
            CreatorList = createrListItems;
        }
        protected override long? AddToDatabase(Department item) => EditorDatabase.OperationOnRecord("INSERT department VALUES ('NULL', @_title)", [item.Title]);
        protected override void UpdateInDatabase(Department item) => EditorDatabase.OperationOnRecord("UPDATE department SET Title = @_title WHERE IdDepartment = @_idDepartment", [item.Title, item.Id]);
        protected override void RemoveFromDatabase(Department item) => EditorDatabase.OperationOnRecord("DELETE FROM department WHERE IdDepartment = @id", [item.Id]);
        protected override void Update(Department Source, Department Destination) => Destination.Title = Source.Title;

    }
}
