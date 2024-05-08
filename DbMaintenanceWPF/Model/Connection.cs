using DbMaintenanceWPF.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DbMaintenanceWPF.Model
{
    class Connection : Utilities.ViewModelBase
    {
        string server, port, dbname, user, pass;
        public string Server
        {
            get { return server; }
            set { server = value; OnPropertyChanged(nameof(Server)); }
        }
        public string Port
        {
            get { return port; }
            set { port = value; OnPropertyChanged(nameof(Port)); }
        }

        public string DbName
        {
            get { return dbname; }
            set { dbname = value; OnPropertyChanged(nameof(DbName)); }
        }

        public string User
        {
            get { return user; }
            set { user = value; Password = ""; OnPropertyChanged(nameof(User)); }
        }

        public string Password
        {
            get { return pass; }
            set { pass = value; OnPropertyChanged(nameof(Password)); }
        }

    }
}
