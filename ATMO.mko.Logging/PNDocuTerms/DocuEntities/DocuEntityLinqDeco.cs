using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mko.RPN;

using TechTerms = ATMO.mko.Logging.PNDocuTerms.DocuEntities.Composer.TechTerms;
using static mko.RPN.UrlSaveStringEncoder;


namespace ATMO.mko.Logging.PNDocuTerms.DocuEntities
{
    /// <summary>
    /// mko, 26.10.2018
    /// Decorator, der einfache struturelle Untersuchungen auf Rückgabetypen ermöglicht.
    /// 
    /// mko, 5.3.2019
    /// Funktionen für den Zugriff auf Date, Time und Version hinzugefügt.
    /// </summary>
    public class DocuEntityLinqDeco : IDocuEntity
    {
        IDocuEntity entity;

        public DocuEntityLinqDeco(IDocuEntity entity)
        {
            this.entity = entity;
        }

        /// <summary>
        /// mko, 22.11.2018
        /// returns true, if this docEntity is a instance with given name.
        /// </summary>
        public bool IsInstance(string name)
        {
            return EntityType == DocuEntityTypes.Instance && DocuEntityHlp.Name(this) == name;
        }


        public string Text => DocuEntityHlp.EntityValue(entity)?.GetText();


        public bool IsMethod(string name)
        {
            return EntityType == DocuEntityTypes.Method && DocuEntityHlp.Name(this) == name;
        }

        public bool IsProperty(string name)
        {
            return EntityType == DocuEntityTypes.Property && DocuEntityHlp.Name(this) == name;
        }

        //public bool IsReturn(string name)
        public bool IsReturn()
        {
            return EntityType == DocuEntityTypes.ReturnValue; // && DocuEntityHlp.Name(this) == name;
        }

        public bool IsEvent(string name)
        {
            return EntityType == DocuEntityTypes.Event && DocuEntityHlp.Name(this) == name;
        }

        
        public bool IsTime()
        {
            return EntityType == DocuEntityTypes.Time;
        }

        public TimeSpan Time => TimeSpan.Parse(entity.Childs.First().Value);

        public bool IsDate()
        {
            return EntityType == DocuEntityTypes.Date;
        }

        public DateTime Date => DateTime.Parse(entity.Childs.First().Value);



        public bool IsVersion()
        {
            return EntityType == DocuEntityTypes.Version;
        }

        public string Version => DocuEntityHlp.EntityValue(entity)?.GetText();


        /// <summary>
        /// mko, 22.11.2018
        /// Converts named entity in a NamedLinqDeco
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DocuEntityWithNameLinqDeco AsNamedLinqDeco
        {            get
            {
                return new DocuEntityWithNameLinqDeco(entity);
            }
        }

        /// <summary>
        /// mko, 22.11.2018
        /// </summary>
        public DocuEntityAsPropertyLinqDeco AsPropLinqDeco
        {
            get
            {
                return new DocuEntityAsPropertyLinqDeco(entity);
            }
        }


        /// <summary>
        /// Alle unterhalb dieses Knotens definierten Instanzen
        /// </summary>
        public IEnumerable<DocuEntityWithNameLinqDeco> Instances => GetAllChildsOfType(this, DocuEntityTypes.Instance);

        /// <summary>
        /// mko, 7.1.2019
        /// Liefert die Instanz mit dem gegebenen Namen, die Kind dieses DocuEntites ist. Wenn es nicht existiert,
        /// dann wird null zurückgegeben.
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public DocuEntityWithNameLinqDeco Instance(string Name)
        {
            return Instances.FirstOrDefault(r => r.Name == Name);
        }

        /// <summary>
        /// Alle unterhalb dieses Knotens definierten Eigenschaften
        /// </summary>
        public IEnumerable<DocuEntityWithNameLinqDeco> Properties => GetAllChildsOfType(this, DocuEntityTypes.Property);

        /// <summary>
        /// mko, 7.1.2019
        /// Liefert die Eigenschaft mit dem gegebenen Namen, die Kind dieses DocuEntites ist. Wenn sie nicht existiert,
        /// dann wird null zurückgegeben.
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public DocuEntityAsPropertyLinqDeco Property(string Name)
        {
            return Properties.FirstOrDefault(r => r.Name == Name)?.AsPropLinqDeco;
        }

        /// <summary>
        /// Alle unterhalb dieses Knotens definierten Methoden
        /// </summary>
        public IEnumerable<DocuEntityWithNameLinqDeco> Methods => GetAllChildsOfType(this, DocuEntityTypes.Method);


        /// <summary>
        /// mko, 7.1.2019
        /// Liefert die Methode mit dem gegebenen Namen, die Kind dieses DocuEntites ist. Wenn sie nicht existiert,
        /// dann wird null zurückgegeben.
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public DocuEntityWithNameLinqDeco Method(string Name)
        {
            return Methods.FirstOrDefault(r => r.Name == Name);
        }

        /// <summary>
        /// Alle unterhalb dieses Knotens definierten Returnwerte
        /// </summary>
        public IEnumerable<DocuEntityWithNameLinqDeco> Returns => GetAllChildsOfType(this, DocuEntityTypes.ReturnValue);


