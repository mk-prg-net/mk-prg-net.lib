//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 8.3.2010
//
//  Projekt.......: mkoItAsp
//  Name..........: DirectorySiteMapProvider
//  Aufgabe/Fkt...: SiteMapProvider für Xml- Verzeichnisse
//                  
//
//
//
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows XP mit .NET 2.0
//  Werkzeuge.....: Visual Studio 2005
//  Autor.........: Martin Korneffel (mko)
//  Version 1.0...: 
//
// </unit_environment>
//
//<unit_history>
//------------------------------------------------------------------
//
//  Version.......: 1.1
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 
//  Änderungen....: 
//
//</unit_history>
//</unit_header>        

using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Text;
using System.Diagnostics;

namespace mkoIt.Asp
{
    public class DirectorySiteMapProvider : StaticSiteMapProvider
    {
        private string _providerName = null;
        private string _sourceFilename = null;
        private XElement _directory = null;

        private List<string> _roles = new List<string>();
        private System.Collections.Specialized.NameValueCollection _attributes = null;

        //private SiteMapProvider _parentProvider;
        //private SiteMapProvider _rootProvider;

        protected new SiteMapNode RootNode = null;

        const string DocumentViewer = "woc.aspx";
        const string IndexViewer = "ix.aspx";

        string _namespaceDirectory = ((System.Xml.Serialization.XmlTypeAttribute)typeof(mkoIt.Xhtml.Directory.dir).GetCustomAttributes(typeof(System.Xml.Serialization.XmlTypeAttribute), false).First()).Namespace;

        private string NamespaceDirectory
        {
            get
            {
                return _namespaceDirectory;
            }
        }

        private System.Xml.XmlNamespaceManager _nsmgr;

        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection attributes)
        {
            base.Initialize(name, attributes);
            lock (this)
            {
                _providerName = name;
                _sourceFilename = attributes["siteMapFile"];

                if (attributes.AllKeys.Contains("roles"))
                    _roles.AddRange(attributes["roles"].Split(','));
            }
        }

        public override SiteMapNode BuildSiteMap()
        {
            lock (this)
            {
                if (RootNode != null)
                    return RootNode;

                // Site- Map- Struktur aufbauen
                base.Clear();

                // Xml- Verzeichnis in ein XElement einlesen
                string pathToOpen = HttpContext.Current.Server.MapPath("~/App_Data/" + _sourceFilename);
                if (!File.Exists(pathToOpen))
                    throw new Exception("Die Verzeichnisdatei " + pathToOpen + " konnte nicht gefunden werden.");

                XmlReaderSettings sett = new XmlReaderSettings();
                sett.IgnoreComments = true;
                sett.IgnoreProcessingInstructions = true;
                sett.IgnoreWhitespace = true;

                XmlReader reader = XmlReader.Create(pathToOpen, sett);

                XmlNameTable ntab = new NameTable();
                _nsmgr = new XmlNamespaceManager(ntab);
                _nsmgr.AddNamespace("vz", NamespaceDirectory);

                _directory = XElement.Load(reader);
                reader.Close();

                // Wurzel der Sitemap bilden

                XElement rootEntry = _directory.XPathSelectElement(@".", _nsmgr);
                if (rootEntry != null)
                    if (SecurityTrimmingEnabled)
                        RootNode = new SiteMapNode(
                            this,
                            // Verzeichnisname == Schlüssel des Verzeichnisses
                            rootEntry.Attribute(XName.Get("id")).Value,
                            "~/" + IndexViewer + "?path=" + rootEntry.Attribute(XName.Get("id")).Value,
                            rootEntry.Attribute(XName.Get("id")).Value,
                            rootEntry.XPathSelectElement("./vz:descr", _nsmgr).Value,
                            _roles,
                            null,
                            null,
                            null);

                    else
                        RootNode = new SiteMapNode(
                            this,
                            // Verzeichnisname == Schlüssel des Verzeichnisses
                            rootEntry.Attribute(XName.Get("id")).Value,
                            "~/" + IndexViewer + "?path=" + rootEntry.Attribute(XName.Get("id")).Value,
                            rootEntry.Attribute(XName.Get("id")).Value,
                            rootEntry.XPathSelectElement("./vz:descr", _nsmgr).Value);


                else
                    throw new Exception(string.Format("Die Wurzel wurde in " + _sourceFilename + " nicht gefunden"));

                AddNode(RootNode);

                XElement parentEntry = rootEntry;

                // XElement rekursiv durchlaufen, und dabei die SiteMap aufbauen
                DirTraverse(RootNode, rootEntry, RootNode.Key);

                return RootNode;

            }
        }

