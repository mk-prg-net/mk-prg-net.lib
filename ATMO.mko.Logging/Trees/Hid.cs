using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ATMO.mko.Logging.PNDocuTerms.DocuEntities;

namespace ATMO.mko.Logging.Trees
{
    /// <summary>
    /// mko, 19.2.2019
    /// Hierarchische ID's zum Abbilden von Baumstrukturen
    /// </summary>
    public struct Hid<TCRef>
    {
        /// <summary>
        /// 
        /// </summary>
        public ulong beg { get; set; }

        public ulong end { get; set; }


        public ulong Length {
            get
            {
                return (end - beg) + 1;
            }
        }

        /// <summary>
        /// Verweist auf den Inhalt, für den die Hid steht
        /// </summary>
        public TCRef CRef { get; set; }


        public static bool operator==(Hid<TCRef> a, Hid<TCRef> b)
        {
            return a.beg == b.beg && a.end == b.end;
        }

        public static bool operator !=(Hid<TCRef> a, Hid<TCRef> b)
        {
            return a.beg != b.beg || a.end != b.end;
        }
    }

    public class Hid {

        /// <summary>
        /// mko, 19.2.2019
        /// Liefert das erste Hid eines neuen Trees
        /// </summary>
        /// <typeparam name="TCRef"></typeparam>
        /// <param name="cref"></param>
        /// <returns></returns>
        public static Hid<TCRef> Root<TCRef>(TCRef cref)
        {
            // Achtung: end = ulong.MaxValue-1, da sonst die Länge größer als ulong.MaxValue wäre
            //          und dann nicht mehr durch einen ulong darstellbar ist!
            return new Hid<TCRef>() { beg = 0L, end = ulong.MaxValue-1, CRef = cref };
        }

        /// <summary>
        /// Erzeugt die erste Hid unterhalb von parent (im "Subraum" von parent).
        /// </summary>
        /// <typeparam name="TCRef"></typeparam>
        /// <param name="cref">referenz auf den Inhalt, für den der Hid steht</param>
        /// <param name="hid">hid des aktuellen Treenode</param>
        /// <param name="tree">Menge aller hids zum gesamten Baum</param>
        /// <returns></returns>
        public static Hid<TCRef> AllocFirst<TCRef>(TCRef cref, Hid<TCRef> parent, ulong width, IComposer pnL)
        {
            var next = new Hid<TCRef>() { beg = parent.beg, end = parent.beg + width -1, CRef = cref };

            TraceHlp.ThrowIndexOutOfRangeExceptionIf(next.end > parent.end,
                pnL.m("AllocFirst",
                    pnL.p("parent", pnL.i("hid",
                                            pnL.p("beg", pnL.txt(parent.beg.ToString())),
                                            pnL.p("end", pnL.txt(parent.end.ToString())))),
                    pnL.eFails(pnL.txt("Parent Subspace is full- can't allocate new Hid"))));

            return next;
        }

        /// <summary>
        /// mko, 19.2.2019
        /// Erzeugt den nächsten Treenode unterhalb von parent. Wenn der verfügbare Subraum unterhalb 
        /// des parent erschöpft ist, dann wird eine ArgumentException geworfen
        /// </summary>
        /// <typeparam name="TCRef"></typeparam>
        /// <param name="cref"></param>
        /// <param name="parent"></param>
        /// <param name="last"></param>
        /// <returns></returns>
        public static Hid<TCRef> AllocNext<TCRef>(TCRef cref, Hid<TCRef> parent, Hid<TCRef> last, IComposer pnL)
        {
            var next = new Hid<TCRef>() { beg = last.end + 1, end = last.end + 1 + (last.end - last.beg) + 1, CRef = cref };

            TraceHlp.ThrowIndexOutOfRangeExceptionIf(next.end > parent.end, 
                pnL.m("AllocNext", 
                    pnL.p("parent", pnL.i("hid", 
                                            pnL.p("beg", pnL.txt(parent.beg.ToString())),
                                            pnL.p("end", pnL.txt(parent.end.ToString())))),
                    pnL.p("last", pnL.i("hid",
                                            pnL.p("beg", pnL.txt(last.beg.ToString())),
                                            pnL.p("end", pnL.txt(last.end.ToString())))),
                    pnL.eFails(pnL.txt("Parent Subspace is full- can't allocate new Hid"))));
            
            return next;
        }


        //-------------------------------------------------------------------------------------------------------------------------------
        // Abfragen auf einem Tree

        /// <summary>
        /// mko, 19.2.2019
        /// </summary>
        /// <typeparam name="TDescriptor"></typeparam>
        /// <param name="hid">hid des aktuellen Treenode</param>
        /// <param name="tree">Menge aller hids zum gesamten Baum</param>
        /// <returns></returns>
        public static IEnumerable<Hid<TCRef>> PathTo<TCRef>(Hid<TCRef> hid, IEnumerable<Hid<TCRef>> tree, IComposer pnL)
        {
            return tree.Where(r => r.beg <= hid.beg && hid.end <= r.end).OrderByDescending(r => r.end - r.beg);
        }


        /// <summary>
        /// mko, 19.2.2019
        /// Liefert das Elternelement zum aktuellen Knoten.
        /// </summary>
        /// <typeparam name="TCRef"></typeparam>
        /// <param name="hid">hid des aktuellen Treenode</param>
        /// <param name="tree">Menge aller hids zum gesamten Baum</param>
        /// <returns></returns>
        public static Hid<TCRef> ParentOf<TCRef>(Hid<TCRef> hid, IEnumerable<Hid<TCRef>> tree)
        {
            var pathRevers =  tree.Where(r => r.beg <= hid.beg && r.end >= hid.end).OrderBy(r => r.end - r.beg);

            Hid<TCRef> parent = hid;
            if(pathRevers.Count() > 1)
            {
                parent = pathRevers.Skip(1).First();
            }

            return parent;
        }

        /// <summary>
        /// mko, 19.2.2019
        /// Bestimmt alle Nachfahren zu einem hid
        /// </summary>
        /// <typeparam name="TCRef"></typeparam>
        /// <param name="hid"></param>
        /// <param name="tree"></param>
        /// <returns></returns>
        public static IEnumerable<Hid<TCRef>> DescendantsOf<TCRef>(Hid<TCRef> hid, IEnumerable<Hid<TCRef>> tree)
        {
            return tree.Where(r => r.beg >= hid.beg && r.end <= hid.end && r != hid);
        }

        /// <summary>
        /// mko, 19.2.2019
        /// Bestimmt alle unmittelbaren Nachfahren (Kinder) zu einer Hid.
        /// Die Menge der Kinder x ist bestimmt durch die Menge der Nachfahren von x, eingeschränkt
        /// auf die Menge jener, deren Elternknoten x ist.
        /// => Implementierung ineffizient, besser in Zukunft (z.B. Ordnen der Hids im Tree- Indexierung)...
        /// </summary>
        /// <typeparam name="TCRef"></typeparam>
        /// <param name="hid"></param>
        /// <param name="tree"></param>
        /// <returns></returns>
        public static IEnumerable<Hid<TCRef>> ChildsOf<TCRef>(Hid<TCRef> hid, IEnumerable<Hid<TCRef>> tree)
        {
            return tree.Where(r => r.beg >= hid.beg && r.end <= hid.end && r != hid && ParentOf(r, tree) == hid);
        }
    }
}
