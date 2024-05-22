using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Base;
using DbMaintenanceWPF.Service.Interface;

namespace DbMaintenanceWPF.Service.ManageDatabase
{
    class BrandRepository : RepositoryInMemory<Brand>
    {
        readonly IEditorDatabase EditorDatabase;
        readonly IReaderDatabase ReaderDatabase;
        readonly ICreaterListItems<Brand> CreatorList;

        public BrandRepository(IEditorDatabase editorDatabase, IReaderDatabase readerDatabase, ICreaterListItems<Brand> createrListItems) : base(createrListItems.GetList())
        {
            EditorDatabase = editorDatabase;
            ReaderDatabase = readerDatabase;
            CreatorList = createrListItems;
        }

        protected override long? AddToDatabase(Brand item) => EditorDatabase.OperationOnRecord("INSERT brand VALUES ('NULL', @_title)", [item.Title]);
        protected override void UpdateInDatabase(int id, Brand item) => EditorDatabase.OperationOnRecord("UPDATE brand SET Title = @_title WHERE IdBrand = @_idBrand", [item.Title, id.ToString()]);
        protected override void RemoveFromDatabase(Brand item) => EditorDatabase.OperationOnRecord("DELETE FROM brand WHERE IdBrand = @id", [item.Id.ToString()]);
        protected override void Update(Brand Source, Brand Destination) => Destination.Title = Source.Title;
    }
}