        private void DirTraverse(SiteMapNode parentNode, XElement parentEntry, string parentLevel)
        {
            string nodeUrlTemplate = "~/{0}?path={1}";

            foreach (XElement child in parentEntry.Elements(XName.Get("e", NamespaceDirectory)))
            {
                string childLevel = parentLevel + "." + child.Attributes(XName.Get("id")).First().Value;

                // Ist das Kind ein gewöhnlicher, oder ein Blattknoten
                string nodeUrl = null;
                bool hasChilds = false;
                if (child.Element(XName.Get("val", NamespaceDirectory)) != null)
                {
                    nodeUrl = string.Format(nodeUrlTemplate, DocumentViewer, childLevel);
                }
                else
                {
                    hasChilds = true;
                    nodeUrl = string.Format(nodeUrlTemplate, IndexViewer, childLevel);
                }

                SiteMapNode childNode = null;

                if (SecurityTrimmingEnabled && _roles.Count > 0)
                    childNode = new SiteMapNode(
                                this,
                        // Schlüssel 
                                childLevel,

                                // url, die mit dem Sitemap korrespondiert
                                nodeUrl,

                                // Titel
                                child.Attribute(XName.Get("t")).Value,

                                // Beschreibung
                                child.XPathSelectElement("./vz:descr", _nsmgr).Value,
                                _roles, null, null, null);
                else
                    childNode = new SiteMapNode(
                                this,
                        // Schlüssel 
                                childLevel,

                                // url, die mit dem Sitemap korrespondiert
                                nodeUrl,

                                // Titel
                                child.Attribute(XName.Get("t")).Value,

                                // Beschreibung
                                child.XPathSelectElement("./vz:descr", _nsmgr).Value);


                AddNode(childNode, parentNode);

                // Rekursiver Abstieg
                if (hasChilds)
                    DirTraverse(childNode, child, childLevel);
            }
        }

        public override bool IsAccessibleToUser(HttpContext context, SiteMapNode node)
        {
            if (!SecurityTrimmingEnabled)
                return true;

            if (WocSecurity.IsReader(node.Key, context.User))
                return true;
            else
                return false;
        }


        protected override SiteMapNode GetRootNodeCore()
        {
            return BuildSiteMap();
        }

        //##############################################################################
        // Verzeichnisoperationen

        //------------------------------------------------------------------------------
        // Generische Operationen (für Verzeichnisse und Dateien gleichermassen)

        public bool SetDescription(string wocName, string description)
        {
            lock (this)
            {
                
                string[] levels = wocName.Split('.');
                XElement currentEntry = _directory.XPathSelectElement(WocNameToXPath(levels), _nsmgr);
                Debug.Assert(currentEntry != null);

                XElement descr = currentEntry.XPathSelectElement("./vz:descr", _nsmgr);
                descr.SetValue(description);

                // Erweiterten Verzeichnisbaum sichern
                SaveDirectoryChanges();

            }
            return true;
        }

