using DbMaintenanceWPF.Items.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbMaintenanceWPF.Models.Items
{
    public class Post : IEntity, IEquatable<Post>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsSelected { get; set; }

        public bool Equals(Post other) => Id == other.Id;
    }
}
