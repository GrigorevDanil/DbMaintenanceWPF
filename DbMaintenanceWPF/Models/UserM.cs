using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.ManageDatabase;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DbMaintenanceWPF.Models
{
    class UserM(UserRepository userR)
    {
        readonly UserRepository UserR = userR;
        public ReadOnlyObservableCollection<User> PublicListUsers => new(new ObservableCollection<User>(UserR.GetAll()));

        public void Update(User user) => UserR.Update(user.Id, user);
        public void Remove(User user) => UserR.Remove(user);
    }
}
