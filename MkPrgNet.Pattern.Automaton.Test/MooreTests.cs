using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace MkPrgNet.Pattern.Automaton.Test
{
    [TestClass]
    public class MooreTests
    {

        enum Drehkreuz
        {
            Zu,
            Auf,
            Aus,
            Err
        }

        class InputBase : IInput
        {
            public InputBase(int prio)
            {
                Priority = prio;
            }

            public bool On
            {
                get
                {
                    return _on;
                }
            }
            protected bool _on = false;

            public int Priority
            {
                get;
            }


            public void Reset()
            {
                _on = false;
            }

        }

        /// <summary>
        /// Dieser Eingabefunktor dokumentiert einen Fehler. Nach dieser 
        /// Eingabe befindet man sich immer im Zustand Err.
        /// </summary>
        class Err : InputBase
        {
            public Err() : base(11)
            {
            }

            public void describe(string txt)
            {
                _on = true;
                _txt = txt;
            }

            public string ErrDescription
            {
                get
                {
                    return _txt;
                }
            }
            string _txt;

        }
        Err err;

        class EinwurfGeld : InputBase
        {
            public EinwurfGeld(Err err) : base(5)
            {
                this.err = err;
            }

            Err err;

            public void Einwurf(int Euro, int Cent)
            {
                if (Euro < 0 || Cent < 0)
                {
                    err.describe("ungültige Münzeinwürfe");
                }
                else
                {
                    _on = Euro == 2 && Cent == 50;
                }

            }
        }

        EinwurfGeld Zahlen;


        class Drehen : InputBase
        {
            public Drehen() : base(2) { }

            public void drehen()
            {
                _on = true;
            }
        }

        Drehen AmKreuz;

        class Ausschalten : InputBase
        {
            public Ausschalten() : base(10) { }

            public void aus()
            {
                _on = true;
            }
        }

        Ausschalten Aus;


        IAutomaton<Drehkreuz> DrehkreuzAutomat;

        [TestInitialize]
        public void Drehkreuz_anlegen()
        {
            var states = new Builder.Impl.MooreAutomatonBuilder<Drehkreuz>();

            states.DefineStateAsStart(Drehkreuz.Zu);
            states.DefineStateAsFinal(Drehkreuz.Aus);
            states.DefineStateAsFinal(Drehkreuz.Err);

            // Eingabefunktoren definieren
            err = new Err();
            Zahlen = new EinwurfGeld(err);
            AmKreuz = new Drehen();
            Aus = new Ausschalten();

            // ZustandsüberGänge definieren
            var transitions = states.CreateTransistionFunctionBuilder();
            transitions.DefTransistionFor(Zahlen, Drehkreuz.Auf, Drehkreuz.Auf, Drehkreuz.Aus, Drehkreuz.Err);
            transitions.DefTransistionFor(AmKreuz, Drehkreuz.Zu, Drehkreuz.Zu, Drehkreuz.Aus, Drehkreuz.Err);
            transitions.DefTransistionFor(Aus, Drehkreuz.Aus, Drehkreuz.Aus, Drehkreuz.Aus, Drehkreuz.Aus);
            transitions.DefTransistionFor(err, Drehkreuz.Err, Drehkreuz.Err, Drehkreuz.Err, Drehkreuz.Err);

            // Ausgabefunktionen definieren
            var outputs = transitions.CreateOutputFunctionBuilder();
            outputs.DefOutputFor(Drehkreuz.Auf, new OutputFunctor<Drehkreuz>((i, preS) => Debug.WriteLine("Bin offen")));
            outputs.DefOutputFor(Drehkreuz.Zu, new OutputFunctor<Drehkreuz>((i, preS) => Debug.WriteLine("Bin zu")));
            outputs.DefOutputFor(Drehkreuz.Aus, new OutputFunctor<Drehkreuz>((i, preS) => Debug.WriteLine("Bin aus")));
            outputs.DefOutputFor(Drehkreuz.Err, new OutputFunctor<Drehkreuz>((i, preS) =>
            {
                if (i is Err)
                {
                    Debug.WriteLine(((Err)i).ErrDescription);
                }
                else
                {
                    Debug.WriteLine("Habe einen Fehler");
                }

            }));


            DrehkreuzAutomat = outputs.CreateMooreAutomaton();
        }

        [TestMethod]
        public void Drehkreuz_testen()
        {

            DrehkreuzAutomat.Start();
            Assert.AreEqual(Drehkreuz.Zu, DrehkreuzAutomat.CurrentState);

            Zahlen.Einwurf(1, 0);
            DrehkreuzAutomat.Transistion();
            Assert.AreEqual(Drehkreuz.Zu, DrehkreuzAutomat.CurrentState);

            Zahlen.Einwurf(2, 50);
            DrehkreuzAutomat.Transistion();
            Assert.AreEqual(Drehkreuz.Auf, DrehkreuzAutomat.CurrentState);

            AmKreuz.drehen();
            DrehkreuzAutomat.Transistion();
            Assert.AreEqual(Drehkreuz.Zu, DrehkreuzAutomat.CurrentState);

            Zahlen.Einwurf(2, 50);
            Aus.aus();

            DrehkreuzAutomat.Transistion();
            Assert.AreEqual(Drehkreuz.Aus, DrehkreuzAutomat.CurrentState);

        }

        [TestMethod]
        public void Drehkreuz_Fehler_provozieren()
        {
            DrehkreuzAutomat.Start();
            Zahlen.Einwurf(-1, 0);
            DrehkreuzAutomat.Transistion();

            Assert.AreEqual(Drehkreuz.Err, DrehkreuzAutomat.CurrentState);       
        }

    }
}