        public void Copy(string srcWocName, string destWocName, string newTitle)
        {
            lock (this)
            {

                // Typ des Verzeichniseintrages bestimmen
                string[] srcLevels = srcWocName.Split('.');
                XElement srcEntry = _directory.XPathSelectElement(WocNameToXPath(srcLevels), _nsmgr);
                Debug.Assert(srcEntry != null);

                // Hat das Element ein val- Kindelement-> Dokument
                if (srcEntry.XPathSelectElement("./vz:val", _nsmgr) != null)
                    DocCopy(srcWocName, destWocName, newTitle, false);
                else
                    DirCopy(srcWocName, destWocName, newTitle, false);
            }
        }

        public void Move(string srcWocName, string destWocName, string newTitle)
        {
            lock (this)
            {

                // Typ des Verzeichniseintrages bestimmen
                string[] srcLevels = srcWocName.Split('.');
                XElement srcEntry = _directory.XPathSelectElement(WocNameToXPath(srcLevels), _nsmgr);
                Debug.Assert(srcEntry != null);

                // Hat das Element ein val- Kindelement-> Dokument
                if (srcEntry.XPathSelectElement("./vz:val", _nsmgr) != null)
                    DocCopy(srcWocName, destWocName, newTitle, true);
                else
                    DirCopy(srcWocName, destWocName, newTitle, true);
            }
        }

        public void MoveLeft(string srcWocName)
        {
            lock (this)
            {

                // Wurzelverzeichnis kann nicht verschoben werden
                if(srcWocName == RootNode.Key)
                    return;

                // Typ des Verzeichniseintrages bestimmen
                string[] srcLevels = srcWocName.Split('.');
                Debug.Assert(srcLevels.Length > 1);

                XElement srcEntry = _directory.XPathSelectElement(WocNameToXPath(srcLevels), _nsmgr);
                Debug.Assert(srcEntry != null);

                XElement homeDir = srcEntry.Parent;

                // Alle Kindelemente vom Typ Eintrag bestimmen
                List<XElement> childs = homeDir.Elements(XName.Get("e", NamespaceDirectory)).ToList();

                // Position des zu nach links zu schiebenden Elementes bestimmen
                int i = 0;
                for (i = 0; i < childs.Count; i++)
                    if (childs[i] == srcEntry)
                        break;
                // Wenn Element schon ganz links, dann Ende
                if (i == 0)
                    return;

                // Kindelemente vom Elternelement abkoppeln
                foreach (XElement child in childs)
                    child.Remove();

                // nach links schieben (Platztausch)
                Debug.Assert(i < childs.Count);
                childs[i] = childs[i - 1];
                childs[i - 1] = srcEntry;

                // Kindelemente wieder anfügen
                foreach (XElement child in childs)
                    homeDir.Add(child);

                // Neue Struktur sichern
                SaveDirectoryChanges();               


            }
        }

        public void MoveRight(string srcWocName)
        {
            lock (this)
            {

                // Wurzelverzeichnis kann nicht verschoben werden
                if (srcWocName == RootNode.Key)
                    return;

                // Typ des Verzeichniseintrages bestimmen
                string[] srcLevels = srcWocName.Split('.');
                Debug.Assert(srcLevels.Length > 1);

                XElement srcEntry = _directory.XPathSelectElement(WocNameToXPath(srcLevels), _nsmgr);
                Debug.Assert(srcEntry != null);

                XElement homeDir = srcEntry.Parent;

                // Alle Kindelemente vom Typ Eintrag bestimmen
                List<XElement> childs = homeDir.Elements(XName.Get("e", NamespaceDirectory)).ToList();

                // Position des zu nach rechts zu schiebenden Elementes bestimmen
                int i = 0;
                for (i = 0; i < childs.Count; i++)
                    if (childs[i] == srcEntry)
                        break;
                // Wenn Element schon ganz rechts, dann Ende
                if (i == childs.Count - 1)
                    return;

                // Kindelemente vom Elternelement abkoppeln
                foreach (XElement child in childs)
                    child.Remove();

                // nach links schieben (Platztausch)
                Debug.Assert(i < childs.Count);
                childs[i] = childs[i + 1];
                childs[i + 1] = srcEntry;

                // Kindelemente wieder anfügen
                foreach (XElement child in childs)
                    homeDir.Add(child);

                // Neue Struktur sichern
                SaveDirectoryChanges();
 
            }
        }


