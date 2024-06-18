using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Interface;
using DbMaintenanceWPF.Service.Repositories;
using System.Collections.ObjectModel;

namespace DbMaintenanceWPF.Models.ItemModels
{
    public class CategoryM(CategoryRepository categoriesR)
    {
        readonly CategoryRepository CategoriesR = categoriesR;
        public ReadOnlyObservableCollection<Category> PublicListCategories => new(new ObservableCollection<Category>(CategoriesR.GetAll()));

        public void Create(Category category) => CategoriesR.Add(category);
        public void Update(Category category) => CategoriesR.Update(category.Id, category);
        public void Remove(Category category) => CategoriesR.Remove(category);
    }
}
