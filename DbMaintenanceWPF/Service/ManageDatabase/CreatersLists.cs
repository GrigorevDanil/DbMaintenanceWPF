using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Interface;
using System;
using System.Collections.Generic;

namespace DbMaintenanceWPF.Service.ManageDatabase
{
    public class CreaterListCategory(IReaderDatabase readerDatabase) : ICreaterListItems<Category>
    {
        readonly IReaderDatabase ReaderDatabase = readerDatabase;

        public List<Category> GetList()
        {
            return new List<Category>(ReaderDatabase.LoadList("SELECT * FROM `category` ", reader =>
            {
                return new Category()
                {
                    Id = reader.GetInt32("IdCategory"),
                    Title = reader.GetString("Title"),
                };
            }));
        }
    }

    public class CreaterListBrand(IReaderDatabase readerDatabase) : ICreaterListItems<Brand>
    {
        readonly IReaderDatabase ReaderDatabase = readerDatabase;

        public List<Brand> GetList()
        {
            return new List<Brand>(ReaderDatabase.LoadList("SELECT * FROM `brand` ", reader =>
            {
                return new Brand()
                {
                    Id = reader.GetInt32("IdBrand"),
                    Title = reader.GetString("Title"),
                };
            }));
        }
    }

    public class CreaterListUnit(IReaderDatabase readerDatabase) : ICreaterListItems<Unit>
    {
        readonly IReaderDatabase ReaderDatabase = readerDatabase;

        public List<Unit> GetList()
        {
            return new List<Unit>(ReaderDatabase.LoadList("SELECT * FROM `unit` ", reader =>
            {
                return new Unit()
                {
                    Id = reader.GetInt32("IdUnit"),
                    ShortName = reader.GetString("ShortName"),
                    Title = reader["Title"] != DBNull.Value ? reader.GetString("Title") : null,
                };
            }));
        }
    }

    public class CreaterListDepartment(IReaderDatabase readerDatabase) : ICreaterListItems<Department>
    {
        readonly IReaderDatabase ReaderDatabase = readerDatabase;

        public List<Department> GetList()
        {
            return new List<Department>(ReaderDatabase.LoadList("SELECT * FROM `department` ", reader =>
            {
                return new Department()
                {
                    Id = reader.GetInt32("IdDepartment"),
                    Title = reader.GetString("Title"),
                };
            }));
        }
    }

    public class CreaterListEmployee(IReaderDatabase readerDatabase) : ICreaterListItems<Employee>
    {
        readonly IReaderDatabase ReaderDatabase = readerDatabase;

        public List<Employee> GetList()
        {
            return new List<Employee>(ReaderDatabase.LoadList("SELECT employee.*, department.Title as 'DepartmentTitle', post.Title as 'PostTitle' FROM `employee` LEFT JOIN department ON employee.IdDepartment = department.IdDepartment LEFT JOIN post ON employee.IdPost = post.IdPost", reader =>
            {
                return new Employee()
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
            }));
        }
    }

    public class CreaterListGiveDetail(IReaderDatabase readerDatabase) : ICreaterListItems<GiveDetail>
    {
        readonly IReaderDatabase ReaderDatabase = readerDatabase;

