using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Log
{
    public class RCContainerLogHnd : mko.Log.ILogHnd
    {
        /// <summary>
        /// Referenz auf den Container mit den Logeinträgen
        /// </summary>
        IEnumerable<RC> _container;

        /// <summary>
        /// Lambda, der die Operation definiert, welche zum Anfügen eines Logeintrages an den 
        /// Container auszuführen ist.
        /// Da eine ICollection eine Queue, List, Dictionary etc. sein kann, ist diese explizite definition 
        /// einer Anfügeoperation notwendig
        /// z.B. für List: (container, rc) => container.Add(RC)
        /// </summary>
        Action<IEnumerable<RC>, RC> _AppendOp;

        public RCContainerLogHnd(IEnumerable<RC> Container, Action<IEnumerable<RC>, RC> AppendOp)
        {
            _container = Container;
            _AppendOp = AppendOp;
        }


        void ILogHnd.OnLog(string userId, ILogInfo info)
        {
            try
            {
                _AppendOp(_container, RC.Create(info));
            }
            catch (Exception)
            {
                SelfDeregisterILogHnd();
            }
        }        

        DgDeregisterILogHnd dgDeregisterILogHnd = null;
        void ILogHnd.SetSelfDeregisterDelegate(DgDeregisterILogHnd dg)
        {
            if (dg != null)
                dgDeregisterILogHnd = dg;
        }

        void SelfDeregisterILogHnd()
        {
            if (dgDeregisterILogHnd != null)
                dgDeregisterILogHnd(this);
        }
    }
}
