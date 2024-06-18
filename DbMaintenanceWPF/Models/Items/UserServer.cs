using DbMaintenanceWPF.Items.Interfaces;
using System;


namespace DbMaintenanceWPF.Models.Items
{
    public class UserServer : IEntity, IEquatable<UserServer>
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Host { get; set; }
        public bool IsSelected { get; set; }

        public bool Equals(UserServer other) => Id == other.Id;
    }
}
