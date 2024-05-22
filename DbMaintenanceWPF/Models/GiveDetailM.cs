using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.ManageDatabase;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DbMaintenanceWPF.Models
{
    class GiveDetailM
    {
        readonly GiveDetailRepository GiveDetailR;
        readonly GiveRepository GiveR;
        readonly ProductRepository ProductR;
        public ReadOnlyObservableCollection<GiveDetail> PublicListGiveDetails => new(new ObservableCollection<GiveDetail>(GiveDetailR.GetAll()));
        public ReadOnlyObservableCollection<Give> PublicListGives => new(new ObservableCollection<Give>(GiveR.GetAll()));
        public ReadOnlyObservableCollection<Product> PublicListProducts => new(new ObservableCollection<Product>(ProductR.GetAll()));

        public GiveDetailM(GiveDetailRepository giveDetailR, GiveRepository giveR, ProductRepository productR)
        {
            GiveDetailR = giveDetailR;
            GiveR = giveR;
            ProductR = productR;
        }

        public void Update(GiveDetail giveDetail) => GiveDetailR.Update(giveDetail.Id, giveDetail);
        public void Remove(GiveDetail giveDetail) => GiveDetailR.Remove(giveDetail);
    }
}
