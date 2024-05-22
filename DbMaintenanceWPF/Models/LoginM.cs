using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Interface;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DbMaintenanceWPF.Models
{
    class LoginM(ICreaterListItems<UserServer> createrItems, IAuthentication authenticationService)
    {
        readonly ICreaterListItems<UserServer> CreaterItems = createrItems;
        readonly IAuthentication AuthenticationService = authenticationService;
        public ReadOnlyObservableCollection<UserServer> PublicListUserServers => new(new ObservableCollection<UserServer>(CreaterItems.GetList()));

        public (bool, string, object) Authentication(bool isServer,string login, string password, string host)
        {
            if (isServer) return AuthenticationService.ValidateUserServer(login, password, host);
            else return AuthenticationService.ValidateUserDatabase(login, password);
        }
    }

}