        /// <summary>
        /// mko, 7.1.2019
        /// Liefert den Return Block einer Methode mit gegebenen Namen, die Kind des aktuellen IDocuEntity ist.
        /// Gibt es eine solche nicht, dann wird null zurückgegeben.
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public DocuEntityWithNameLinqDeco ReturnValueOf(string Name)
        {
            DocuEntityWithNameLinqDeco ret = null;

            //var retp=  entity.Childs.First(r => r.EntityType == DocuEntityTypes.Method && r.Name() == Name)?.Childs.First(r => r.EntityType == DocuEntityTypes.ReturnValue);
            var retp = GetAllChildsOfType(entity, DocuEntityTypes.Method).First(r => r.EntityType == DocuEntityTypes.Method && r.Name() == Name)?.Childs.First(r => r.EntityType == DocuEntityTypes.ReturnValue);
            if (retp != null)
            {
                ret = new DocuEntityWithNameLinqDeco(retp);
            }

            return ret;
        }

        /// <summary>
        /// mko, 7.1.2018
        /// Liefert den Return- Wert der aktuellen Methode
        /// </summary>
        /// <returns></returns>
        public DocuEntityWithNameLinqDeco ReturnValue
        {
            get
            {
                DocuEntityWithNameLinqDeco ret = null;

                // Es muss innerhalb einer Methode mit gegebenen Namen gesucht werden.
                if (EntityType == DocuEntityTypes.Method)
                {
                    var retVal = GetAllChildsOfType(entity, DocuEntityTypes.ReturnValue).FirstOrDefault();
                    if (retVal != null)
                    {
                        ret = new DocuEntityWithNameLinqDeco(retVal);
                    }
                }

                return ret;
            }
        }


        /// <summary>
        /// Alle unterhalb dieses Knotens definierten Ereignisse
        /// </summary>
        public IEnumerable<DocuEntityWithNameLinqDeco> Events => GetAllChildsOfType(this, DocuEntityTypes.Event);


        /// <summary>
        /// mko, 27.2.2019
        /// Extrahiert unmittelbar unter diesem Knoten das erste eFails Entity.
        /// </summary>
        public DocuEntityWithNameLinqDeco eFails => Events?.FirstOrDefault(r => r.Name == TechTerms.eFails);


        /// <summary>
        /// mko, 27.2.2019
        /// Extrahiert unmittelbar unter diesem Knoten das erste eWarn Entity.
        /// </summary>
        public DocuEntityWithNameLinqDeco eWarn => Events?.FirstOrDefault(r => r.Name == TechTerms.eWarn);

        /// <summary>
        /// mko, 27.2.2019
        /// Extrahiert unmittelbar unter diesem Knoten das erste eSucceded Entity.
        /// </summary>
        public DocuEntityWithNameLinqDeco eSucceded => Events?.FirstOrDefault(r => r.Name == TechTerms.eSucceeded);

        /// <summary>
        /// mko, 27.2.2019
        /// Extrahiert unmittelbar unter diesem Knoten das erste eStart Entity.
        /// </summary>
        public DocuEntityWithNameLinqDeco eStart => Events?.FirstOrDefault(r => r.Name == TechTerms.eStart);

        /// <summary>
        /// mko, 27.2.2019
        /// Extrahiert unmittelbar unter diesem Knoten das erste eEnd Entity.
        /// </summary>
        public DocuEntityWithNameLinqDeco eEnd => Events?.FirstOrDefault(r => r.Name == TechTerms.eEnd);






        /// <summary>
        /// mko, 7.1.2019
        /// Liefert die Instanz mit dem gegebenen Namen, die Kind dieses DocuEntites ist. Wenn es nicht existiert,
        /// dann wird null zurückgegeben.
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public DocuEntityWithNameLinqDeco Event(string Name)
        {
            return Events.FirstOrDefault(r => r.Name == Name);
        }


        private IEnumerable<DocuEntityWithNameLinqDeco> GetAllChildsOfType(IDocuEntity entity, DocuEntityTypes EntityType)
        {
            // Funktion niemals aufrufen für folgende Typen
            Debug.Assert(!(entity.EntityType == DocuEntityTypes.Date
                || entity.EntityType == DocuEntityTypes.KillIfNot
                || entity.EntityType == DocuEntityTypes.Name
                || entity.EntityType == DocuEntityTypes.String
                || entity.EntityType == DocuEntityTypes.Text
                || entity.EntityType == DocuEntityTypes.Time
                || entity.EntityType == DocuEntityTypes.Version
                ));

            if (entity.Childs.Any(r => r.EntityType == DocuEntityTypes.List))
            {
                return GetAllChildsOfType(entity.Childs.First(r => r.EntityType == DocuEntityTypes.List), EntityType);
            }
            else if (entity.Childs.Any(r => r.EntityType == EntityType))
            {
                // Benannte Dokumentationseinheiten
                return entity.Childs.Where(r => r.EntityType == EntityType).Select(r => new DocuEntityWithNameLinqDeco(r));
            }
            else
            {
                // Wenn es keine Childs vom Typ EntityType gibt, dann leere Liste zurückgeben.
                return new DocuEntityWithNameLinqDeco[] { };
            }
        }

        // IDocuEntity members
        public DocuEntityTypes EntityType => entity.EntityType;

        public IEnumerable<IDocuEntity> Childs => entity.Childs;

        public bool IsFunctionName => entity.IsFunctionName;

        public bool IsInteger => entity.IsInteger;

        public bool IsBoolean => entity.IsBoolean;

        public bool IsNummeric => entity.IsNummeric;

        public string Value => entity.Value;

        public int CountOfEvaluatedTokens => entity.CountOfEvaluatedTokens;

        public IToken Copy()
        {
            return entity.Copy();
        }
    }
}
