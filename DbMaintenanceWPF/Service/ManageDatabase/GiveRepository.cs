using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Base;
using DbMaintenanceWPF.Service.Interface;

namespace DbMaintenanceWPF.Service.ManageDatabase
{
    class GiveRepository : RepositoryInMemory<Give>
    {
        readonly IEditorDatabase EditorDatabase;
        readonly IReaderDatabase ReaderDatabase;
        readonly ICreaterListItems<Give> CreatorList;

        public GiveRepository(IEditorDatabase editorDatabase, IReaderDatabase readerDatabase, ICreaterListItems<Give> createrListItems) : base(createrListItems.GetList())
        {
            EditorDatabase = editorDatabase;
            ReaderDatabase = readerDatabase;
            CreatorList = createrListItems;
        }
        protected override long? AddToDatabase(Give item) => EditorDatabase.OperationOnRecord("INSERT give VALUES ('NULL', @_idEmployee, @_dateGive)", [item.Employee.Id.ToString(), item.DateGive.ToString("yyyy-MM-dd")]);
        protected override void UpdateInDatabase(int id, Give item) => EditorDatabase.OperationOnRecord("UPDATE give SET IdEmployee = @_idEmployee, DateGive = @_dateGive WHERE IdGive = @_idGive", [item.Employee.Id.ToString(), id.ToString()]);
        protected override void RemoveFromDatabase(Give item) => EditorDatabase.OperationOnRecord("DELETE FROM give WHERE IdGive = @id", [item.Id.ToString()]);
        protected override void Update(Give Source, Give Destination)
        {
            Destination.Employee = Source.Employee;
            Destination.DateGive = Source.DateGive;

        }

    }
}
