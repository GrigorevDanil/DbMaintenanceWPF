using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Base;
using DbMaintenanceWPF.Service.Interface;

namespace DbMaintenanceWPF.Service.ManageDatabase
{
    class GiveDetailRepository : RepositoryInMemory<GiveDetail>
    {
        readonly IEditorDatabase EditorDatabase;
        readonly IReaderDatabase ReaderDatabase;
        readonly ICreaterListItems<GiveDetail> CreatorList;

        public GiveDetailRepository(IEditorDatabase editorDatabase, IReaderDatabase readerDatabase, ICreaterListItems<GiveDetail> createrListItems) : base(createrListItems.GetList())
        {
            EditorDatabase = editorDatabase;
            ReaderDatabase = readerDatabase;
            CreatorList = createrListItems;
        }
        protected override long? AddToDatabase(GiveDetail item) => EditorDatabase.OperationOnRecord("INSERT givedetail VALUES ('NULL', @_idGive, @_idProduct, @_countProduct)", [item.Give.Id.ToString(), item.Product.Id.ToString(), item.CountProduct.ToString()]);
        protected override void UpdateInDatabase(int id, GiveDetail item) => EditorDatabase.OperationOnRecord("UPDATE givedetail SET IdGive = @_idGive, IdProduct = @_idProduct, CountProduct = @_countProduct  WHERE IdEmployee = @_idEmployee", [item.Give.Id.ToString(), item.Product.Id.ToString(), item.CountProduct.ToString(),id.ToString()]);
        protected override void RemoveFromDatabase(GiveDetail item) => EditorDatabase.OperationOnRecord("DELETE FROM givedetail WHERE IdGiveDetail = @id", [item.Id.ToString()]);
        protected override void Update(GiveDetail Source, GiveDetail Destination)
        {
            Destination.Give = Source.Give;
            Destination.Product = Source.Product;
            Destination.CountProduct = Source.CountProduct;
        }

    }
}