        public void Delete(string srcWocName)
        {
            lock (this)
            {
                // Typ des Verzeichniseintrages bestimmen
                string[] srcLevels = srcWocName.Split('.');
                XElement srcEntry = _directory.XPathSelectElement(WocNameToXPath(srcLevels), _nsmgr);
                Debug.Assert(srcEntry != null);

                // Hat das Element ein val- Kindelement-> Dokument
                if (srcEntry.XPathSelectElement("./vz:val", _nsmgr) != null)
                    DocDelete(srcWocName);
                else
                    DirDelete(srcWocName);
            }
        }

        //------------------------------------------------------------------------------
        // Typspeziefische Operationen (separat für Verzeichnisse und Dateien )

        public bool NewSubDir(string currentLevel, string nameNewSubdir, string newTitle, string descr)
        {
            // XPath- Ausdruck zur Abfrage des aktuellen Knotens 
            lock (this)
            {
                string[] levels = currentLevel.Split('.');

                // Zugriff auf den Knoten
                XElement currentEntry = _directory.XPathSelectElement(WocNameToXPath(levels), _nsmgr);
                Debug.Assert(currentEntry != null);

                mkoIt.Xhtml.Directory.e newDir = new mkoIt.Xhtml.Directory.e();
                newDir.id = nameNewSubdir;
                newDir.t = newTitle;
                newDir.descr = descr;
                newDir.ver = 1;
                newDir.d = DateTime.Now;
                newDir.ix = 1;

                // Prüfen, ob das Unterverzeichnis bereits existiert
                string[] newLevels = (currentLevel + "." + newDir.id).Split('.');
                if (_directory.XPathSelectElement(WocNameToXPath(newLevels), _nsmgr) != null)
                    throw new Exception("Das Unterverzeichnis " + nameNewSubdir + " existiert bereits");

                string newDirSerialized;
                mko.algorithm.InfosSerializer<mkoIt.Xhtml.Directory.e>.WriteToXml(newDir, out newDirSerialized);

                currentEntry.Add(XElement.Parse(newDirSerialized));

                // Alle Index- Attribute neu nummerieren
                int ix = 1;
                foreach (XElement e in currentEntry.XPathSelectElements("./vz:e", _nsmgr))
                {
                    e.SetAttributeValue(XName.Get("ix"), ix++);
                }


                // Erweiterten Verzeichnisbaum sichern
                SaveDirectoryChanges();

            }

            return true;
        }

        private void SaveDirectoryChanges()
        {
            try
            {
                string pathToOpen = HttpContext.Current.Server.MapPath("~/App_Data/" + _sourceFilename);
                if (!File.Exists(pathToOpen))
                    throw new Exception("Die Verzeichnisdatei " + pathToOpen + " konnte nicht gefunden werden.");

                _directory.Save(pathToOpen);
            }
            finally
            {
                // Signalisieren, daß das Verzeichnis neu geladen werden muss
                RootNode = null;
            }
        }

