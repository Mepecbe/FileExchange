using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Resources.LockersModule
{
    public static class SyncLockers
    {
        public static Mutex FileLocker = new Mutex();

    }
}
