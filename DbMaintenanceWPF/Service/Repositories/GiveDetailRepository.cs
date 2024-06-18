using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Base;
using DbMaintenanceWPF.Service.Interface;

namespace DbMaintenanceWPF.Service.Repositories
{
    public class GiveDetailRepository : RepositoryInMemory<GiveDetail>
    {
        readonly IEditorDatabase EditorDatabase;
        readonly IReaderDatabase ReaderDatabase;
        readonly ICreaterEntity<GiveDetail> CreatorList;

        public GiveDetailRepository(IEditorDatabase editorDatabase, IReaderDatabase readerDatabase, ICreaterEntity<GiveDetail> createrListItems) : base(createrListItems.GetList())
        {
            EditorDatabase = editorDatabase;
            ReaderDatabase = readerDatabase;
            CreatorList = createrListItems;
        }
        protected override long? AddToDatabase(GiveDetail item) => EditorDatabase.OperationOnRecord("INSERT givedetail VALUES ('NULL', @_idGive, @_idProduct, @_countProduct)", [item.Give.Id, item.Product.Id, item.CountProduct]);
        protected override void UpdateInDatabase(GiveDetail item) => EditorDatabase.OperationOnRecord("UPDATE givedetail SET IdGive = @_idGive, IdProduct = @_idProduct, CountProduct = @_countProduct  WHERE IdGiveDetail = @_idGiveDetail", [item.Give.Id, item.Product.Id, item.CountProduct, item.Id]);
        protected override void RemoveFromDatabase(GiveDetail item) => EditorDatabase.OperationOnRecord("DELETE FROM givedetail WHERE IdGiveDetail = @id", [item.Id.ToString()]);
        protected override void Update(GiveDetail Source, GiveDetail Destination)
        {
            Destination.Give = Source.Give;
            Destination.Product = Source.Product;
            Destination.CountProduct = Source.CountProduct;
        }

    }
}