        public bool DirCopy(string srcWocName, string destWocName, string newTitle, bool deleteSrcWoc)
        {
            // XPath- Ausdruck zur Abfrage des aktuellen Knotens 
            lock (this)
            {
                if (srcWocName == destWocName)
                    return true;

                if (srcWocName == RootNode.Key)
                    throw new Exception("Das Hauptverzeichnis kann nicht kopiert werden");

                if (destWocName == RootNode.Key)
                    throw new Exception("Das Hauptverzeichnis darf nicht das Ziel einer Kopier-/Verschiebeoperation sein.");



                string[] srcLevels = srcWocName.Split('.');
                string[] destLevels = destWocName.Split('.');

                // Zugriff auf den bestehenden Knoten
                XElement srcEntry = _directory.XPathSelectElement(WocNameToXPath(srcLevels), _nsmgr);
                Debug.Assert(srcEntry != null);

                // Prüfen, ob das Verzeichnis für den neuen Namen existiert                
                XElement destHomeDir = _directory.XPathSelectElement(WocNameToXPath(destLevels, destLevels.Length - 1), _nsmgr);
                if (destHomeDir == null)
                    throw new Exception("Das Zielverzeichnis existiert nicht");

                // Prüfen, ob der neue Name nicht bereits für ein anderes Dokument vergeben wurde                                
                if (_directory.XPathSelectElement(WocNameToXPath(destLevels), _nsmgr) != null)
                    throw new Exception("Der Name " + destWocName + " wurde bereits für ein Unterverzeichnis verwendet");

                // Neuen Verzeichniseintrag anlegen
                mkoIt.Xhtml.Directory.e newDir = new mkoIt.Xhtml.Directory.e();
                newDir.id = GetWocNodeId(destLevels);
                newDir.t = newTitle;
                newDir.descr = srcEntry.XPathSelectElement("./vz:descr", _nsmgr).Value;
                if (srcEntry.Attribute(XName.Get("ver")) != null)
                    newDir.ver = int.Parse(srcEntry.Attribute(XName.Get("ver")).Value);
                else
                    newDir.ver = 1;
                newDir.d = DateTime.Now;
                newDir.ix = 1;

                string newDirSerialized;
                mko.algorithm.InfosSerializer<mkoIt.Xhtml.Directory.e>.WriteToXml(newDir, out newDirSerialized);

                destHomeDir.Add(XElement.Parse(newDirSerialized));

                // Alle Dokumente und Unterverzeichnisse an die neue Position verschieben
                foreach (XElement entry in srcEntry.Elements(XName.Get("e", NamespaceDirectory)).ToArray())
                {
                    string srcSubWocName = srcWocName + "." + entry.Attribute(XName.Get("id")).Value;
                    string newSubTitle = entry.Attribute(XName.Get("t")).Value;
                    string destSubWocName = destWocName + "." + entry.Attribute(XName.Get("id")).Value;
                    if (entry.XPathSelectElement("./vz:val", _nsmgr) != null)
                    {
                        // Dokument verschieben
                        DocCopy(srcSubWocName, destSubWocName, newSubTitle, deleteSrcWoc);
                    }
                    else
                    {
                        // Unterverzeichnis verschieben-> Rekursion
                        DirCopy(srcSubWocName, destSubWocName, newSubTitle, deleteSrcWoc);
                    }

                    // Zwischenstand sichern
                    BuildSiteMap();
                }

                // Löschen des alten Knotens
                if (deleteSrcWoc)
                    srcEntry.Remove();

                // Erweiterten Verzeichnisbaum sichern
                SaveDirectoryChanges();

            }

            return true;
        }

        public bool DirDelete(string srcWocName)
        {
            // XPath- Ausdruck zur Abfrage des aktuellen Knotens 
            lock (this)
            {
                if (srcWocName == RootNode.Key)
                    throw new Exception("Das Hauptverzeichnis kann nicht gelöscht werden");

                string[] srcLevels = srcWocName.Split('.');

                // Zugriff auf den bestehenden Knoten
                XElement srcEntry = _directory.XPathSelectElement(WocNameToXPath(srcLevels), _nsmgr);
                Debug.Assert(srcEntry != null);

                // Alle Dokumente und Unterverzeichnisse an die neue Position verschieben
                foreach (XElement entry in srcEntry.Elements(XName.Get("e", NamespaceDirectory)).ToArray())
                {
                    string srcSubWocName = srcWocName + "." + entry.Attribute(XName.Get("id")).Value;
                    if (entry.XPathSelectElement("./vz:val", _nsmgr) != null)
                    {
                        // Dokument verschieben
                        DocDelete(srcSubWocName);
                    }
                    else
                    {
                        // Unterverzeichnis verschieben-> Rekursion
                        DirDelete(srcSubWocName);
                    }
                }

                // Löschen des alten Knotens                
                srcEntry.Remove();

                // Erweiterten Verzeichnisbaum sichern
                SaveDirectoryChanges();

            }
            return true;
        }

