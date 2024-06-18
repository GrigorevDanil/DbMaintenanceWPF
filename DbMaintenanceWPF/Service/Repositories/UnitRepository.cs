using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Base;
using DbMaintenanceWPF.Service.Interface;

namespace DbMaintenanceWPF.Service.Repositories
{
    public class UnitRepository : RepositoryInMemory<Unit>
    {
        readonly IEditorDatabase EditorDatabase;
        readonly IReaderDatabase ReaderDatabase;
        readonly ICreaterEntity<Unit> CreatorList;

        public UnitRepository(IEditorDatabase editorDatabase, IReaderDatabase readerDatabase, ICreaterEntity<Unit> createrListItems) : base(createrListItems.GetList())
        {
            EditorDatabase = editorDatabase;
            ReaderDatabase = readerDatabase;
            CreatorList = createrListItems;
        }

        protected override long? AddToDatabase(Unit item) => EditorDatabase.OperationOnRecord("INSERT unit VALUES ('NULL', @_shortName ,@_title, @_okei)", [item.ShortName, item.Title, item.OKEI]);
        protected override void UpdateInDatabase(Unit item) => EditorDatabase.OperationOnRecord("UPDATE unit SET ShortName = @_shortName, Title = @_title, OKEI = @_okei WHERE IdUnit = @_idUnit", [item.ShortName, item.Title, item.OKEI, item.Id]);
        protected override void RemoveFromDatabase(Unit item) => EditorDatabase.OperationOnRecord("DELETE FROM unit WHERE IdUnit = @id", [item.Id]);
        protected override void Update(Unit Source, Unit Destination)
        {
            Destination.ShortName = Source.ShortName;
            Destination.Title = Source.Title;
            Destination.OKEI = Source.OKEI;
        }

    }
}
