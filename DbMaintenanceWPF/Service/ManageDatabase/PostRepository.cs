using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Base;
using DbMaintenanceWPF.Service.Interface;


namespace DbMaintenanceWPF.Service.ManageDatabase
{
    class PostRepository : RepositoryInMemory<Post>
    {
        readonly IEditorDatabase EditorDatabase;
        readonly IReaderDatabase ReaderDatabase;
        readonly ICreaterListItems<Post> CreatorList;

        public PostRepository(IEditorDatabase editorDatabase, IReaderDatabase readerDatabase, ICreaterListItems<Post> createrListItems) : base(createrListItems.GetList())
        {
            EditorDatabase = editorDatabase;
            ReaderDatabase = readerDatabase;
            CreatorList = createrListItems;
        }

        protected override long? AddToDatabase(Post item) => EditorDatabase.OperationOnRecord("INSERT post VALUES ('NULL', @_title)", [item.Title]);
        protected override void UpdateInDatabase(int id, Post item) => EditorDatabase.OperationOnRecord("UPDATE post SET Title = @_title WHERE IdPost = @_idPost", [item.Title, id.ToString()]);
        protected override void RemoveFromDatabase(Post item) => EditorDatabase.OperationOnRecord("DELETE FROM post WHERE IdPost = @id", [item.Id.ToString()]);
        protected override void Update(Post Source, Post Destination) => Destination.Title = Source.Title;
    }
}