        //----------------------------------------------------------------------------------
        // Dokumentoperationen

        public bool NewDoc(string currentLevel, string nameNewDoc, string newTitle, string descr)
        {
            // XPath- Ausdruck zur Abfrage des aktuellen Knotens 
            lock (this)
            {
                string[] levels = currentLevel.Split('.');

                // Zugriff auf den Knoten

                XElement currentEntry = _directory.XPathSelectElement(WocNameToXPath(levels), _nsmgr);
                Debug.Assert(currentEntry != null);

                mkoIt.Xhtml.Directory.e newDir = new mkoIt.Xhtml.Directory.e();
                newDir.id = nameNewDoc;
                newDir.t = newTitle;
                newDir.descr = descr;
                newDir.ver = 1;
                newDir.d = DateTime.Now;
                newDir.ix = 1;

                // Prüfen, ob das Dokument bereits existiert
                string[] newLevels = (currentLevel + "." + newDir.id).Split('.');
                if (_directory.XPathSelectElement(WocNameToXPath(newLevels), _nsmgr) != null)
                    throw new Exception("Das Unterverzeichnis " + nameNewDoc + " existiert bereits");


                // Markieren des Knotens als Dokumentknoten
                mkoIt.Xhtml.Directory.eVal val = new mkoIt.Xhtml.Directory.eVal();
                val.parser = "Editor.aspx";
                val.viewer = "woc.aspx";
                val.Value = currentLevel + "." + newDir.id;

                newDir.Items = new object[] { val };

                string newDirSerialized;
                mko.algorithm.InfosSerializer<mkoIt.Xhtml.Directory.e>.WriteToXml(newDir, out newDirSerialized);

                currentEntry.Add(XElement.Parse(newDirSerialized));

                // Alle Index- Attribute neu nummerieren
                int ix = 1;
                foreach (XElement e in currentEntry.XPathSelectElements("./vz:e", _nsmgr))
                {
                    e.SetAttributeValue(XName.Get("ix"), ix++);
                }

                // Erweiterten Verzeichnisbaum sichern
                SaveDirectoryChanges();

            }

            return true;
        }

