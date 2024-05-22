using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Base;
using DbMaintenanceWPF.Service.Interface;


namespace DbMaintenanceWPF.Service.ManageDatabase
{
    class CategoryRepository : RepositoryInMemory<Category>
    {
        readonly IEditorDatabase EditorDatabase;
        readonly IReaderDatabase ReaderDatabase;
        readonly ICreaterListItems<Category> CreatorList;

        public CategoryRepository(IEditorDatabase editorDatabase, IReaderDatabase readerDatabase, ICreaterListItems<Category> createrListItems) : base(createrListItems.GetList())
        {
            EditorDatabase = editorDatabase;
            ReaderDatabase = readerDatabase;
            CreatorList = createrListItems;
        }

        protected override long? AddToDatabase(Category item) => EditorDatabase.OperationOnRecord("INSERT category VALUES ('NULL', @_title)", [item.Title]);
        protected override void UpdateInDatabase(int id, Category item) => EditorDatabase.OperationOnRecord("UPDATE category SET Title = @_title WHERE IdCategory = @_idCategory", [item.Title, id.ToString()]);
        protected override void RemoveFromDatabase(Category item) => EditorDatabase.OperationOnRecord("DELETE FROM category WHERE IdCategory = @id", [item.Id.ToString()]);
        protected override void Update(Category Source, Category Destination) => Destination.Title = Source.Title;

    }
}
