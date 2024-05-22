using DbMaintenanceWPF.Models.Items;
using DbMaintenanceWPF.Service.ManageDatabase;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DbMaintenanceWPF.Models
{
    class UnitM
    {
        readonly UnitRepository UnitR;

        public ReadOnlyObservableCollection<Unit> PublicListUnits => new(new ObservableCollection<Unit>(UnitR.GetAll()));

        public UnitM(UnitRepository unitR) => UnitR = unitR;

        public void Update(Unit unit) => UnitR.Update(unit.Id, unit);
        public void Remove(Unit unit) => UnitR.Remove(unit);
    }
}
