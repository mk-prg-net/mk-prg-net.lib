 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.IO;
using System.Diagnostics;

namespace mkoIt.Xhtml
{
    public class Structure
    {

        public class Headline
        {
            public int Level { get; set; }
            public string Text { get; set; }
            public string Anchor { get; set; }
            public long Line { get; set; }
        }

        /// <summary>
        /// Alle Überschriften in einem xhtml- dokument finden, und die 
        /// Positonen in einer Liste aus Headlines beschreiben
        /// </summary>
        /// <param name="xthmlStream"></param>
        /// <returns></returns>
        public static List<Headline> filterHeader(string xhtml)
        {
            List<Headline> hls = new List<Headline>();
            xhtml = "<body>" + xhtml + "</body>";

            System.IO.StringReader str = new StringReader(xhtml);
            XmlReader reader = XmlReader.Create(str);
            XElement doc = XElement.Load(reader, LoadOptions.SetLineInfo);

            // Finden aller Headlines
            var headlines = doc.Elements().Where(e => System.Text.RegularExpressions.Regex.IsMatch(e.Name.LocalName, @"h\d"));

            foreach (var h in headlines)
            {
                string anchor = "";
                long line = 0;
                IXmlLineInfo lineInfo = h;
                line = lineInfo.LineNumber;

                if (h.Element(XName.Get("a")) != null)
                {
                    XElement a = h.Element(XName.Get("a"));
                    XName n = XName.Get("id");
                    if (a.Name.LocalName == "a" && !string.IsNullOrEmpty(a.Attribute(n).Value))
                    {
                        anchor = a.Attribute(n).Value;
                    }
                }
                Headline hl = new Headline
                {
                    Anchor = anchor,
                    Level = int.Parse(h.Name.LocalName[1].ToString()),
                    Line = line,
                    Text = h.Value
                };

                hls.Add(hl);
            }           

            return hls;
        }

        private struct hLevel
        {
            // Hierarchiestufe der Überschrift im Dokument
            public int Level { get; set; }
            // Folgenummer der Überschrift auf der Hierarchiestufe
            public int SiblingsCount { get; set; }
        }

        // Das Basisverzeichnis für alle Bilder in einem Dokument wird geändert
        public static string SetImgSrcBase(string xhtml, string ImgSrcBase)
        {
            xhtml = "<body>" + xhtml + "</body>";
            System.IO.StringReader str = new StringReader(xhtml);
            XmlReader reader = XmlReader.Create(str);
            XElement doc = XElement.Load(reader, LoadOptions.SetLineInfo);

            // Urls der Bilder ändern
            foreach (XElement img in doc.XPathSelectElements(@".//img"))
            {
                string src = img.Attribute(XName.Get("src")).Value;
                if (string.IsNullOrEmpty(ImgSrcBase))
                {
                    int ix = src.LastIndexOf("/");
                    if (ix > -1)                    
                    {
                        src = src.Substring(ix + 1);
                    }
                }
                else
                {
                    int ix = src.LastIndexOf("/");
                    if (ix == -1)
                    {
                        src = ImgSrcBase + "/" + src;
                    }
                    else
                    {
                        src = ImgSrcBase + src.Substring(ix);
                    }
                }                    
                
                img.SetAttributeValue(XName.Get("src"), src);
            }

            StringBuilder bld = new StringBuilder();
            foreach (XElement xe in doc.Elements())
                bld.Append(xe.ToString());

            return bld.ToString();
        }


        public static bool IsImgSrcInWocFormat(string src, string wocName)
        {
            if (src.Contains(wocName))
                return true;
            else
                return false;
        }

        public static string ImgSrcInWocFormat(string src, string wocName)
        {
            Debug.Assert(!IsImgSrcInWocFormat(src, wocName));
            src.Replace('/', System.IO.Path.DirectorySeparatorChar);
            return (System.IO.Path.GetDirectoryName(src) + System.IO.Path.DirectorySeparatorChar + wocName + "." + System.IO.Path.GetFileName(src)).Replace(System.IO.Path.DirectorySeparatorChar, '/');
        }


        public struct MapImgSrcWoc
        {
            public string OriginalImgSrc { get; set; }
            public string WocImgSrc { get; set; }
        }


