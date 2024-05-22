using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbMaintenanceWPF.Infrastructure.Commands
{
    internal class HelpCommand : Base.Command
    {
        public override bool CanExecute(object parameter) => true;

        public override void Execute(object parameter) => Process.Start(Directory.GetCurrentDirectory() + "/Help/help5.chm");
    }
}
