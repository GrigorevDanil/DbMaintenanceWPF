using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbMaintenanceWPF
{
    internal class EventArgs<T> : EventArgs
    {
        public T Arg { get; set; }

        public EventArgs(T Arg) => this.Arg = Arg;

        public static implicit operator EventArgs<T>(T arg) => new(arg);

        public static implicit operator T(EventArgs<T> arg) => arg.Arg;
    }
}
