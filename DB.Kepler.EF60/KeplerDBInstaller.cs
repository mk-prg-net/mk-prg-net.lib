using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using mko.Newton;

namespace DB.Kepler.EF60
{
    public class KeplerDBInstaller
    {

        /// <summary>
        /// Datenbank erzeugen, falls diese noch nicht existiert
        /// </summary>
        public static void CreateDB()
        {

            try
            {
                using (var ctx = new KeplerDBEntities())
                {
                    // Datenbank erzeugen

                    if (ctx.Database.Exists())
                        ctx.Database.Delete();

                    Debug.Assert(!ctx.Database.Exists());

                    ctx.Database.Create();

                    Debug.Assert(ctx.Database.Exists());


                }
            }
            catch (Exception ex)
            {

                string msg = mko.TraceHlp.FormatErrMsg("KeplerDBInstaller", "CreateDB", ex.Message);
                throw new Exception(msg, ex);

            }
        }

        /// <summary>
        /// Anlegen grundlegender Definitionstabellen
        /// </summary>
        public static void InitBaseTables()
        {
            try
            {
                using (var ctx = new KeplerDBEntities())
                {
                    // Basistabellen füllen

                    // 1) Himmelskörper
                    mko.algorithm.ForEachEnumMember<global::Kepler.Bo.HimmelskoerperTypen>.execute((Name, Id) =>
                    {
                        var Entity = ctx.HimmelskoerperTypenTab.Create();
                        Entity.ID = Id;
                        Entity.Name = Name;
                        ctx.HimmelskoerperTypenTab.Add(Entity);
                    });

                    // 2) Länder
                    Tuple<string, string>[] Laender = {   
                                                          new Tuple<string, string>("AT", "Österreich"),
                                                          new Tuple<string, string>("CH", "Schweiz"),
                                                          new Tuple<string, string>("CN", "China"),
                                                          new Tuple<string, string>("D", "Deutschland"),
                                                          new Tuple<string, string>("EU", "Europäische Union- ESA"),
                                                          new Tuple<string, string>("FR", "Frankreich"),
                                                          new Tuple<string, string>("GB", "Groß Britanien"),
                                                          new Tuple<string, string>("IL", "Israel"),
                                                          new Tuple<string, string>("IN", "Indien"),
                                                          new Tuple<string, string>("IR", "Iran"),
                                                          new Tuple<string, string>("JP", "Japan"),
                                                          new Tuple<string, string>("KP", "Nord Korea"),
                                                          new Tuple<string, string>("KZ", "Kasachstan"),
                                                          new Tuple<string, string>("RU", "Russland"),
                                                          new Tuple<string, string>("SU", "Sowjetunion"),
                                                          new Tuple<string, string>("UA", "Ukraine"),
                                                          new Tuple<string, string>("USA", "Vereinigte Staaten von Amerika")                                                          
                                                      };
                    foreach (var tp in Laender)
                    {
                        var Entity = ctx.LaenderTab.Create();
                        Entity.Laenderkennzeichen = tp.Item1;
                        Entity.Name = tp.Item2;
                        ctx.LaenderTab.Add(Entity);
                    }

                    // 3) Raumschiff- Aufgaben
                    string[] Aufgaben = { "Militär", "Metrologie", "Wissenschaft", "Land- und Forstwirtschaft", "Navigation", "Telekommunikation", "Prestige" };
                    foreach (var Aufg in Aufgaben)
                    {
                        var Entity = ctx.AufgabenTab.Create();
                        Entity.Aufgabenbeschreibung = Aufg;
                        ctx.AufgabenTab.Add(Entity);
                    }

                    // 4) Spektralklassen

                    var SpektCo = new Container.SpektralklassenCo(ctx);
                    foreach (var spekt in global::Kepler.Defs.Spektralklasse.ListeSpektralklassen.Keys)
                    {
                        var spektralklasse = global::Kepler.Defs.Spektralklasse.ListeSpektralklassen[spekt];

                        global::Kepler.ISpektralklasse Entity = SpektCo.CreateBo();

                        Entity.SpektralklasseId = spektralklasse.SpektralklasseId;
                        Entity.Farbe = spektralklasse.Farbe;
                        Entity.Tmin = spektralklasse.Tmin;
                        Entity.Tmax = spektralklasse.Tmax;
                        Entity.Masse_Hauptreihenstern_in_Sonnenmassen = spektralklasse.Masse_Hauptreihenstern_in_Sonnenmassen;

                        SpektCo.AddToCollection((Spektralklasse)Entity);

                    }

                    ctx.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                string msg = mko.TraceHlp.FormatErrMsg("KeplerDBInstaller", "CreateDB", ex.Message);
                throw new Exception(msg, ex);
            }
        }

        /// <summary>
        /// Anlegen eines Sterns, Planet oder Mond
        /// </summary>
        /// <param name="Context"></param>
        /// <param name="TypHK"></param>
        /// <param name="Name"></param>
        /// <param name="MasseKg"></param>
        /// <param name="DAequator_km"></param>
        /// <param name="DPol_km"></param>
        /// <param name="Fallbeschleunigung"></param>
        /// <param name="TOberflaeche_K"></param>
        /// <param name="DauerUmdrehung_h"></param>
        /// <param name="RotAchsneigung"></param>
        /// <returns></returns>
        static Himmelskoerper CreateSternPlanetMond(KeplerDBEntities Context,
                                        HimmelskoerperTyp TypHK,
                                        string Name,
                                        double MasseKg = 0,
                                        double DAequator_km = 0.0,
                                        double DPol_km = 0.0,
                                        double Fallbeschleunigung = 0.0,
                                        double TOberflaeche_K = 0.0,
                                        double DauerUmdrehung_h = 0.0,
                                        double RotAchsneigung = 0.0)
        {
            // Masterdatensatz als Proxy anlegen
            var HK = Context.HimmelskoerperTab.Create();
            HK.Name = Name;
            HK.Masse_in_kg = MasseKg;

            // Der Auflistung der Himmelskörper hinzufügen
            Context.HimmelskoerperTab.Add(HK);

            // Einem Himmelskörpertyp zuordnen
            TypHK.Himmelskoerper.Add(HK);

            // Details zu einem Stern/Planet/Mond anlegen
            var Details = Context.Sterne_Planeten_MondeTab.Create();

            Details.Oberflaechentemperatur_in_K = TOberflaeche_K;
            Details.Aequatordurchmesser_in_km = DAequator_km;
            Details.Polardurchmesser_in_km = DPol_km;
            Details.Fallbeschleunigung_in_meter_pro_sec = Fallbeschleunigung;
            Details.Rotationsachsenneigung_in_Grad = RotAchsneigung;
            Details.Rotationsperiode_in_Stunden = DauerUmdrehung_h;

            HK.Sterne_Planeten_MondeTab = Details;

            return HK;
        }


        static Himmelskoerper CreateRaumschiff(KeplerDBEntities Context,
                                                string Name,
                                                double MasseKg,
                                                Land Land,
                                                DateTime Start_Mission,
                                                params Aufgabe[] Aufgaben)
        {
            // Masterdatensatz: Raumschiff wird zuerst als Himmelskörper definiert
            var HK = Context.HimmelskoerperTab.Create();
            HK.Name = Name;
            HK.Masse_in_kg = MasseKg;

            // Der Auflistung der Himmelskörper hinzufügen
            Context.HimmelskoerperTab.Add(HK);

            // Dem Himmelskörpertyp Raumschiff zuordnen
            Context.HimmelskoerperTypenTab.Where(r => r.Name == "Raumschiff").Single().Himmelskoerper.Add(HK);

            // Detaildatensatz mit Daten zum Raumschiff anlegen
            var Raumschiff = Context.RaumschiffeTab.Create();

            Raumschiff.Land = Land;

            foreach (var aufg in Aufgaben)
            {
                Raumschiff.Aufgaben.Add(aufg);
            }

            Raumschiff.Start_der_Mission = Start_Mission;

            // Detaildatensatz an Masterdatensatz anhängen.
            HK.RaumschiffeTab = Raumschiff;

            return HK;

        }



        /// <summary>
        /// Anlegen einer Umlaufbahn
        /// </summary>
        /// <param name="Context"></param>
        /// <param name="UmlaufbahnZentralobjekt"></param>
        /// <param name="UmlaufbahnGrosseHalbachse_km"></param>
        /// <param name="UmlaufbahnExzentrizität"></param>
        /// <param name="Umlaufgeschwindigkeit_km_per_sec"></param>
        /// <param name="Umlaufdauer_Tage"></param>
        /// <returns></returns>
        static Umlaufbahn CreateUmlaufbahn(KeplerDBEntities Context,
                                                    Himmelskoerper UmlaufbahnZentralobjekt,
                                                    double UmlaufbahnGrosseHalbachse_km,
                                                    double UmlaufbahnExzentrizität,
                                                    double Umlaufgeschwindigkeit_km_per_sec,
                                                    double Umlaufdauer_Tage)
        {
            // Umlaufbahn beschreiben
            var Umlaufbahn = Context.UmlaufbahnenTab.Create();

            Umlaufbahn.Exzentritzitaet = UmlaufbahnExzentrizität;
            Umlaufbahn.Laenge_grosse_Halbachse_in_km = UmlaufbahnGrosseHalbachse_km;
            Umlaufbahn.Mittlere_Umlaufgeschwindigkeit_in_km_pro_sec = Umlaufgeschwindigkeit_km_per_sec;
            Umlaufbahn.Umlaufdauer_in_Tagen = Umlaufdauer_Tage;

            UmlaufbahnZentralobjekt.TrabantenUmlaufbahnen.Add(Umlaufbahn);

            return Umlaufbahn;
        }



        /// <summary>
        /// Datenbank mit Datensätzen zu Sternen, Planeten und Monde füllen
        /// </summary>
        public static void FillDBWithStarsPlanetsAndMoons()
        {
            try
            {
                using (var ctx = new KeplerDBEntities())
                {
                    mko.Newton.Init.Do();

                    // Container für Geschäftsobjekte anlegen
                    var Spektralklassen = new Container.SpektralklassenCo(ctx);
                    var Galaxieen = new Container.GalaxieenCo(ctx);
                    var Sterne = new Container.SterneCo(ctx);
                    var Planeten = new Container.PlanetenCo(ctx);
                    var Monde = new Container.MondeCo(ctx);
                    var Raumschiffe = new Container.RaumschiffeCo(ctx);


                    var TypUrknall = ctx.HimmelskoerperTypenTab.Single(r => r.ID == (int)global::Kepler.Bo.HimmelskoerperTypen.Urknall);
                    var TypGalaxiekern = ctx.HimmelskoerperTypenTab.Single(r => r.ID == (int)global::Kepler.Bo.HimmelskoerperTypen.Galaxiekern);
                    var TypGalaxie = ctx.HimmelskoerperTypenTab.Single(r => r.ID == (int)global::Kepler.Bo.HimmelskoerperTypen.Galaxie);
                    var TypStern = ctx.HimmelskoerperTypenTab.Single(r => r.ID == (int)global::Kepler.Bo.HimmelskoerperTypen.Stern);
                    var TypPlanet = ctx.HimmelskoerperTypenTab.Single(r => r.ID == (int)global::Kepler.Bo.HimmelskoerperTypen.Planet);
                    var TypMond = ctx.HimmelskoerperTypenTab.Single(r => r.ID == (int)global::Kepler.Bo.HimmelskoerperTypen.Mond);
                    var TypAsteroid = ctx.HimmelskoerperTypenTab.Single(r => r.ID == (int)global::Kepler.Bo.HimmelskoerperTypen.Asteroid);
                    var TypKomet = ctx.HimmelskoerperTypenTab.Single(r => r.ID == (int)global::Kepler.Bo.HimmelskoerperTypen.Komet);
                    var TypRaumschiff = ctx.HimmelskoerperTypenTab.Single(r => r.ID == (int)global::Kepler.Bo.HimmelskoerperTypen.Raumschiff);

                    // Wir erschaffen die Welt: 
                    // Zentrum des Universums ist der Urknall, von dem alles wegfliegt
                    var Urknall = CreateSternPlanetMond(
                        Context: ctx,
                        TypHK: TypUrknall,
                        Name: "Urknall",
                        MasseKg: -1,
                        DAequator_km: 0,
                        DPol_km: 0,
                        Fallbeschleunigung: -1,
                        TOberflaeche_K: -1,
                        DauerUmdrehung_h: 0,
                        RotAchsneigung: 0);

                    ctx.SaveChanges();


                    // als nächstes ein paar Galaxieen
                    global::Kepler.IGalaxie Milchstrasse = Galaxieen.CreateBoAndAddToCollection();
                    Milchstrasse.Name = "Milchstrasse";
                    Milchstrasse.Masse_in_millionen_Sonnenmassen = 1.4e12;
                    Milchstrasse.Polardurchmesser_in_km = mko.Newton.Length.Kilometer(mko.Newton.Length.Lightyear(16000)).Vector.Length;
                    Milchstrasse.Aequatordurchmesser_in_km = mko.Newton.Length.Kilometer(mko.Newton.Length.Lightyear(100000)).Vector.Length;



                    // Sonne anlegen, indem ein neues Objekt dem Sterne- Container hinzugefügt wird
                    global::Kepler.IStern Sonne = Sterne.CreateBoAndAddToCollection();                    

                    Sonne.Name =  "Sonne";
                    Sonne.Masse_in_kg = mko.Newton.Mass.MassOfSun.Value;
                    Sonne.Aequatordurchmesser_in_km = mko.Newton.Length.DiameterSun.Vector.Length;
                    Sonne.Polardurchmesser_in_km = mko.Newton.Length.DiameterSun.Vector.Length;
                    Sonne.Fallbeschleunigung_in_meter_pro_sec = mko.Newton.Acceleration.GravityOnSun.Vector.Length;
                    Sonne.Oberflaechentemperatur_in_K = 6000;
                    Sonne.Rotationsperiode_in_Stunden = mko.Newton.Time.SideralRotationPeriodSun.Value;
                    Sonne.Rotationsachsenneigung_in_Grad = mko.Newton.Angle.AxialTiltOfSun.Vector.Length;

                    Sonne.Umlaufbahn.Zentralobjekt = Milchstrasse;
                    Sonne.Umlaufbahn.Laenge_grosse_Halbachse_in_km = mko.Newton.Length.Kilometer(mko.Newton.Length.Lightyear(28000)).Vector.Length;


                    Sonne.Spektralklasse = Spektralklassen.GetSpektralklasse(global::Kepler.SpektralklasseID.G);

                    // Beteigeuze
                    global::Kepler.IStern Beteigeuze = Sterne.CreateBoAndAddToCollection();                    

                    Beteigeuze.Name = "Beteigeuze";
                    Beteigeuze.Aequatordurchmesser_in_km = 662 * mko.Newton.Length.Kilometer(mko.Newton.Length.DiameterSun).Vector.Length;
                    Beteigeuze.Masse_in_kg = 20.0 * mko.Newton.Mass.Kilogram(mko.Newton.Mass.MassOfSun).Value;
                    Beteigeuze.Oberflaechentemperatur_in_K = 3450;

                    Beteigeuze.Umlaufbahn.Zentralobjekt = Milchstrasse;
                    Beteigeuze.Umlaufbahn.Laenge_grosse_Halbachse_in_km = mko.Newton.Length.Kilometer(mko.Newton.Length.Lightyear(28000)).Vector.Length;

                    Beteigeuze.Spektralklasse = Spektralklassen.GetSpektralklasse(global::Kepler.SpektralklasseID.M);

                    ctx.SaveChanges();
                                       

                    // Merkur
                    global::Kepler.IPlanet Merkur = Planeten.CreateBoAndAddToCollection();                    

                    Merkur.Name =  "Merkur";
                    Merkur.Masse_in_kg = mko.Newton.Mass.MassOfMercury.Value;
                    Merkur.Aequatordurchmesser_in_km= mko.Newton.Length.DiameterMercury.Vector.Length;
                    Merkur.Polardurchmesser_in_km = mko.Newton.Length.DiameterMercury.Vector.Length;
                    Merkur.Fallbeschleunigung_in_meter_pro_sec = mko.Newton.Acceleration.GravityOnMercury.Vector.Length;
                    Merkur.Oberflaechentemperatur_in_K =  0;
                    Merkur.Rotationsperiode_in_Stunden = mko.Newton.Time.SideralRotationPeriodMercury.Value;
                    Merkur.Rotationsachsenneigung_in_Grad = mko.Newton.Angle.AxialTiltOfMercury.Vector.Length;                    

                    Merkur.Umlaufbahn.Zentralobjekt = Sonne;
                    Merkur.Umlaufbahn.Laenge_grosse_Halbachse_in_km = mko.Newton.Length.Kilometer(mko.Newton.Length.SemiMajorAxisMercury).Vector.Length;
                    Merkur.Umlaufbahn.Exzentritzitaet= 0.205630;
                    Merkur.Umlaufbahn.Mittlere_Umlaufgeschwindigkeit_in_km_pro_sec =  mko.Newton.Velocity.VelocityOfMercury.Vector.Length;
                    Merkur.Umlaufbahn.Umlaufdauer_in_Tagen = mko.Newton.Time.OrbitalPeriodMercury.Value;

                    ctx.SaveChanges();

                    // Venus
                    global::Kepler.IPlanet Venus = Planeten.CreateBoAndAddToCollection();                    

                    Venus.Name =  "Venus";
                    Venus.Masse_in_kg = mko.Newton.Mass.MassOfVenus.Value;
                    Venus.Aequatordurchmesser_in_km = mko.Newton.Length.DiameterVenus.Vector.Length;
                    Venus.Polardurchmesser_in_km = mko.Newton.Length.DiameterVenus.Vector.Length;
                    Venus.Fallbeschleunigung_in_meter_pro_sec = mko.Newton.Acceleration.GravityOnVenus.Vector.Length;
                    Venus.Oberflaechentemperatur_in_K = 470;
                    Venus.Rotationsperiode_in_Stunden = mko.Newton.Time.SideralRotationPeriodVenus.Value;
                    Venus.Rotationsachsenneigung_in_Grad = mko.Newton.Angle.AxialTiltOfVenus.Vector.Length;

                    Venus.Umlaufbahn.Zentralobjekt= Sonne;
                    Venus.Umlaufbahn.Laenge_grosse_Halbachse_in_km = mko.Newton.Length.Kilometer(mko.Newton.Length.SemiMajorAxisVenus).Vector.Length;
                    Venus.Umlaufbahn.Exzentritzitaet = 0.006756;
                    Venus.Umlaufbahn.Mittlere_Umlaufgeschwindigkeit_in_km_pro_sec = mko.Newton.Velocity.VelocityOfVenus.Vector.Length;
                    Venus.Umlaufbahn.Umlaufdauer_in_Tagen = mko.Newton.Time.OrbitalPeriodVenus.Value;

                    ctx.SaveChanges();


                    // Erde
                    global::Kepler.IPlanet Erde = Planeten.CreateBoAndAddToCollection();                    

                    Erde.Name =  "Erde";
                    Erde.Masse_in_kg = mko.Newton.Mass.MassOfEarth.Value;
                    Erde.Aequatordurchmesser_in_km = mko.Newton.Length.DiameterEarth.Vector.Length;
                    Erde.Polardurchmesser_in_km = mko.Newton.Length.DiameterEarthPolar.Vector.Length;
                    Erde.Fallbeschleunigung_in_meter_pro_sec = mko.Newton.Acceleration.GravityOnEarth.Vector.Length;
                    Erde.Oberflaechentemperatur_in_K = 15;
                    Erde.Rotationsperiode_in_Stunden= mko.Newton.Time.SideralRotationPeriodEarth.Value;
                    Erde.Rotationsachsenneigung_in_Grad = mko.Newton.Angle.AxialTiltOfEarth.Vector.Length;

                    Erde.Umlaufbahn.Zentralobjekt =  Sonne;
                    Erde.Umlaufbahn.Laenge_grosse_Halbachse_in_km =mko.Newton.Length.Kilometer(mko.Newton.Length.SemiMajorAxisEarth).Vector.Length;
                    Erde.Umlaufbahn.Exzentritzitaet = 0.01671123;
                    Erde.Umlaufbahn.Mittlere_Umlaufgeschwindigkeit_in_km_pro_sec = mko.Newton.Velocity.VelocityOfEarth.Vector.Length;
                    Erde.Umlaufbahn.Umlaufdauer_in_Tagen = mko.Newton.Time.OrbitalPeriodEarth.Value;

                    ctx.SaveChanges();

                    // Erdmond
                    global::Kepler.IMond Mond = Monde.CreateBoAndAddToCollection();                    

                    Mond.Name =  "Mond";
                    Mond.Masse_in_kg = mko.Newton.Mass.MassOfEarthMoon.Value;
                    Mond.Aequatordurchmesser_in_km = mko.Newton.Length.DiameterEarthMoon.Vector.Length;
                    Mond.Polardurchmesser_in_km = mko.Newton.Length.DiameterEarthMoon.Vector.Length;
                    Mond.Fallbeschleunigung_in_meter_pro_sec = mko.Newton.Acceleration.GravityOnEarthMoon.Vector.Length;
                    Mond.Oberflaechentemperatur_in_K = 0;
                    Mond.Rotationsperiode_in_Stunden= mko.Newton.Time.SideralRotationPeriodEarthMoon.Value;
                    Mond.Rotationsachsenneigung_in_Grad = mko.Newton.Angle.AxialTiltOfEarthMoon.Vector.Length;

                    Mond.Umlaufbahn.Zentralobjekt = Erde;
                    Mond.Umlaufbahn.Laenge_grosse_Halbachse_in_km= mko.Newton.Length.Kilometer(mko.Newton.Length.SemiMajorAxisEarthMoon).Vector.Length;
                    Mond.Umlaufbahn.Exzentritzitaet =  0.0549;
                    Mond.Umlaufbahn.Mittlere_Umlaufgeschwindigkeit_in_km_pro_sec = mko.Newton.Velocity.VelocityOfEarthMoon.Vector.Length;
                    Mond.Umlaufbahn.Umlaufdauer_in_Tagen = mko.Newton.Time.OrbitalPeriodEarthMoon.Value;

                    ctx.SaveChanges();

                    // Mars
                    var Mars = CreateSternPlanetMond(
                        Context: ctx,
                        TypHK: TypPlanet,
                        Name: "Mars",
                        MasseKg: mko.Newton.Mass.MassOfMars.Value,
                        DAequator_km: mko.Newton.Length.DiameterMars.Vector.Length,
                        DPol_km: mko.Newton.Length.DiameterMarsPolar.Vector.Length,
                        Fallbeschleunigung: mko.Newton.Acceleration.GravityOnMars.Vector.Length,
                        TOberflaeche_K: -12.5,
                        DauerUmdrehung_h: mko.Newton.Time.SideralRotationPeriodMars.Value,
                        RotAchsneigung: mko.Newton.Angle.AxialTiltOfMars.Vector.Length);

                    Mars.Umlaufbahn = CreateUmlaufbahn(
                        Context: ctx,
                        UmlaufbahnZentralobjekt: (Himmelskoerper) Sonne,
                        UmlaufbahnGrosseHalbachse_km: mko.Newton.Length.Kilometer(mko.Newton.Length.SemiMajorAxisMars).Vector.Length,
                        UmlaufbahnExzentrizität: 0.093315,
                        Umlaufgeschwindigkeit_km_per_sec: mko.Newton.Velocity.VelocityOfMars.Vector.Length,
                        Umlaufdauer_Tage: mko.Newton.Time.OrbitalPeriodMars.Value);

                    var MarsPhobos = CreateSternPlanetMond(
                        Context: ctx,
                        TypHK: TypMond,
                        Name: "Phobos",
                        MasseKg: mko.Newton.Mass.MassOfMarsMoonPhobos.Value,
                        DAequator_km: mko.Newton.Length.DiameterMarsMoonPhobos.Vector.Length,
                        DPol_km: mko.Newton.Length.DiameterMarsMoonPhobos.Vector.Length,
                        Fallbeschleunigung: mko.Newton.Acceleration.GravityOnMarsMoonPhobos.Vector.Length,
                        TOberflaeche_K: 0,
                        DauerUmdrehung_h: mko.Newton.Time.SideralRotationPeriodMarsMoonPhobos.Value,
                        RotAchsneigung: mko.Newton.Angle.AxialTiltOfMarsMoonPhobos.Vector.Length);

                    MarsPhobos.Umlaufbahn = CreateUmlaufbahn(
                        Context: ctx,
                        UmlaufbahnZentralobjekt: Mars,
                        UmlaufbahnGrosseHalbachse_km: mko.Newton.Length.Kilometer(mko.Newton.Length.SemiMajorAxisMoonPhobos).Vector.Length,
                        UmlaufbahnExzentrizität: 0.0002,
                        Umlaufgeschwindigkeit_km_per_sec: mko.Newton.Velocity.VelocityOfMarsMoonPhobos.Vector.Length,
                        Umlaufdauer_Tage: mko.Newton.Time.OrbitalPeriodMarsMoonPhobos.Value);

                    var MarsDeimos = CreateSternPlanetMond(
                        Context: ctx,
                        TypHK: TypMond,
                        Name: "Deimos",
                        MasseKg: mko.Newton.Mass.MassOfMarsMoonDeimos.Value,
                        DAequator_km: mko.Newton.Length.DiameterMarsMoonDeimos.Vector.Length,
                        DPol_km: mko.Newton.Length.DiameterMarsMoonDeimos.Vector.Length,
                        Fallbeschleunigung: mko.Newton.Acceleration.GravityOnMarsMoonDeimos.Vector.Length,
                        TOberflaeche_K: 0,
                        DauerUmdrehung_h: mko.Newton.Time.SideralRotationPeriodMarsMoonDeimos.Value,
                        RotAchsneigung: mko.Newton.Angle.AxialTiltOfMarsMoonDeimos.Vector.Length);

                    MarsDeimos.Umlaufbahn = CreateUmlaufbahn(
                        Context: ctx,
                        UmlaufbahnZentralobjekt: Mars,
                        UmlaufbahnGrosseHalbachse_km: mko.Newton.Length.Kilometer(mko.Newton.Length.SemiMajorAxisMoonDeimos).Vector.Length,
                        UmlaufbahnExzentrizität: 0.0002,
                        Umlaufgeschwindigkeit_km_per_sec: mko.Newton.Velocity.VelocityOfMarsMoonDeimos.Vector.Length,
                        Umlaufdauer_Tage: mko.Newton.Time.OrbitalPeriodMarsMoonDeimos.Value);

                    // Jupiter
                    var Jupiter = CreateSternPlanetMond(
                        Context: ctx,
                        TypHK: TypPlanet,
                        Name: "Jupiter",
                        MasseKg: mko.Newton.Mass.MassOfJupiter.Value,
                        DAequator_km: mko.Newton.Length.DiameterJupiter.Vector.Length,
                        DPol_km: mko.Newton.Length.DiameterJupiter.Vector.Length,
                        Fallbeschleunigung: mko.Newton.Acceleration.GravityOnJupiter.Vector.Length,
                        TOberflaeche_K: -100,
                        DauerUmdrehung_h: mko.Newton.Time.SideralRotationPeriodJupiter.Value,
                        RotAchsneigung: mko.Newton.Angle.AxialTiltOfJupiter.Vector.Length);

                    Jupiter.Umlaufbahn = CreateUmlaufbahn(
                        Context: ctx,
                        UmlaufbahnZentralobjekt: (Himmelskoerper) Sonne,
                        UmlaufbahnGrosseHalbachse_km: mko.Newton.Length.Kilometer(mko.Newton.Length.SemiMajorAxisJupiter).Vector.Length,
                        UmlaufbahnExzentrizität: 0.048775,
                        Umlaufgeschwindigkeit_km_per_sec: mko.Newton.Velocity.VelocityOfJupiter.Vector.Length,
                        Umlaufdauer_Tage: mko.Newton.Time.OrbitalPeriodJupiter.Value);

                    // Jupiter
                    var Ganymede = CreateSternPlanetMond(
                        Context: ctx,
                        TypHK: TypMond,
                        Name: "Ganymede",
                        MasseKg: mko.Newton.Mass.MassOfJupiterMoonGanymede.Value,
                        DAequator_km: mko.Newton.Length.DiameterJupiterMoonGanymede.Vector.Length,
                        DPol_km: mko.Newton.Length.DiameterJupiterMoonGanymede.Vector.Length,
                        Fallbeschleunigung: mko.Newton.Acceleration.GravityOnJupiterMoonGanymede.Vector.Length,
                        TOberflaeche_K: 0,
                        DauerUmdrehung_h: mko.Newton.Time.SideralRotationPeriodJupiterMoonGanymede.Value,
                        RotAchsneigung: mko.Newton.Angle.AxialTiltOfJupiterMoonGanymede.Vector.Length);

                    Ganymede.Umlaufbahn = CreateUmlaufbahn(
                        Context: ctx,
                        UmlaufbahnZentralobjekt: Jupiter,
                        UmlaufbahnGrosseHalbachse_km: mko.Newton.Length.Kilometer(mko.Newton.Length.SemiMajorAxisJupiterMoonGanymede).Vector.Length,
                        UmlaufbahnExzentrizität: 0.0013,
                        Umlaufgeschwindigkeit_km_per_sec: mko.Newton.Velocity.VelocityOfJupiterMoonGanymede.Vector.Length,
                        Umlaufdauer_Tage: mko.Newton.Time.OrbitalPeriodJupiterMoonGanymede.Value);

                    var Callisto = CreateSternPlanetMond(
                        Context: ctx,
                        TypHK: TypMond,
                        Name: "Callisto",
                        MasseKg: mko.Newton.Mass.MassOfJupiterMoonCallisto.Value,
                        DAequator_km: mko.Newton.Length.DiameterJupiterMoonCallisto.Vector.Length,
                        DPol_km: mko.Newton.Length.DiameterJupiterMoonCallisto.Vector.Length,
                        Fallbeschleunigung: mko.Newton.Acceleration.GravityOnJupiterMoonCallisto.Vector.Length,
                        TOberflaeche_K: 0,
                        DauerUmdrehung_h: mko.Newton.Time.SideralRotationPeriodJupiterMoonCallisto.Value,
                        RotAchsneigung: mko.Newton.Angle.AxialTiltOfJupiterMoonCallisto.Vector.Length);

                    Callisto.Umlaufbahn = CreateUmlaufbahn(
                        Context: ctx,
                        UmlaufbahnZentralobjekt: Jupiter,
                        UmlaufbahnGrosseHalbachse_km: mko.Newton.Length.Kilometer(mko.Newton.Length.SemiMajorAxisJupiterMoonCallisto).Vector.Length,
                        UmlaufbahnExzentrizität: 0.007,
                        Umlaufgeschwindigkeit_km_per_sec: mko.Newton.Velocity.VelocityOfJupiterMoonCallisto.Vector.Length,
                        Umlaufdauer_Tage: mko.Newton.Time.OrbitalPeriodJupiterMoonCallisto.Value);

                    var Io = CreateSternPlanetMond(
                        Context: ctx,
                        TypHK: TypMond,
                        Name: "Io",
                        MasseKg: mko.Newton.Mass.MassOfJupiterMoonIo.Value,
                        DAequator_km: mko.Newton.Length.DiameterJupiterMoonIo.Vector.Length,
                        DPol_km: mko.Newton.Length.DiameterJupiterMoonIo.Vector.Length,
                        Fallbeschleunigung: mko.Newton.Acceleration.GravityOnJupiterMoonIo.Vector.Length,
                        TOberflaeche_K: 0,
                        DauerUmdrehung_h: mko.Newton.Time.SideralRotationPeriodJupiterMoonIo.Value,
                        RotAchsneigung: mko.Newton.Angle.AxialTiltOfJupiterMoonIo.Vector.Length);

                    Io.Umlaufbahn = CreateUmlaufbahn(
                        Context: ctx,
                        UmlaufbahnZentralobjekt: Jupiter,
                        UmlaufbahnGrosseHalbachse_km: mko.Newton.Length.Kilometer(mko.Newton.Length.SemiMajorAxisJupiterMoonIo).Vector.Length,
                        UmlaufbahnExzentrizität: 0.0041,
                        Umlaufgeschwindigkeit_km_per_sec: mko.Newton.Velocity.VelocityOfJupiterMoonIo.Vector.Length,
                        Umlaufdauer_Tage: mko.Newton.Time.OrbitalPeriodJupiterMoonIo.Value);

                    var Europa = CreateSternPlanetMond(
                        Context: ctx,
                        TypHK: TypMond,
                        Name: "Europa",
                        MasseKg: mko.Newton.Mass.MassOfJupiterMoonEuropa.Value,
                        DAequator_km: mko.Newton.Length.DiameterJupiterMoonEuropa.Vector.Length,
                        DPol_km: mko.Newton.Length.DiameterJupiterMoonEuropa.Vector.Length,
                        Fallbeschleunigung: mko.Newton.Acceleration.GravityOnJupiterMoonEuropa.Vector.Length,
                        TOberflaeche_K: 0,
                        DauerUmdrehung_h: mko.Newton.Time.SideralRotationPeriodJupiterMoonEuropa.Value,
                        RotAchsneigung: mko.Newton.Angle.AxialTiltOfJupiterMoonEuropa.Vector.Length);

                    Europa.Umlaufbahn = CreateUmlaufbahn(
                        Context: ctx,
                        UmlaufbahnZentralobjekt: Jupiter,
                        UmlaufbahnGrosseHalbachse_km: mko.Newton.Length.Kilometer(mko.Newton.Length.SemiMajorAxisJupiterMoonEuropa).Vector.Length,
                        UmlaufbahnExzentrizität: 0.009,
                        Umlaufgeschwindigkeit_km_per_sec: mko.Newton.Velocity.VelocityOfJupiterMoonEuropa.Vector.Length,
                        Umlaufdauer_Tage: mko.Newton.Time.OrbitalPeriodJupiterMoonEuropa.Value);

                    // Saturn
                    var Saturn = CreateSternPlanetMond(
                        Context: ctx,
                        TypHK: TypPlanet,
                        Name: "Saturn",
                        MasseKg: mko.Newton.Mass.MassOfSaturn.Value,
                        DAequator_km: mko.Newton.Length.DiameterSaturn.Vector.Length,
                        DPol_km: mko.Newton.Length.DiameterSaturnPolar.Vector.Length,
                        Fallbeschleunigung: mko.Newton.Acceleration.GravityOnSaturn.Vector.Length,
                        TOberflaeche_K: -100,
                        DauerUmdrehung_h: mko.Newton.Time.SideralRotationPeriodSaturn.Value,
                        RotAchsneigung: mko.Newton.Angle.AxialTiltOfSaturn.Vector.Length);

                    Saturn.Umlaufbahn = CreateUmlaufbahn(
                        Context: ctx,
                        UmlaufbahnZentralobjekt: (Himmelskoerper)Sonne,
                        UmlaufbahnGrosseHalbachse_km: mko.Newton.Length.Kilometer(mko.Newton.Length.SemiMajorAxisSaturn).Vector.Length,
                        UmlaufbahnExzentrizität: 0.055723219,
                        Umlaufgeschwindigkeit_km_per_sec: mko.Newton.Velocity.VelocityOfSaturn.Vector.Length,
                        Umlaufdauer_Tage: mko.Newton.Time.OrbitalPeriodSaturn.Value);

                    // Titan
                    var Titan = CreateSternPlanetMond(
                        Context: ctx,
                        TypHK: TypMond,
                        Name: "Titan",
                        MasseKg: mko.Newton.Mass.MassOfSaturnMoonTitan.Value,
                        DAequator_km: mko.Newton.Length.DiameterSaturnMoonTitan.Vector.Length,
                        DPol_km: mko.Newton.Length.DiameterSaturnMoonTitan.Vector.Length,
                        Fallbeschleunigung: mko.Newton.Acceleration.GravityOnSaturnMoonTitan.Vector.Length,
                        TOberflaeche_K: -179,
                        DauerUmdrehung_h: mko.Newton.Time.Days(15.945).Value,
                        RotAchsneigung: mko.Newton.Angle.Degree(0.33).Vector[0]);

                    Titan.Umlaufbahn = CreateUmlaufbahn(
                        Context: ctx,
                        UmlaufbahnZentralobjekt: Saturn,
                        UmlaufbahnGrosseHalbachse_km: mko.Newton.Length.Kilometer(mko.Newton.Length.SemiMajorAxisSaturnMoonTitan).Vector.Length,
                        UmlaufbahnExzentrizität: 0.009,
                        Umlaufgeschwindigkeit_km_per_sec: mko.Newton.Velocity.VelocityOfSaturnMoonTitan.Vector.Length,
                        Umlaufdauer_Tage: mko.Newton.Time.Days(15.945).Value);

                    // Enceladus
                    var Enceladus = CreateSternPlanetMond(
                        Context: ctx,
                        TypHK: TypMond,
                        Name: "Enceladus",
                        MasseKg: mko.Newton.Mass.MassOfSaturnMoonEnceladus.Value,
                        DAequator_km: mko.Newton.Length.DiameterSaturnMoonEnceladus.Vector.Length,
                        DPol_km: mko.Newton.Length.DiameterSaturnMoonEnceladus.Vector.Length,
                        Fallbeschleunigung: mko.Newton.Acceleration.GravityOnSaturnMoonEnceladus.Vector.Length,
                        TOberflaeche_K: -240,
                        DauerUmdrehung_h: mko.Newton.Time.Days(1.370217824).Value,
                        RotAchsneigung: mko.Newton.Angle.Degree(0.019).Vector[0]);

                    Enceladus.Umlaufbahn = CreateUmlaufbahn(
                        Context: ctx,
                        UmlaufbahnZentralobjekt: Saturn,
                        UmlaufbahnGrosseHalbachse_km: mko.Newton.Length.Kilometer(mko.Newton.Length.SemiMajorAxisSaturnMoonEnceladus).Vector.Length,
                        UmlaufbahnExzentrizität: 0.0047,
                        Umlaufgeschwindigkeit_km_per_sec: mko.Newton.Velocity.VelocityOfSaturnMoonEnceladus.Vector.Length,
                        Umlaufdauer_Tage: mko.Newton.Time.Days(1.370217824).Value);

                    // Reha	Mond	Saturn	527040	km 	1532	2,317E+21
                    global::Kepler.IMond Reha = Monde.CreateBoAndAddToCollection();                    

                    Reha.Name = "Reha";
                    Reha.Masse_in_kg = 2.317E+21;
                    Reha.Aequatordurchmesser_in_km = 1532;
                    Reha.Umlaufbahn.Zentralobjekt = Saturn;
                    Reha.Umlaufbahn.Laenge_grosse_Halbachse_in_km = 527040;


                    // Dione	Mond	Saturn	377420	km	1127,6	1,096E+21
                    global::Kepler.IMond Dione = Monde.CreateBoAndAddToCollection();
                    
                    Dione.Name = "Dione";
                    Dione.Masse_in_kg = 1.096E+21;
                    Dione.Aequatordurchmesser_in_km = 1127.6;
                    Dione.Umlaufbahn.Zentralobjekt = Saturn;
                    Dione.Umlaufbahn.Laenge_grosse_Halbachse_in_km = 377420;

                    // Iapetus	Mond	Saturn	3561300	km	1436	1,600E+21
                    global::Kepler.IMond Iapetus = Monde.CreateBoAndAddToCollection();                    

                    Iapetus.Name = "Iapetus";
                    Iapetus.Masse_in_kg = 1.600E+21;
                    Iapetus.Aequatordurchmesser_in_km = 1436;
                    Iapetus.Umlaufbahn.Zentralobjekt = Saturn;
                    Iapetus.Umlaufbahn.Laenge_grosse_Halbachse_in_km = 3561300;

                    // Tethys	Mond	Saturn	294619	km	1062	6,174E+20
                    global::Kepler.IMond Tethys = Monde.CreateBoAndAddToCollection();                    

                    Tethys.Name = "Tethys";
                    Tethys.Masse_in_kg = 6.174E+20;
                    Tethys.Aequatordurchmesser_in_km = 1062;
                    Tethys.Umlaufbahn.Zentralobjekt = Saturn;
                    Tethys.Umlaufbahn.Laenge_grosse_Halbachse_in_km = 294619;


                    // Uranus
                    var Uranus = CreateSternPlanetMond(
                        Context: ctx,
                        TypHK: TypPlanet,
                        Name: "Uranus",
                        MasseKg: mko.Newton.Mass.MassOfUranus.Value,
                        DAequator_km: mko.Newton.Length.DiameterUranus.Vector.Length,
                        DPol_km: mko.Newton.Length.DiameterUranusPolar.Vector.Length,
                        Fallbeschleunigung: mko.Newton.Acceleration.GravityOnUranus.Vector.Length,
                        TOberflaeche_K: -100,
                        DauerUmdrehung_h: mko.Newton.Time.SideralRotationPeriodUranus.Value,
                        RotAchsneigung: mko.Newton.Angle.AxialTiltOfUranus.Vector.Length);

                    Uranus.Umlaufbahn = CreateUmlaufbahn(
                        Context: ctx,
                        UmlaufbahnZentralobjekt: (Himmelskoerper)Sonne,
                        UmlaufbahnGrosseHalbachse_km: mko.Newton.Length.Kilometer(mko.Newton.Length.SemiMajorAxisUranus).Vector.Length,
                        UmlaufbahnExzentrizität: 0.044405586,
                        Umlaufgeschwindigkeit_km_per_sec: mko.Newton.Velocity.VelocityOfUranus.Vector.Length,
                        Umlaufdauer_Tage: mko.Newton.Time.OrbitalPeriodUranus.Value);


                    // Ariel	Mond	Uranus	190900	km	1157,8	1,353E+21
                    global::Kepler.IMond Ariel = Monde.CreateBoAndAddToCollection();

                    Ariel.Name = "Ariel";
                    Ariel.Masse_in_kg = 1.353E+21;
                    Ariel.Aequatordurchmesser_in_km = 1157.8;
                    Ariel.Umlaufbahn.Zentralobjekt = Uranus;
                    Ariel.Umlaufbahn.Laenge_grosse_Halbachse_in_km = 190900;


                    // Umbriel	Mond	Uranus	266300	km	1169	1,172E+21
                    global::Kepler.IMond Umbriel = Monde.CreateBoAndAddToCollection();

                    Umbriel.Name = "Umbriel";
                    Umbriel.Masse_in_kg = 1.172E+21;
                    Umbriel.Aequatordurchmesser_in_km = 1169;
                    Umbriel.Umlaufbahn.Zentralobjekt = Uranus;
                    Umbriel.Umlaufbahn.Laenge_grosse_Halbachse_in_km = 266300;

                    // Titania	Mond	Uranus	436300	km	1577,8	3,527E+21
                    global::Kepler.IMond Titania = Monde.CreateBoAndAddToCollection();

                    Titania.Name = "Titania";
                    Titania.Masse_in_kg = 3.527E+21;
                    Titania.Aequatordurchmesser_in_km = 1577.8;
                    Titania.Umlaufbahn.Zentralobjekt = Uranus;
                    Titania.Umlaufbahn.Laenge_grosse_Halbachse_in_km = 436300;

                    // Oberon	Mond	Uranus	583519	km	1522,8	3,014E+21
                    global::Kepler.IMond Oberon = Monde.CreateBoAndAddToCollection();

                    Oberon.Name = "Oberon";
                    Oberon.Masse_in_kg = 3.014E+21;
                    Oberon.Aequatordurchmesser_in_km = 1522.8;
                    Oberon.Umlaufbahn.Zentralobjekt = Uranus;
                    Oberon.Umlaufbahn.Laenge_grosse_Halbachse_in_km = 583519;

                    // Miranda	Mond	Uranus	129872	km	471,6	6,590E+19
                    global::Kepler.IMond Miranda = Monde.CreateBoAndAddToCollection();

                    Miranda.Name = "Miranda";
                    Miranda.Masse_in_kg = 6.590E+19;
                    Miranda.Aequatordurchmesser_in_km = 471.6;
                    Miranda.Umlaufbahn.Zentralobjekt = Uranus;
                    Miranda.Umlaufbahn.Laenge_grosse_Halbachse_in_km = 129872;



                    // Neptun
                    var Neptun = CreateSternPlanetMond(
                        Context: ctx,
                        TypHK: TypPlanet,
                        Name: "Neptun",
                        MasseKg: mko.Newton.Mass.MassOfNeptune.Value,
                        DAequator_km: mko.Newton.Length.DiameterNeptune.Vector.Length,
                        DPol_km: mko.Newton.Length.DiameterNeptunePolar.Vector.Length,
                        Fallbeschleunigung: mko.Newton.Acceleration.GravityOnNeptune.Vector.Length,
                        TOberflaeche_K: -100,
                        DauerUmdrehung_h: mko.Newton.Time.SideralRotationPeriodNeptune.Value,
                        RotAchsneigung: mko.Newton.Angle.AxialTiltOfNeptune.Vector.Length);

                    Neptun.Umlaufbahn = CreateUmlaufbahn(
                        Context: ctx,
                        UmlaufbahnZentralobjekt: (Himmelskoerper) Sonne,
                        UmlaufbahnGrosseHalbachse_km: mko.Newton.Length.Kilometer(mko.Newton.Length.SemiMajorAxisNeptune).Vector.Length,
                        UmlaufbahnExzentrizität: 0.011214269,
                        Umlaufgeschwindigkeit_km_per_sec: mko.Newton.Velocity.VelocityOfNeptune.Vector.Length,
                        Umlaufdauer_Tage: mko.Newton.Time.OrbitalPeriodNeptune.Value);

                    // Triton	Mond	Neptun	354759	km	2706	2,147E+22
                    global::Kepler.IMond Triton = Monde.CreateBoAndAddToCollection();

                    Triton.Name = "Triton";
                    Triton.Masse_in_kg = 2.147E+22;
                    Triton.Aequatordurchmesser_in_km = 2706;
                    Triton.Umlaufbahn.Zentralobjekt = Neptun;
                    Triton.Umlaufbahn.Laenge_grosse_Halbachse_in_km = 354759;

                    // Nereid	Mond	Neptun	5513787	km	340	3,100E+19
                    global::Kepler.IMond Nereid = Monde.CreateBoAndAddToCollection();

                    Nereid.Name = "Nereid";
                    Nereid.Masse_in_kg = 3.100E+19;
                    Nereid.Aequatordurchmesser_in_km = 340;
                    Nereid.Umlaufbahn.Zentralobjekt = Neptun;
                    Nereid.Umlaufbahn.Laenge_grosse_Halbachse_in_km = 5513787;


                    ctx.SaveChanges();

                    // Länder 
                    var SU = ctx.LaenderTab.Where(r => r.Laenderkennzeichen == "SU").Single();
                    var USA = ctx.LaenderTab.Where(r => r.Laenderkennzeichen == "USA").Single();
                    var RU = ctx.LaenderTab.Where(r => r.Laenderkennzeichen == "RU").Single();
                    var CN = ctx.LaenderTab.Where(r => r.Laenderkennzeichen == "CN").Single();
                    var KP = ctx.LaenderTab.Where(r => r.Laenderkennzeichen == "KP").Single();
                    var IR = ctx.LaenderTab.Where(r => r.Laenderkennzeichen == "IR").Single();

                    // Aufgaben
                    var Wissenschaft = ctx.AufgabenTab.Where(r => r.Aufgabenbeschreibung == "Wissenschaft").Single();
                    var Militär = ctx.AufgabenTab.Where(r => r.Aufgabenbeschreibung == "Militär").Single();
                    var Telekommunikation = ctx.AufgabenTab.Where(r => r.Aufgabenbeschreibung == "Telekommunikation").Single();
                    var Prestige = ctx.AufgabenTab.Where(r => r.Aufgabenbeschreibung == "Prestige").Single();


                    // Raumschiffe anlegen
                    CreateRaumschiff(Context: ctx,
                        Name: "Wostok 1",
                        MasseKg: 4730,
                        Land: SU,
                        Start_Mission: new DateTime(1961, 4, 12),
                        Aufgaben: new Aufgabe[] { Wissenschaft, Prestige });

                    CreateRaumschiff(Context: ctx,
                        Name: "Apollo 11",
                        MasseKg: 5560 + 23243 + 15095,
                        Land: USA,
                        Aufgaben: new Aufgabe[] { Wissenschaft, Prestige },
                        Start_Mission: new DateTime(1969, 7, 16));

                    CreateRaumschiff(Context: ctx,
                        Name: "Shenzhou 5",
                        MasseKg: 7790,
                        Land: CN,
                        Aufgaben: Wissenschaft,
                        Start_Mission: new DateTime(2003, 10, 15));

                    CreateRaumschiff(Context: ctx,
                        Name: "Kwangmyŏngsŏng",
                        MasseKg: 100,
                        Land: KP,
                        Aufgaben: new Aufgabe[] { Wissenschaft, Militär },
                        Start_Mission: new DateTime(2012, 12, 12));

                    CreateRaumschiff(Context: ctx,
                        Name: "Omid",
                        MasseKg: 23,
                        Land: IR,
                        Aufgaben: new Aufgabe[] { Telekommunikation, Militär },
                        Start_Mission: new DateTime(2009, 2, 2));

                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                string msg = mko.TraceHlp.FormatErrMsg("KeplerDBInstaller", "CreateDB", ex.Message);
                throw new Exception(msg, ex);
            }
        }
    }
}
