using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mko.WinForm
{
    public partial class FormWithTabControl : Form
    {
        protected List<mko.Log.RC> logContainer = new List<Log.RC>();
        protected mko.Log.LogServer log = new Log.LogServer();

        mko.Log.RCContainerLogHnd logHndContainer;
        mko.Log.WinFormStatusStripLogHnd logHndMainStatusStrip;

        mko.Algo.NumberTheory.Fibonacci Fibonacci = new Algo.NumberTheory.Fibonacci();

        public FormWithTabControl()
        {
            InitializeComponent();

            logHndContainer = new Log.RCContainerLogHnd(logContainer, (col, rc) => { ((List<Log.RC>)col).Insert(0, rc); BindingSourceLogTab.ResetBindings(false); });
            log.registerLogHnd(logHndContainer);

            logHndMainStatusStrip = new Log.WinFormStatusStripLogHnd(statusStripBaseForm, "toolStripStatusLabelBaseForm");
            log.registerLogHnd(logHndMainStatusStrip);
            
            BindingSourceLogTab.DataSource = logContainer;

            log.Log(mko.Log.RC.CreateStatus("Gestartet"));
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            Application.Exit();         
        }

        private void btnBaseFormTabPageLogsClear_Click(object sender, EventArgs e)
        {
            Fibonacci.Reset();

            logContainer.Clear();
            log.Log(mko.Log.RC.CreateStatus("Neustart"));
        }

        private void btnBaseFormTabPageLogsFibonacci_Click(object sender, EventArgs e)
        {
            var FibN = Fibonacci.Next();

            var btn = sender as Button;
            btn.Text = "Fibonacci[" + (FibN.Item1 + 1) + "]";

            log.Log(mko.Log.RC.CreateMsg("Fibonacci_Zahl[" + FibN.Item1 + "] = " + FibN.Item2));


        }

        private void btnBaseFormTabControlLogFactorization_Click(object sender, EventArgs e)
        {
            long z = (long)numericUpDownBaseFormTabControlLog.Value;
            var factors = mko.Algo.NumberTheory.PrimeFactors.Factorization(z);

            var bld = new StringBuilder();
            foreach (long f in factors)
            {
                bld.Append(f);
                bld.Append(", ");
            }

            log.Log(mko.Log.RC.CreateMsg("Primfaktoren von " + z + "= " + bld.ToString() ));

        }
    }
}
