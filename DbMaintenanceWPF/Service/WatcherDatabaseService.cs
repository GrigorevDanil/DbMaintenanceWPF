using DbMaintenanceWPF.Items.Interfaces;
using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Factories;
using DbMaintenanceWPF.Service.Interface;
using DbMaintenanceWPF.Service.Interface.Factories;
using DbMaintenanceWPF.Service.Repositories;
using System;
using System.Data;

namespace DbMaintenanceWPF.Service
{
    public class WatcherDatabaseService(IReaderDatabase readerDatabase, IEditorDatabase editorDatabase) : IWatcherDatabase
    {
        readonly IRepositoryFactory repositoryFactory = new RepositoryFactory();
        readonly ICreaterFactory createrFactory = new CreaterFactory();

        public void SynchronizeRecordings()
        {
            editorDatabase.OperationOnRecord("CALL CleanupLogTable()", []);

            DataTable tableLog = readerDatabase.OperationSelect("SELECT * FROM `databaselogs` WHERE `DateOperation` > NOW() - INTERVAL 60 SECOND ORDER BY `DateOperation` ASC", []);

            foreach (DataRow row in tableLog.Rows) ProcessRow(row, int.Parse(row["IdRecord"].ToString()));

        }

        private void ProcessRow(DataRow row, int idRecord)
        {
            switch (row["TableName"].ToString())
            {
                case "brand":  ProcessBrand(row, idRecord); break;
                case "category": ProcessCategory(row, idRecord); break;
                case "department": ProcessDepartment(row, idRecord);  break;
                case "employee": ProcessEmployee(row, idRecord); break;
                case "givedetail": ProcessGiveDetail(row, idRecord); break;
                case "give": ProcessGive(row, idRecord); break;
                case "post": ProcessPost(row, idRecord); break;
                case "product": ProcessProduct(row, idRecord); break;
                case "provider": ProcessProvider(row, idRecord); break;
                case "purchase": ProcessPurchase(row, idRecord); break;
                case "unit": ProcessUnit(row, idRecord); break;
                case "user": ProcessUser(row, idRecord); break;
            }
        }

        private void ProcessOperation<T>(string operation, IRepository<T> repository, T entity, int idRecord) where T : IEntity
        {
            switch (operation.ToUpper())
            {
                case "INSERT":
                    repository.AddOnlyList(entity);
                    break;
                case "UPDATE":
                    repository.UpdateOnlyList(entity.Id, entity);
                    break;
                case "DELETE":
                    repository.RemoveOnlyList(idRecord);
                    break;
            }
        }

        #region Обработки каждой таблицы
        private void ProcessBrand(DataRow row, int idRecord)
        {
            var entity = createrFactory.CreateCreaterBrand().GetEntity(idRecord);
            var repository = repositoryFactory.CreateBrandRepository();
            ProcessOperation(row["OperationRecord"].ToString(), repository, entity, idRecord);
        }

        private void ProcessCategory(DataRow row, int idRecord)
        {
            var entity = createrFactory.CreateCreaterCategory().GetEntity(idRecord);
            var repository = repositoryFactory.CreateCategoryRepository();
            ProcessOperation(row["OperationRecord"].ToString(), repository, entity, idRecord);
        }

        private void ProcessDepartment(DataRow row, int idRecord)
        {
            var entity = createrFactory.CreateCreaterDepartment().GetEntity(idRecord);
            var repository = repositoryFactory.CreateDepartmentRepository();
            ProcessOperation(row["OperationRecord"].ToString(), repository, entity, idRecord);
        }
        private void ProcessEmployee(DataRow row, int idRecord)
        {
            var entity = createrFactory.CreateCreaterEmployee().GetEntity(idRecord);
            var repository = repositoryFactory.CreateEmployeeRepository();
            ProcessOperation(row["OperationRecord"].ToString(), repository, entity, idRecord);
        }
        private void ProcessGiveDetail(DataRow row, int idRecord)
        {
            var entity = createrFactory.CreateCreaterGiveDetail().GetEntity(idRecord);
            var repository = repositoryFactory.CreateGiveDetailRepository();
            ProcessOperation(row["OperationRecord"].ToString(), repository, entity, idRecord);
        }
        private void ProcessGive(DataRow row, int idRecord)
        {
            var entity = createrFactory.CreateCreaterGive().GetEntity(idRecord);
            var repository = repositoryFactory.CreateGiveRepository();
            ProcessOperation(row["OperationRecord"].ToString(), repository, entity, idRecord);
        }
        private void ProcessPost(DataRow row, int idRecord)
        {
            var entity = createrFactory.CreateCreaterPost().GetEntity(idRecord);
            var repository = repositoryFactory.CreatePostRepository();
            ProcessOperation(row["OperationRecord"].ToString(), repository, entity, idRecord);
        }
        private void ProcessProduct(DataRow row, int idRecord)
        {
            var entity = createrFactory.CreateCreaterProduct().GetEntity(idRecord);
            var repository = repositoryFactory.CreateProductRepository();
            ProcessOperation(row["OperationRecord"].ToString(), repository, entity, idRecord);
        }
        private void ProcessProvider(DataRow row, int idRecord)
        {
            var entity = createrFactory.CreateCreaterProvider().GetEntity(idRecord);
            var repository = repositoryFactory.CreateProviderRepository();
            ProcessOperation(row["OperationRecord"].ToString(), repository, entity, idRecord);
        }
        private void ProcessPurchase(DataRow row, int idRecord)
        {
            var entity = createrFactory.CreateCreaterPurchase().GetEntity(idRecord);
            var repository = repositoryFactory.CreatePurchaseRepository();
            ProcessOperation(row["OperationRecord"].ToString(), repository, entity, idRecord);
        }

        private void ProcessUnit(DataRow row, int idRecord)
        {
            var entity = createrFactory.CreateCreaterUnit().GetEntity(idRecord);
            var repository = repositoryFactory.CreateUnitRepository();
            ProcessOperation(row["OperationRecord"].ToString(), repository, entity, idRecord);
        }

        private void ProcessUser(DataRow row, int idRecord)
        {
            var entity = createrFactory.CreateCreaterUser().GetEntity(idRecord);
            var repository = repositoryFactory.CreateUserRepository();
            ProcessOperation(row["OperationRecord"].ToString(), repository, entity, idRecord);
        }

        #endregion

    }
}
