using DbMaintenanceWPF.Items.Interfaces;


namespace DbMaintenanceWPF.Models.Items
{
    public class UserServer : IEntity
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Host { get; set; }
        public bool IsSelected { get; set; }
    }
}
