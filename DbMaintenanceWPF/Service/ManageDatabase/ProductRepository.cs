using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Base;
using DbMaintenanceWPF.Service.Interface;

namespace DbMaintenanceWPF.Service.ManageDatabase
{
    class ProductRepository : RepositoryInMemory<Product>
    {
        readonly IEditorDatabase EditorDatabase;
        readonly IReaderDatabase ReaderDatabase;
        readonly ICreaterListItems<Product> CreatorList;

        public ProductRepository(IEditorDatabase editorDatabase, IReaderDatabase readerDatabase, ICreaterListItems<Product> createrListItems) : base(createrListItems.GetList())
        {
            EditorDatabase = editorDatabase;
            ReaderDatabase = readerDatabase;
            CreatorList = createrListItems;
        }
        protected override long? AddToDatabase(Product item) => EditorDatabase.OperationOnRecord("INSERT product VALUES ('NULL', @_idCategory, @_idBrand, @_idUnit, @_title, @_model, @_count, @_desc)", [item.Category.Id.ToString(), item.Brand.Id.ToString(), item.Unit.Id.ToString(), item.Title, item.Model, item.CountProduct.ToString(), item.Description]);
        protected override void UpdateInDatabase(int id, Product item) => EditorDatabase.OperationOnRecord("UPDATE product SET IdCategory = @_idCategory, IdBrand = @_idBrand, IdUnit = @_idUnit, Title = @_title, Model = @_model, CountProduct = @_count, Description = @_desc WHERE IdProduct = @_idProduct", [item.Category.Id.ToString(), item.Brand.Id.ToString(), item.Unit.Id.ToString(), item.Title, item.Model, item.CountProduct.ToString(), item.Description, id.ToString()]);
        protected override void RemoveFromDatabase(Product item) => EditorDatabase.OperationOnRecord("DELETE FROM product WHERE IdProduct = @id", [item.Id.ToString()]);
        protected override void Update(Product Source, Product Destination)
        {
            Destination.Category = Source.Category;
            Destination.Brand = Source.Brand;
            Destination.Unit = Source.Unit;
            Destination.Title = Source.Title;
            Destination.Model = Source.Model;
            Destination.CountProduct = Source.CountProduct;
            Destination.Description = Source.Description;
        }

    }
}
