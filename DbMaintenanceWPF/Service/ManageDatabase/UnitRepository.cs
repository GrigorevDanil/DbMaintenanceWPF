using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Base;
using DbMaintenanceWPF.Service.Interface;

namespace DbMaintenanceWPF.Service.ManageDatabase
{
    class UnitRepository : RepositoryInMemory<Unit>
    {
        readonly IEditorDatabase EditorDatabase;
        readonly IReaderDatabase ReaderDatabase;
        readonly ICreaterListItems<Unit> CreatorList;

        public UnitRepository(IEditorDatabase editorDatabase, IReaderDatabase readerDatabase, ICreaterListItems<Unit> createrListItems) : base(createrListItems.GetList())
        {
            EditorDatabase = editorDatabase;
            ReaderDatabase = readerDatabase;
            CreatorList = createrListItems;
        }

        protected override long? AddToDatabase(Unit item) => EditorDatabase.OperationOnRecord("INSERT unit VALUES ('NULL', @_shortName ,@_title)", [item.ShortName, item.Title]);
        protected override void UpdateInDatabase(int id, Unit item) => EditorDatabase.OperationOnRecord("UPDATE unit SET ShortName = @_shortName, Title = @_title WHERE IdUnit = @_idUnit", [item.ShortName, item.Title, id.ToString()]);
        protected override void RemoveFromDatabase(Unit item) => EditorDatabase.OperationOnRecord("DELETE FROM unit WHERE IdUnit = @id", [item.Id.ToString()]);
        protected override void Update(Unit Source, Unit Destination)
        {
            Destination.ShortName = Source.ShortName;
            Destination.Title = Source.Title;
        }

    }
}
