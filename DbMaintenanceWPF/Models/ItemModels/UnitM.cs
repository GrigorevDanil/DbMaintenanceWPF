using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.Interface;
using DbMaintenanceWPF.Service.Repositories;
using System.Collections.ObjectModel;

namespace DbMaintenanceWPF.Models.ItemModels
{
    public class UnitM(UnitRepository unitR)
    {
        readonly UnitRepository UnitR = unitR;

        public ReadOnlyObservableCollection<Unit> PublicListUnits => new(new ObservableCollection<Unit>(UnitR.GetAll()));

        public void Create(Unit unit) => UnitR.Add(unit);
        public void Update(Unit unit) => UnitR.Update(unit.Id, unit);
        public void Remove(Unit unit) => UnitR.Remove(unit);
    }
}
