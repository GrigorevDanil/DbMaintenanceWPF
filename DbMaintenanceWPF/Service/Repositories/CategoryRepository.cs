using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Base;
using DbMaintenanceWPF.Service.Interface;


namespace DbMaintenanceWPF.Service.Repositories
{
    public class CategoryRepository : RepositoryInMemory<Category>
    {
        readonly IEditorDatabase EditorDatabase;
        readonly IReaderDatabase ReaderDatabase;
        readonly ICreaterEntity<Category> CreatorList;

        public CategoryRepository(IEditorDatabase editorDatabase, IReaderDatabase readerDatabase, ICreaterEntity<Category> createrListItems) : base(createrListItems.GetList())
        {
            EditorDatabase = editorDatabase;
            ReaderDatabase = readerDatabase;
            CreatorList = createrListItems;
        }

        protected override long? AddToDatabase(Category item) => EditorDatabase.OperationOnRecord("INSERT category VALUES ('NULL', @_title)", [item.Title]);
        protected override void UpdateInDatabase(Category item) => EditorDatabase.OperationOnRecord("UPDATE category SET Title = @_title WHERE IdCategory = @_idCategory", [item.Title, item.Id]);
        protected override void RemoveFromDatabase(Category item) => EditorDatabase.OperationOnRecord("DELETE FROM category WHERE IdCategory = @id", [item.Id]);
        protected override void Update(Category Source, Category Destination) => Destination.Title = Source.Title;

    }
}
