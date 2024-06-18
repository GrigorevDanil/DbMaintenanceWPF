using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Interface;
using DbMaintenanceWPF.Service.Repositories;
using System.Collections.ObjectModel;

namespace DbMaintenanceWPF.Models.ItemModels
{
    public class UserM(UserRepository userR)
    {
        readonly UserRepository UserR = userR;
        public ReadOnlyObservableCollection<User> PublicListUsers => new(new ObservableCollection<User>(UserR.GetAll()));

        public void Create(User user) => UserR.Add(user);
        public void Update(User user) => UserR.Update(user.Id, user);
        public void Remove(User user) => UserR.Remove(user);
    }
}
