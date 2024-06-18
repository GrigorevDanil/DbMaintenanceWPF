using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Base;
using DbMaintenanceWPF.Service.Interface;

namespace DbMaintenanceWPF.Service.Repositories
{
    public class ProviderRepository : RepositoryInMemory<Provider>
    {
        readonly IEditorDatabase EditorDatabase;
        readonly IReaderDatabase ReaderDatabase;
        readonly ICreaterEntity<Provider> CreatorList;

        public ProviderRepository(IEditorDatabase editorDatabase, IReaderDatabase readerDatabase, ICreaterEntity<Provider> createrListItems) : base(createrListItems.GetList())
        {
            EditorDatabase = editorDatabase;
            ReaderDatabase = readerDatabase;
            CreatorList = createrListItems;
        }
        protected override long? AddToDatabase(Provider item) => EditorDatabase.OperationOnRecord("INSERT provider VALUES ('NULL', @_titleCompany, @_contactName, @_adrs, @_numPhone, @_email)", [item.TitleCompany, item.ContactName, item.Address, item.Address, item.NumPhone, item.Email]);
        protected override void UpdateInDatabase(Provider item) => EditorDatabase.OperationOnRecord("UPDATE provider SET TitleCompany = @_titleCompany, ContactName = @_contactName, Address = @_adrs, NumPhone = @_numPhone, Email = @_email  WHERE IdProvider = @_idProvider", [item.TitleCompany, item.ContactName, item.Address, item.Address, item.NumPhone, item.Email, item.Id]);
        protected override void RemoveFromDatabase(Provider item) => EditorDatabase.OperationOnRecord("DELETE FROM provider WHERE IdProvider = @id", [item.Id]);
        protected override void Update(Provider Source, Provider Destination)
        {
            Destination.TitleCompany = Source.TitleCompany;
            Destination.ContactName = Source.ContactName;
            Destination.Address = Source.Address;
            Destination.NumPhone = Source.NumPhone;
            Destination.Email = Source.Email;
 
        }

    }
}
