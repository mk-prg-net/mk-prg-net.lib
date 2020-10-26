using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ATMO.mko.Logging
{
    public class RCContainerLogHnd : ATMO.mko.Logging.ILogHnd
    {
        /// <summary>
        /// Referenz auf den Container mit den Logeinträgen
        /// </summary>
        IEnumerable<ILogInfo> _container;

        /// <summary>
        /// Lambda, der die Operation definiert, welche zum Anfügen eines Logeintrages an den 
        /// Container auszuführen ist.
        /// Da eine ICollection eine Queue, List, Dictionary etc. sein kann, ist diese explizite definition 
        /// einer Anfügeoperation notwendig
        /// z.B. für List: (container, rc) => container.Add(RC)
        /// </summary>
        Action<IEnumerable<ILogInfo>, ILogInfo> _AppendOp;

        /// <summary>
        /// mko, 26.9.2017
        /// </summary>
        IEnumerable<ITraceInfo> _TiContainer;
        Action<IEnumerable<ITraceInfo>, ITraceInfo> _AppendTiOp;

        public RCContainerLogHnd(IEnumerable<ILogInfo> Container, Action<IEnumerable<ILogInfo>, ILogInfo> AppendOp, IEnumerable<ITraceInfo> TraceInfoContainer, Action<IEnumerable<ITraceInfo>, ITraceInfo> AppendTiOp)
        {
            _container = Container;
            _AppendOp = AppendOp;

            _TiContainer = TraceInfoContainer;
            _AppendTiOp = AppendTiOp;
        }


        void ILogHnd.OnLog(long logCounter, string userId, ILogInfo info)
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

        /// <summary>
        /// mko, 26.9.2017
        /// </summary>
        /// <param name="ti"></param>
        public void OnLog(long logCounter, ITraceInfo ti)
        {
            _AppendTiOp(_TiContainer, ti);
        }
    }
}
