using DbMaintenanceWPF.Items.Interfaces;

namespace DbMaintenanceWPF.Models.Items
{
    public class Category : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsSelected { get; set; }
    }
}
