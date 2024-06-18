using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Interface;
using DbMaintenanceWPF.Service.Repositories;
using System.Collections.ObjectModel;

namespace DbMaintenanceWPF.Models.ItemModels
{
    public class GiveDetailM(GiveDetailRepository giveDetailR, GiveRepository giveR, ProductRepository productR)
    {
        readonly GiveDetailRepository GiveDetailR = giveDetailR;
        readonly GiveRepository GiveR = giveR;
        readonly ProductRepository ProductR = productR;
        public ReadOnlyObservableCollection<GiveDetail> PublicListGiveDetails => new(new ObservableCollection<GiveDetail>(GiveDetailR.GetAll()));
        public ReadOnlyObservableCollection<Give> PublicListGives => new(new ObservableCollection<Give>(GiveR.GetAll()));
        public ReadOnlyObservableCollection<Product> PublicListProducts => new(new ObservableCollection<Product>(ProductR.GetAll()));

        public void Create(GiveDetail giveDetail) => GiveDetailR.Add(giveDetail);
        public void Update(GiveDetail giveDetail) => GiveDetailR.Update(giveDetail.Id, giveDetail);
        public void Remove(GiveDetail giveDetail) => GiveDetailR.Remove(giveDetail);
    }
}