        public List<GiveDetail> GetList()
        {
            return new List<GiveDetail>(ReaderDatabase.LoadList("SELECT givedetail.*, employee.IdEmployee ,employee.Surname, employee.Name, employee.Lastname, give.DateGive, product.Title, product.Model  FROM `givedetail` LEFT JOIN give ON givedetail.IdGive = give.IdGive LEFT JOIN product ON givedetail.IdProduct = product.IdProduct LEFT JOIN employee ON give.IdEmployee = employee.IdEmployee", reader =>
            {
                return new GiveDetail()
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
                    },
                    CountProduct = reader.GetInt32("CountProduct")
                };
            }));
        }


    }

    public class CreaterListGive(IReaderDatabase readerDatabase) : ICreaterListItems<Give>
    {
        readonly IReaderDatabase ReaderDatabase = readerDatabase;

        public List<Give> GetList()
        {
            return new List<Give>(ReaderDatabase.LoadList("SELECT give.*,employee.Surname, employee.Name, employee.Lastname FROM `give` LEFT JOIN employee ON give.IdEmployee = employee.IdEmployee ", reader =>
            {
                return new Give()
                {
                    Id = reader.GetInt32("IdGive"),
                    Employee = new Employee { Id = reader.GetInt32("IdEmployee"), Surname = reader.GetString("Surname"), Name = reader.GetString("Name"), Lastname = reader["Lastname"] != DBNull.Value ? reader.GetString("Lastname") : null },
                    DateGive = reader.GetDateTime("DateGive")
                };
            }));
        }
    }

    public class CreaterListPost(IReaderDatabase readerDatabase) : ICreaterListItems<Post>
    {
        readonly IReaderDatabase ReaderDatabase = readerDatabase;

        public List<Post> GetList()
        {
            return new List<Post>(ReaderDatabase.LoadList("SELECT * FROM `post` ", reader =>
            {
                return new Post()
                {
                    Id = reader.GetInt32("IdPost"),
                    Title = reader.GetString("Title"),
                };
            }));
        }
    }

    public class CreaterListProduct(IReaderDatabase readerDatabase) : ICreaterListItems<Product>
    {
        readonly IReaderDatabase ReaderDatabase = readerDatabase;

        public List<Product> GetList()
        {
            return new List<Product>(ReaderDatabase.LoadList("SELECT product.*, product.Title as 'ProductTitle' , category.Title as 'CategoryTitle', brand.Title as 'BrandTitle', unit.ShortName, unit.Title as 'UnitTitle' FROM `product` LEFT JOIN brand ON product.IdBrand = brand.IdBrand LEFT JOIN category ON product.IdCategory = category.IdCategory LEFT JOIN unit ON product.IdUnit = unit.IdUnit", reader =>
            {
                return new Product()
                {
                    Id = reader.GetInt32("IdProduct"),
                    Category = new Category() { Id = reader.GetInt32("IdCategory"), Title = reader.GetString("CategoryTitle") },
                    Brand = new Brand() { Id = reader.GetInt32("IdBrand"), Title = reader.GetString("BrandTitle") },
                    Unit = new Unit() { Id = reader.GetInt32("IdUnit"), ShortName = reader.GetString("ShortName"), Title = reader["UnitTitle"] != DBNull.Value ? reader.GetString("UnitTitle") : null },
                    Title = reader.GetString("ProductTitle"),
                    Model = reader.GetString("Model"),
                    CountProduct = reader.GetInt32("CountProduct"),
                    Description = reader["Description"] != DBNull.Value ? reader.GetString("Description") : null,
                };
            }));
        }
    }

    public class CreaterListProvider(IReaderDatabase readerDatabase) : ICreaterListItems<Provider>
    {
        readonly IReaderDatabase ReaderDatabase = readerDatabase;

        public List<Provider> GetList()
        {
            return new List<Provider>(ReaderDatabase.LoadList("SELECT * FROM `provider` ", reader =>
            {
                return new Provider()
                {
                    Id = reader.GetInt32("IdProvider"),
                    TitleCompany = reader["TitleCompany"] != DBNull.Value ? reader.GetString("TitleCompany") : null,
                    ContactName = reader.GetString("ContactName"),
                    Address = reader.GetString("Address"),
                    NumPhone = reader.GetString("NumPhone"),
                    Email = reader["Email"] != DBNull.Value ? reader.GetString("Email") : null
                };
            }));
        }
    }

    public class CreaterListPurchase(IReaderDatabase readerDatabase) : ICreaterListItems<Purchase>
    {
        readonly IReaderDatabase ReaderDatabase = readerDatabase;

        public List<Purchase> GetList()
        {
            return new List<Purchase>(ReaderDatabase.LoadList("SELECT purchase.*, product.Title, product.Model, provider.TitleCompany, provider.ContactName FROM `purchase` LEFT JOIN product ON purchase.IdProduct = product.IdProduct LEFT JOIN provider ON purchase.IdProvider = provider.IdProvider", reader =>
            {
                return new Purchase()
                {
                    Id = reader.GetInt32("IdPurchase"),
                    Product = new Product() { Id = reader.GetInt32("IdProduct"), Title = reader.GetString("Title"), Model = reader.GetString("Model") },
                    Provider = new Provider() { Id = reader.GetInt32("IdProvider"), TitleCompany = reader["TitleCompany"] != DBNull.Value ? reader.GetString("TitleCompany") : null, ContactName = reader.GetString("ContactName") },
                    DateProvide = reader["DateProvide"] != DBNull.Value ? reader.GetDateTime("DateProvide") : null,
                    CountProduct = reader.GetInt32("CountProduct"),
                    Price = reader.GetInt32("Price")
                };
            }));
        }
    }

    public class CreaterListUser(IReaderDatabase readerDatabase, IImageHelper imageHelper) : ICreaterListItems<User>
    {
        readonly IReaderDatabase ReaderDatabase = readerDatabase;
        readonly IImageHelper ImageHelper = imageHelper;

        public List<User> GetList()
        {
            return new List<User>(ReaderDatabase.LoadList("SELECT * FROM `user` ", reader =>
            {
                return new User()
                {
                    Id = reader.GetInt32("IdUser"),
                    Image = ImageHelper.BytesToImageSource(reader, "Image"),
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
            }));
        }
    }

    public class CreaterListUserServer(IReaderDatabase readerDatabase) : ICreaterListItems<UserServer>
    {
        readonly IReaderDatabase ReaderDatabase = readerDatabase;

        public List<UserServer> GetList()
        {
            return new List<UserServer>(ReaderDatabase.LoadList("SELECT User, Host FROM mysql.user", reader =>
            {
                return new UserServer()
                {
                    Login = reader.GetString("User"),
                    Host = reader.GetString("Host")
                };
            }));
        }
    }
}
