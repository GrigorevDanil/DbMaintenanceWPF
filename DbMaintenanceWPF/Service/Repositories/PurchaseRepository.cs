using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Base;
using DbMaintenanceWPF.Service.Interface;

namespace DbMaintenanceWPF.Service.Repositories
{
    public class PurchaseRepository : RepositoryInMemory<Purchase>
    {
        readonly IEditorDatabase EditorDatabase;
        readonly IReaderDatabase ReaderDatabase;
        readonly ICreaterEntity<Purchase> CreatorList;

        public PurchaseRepository(IEditorDatabase editorDatabase, IReaderDatabase readerDatabase, ICreaterEntity<Purchase> createrListItems) : base(createrListItems.GetList())
        {
            EditorDatabase = editorDatabase;
            ReaderDatabase = readerDatabase;
            CreatorList = createrListItems;
        }
        protected override long? AddToDatabase(Purchase item) => EditorDatabase.OperationOnRecord("INSERT purchase VALUES ('NULL', @_idProduct, @_idProvider, @_dateProvide, @_count, @_price)", [item.Product.Id, item.Provider.Id, item.DateProvide?.ToString("yyyy-MM-dd"), item.CountProduct, item.Price]);
        protected override void UpdateInDatabase(Purchase item) => EditorDatabase.OperationOnRecord("UPDATE purchase SET IdProduct = @_idProduct, IdProvider = @_idProvider, DateProvide = @_dateProvide, CountProduct = @_count, Price = @_price WHERE IdProduct = @_idPurchase", [item.Product.Id, item.Provider.Id, item.DateProvide?.ToString("yyyy-MM-dd"), item.CountProduct, item.Price, item.Id, item.Id]);
        protected override void RemoveFromDatabase(Purchase item) => EditorDatabase.OperationOnRecord("DELETE FROM purchase WHERE IdPurchase = @id", [item.Id]);
        protected override void Update(Purchase Source, Purchase Destination)
        {
            Destination.Product = Source.Product;
            Destination.Provider = Source.Provider;
            Destination.DateProvide = Source.DateProvide;
            Destination.CountProduct = Source.CountProduct;
            Destination.Price = Source.Price;
        }

    }
}