        /// <summary>
        /// Ein xhtml- Dokument mit lokalen Referenzen auf Bilddateien wird in ein Woc gewandelt, indem
        /// 1) Alle Überschriften markiert werden
        /// 2) Alle Bildreferenzen in Wocnamen umgewandelt werden
        /// </summary>
        /// <param name="xhtml">Inhalt des Body- Elements einer xhtml- Datei</param>
        /// <param name="wocName">Zukünftiger Woc- Name der xhtml- Datei</param>
        /// <param name="ImgSrcBase">Relativer Pfad zum Bilderverzeichnis auf dem Woc- Server</param>
        /// <param name="mapImgSrcToWoc">Liste aller Bilddateien, die in einem Wocnamen umbenannt werden müssen</param>
        /// <returns></returns>
        public static string ConvertToWocFormat(string xhtml, string wocName, string ImgSrcBase, out List<MapImgSrcWoc> mapImgSrcToWoc) {

            xhtml = "<body>" + xhtml + "</body>";
            System.IO.StringReader str = new StringReader(xhtml);
            XmlReader reader = XmlReader.Create(str);
            XElement doc = XElement.Load(reader, LoadOptions.SetLineInfo);

            korrigiere(doc);

            headlineMarker(doc);

            mapImgSrcToWoc = new List<MapImgSrcWoc>();
            convertImgSrcInWoc(wocName, ImgSrcBase, doc, mapImgSrcToWoc);

            StringBuilder bld = new StringBuilder();
            foreach (XElement xe in doc.Elements())
                bld.Append(xe.ToString());

            return bld.ToString();
        }

        private static void convertImgSrcInWoc(string wocName, string ImgSrcBase, XElement doc, List<MapImgSrcWoc> mapImgSrcToWoc)
        {
            // Urls der Bilder ändern
            foreach (XElement img in doc.XPathSelectElements(@".//img"))
            {
                string src = img.Attribute(XName.Get("src")).Value;

                if (string.IsNullOrEmpty(ImgSrcBase))
                {
                    int ix = src.LastIndexOf("/");
                    if (ix > -1)
                    {
                        src = src.Substring(ix + 1);
                    }
                }
                else
                {
                    int ix = src.LastIndexOf("/");
                    if (ix == -1)
                    {
                        src = ImgSrcBase + "/" + src;
                    }
                    else
                    {
                        src = ImgSrcBase + src.Substring(ix);
                    }
                }

                // Umwandeln des Dateinamens des Bildes in ein Woc- Format, falls noch nicht erfolgt
                MapImgSrcWoc map = new MapImgSrcWoc();
                map.OriginalImgSrc = System.IO.Path.GetFileName(src.Replace('/', System.IO.Path.DirectorySeparatorChar));


                if (!IsImgSrcInWocFormat(src, wocName))
                {                    
                    src = ImgSrcInWocFormat(src, wocName);                                        
                }

                map.WocImgSrc = System.IO.Path.GetFileName(src.Replace('/', System.IO.Path.DirectorySeparatorChar)); 
                mapImgSrcToWoc.Add(map);

                img.SetAttributeValue(XName.Get("src"), src);
            }
        }
        
        // Dokument zur Aufnahme in das Content- Managment System aufbereiten
        public static string CmsFormat(
            // Dokument als Zeichenkette in Rohform (z.B. aus dem XStandard Editor)
            string xhtml)
        {
            xhtml = "<body>" + xhtml + "</body>";
            System.IO.StringReader str = new StringReader(xhtml);
            XmlReader reader = XmlReader.Create(str);
            XElement doc = XElement.Load(reader, LoadOptions.SetLineInfo);


            headlineMarker(doc);

            StringBuilder bld = new StringBuilder();
            foreach (XElement xe in doc.Elements())
                bld.Append(xe.ToString());

            return bld.ToString();

        }

        /// <summary>
        /// korrigiere
        /// </summary>
        /// <param name="doc"></param>
        private static void korrigiere(XElement doc)
        {
            var headlinesWithImage = doc.Elements().Where(e => System.Text.RegularExpressions.Regex.IsMatch(e.Name.LocalName, @"h\d") && e.Elements().Any(ee => ee.Name == "img"));

            IEnumerator<XElement> it  = headlinesWithImage.GetEnumerator();

            foreach(XElement hx in headlinesWithImage)
            {   
                XElement img = hx.Element(XName.Get("img"));
                img.Remove();

                List<XAttribute> toRemove = new List<XAttribute>();
                for (XAttribute att = img.FirstAttribute; att != null; att = att.NextAttribute)
                {
                    if (att.Name == "align")
                        toRemove.Add(att);
                    else if (att.Name == "border")
                        toRemove.Add(att);
                    else if (att.Name == "name")
                        toRemove.Add(att);
                }

                foreach (XAttribute att in toRemove)
                    att.Remove();

                // Bild in einen Absatz einbetten
                XElement p = new XElement(XName.Get("p"));
                hx.AddAfterSelf(p);
                p.AddFirst(img);                

                XElement br = hx.Element(XName.Get("br"));
                if(br != null)
                    br.Remove();


            }

        }

