using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Interface;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DbMaintenanceWPF.Service.Repositories
{
    public class CreaterEntityCategory(IReaderDatabase readerDatabase) : ICreaterEntity<Category>
    {
        public Category GetEntity(int id) => readerDatabase.LoadRecord($"SELECT * FROM `category` WHERE IdCategory = {id}", ReadCategory);

        public List<Category> GetList() =>
                                readerDatabase.LoadList("SELECT * FROM `category`", ReadCategory)
                                .OrderBy(category => category.Title)
                                .ToList();

        Category ReadCategory(MySqlDataReader reader) => new()
            {
                Id = reader.GetInt32("IdCategory"),
                Title = reader.GetString("Title"),
            };
    }

    public class CreaterEntityBrand(IReaderDatabase readerDatabase) : ICreaterEntity<Brand>
    {
        public Brand GetEntity(int id) => readerDatabase.LoadRecord($"SELECT * FROM `brand` WHERE IdBrand = {id}", ReadBrand);

        public List<Brand> GetList() => 
            new(readerDatabase.LoadList("SELECT * FROM `brand` ", ReadBrand)
            .OrderBy(brand => brand.Title));

        Brand ReadBrand(MySqlDataReader reader) => new()
        {
            Id = reader.GetInt32("IdBrand"),
            Title = reader.GetString("Title"),
        };
    }

    public class CreaterEntityUnit(IReaderDatabase readerDatabase) : ICreaterEntity<Unit>
    {
        public Unit GetEntity(int id) => readerDatabase.LoadRecord($"SELECT * FROM `unit` WHERE IdUnit = {id}", ReadUnit);

        public List<Unit> GetList() => 
            new(readerDatabase.LoadList("SELECT * FROM `unit` ", ReadUnit)
                .OrderBy(unit => unit.Title));

        Unit ReadUnit(MySqlDataReader reader) => new()
        {
            Id = reader.GetInt32("IdUnit"),
            ShortName = reader.GetString("ShortName"),
            OKEI = reader.GetInt32("OKEI"),
            Title = reader["Title"] != DBNull.Value ? reader.GetString("Title") : null,
        };
    }

    public class CreaterEntityDepartment(IReaderDatabase readerDatabase) : ICreaterEntity<Department>
    {
        public Department GetEntity(int id) => readerDatabase.LoadRecord($"SELECT * FROM `department` WHERE IdDepartment = {id}", ReadDepartment);

        public List<Department> GetList() => 
            new(readerDatabase.LoadList("SELECT * FROM `department` ", ReadDepartment)
            .OrderBy(department => department.Title));

        Department ReadDepartment(MySqlDataReader reader) => new()
        {
            Id = reader.GetInt32("IdDepartment"),
            Title = reader.GetString("Title"),
        };
    }

    public class CreaterEntityEmployee(IReaderDatabase readerDatabase) : ICreaterEntity<Employee>
    {
        public Employee GetEntity(int id) => readerDatabase.LoadRecord($"SELECT employee.*, department.Title as 'DepartmentTitle', post.Title as 'PostTitle' FROM `employee` LEFT JOIN department ON employee.IdDepartment = department.IdDepartment LEFT JOIN post ON employee.IdPost = post.IdPost WHERE IdEmployee = {id}", ReadEmployee);

        public List<Employee> GetList() => 
            new(readerDatabase.LoadList("SELECT employee.*, department.Title as 'DepartmentTitle', post.Title as 'PostTitle' FROM `employee` LEFT JOIN department ON employee.IdDepartment = department.IdDepartment LEFT JOIN post ON employee.IdPost = post.IdPost", ReadEmployee)
            .OrderBy(emp => emp.TextEmployee));

        Employee ReadEmployee(MySqlDataReader reader) => new()
        {
            Id = reader.GetInt32("IdEmployee"),
            Department = new Department() { Id = reader.GetInt32("IdDepartment"), Title = reader.GetString("DepartmentTitle") },
            Post = new Post() { Id = reader.GetInt32("IdPost"), Title = reader.GetString("PostTitle") },
            Surname = reader.GetString("Surname"),
            Name = reader.GetString("Name"),
            Lastname = reader["Lastname"] != DBNull.Value ? reader.GetString("Lastname") : null,
            Email = reader["Email"] != DBNull.Value ? reader.GetString("Email") : null,
            NumPhone = reader.GetString("NumPhone"),
            Address = reader.GetString("Address"),
        };

    }

    public class CreaterEntityGiveDetail(IReaderDatabase readerDatabase) : ICreaterEntity<GiveDetail>
    {
        public GiveDetail GetEntity(int id) => readerDatabase.LoadRecord($"SELECT givedetail.*, employee.IdEmployee ,employee.Surname, employee.Name, employee.Lastname, give.DateGive, product.Title, product.Model,product.OKPD, product.Price, unit.ShortName, unit.Title as 'UnitTitle', unit.OKEI  \r\nFROM `givedetail` \r\nLEFT JOIN give ON givedetail.IdGive = give.IdGive \r\nLEFT JOIN product ON givedetail.IdProduct = product.IdProduct \r\nLEFT JOIN unit ON unit.IdUnit = product.IdUnit \r\nLEFT JOIN employee ON give.IdEmployee = employee.IdEmployee WHERE IdGiveDetail = {id}", ReadGiveDetail);

        public List<GiveDetail> GetList() => 
            new(readerDatabase.LoadList("SELECT givedetail.*, employee.IdEmployee ,employee.Surname, employee.Name, employee.Lastname, give.DateGive, product.Title, product.Model,product.OKPD, product.Price, unit.ShortName, unit.Title as 'UnitTitle', unit.OKEI  \r\nFROM `givedetail` \r\nLEFT JOIN give ON givedetail.IdGive = give.IdGive \r\nLEFT JOIN product ON givedetail.IdProduct = product.IdProduct \r\nLEFT JOIN unit ON unit.IdUnit = product.IdUnit \r\nLEFT JOIN employee ON give.IdEmployee = employee.IdEmployee", ReadGiveDetail)
            .OrderBy(giveDetail => giveDetail.Give.TextGive));

        GiveDetail ReadGiveDetail(MySqlDataReader reader) => new()
        {
            Id = reader.GetInt32("IdGiveDetail"),
            Give = new Give()
            {
                Id = reader.GetInt32("IdGive"),
                DateGive = reader.GetDateTime("DateGive"),
                Employee = new Employee()
                {
                    Id = reader.GetInt32("IdEmployee"),
                    Surname = reader.GetString("Surname"),
                    Name = reader.GetString("Name"),
                    Lastname = reader.GetString("Lastname")
                }
            },
            Product = new Product()
            {
                Id = reader.GetInt32("IdProduct"),
                Title = reader.GetString("Title"),
                Model = reader.GetString("Model"),
                OKPD = reader.GetString("OKPD"),
                Price = reader.GetInt32("Price"),
                Unit = new Unit() { ShortName = reader.GetString("ShortName"), Title = reader.GetString("UnitTitle"), OKEI = reader.GetInt32("OKEI") }
            },
            CountProduct = reader.GetInt32("CountProduct")
        };

    }

    public class CreaterEntityGive(IReaderDatabase readerDatabase) : ICreaterEntity<Give>
    {
        public Give GetEntity(int id) => readerDatabase.LoadRecord($"SELECT give.*,employee.Surname, employee.Name, employee.Lastname FROM `give` LEFT JOIN employee ON give.IdEmployee = employee.IdEmployee WHERE IdGive = {id}", ReadGive);

        public List<Give> GetList() => 
            new(readerDatabase.LoadList("SELECT give.*,employee.Surname, employee.Name, employee.Lastname FROM `give` LEFT JOIN employee ON give.IdEmployee = employee.IdEmployee ", ReadGive)
            .OrderBy(give => give.TextGive));

        Give ReadGive(MySqlDataReader reader) => new()
        {
            Id = reader.GetInt32("IdGive"),
            Employee = new Employee { Id = reader.GetInt32("IdEmployee"), Surname = reader.GetString("Surname"), Name = reader.GetString("Name"), Lastname = reader["Lastname"] != DBNull.Value ? reader.GetString("Lastname") : null },
            DateGive = reader.GetDateTime("DateGive")
        };
    }

    public class CreaterEntityPost(IReaderDatabase readerDatabase) : ICreaterEntity<Post>
    {
        public Post GetEntity(int id) => readerDatabase.LoadRecord($"SELECT * FROM `post` WHERE IdPost = {id}", ReadPost);
        public List<Post> GetList() => 
            new(readerDatabase.LoadList("SELECT * FROM `post` ", ReadPost)
            .OrderBy(post => post.Title));
        Post ReadPost(MySqlDataReader reader) => new()
        {
            Id = reader.GetInt32("IdPost"),
            Title = reader.GetString("Title"),
        };

    }

    public class CreaterEntityProduct(IReaderDatabase readerDatabase) : ICreaterEntity<Product>
    {
        public Product GetEntity(int id) => readerDatabase.LoadRecord($"SELECT product.*, product.Title as 'ProductTitle' , category.Title as 'CategoryTitle', brand.Title as 'BrandTitle', unit.ShortName, unit.Title as 'UnitTitle' FROM `product` LEFT JOIN brand ON product.IdBrand = brand.IdBrand LEFT JOIN category ON product.IdCategory = category.IdCategory LEFT JOIN unit ON product.IdUnit = unit.IdUnit WHERE IdProduct = {id}", ReadProduct);

        public List<Product> GetList() =>
            new(readerDatabase.LoadList("SELECT product.*, product.Title as 'ProductTitle' , category.Title as 'CategoryTitle', brand.Title as 'BrandTitle', unit.ShortName, unit.Title as 'UnitTitle' FROM `product` LEFT JOIN brand ON product.IdBrand = brand.IdBrand LEFT JOIN category ON product.IdCategory = category.IdCategory LEFT JOIN unit ON product.IdUnit = unit.IdUnit", ReadProduct)
            .OrderBy(product => product.Title));

        Product ReadProduct(MySqlDataReader reader) => new()
        {
            Id = reader.GetInt32("IdProduct"),
            Category = new Category() { Id = reader.GetInt32("IdCategory"), Title = reader.GetString("CategoryTitle") },
            Brand = new Brand() { Id = reader.GetInt32("IdBrand"), Title = reader.GetString("BrandTitle") },
            Unit = new Unit() { Id = reader.GetInt32("IdUnit"), ShortName = reader.GetString("ShortName"), Title = reader["UnitTitle"] != DBNull.Value ? reader.GetString("UnitTitle") : null },
            Title = reader.GetString("ProductTitle"),
            Model = reader.GetString("Model"),
            CountProduct = reader.GetInt32("CountProduct"),
            Price = reader.GetInt32("Price"),
            OKPD = reader.GetString("OKPD"),
            Description = reader["Description"] != DBNull.Value ? reader.GetString("Description") : null,
        };

    }

    public class CreaterEntityProvider(IReaderDatabase readerDatabase) : ICreaterEntity<Provider>
    {
        public Provider GetEntity(int id) => readerDatabase.LoadRecord($"SELECT * FROM `provider` WHERE IdProvider = {id}", ReadProvider);

        public List<Provider> GetList() =>
            new(readerDatabase.LoadList("SELECT * FROM `provider` ", ReadProvider)
            .OrderBy(provider => provider.ContactName));

        Provider ReadProvider(MySqlDataReader reader) => new()
        {
            Id = reader.GetInt32("IdProvider"),
            TitleCompany = reader["TitleCompany"] != DBNull.Value ? reader.GetString("TitleCompany") : null,
            ContactName = reader.GetString("ContactName"),
            Address = reader.GetString("Address"),
            NumPhone = reader.GetString("NumPhone"),
            Email = reader["Email"] != DBNull.Value ? reader.GetString("Email") : null
        };

    }

    public class CreaterEntityPurchase(IReaderDatabase readerDatabase) : ICreaterEntity<Purchase>
    {
        public Purchase GetEntity(int id) => readerDatabase.LoadRecord($"SELECT purchase.*, product.Title, product.Model, provider.TitleCompany, provider.ContactName FROM `purchase` LEFT JOIN product ON purchase.IdProduct = product.IdProduct LEFT JOIN provider ON purchase.IdProvider = provider.IdProvider WHERE IdPurchase = {id}", ReadPurchase);

        public List<Purchase> GetList() => 
            new(readerDatabase.LoadList("SELECT purchase.*, product.Title, product.Model, provider.TitleCompany, provider.ContactName FROM `purchase` LEFT JOIN product ON purchase.IdProduct = product.IdProduct LEFT JOIN provider ON purchase.IdProvider = provider.IdProvider", ReadPurchase )
            .OrderBy(purchase => purchase.Product.TextProduct));

        Purchase ReadPurchase(MySqlDataReader reader) => new()
        {
            Id = reader.GetInt32("IdPurchase"),
            Product = new Product() { Id = reader.GetInt32("IdProduct"), Title = reader.GetString("Title"), Model = reader.GetString("Model") },
            Provider = new Provider() { Id = reader.GetInt32("IdProvider"), TitleCompany = reader["TitleCompany"] != DBNull.Value ? reader.GetString("TitleCompany") : null, ContactName = reader.GetString("ContactName") },
            DateProvide = reader["DateProvide"] != DBNull.Value ? reader.GetDateTime("DateProvide") : null,
            CountProduct = reader.GetInt32("CountProduct"),
            Price = reader.GetInt32("Price")
        };

    }

    public class CreaterEntityUser(IReaderDatabase readerDatabase, IImageHelper imageHelper) : ICreaterEntity<User>
    {
        public User GetEntity(int id) => readerDatabase.LoadRecord($"SELECT * FROM `user` WHERE IdUser = {id} ", ReadUser);

        public List<User> GetList() => 
            new(readerDatabase.LoadList("SELECT * FROM `user` ", ReadUser)
            .OrderBy(user => user.Login));

        User ReadUser(MySqlDataReader reader) => new()
        {
            Id = reader.GetInt32("IdUser"),
            Image = imageHelper.BytesToImageSource(reader, "Image"),
            Name = reader.GetString("Name"),
            Surname = reader.GetString("Surname"),
            Login = reader.GetString("Login"),
            PasswordHash = reader.GetString("PasswordHash"),
            Salt = reader.GetString("Salt"),
            Role = reader.GetString("Role"),
            CountAttemp = reader.GetInt32("CountAttemp"),
            IsLock = reader.GetBoolean("IsLock"),
            DateLock = reader["DateLock"] != DBNull.Value ? reader.GetDateTime("DateLock") : null
        };
    }

    public class CreaterEntityUserServer(IReaderDatabase readerDatabase) : ICreaterEntity<UserServer>
    {
        public UserServer GetEntity(int id) => readerDatabase.LoadRecord("SELECT User, Host FROM mysql.user", ReadUserServer);
        public List<UserServer> GetList() => 
            new(readerDatabase.LoadList("SELECT User, Host FROM mysql.user", ReadUserServer)
            .OrderBy(userServer => userServer.Login));

        UserServer ReadUserServer(MySqlDataReader reader) => new()
        {
            Login = reader.GetString("User"),
            Host = reader.GetString("Host")
        };
    }
}