        /// <summary>
        /// Verschiebt ein Woc- Dokument innerhalb der Woc Hierarchie.
        /// Ein Woc- Name ist der Dateiname der Xhtml- Datei im App_Data Verzeichnis ohne Extension
        /// </summary>
        /// <param name="oldWocName"></param>
        /// <param name="newWocName"></param>
        /// <returns></returns>
        public bool DocCopy(string srcWocName, string destWocName, string newTitle, bool deleteSrcWoc)
        {
            lock (this)
            {
                if (srcWocName == destWocName)
                    return true;

                 
                string[] srcLevels = srcWocName.Split('.');
                string[] destLevels = destWocName.Split('.');

                // Zugriff auf den bestehenden Knoten
                XElement srcEntry = _directory.XPathSelectElement(WocNameToXPath(srcLevels), _nsmgr);
                Debug.Assert(srcEntry != null);

                // Prüfen, ob das Verzeichnis für den neuen Namen existiert                
                XElement destHomeDir = _directory.XPathSelectElement(WocNameToXPath(destLevels, destLevels.Length - 1), _nsmgr);
                if (destHomeDir == null)
                    throw new Exception("Das Zielverzeichnis existiert nicht");

                // Prüfen, ob der neue Name nicht bereits für ein anderes Dokument vergeben wurde                                
                if (_directory.XPathSelectElement(WocNameToXPath(destLevels), _nsmgr) != null)
                    throw new Exception("Der Name " + destWocName + " wurde bereits für ein Unterverzeichnis verwendet");

                // Neuen Verzeichniseintrag anlegen
                mkoIt.Xhtml.Directory.e newDir = new mkoIt.Xhtml.Directory.e();
                newDir.id = GetWocNodeId(destLevels);
                newDir.t = newTitle;
                newDir.descr = srcEntry.XPathSelectElement("./vz:descr", _nsmgr).Value;
                if (srcEntry.Attribute(XName.Get("ver")) != null)
                    newDir.ver = int.Parse(srcEntry.Attribute(XName.Get("ver")).Value) + 1;
                else
                    newDir.ver = 1;
                newDir.d = DateTime.Now;
                newDir.ix = 1;

                // Markieren des Knotens als Dokumentknoten
                mkoIt.Xhtml.Directory.eVal val = new mkoIt.Xhtml.Directory.eVal();
                val.parser = "Editor.aspx";
                val.viewer = "woc.aspx";
                val.Value = destWocName;

                newDir.Items = new object[] { val };

                string newDocSerialized;
                mko.algorithm.InfosSerializer<mkoIt.Xhtml.Directory.e>.WriteToXml(newDir, out newDocSerialized);

                destHomeDir.Add(XElement.Parse(newDocSerialized));

                // Alten Verzeichniseintrag löschen
                if (deleteSrcWoc)
                    srcEntry.Remove();

                // xhtml Datei an neuen Speicherort verschieben/kopieren und Hyperlinks im Dokument anpassen
                string oldXhtmlName = HttpContext.Current.Server.MapPath("~/App_Data/" + srcWocName + ".xhtml");
                
                if (File.Exists(oldXhtmlName))
                {
                    string newXhtmlName = HttpContext.Current.Server.MapPath("~/App_Data/" + destWocName + ".xhtml");

                    if (File.Exists(destWocName))
                        throw new Exception("Das Dokument " + srcWocName + ".xhtml kann nicht in " + destWocName + " umbenannt werden, da  Datei bereits existiert !");

                    if (deleteSrcWoc)
                        File.Move(oldXhtmlName, newXhtmlName);
                    else
                        File.Copy(oldXhtmlName, newXhtmlName);

                    // Zugehörige Bilder umbenennen
                    // Alle Bilder zum Dokument bestimmen                
                    foreach (string img in Directory.GetFiles(HttpContext.Current.Server.MapPath("~/Bilder")))
                    {
                        if (img.Contains(srcWocName))
                        {
                            // Umbenennen
                            string newImgName = img.Replace(srcWocName, destWocName);
                            if (deleteSrcWoc)
                                File.Move(img, newImgName);
                            else
                                File.Copy(img, newImgName);
                        }
                    }

                    // Bildreferenzen im Xhtml- Dokument ändern         
                    string txtXhtml = "<body>" + File.ReadAllText(newXhtmlName) + "</body>";

                    XElement xhtmlDoc = XElement.Parse(txtXhtml);
                    foreach (XElement img in xhtmlDoc.XPathSelectElements("//img"))
                    {
                        string oldSrc = img.Attribute(XName.Get("src")).Value;
                        string newSrc = oldSrc.Replace(srcWocName, destWocName);
                        img.SetAttributeValue(XName.Get("src"), newSrc);
                    }

                    // Geändertes Xhtml zurückschreiben
                    StringBuilder bld = new StringBuilder();
                    foreach (XElement xe in xhtmlDoc.Elements())
                        bld.Append(xe.ToString());

                    File.WriteAllText(newXhtmlName, bld.ToString());
                }

                // Erweiterten Verzeichnisbaum sichern
                SaveDirectoryChanges();
            }

            return true;
        }

