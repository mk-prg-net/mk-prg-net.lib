using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Kepler.EF60.ImportAsteroids
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Importieren der Asteroiden aus der NASA- Tabelle");
            try
            {
                mko.Newton.Init.Do();
                using (var ctx = new KeplerDBEntities())
                {
                    // Datei öffnen
                    string fname = args[0];
                    Console.WriteLine("Datenfile: " + fname);
                    System.IO.StreamReader reader = new System.IO.StreamReader(fname);

                    var AsteroidTyp = ctx.HimmelskoerperTypenTab.Single(e => e.Name == "Asteroid");
                    Debug.Assert(AsteroidTyp != null);

                    var Sonne = ctx.HimmelskoerperTab.Single(e => e.Name == "Sonne");
                    Debug.Assert(Sonne != null);

                    int i = 0;
                    // Kopfzeile überspringen
                    reader.ReadLine();
                    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

                    Console.WriteLine("# eingelesener Asteroiden:");
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string[] cols = line.Split(',');

                        if (cols.Length != 8)
                            throw new Exception("In Zeile " + i + " ist die Anzahl der Spalten # 8");

                        //AsteroidName,DiameterInKm,e,a,rot_per_year,albedo,rot_per_day,GM
                        string AsteroidName = cols[0].Replace('"', ' ').Trim();

                        AsteroidName = AsteroidName.Substring(AsteroidName.LastIndexOf(' ')).Trim();
                        double diameterInKm;
                        if (!double.TryParse(cols[1], out diameterInKm))
                            diameterInKm = 0;
                        double e;
                        if (!double.TryParse(cols[2], out e))
                            e = 0;
                        double a;
                        if (!double.TryParse(cols[3], out a))
                            a = 0;
                        double rot_per_year;
                        if (!double.TryParse(cols[4], out rot_per_year))
                            rot_per_year = 0;
                        double albedo;
                        if (!double.TryParse(cols[5], out albedo))
                            albedo = 0;
                        double rot_per_day;
                        if (!double.TryParse(cols[6], out rot_per_day))
                            rot_per_day = 0;
                        double GM;
                        if (!double.TryParse(cols[7], out GM))
                            GM = 0;
                        double Mass_in_Kg = GM / 6.67259e-20;

                        var Asteroid = ctx.HimmelskoerperTab.Create();

                        Asteroid.HimmelskoerperTyp = AsteroidTyp;
                        Asteroid.Name = AsteroidName;

                        Asteroid.Masse_in_kg = Mass_in_Kg;

                        Asteroid.Umlaufbahn = ctx.UmlaufbahnenTab.Create();
                        Asteroid.Umlaufbahn.Laenge_grosse_Halbachse_in_km = mko.Newton.Length.Kilometer(mko.Newton.Length.AU(a) * 2.0).Vector[0];
                        Asteroid.Umlaufbahn.Mittlere_Umlaufgeschwindigkeit_in_km_pro_sec = mko.Newton.Velocity.KilometerPerSec(mko.Newton.Length.AU(a) * Math.PI * 2, mko.Newton.Time.Days(365.0 * rot_per_year)).Vector[0];
                        Asteroid.Umlaufbahn.Exzentritzitaet = e;
                        Asteroid.Umlaufbahn.Umlaufdauer_in_Tagen = rot_per_year == 0.0 ? 0.0 : rot_per_year * 365.0;
                        Asteroid.Umlaufbahn.Zentralobjekt = Sonne;
                        Asteroid.Sterne_Planeten_MondeTab = ctx.Sterne_Planeten_MondeTab.Create();
                        Asteroid.Sterne_Planeten_MondeTab.Aequatordurchmesser_in_km = diameterInKm;
                        Asteroid.Sterne_Planeten_MondeTab.Rotationsperiode_in_Stunden = rot_per_day == 0.0 ? 0.0 : rot_per_day;

                        ctx.HimmelskoerperTab.Add(Asteroid);


                        if (i % 100 == 0)
                            try
                            {
                                Console.Write("" + i + " Asteroiden eingelesen\r");
                                ctx.SaveChanges();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("" + i + " Asteroiden eingelesen und eine Ausnahme aufgetreten");
                            }
                        i++;
                    }

                    try
                    {
                        ctx.SaveChanges();
                        Console.WriteLine("" + i + " Asteroiden insgesamt eingelesen");

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("" + i + " Asteroiden insgesamt eingelesen und eine Ausnahme beim Sichern aufgetreten");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Eine allgemeine Ausnahme ist aufgetreten.");
            }
        }
    }
}
