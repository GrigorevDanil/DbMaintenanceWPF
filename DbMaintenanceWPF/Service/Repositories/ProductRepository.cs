using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Base;
using DbMaintenanceWPF.Service.Interface;

namespace DbMaintenanceWPF.Service.Repositories
{
    public class ProductRepository : RepositoryInMemory<Product>
    {
        readonly IEditorDatabase EditorDatabase;
        readonly IReaderDatabase ReaderDatabase;
        readonly ICreaterEntity<Product> CreatorList;

        public ProductRepository(IEditorDatabase editorDatabase, IReaderDatabase readerDatabase, ICreaterEntity<Product> createrListItems) : base(createrListItems.GetList())
        {
            EditorDatabase = editorDatabase;
            ReaderDatabase = readerDatabase;
            CreatorList = createrListItems;
        }
        protected override long? AddToDatabase(Product item) => EditorDatabase.OperationOnRecord("INSERT product VALUES ('NULL', @_idCategory, @_idBrand, @_idUnit, @_title, @_model, @_count, @_price,@_OKPD, @_desc)", [item.Category.Id, item.Brand.Id, item.Unit.Id, item.Title, item.Model, item.CountProduct,item.Price,item.OKPD, item.Description]);
        protected override void UpdateInDatabase(Product item) => EditorDatabase.OperationOnRecord("UPDATE product SET IdCategory = @_idCategory, IdBrand = @_idBrand, IdUnit = @_idUnit, Title = @_title, Model = @_model, CountProduct = @_count, Price = @_price, OKPD = @_OKPD, Description = @_desc WHERE IdProduct = @_idProduct", [item.Category.Id, item.Brand.Id, item.Unit.Id, item.Title, item.Model, item.CountProduct, item.Price,item.OKPD, item.Description, item.Id]);
        protected override void RemoveFromDatabase(Product item) => EditorDatabase.OperationOnRecord("DELETE FROM product WHERE IdProduct = @id", [item.Id]);
        protected override void Update(Product Source, Product Destination)
        {
            Destination.Category = Source.Category;
            Destination.Brand = Source.Brand;
            Destination.Unit = Source.Unit;
            Destination.Title = Source.Title;
            Destination.Model = Source.Model;
            Destination.CountProduct = Source.CountProduct;
            Destination.Price = Source.Price;
            Destination.OKPD = Source.OKPD;
            Destination.Description = Source.Description;
        }

    }
}