        public bool DocDelete(string srcWocName)
        {
            lock (this)
            {
                string[] srcLevels = srcWocName.Split('.');

                // Zugriff auf den bestehenden Knoten
                XElement srcEntry = _directory.XPathSelectElement(WocNameToXPath(srcLevels), _nsmgr);
                Debug.Assert(srcEntry != null);

                // Alten Verzeichniseintrag löschen
                srcEntry.Remove();

                // xhtml- Datei löschen
                string oldXhtmlName = HttpContext.Current.Server.MapPath("~/App_Data/" + srcWocName + ".xhtml");

                if (File.Exists(oldXhtmlName))
                    File.Delete(oldXhtmlName);

                // Zugehörige Bilder löschen
                // Alle Bilder zum Dokument bestimmen                
                foreach (string img in Directory.GetFiles(HttpContext.Current.Server.MapPath("~/Bilder")))
                {
                    if (img.Contains(srcWocName))
                        File.Delete(img);
                }

                // Erweiterten Verzeichnisbaum sichern
                SaveDirectoryChanges();
            }

            return true;
        }

        /// <summary>
        /// Entfernt alle führenden und nachfolgenden Leerzeichen, wnadelt Umlaute in umschreibungen um etc.
        /// </summary>
        /// <param name="woc"></param>
        /// <returns></returns>
        public static string NormalizeWocName(string woc)
        {            
            return woc.Trim().ToLower().Replace(' ', '_').Replace("ö", "oe").Replace("ä", "ae").Replace("ü", "ue").Replace("ß", "sz");
        }

        public static void NormalizeWocLevels(string[] levels)
        {
            for (int i = 0; i < levels.Length; i++)
            {
                levels[i] = NormalizeWocToken(levels[i]);
            }
        }

        /// <summary>
        /// Ein WocToken ist in einem Punktstrukturierten Namen (z.B. wissen.programmierung.asp_net) ein 
        /// Partikel (z.B. programmierung oder asp_net). 
        /// Durch die Normalisierung werden sonderzeichen in Umschreibungen umgewandelt
        /// </summary>
        /// <param name="wocToken"></param>
        /// <returns></returns>
        public static string NormalizeWocToken(string wocToken)
        {
            return System.Xml.XmlConvert.EncodeLocalName(
                wocToken
                    .Trim()
                    .ToLower()
                    .Replace('.', '_')
                    .Replace(' ', '_')
                    .Replace("ö", "oe")
                    .Replace("ä", "ae")
                    .Replace("ü", "ue")
                    .Replace("ß", "sz"));
        }

        private static string WocNameToXPath(string[] Levels)
        {
            string xPathExp = ".";
            if (Levels.Length > 1)
                for (int i = 1; i < Levels.Length; i++)
                {
                    xPathExp = xPathExp + "/vz:e[@id='" + Levels[i] + "']";
                }
            return xPathExp;
        }

        private static string WocNameToXPath(string[] Levels, int upToLevel)
        {
            Debug.Assert(upToLevel > 0 && upToLevel <= Levels.Length);
            string xPathExp = ".";
            if (Levels.Length > 1)
                for (int i = 1; i < upToLevel; i++)
                {
                    xPathExp = xPathExp + "/vz:e[@id='" + Levels[i] + "']";
                }
            return xPathExp;
        }

        public static string GetWocNodeId(string[] levels)
        {
            if (levels.Length <= 1)
                throw new Exception("Aus einem leeren Woc- Name kann keine NodeID bestimmt werden");
            return levels[levels.Length - 1];
        }

        public static string GetWocPath(string[] levels)
        {
            string path = levels[0];
            for (int i = 1; i < levels.Length - 1; i++)
            {
                path += "." + levels[i];
            }
            return path;
        }

        public static string GetWocName(string[] levels)
        {
            string path = levels[0];
            for (int i = 1; i < levels.Length; i++)
                path += "." + levels[i];
            return path;
        }

    }
}

