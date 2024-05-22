using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.ManageDatabase;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DbMaintenanceWPF.Models
{
    class CategoryM
    {
        readonly CategoryRepository CategoriesR;
        public ReadOnlyObservableCollection<Category> PublicListCategories => new(new ObservableCollection<Category>(CategoriesR.GetAll()));

        public CategoryM(CategoryRepository categoriesR) => CategoriesR = categoriesR;

        public void Create(Category category) => CategoriesR.Add(category);
        public void Update(Category category) => CategoriesR.Update(category.Id, category);
        public void Remove(Category category) => CategoriesR.Remove(category);
    }
}