        /// <summary>
        /// Eliminieren von Einbettungen 
        /// </summary>
        /// <param name="doc"></param>
        private static void headlineMarker(XElement doc)
        {
            // Finden aller Headlines
            var headlines = doc.Elements().Where(e => System.Text.RegularExpressions.Regex.IsMatch(e.Name.LocalName, @"h\d"));

            int pos = 0;
            Stack<hLevel> hnummer = new Stack<hLevel>();
            string hierarchieWurzel = "";
            bool hnummerOn = false;
            foreach (var h in headlines)
            {
                string newAnchorName = AnchorName(pos++);
                XElement oldAnchor = doc.XPathSelectElement(".//a[@id='" + newAnchorName + "']");
                if (oldAnchor != null)
                {
                    // Alten Anker löschen
                    oldAnchor.Remove();

                }

                string überschrift = h.Value.Trim();
                string anfang = "";
                if (überschrift.Contains(" "))
                    anfang = überschrift.Substring(0, überschrift.IndexOf(' '));

                if (h.Name.LocalName == "h1")
                {
                    // Prüfen, ob der Text mit einer Hierarchienummer beginnt
                    if (HierachischeNummern.IstHierarchischeNummer(anfang))
                    {
                        hnummerOn = true;
                        hierarchieWurzel = anfang;
                        hnummer.Clear();
                        hLevel hl = new hLevel();
                        hl.Level = HierachischeNummern.Level(hierarchieWurzel);
                        hl.SiblingsCount = HierachischeNummern.ValueAtLevel(hierarchieWurzel, hl.Level - 1);
                        hnummer.Push(hl);
                    }
                }
                else if (hnummerOn)
                {
                    // Alte hierarchische Nummern löschen
                    if (HierachischeNummern.IstHierarchischeNummer(anfang))
                    {
                        überschrift = überschrift.Substring(anfang.Length);
                    }

                    int LevelÜberschrift = int.Parse(h.Name.LocalName.Substring(1));

                    // Hierarchiche Nummern bilden

                    // Der Vorgänger ist auf dem gleichen Level
                    if (hnummer.Count() == LevelÜberschrift)
                    {
                        // -> Count auf dem Level inkermentieren
                        hLevel top = hnummer.Pop();
                        top.SiblingsCount++;
                        hnummer.Push(top);
                    }
                    else if (hnummer.Count < LevelÜberschrift)
                    {
                        // Der Vorgänger ist auf einem höheren Level
                        // Hinzufügen eines neuer Levels
                        for (int i = hnummer.Count; i < LevelÜberschrift; i++)
                        {
                            hLevel hl = new hLevel();
                            hl.Level = hnummer.Peek().Level + 1;
                            hl.SiblingsCount = 1;

                            hnummer.Push(hl);
                        }
                    }
                    else
                    {
                        // Der Vorgänger ist auf einem tieferen Level
                        int abzubauendeLevels = hnummer.Count - LevelÜberschrift;
                        while (abzubauendeLevels > 0)
                        {
                            hnummer.Pop();
                            abzubauendeLevels--;
                        }

                        // -> Count auf dem Level inkermentieren
                        hLevel top = hnummer.Pop();
                        top.SiblingsCount++;
                        hnummer.Push(top);
                    }

                    // Neuen Hierarchienummer bilden
                    StringBuilder bldHn = new StringBuilder(hierarchieWurzel);
                    for (int i = 1; i < hnummer.Count; i++)
                    {
                        hLevel hl = hnummer.ElementAt(hnummer.Count - i - 1);
                        bldHn.Append(".");
                        bldHn.Append(hl.SiblingsCount.ToString());
                    }

                    überschrift = bldHn.ToString() + " " + überschrift;

                    h.SetValue(überschrift);

                }

                AddNewAnchor(h, newAnchorName);

            }
        }

        private static void AddNewAnchor(XElement h, string newAnchorName)
        {
            // neuen Anker vor der Headline einfügen
            XElement newAnchor = new XElement(XName.Get("a"));
            newAnchor.Add(new XAttribute(XName.Get("id"), newAnchorName));
            h.AddFirst(newAnchor);
        }

        /// <summary>
        /// Prüft, ob ein Anker vor einer Headline dem hier verwendeten Namensschema entspricht
        /// </summary>
        /// <param name="AnchorName"></param>
        /// <returns></returns>
        static bool TestHeadlineAnchor(string AnchorName)
        {
            if (AnchorName.StartsWith("mkoItHeadline"))
                return true;
            else
                return false;

        }
        static string AnchorName(int pos)
        {
            return "mkoItHeadline" + pos;
        }

    }
}
