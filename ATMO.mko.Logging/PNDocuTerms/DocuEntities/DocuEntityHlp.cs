using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMO.mko.Logging.PNDocuTerms.DocuEntities
{
    /// <summary>
    /// mko, 7.3.2018
    /// </summary>
    public static partial class DocuEntityHlp
    {

        /// <summary>
        /// mko, 27.2.2019
        /// Verpackt DocuEntity in einen Decorator, über den es mittels Linq- artiger Ausdrücke 
        /// untersucht werden kann.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static DocuEntityLinqDeco AsLinq(this IDocuEntity entity)
        {
            return new DocuEntityLinqDeco(entity);
        }


        public static bool IsValidPropertyValue(DocuEntityTypes type)
        {
            return type == DocuEntityTypes.Instance
                || type == DocuEntityTypes.Text

                // mko, 26.3.2018: Strings allowed again
                || type == DocuEntityTypes.String
                || type == DocuEntityTypes.List;
        }


        public static bool IsValidMethodParameterType(DocuEntityTypes type)
        {
            // mko, 21.12.2018
            // Parameter eine Methode müssen, falls vorhanden, in einer Liste eingeschlossen sein   
            return type == DocuEntityTypes.List;

        }

        public static bool IsValidMethodParameterListMember(DocuEntityTypes type)
        {
            // mko, 18.10.2018
            // mko, 21.12.2018
            // IsValidPropertyValue(type) ersetzt durch type == DocuEntityTypes.Property
            // erweitert um || type == DocuEntityTypes.ReturnValue || type == DocuEntityTypes.Event;
            //return IsValidPropertyValue(type) || type == DocuEntityTypes.ReturnValue || type == DocuEntityTypes.Event;
            return type == DocuEntityTypes.Property
                || type == DocuEntityTypes.ReturnValue
                || type == DocuEntityTypes.Event
                || type == DocuEntityTypes.List
                || type == DocuEntityTypes.Time
                || type == DocuEntityTypes.Version
                || type == DocuEntityTypes.Date;
        }


        public static bool IsValidInstanceMember(DocuEntityTypes type)
        {
            return type == DocuEntityTypes.List;
        }

        public static bool IsValidListMember(DocuEntityTypes type)
        {
            return type == DocuEntityTypes.List
                || type == DocuEntityTypes.Instance
                || type == DocuEntityTypes.Property
                || type == DocuEntityTypes.PropertySet
                || type == DocuEntityTypes.Method
                || type == DocuEntityTypes.Version
                || type == DocuEntityTypes.ReturnValue
                || type == DocuEntityTypes.Event;

        }

        /// <summary>
        /// mko, ?
        /// Returns Name of named Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string Name(this IDocuEntity entity)
        {
            // check, if Name exists
            var first = entity.Childs.FirstOrDefault();
            TraceHlp.ThrowArgExIfNot(first != null && first is String, $"Name of a non named DocuEntity of type {entity.EntityType} requested ");
            return entity.Childs.FirstOrDefault()?.Value;
        }

        /// <summary>
        /// mko, 28.2.2019
        /// Returns true, if entiy is a named entity (e.g. Method, Instance...)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static bool IsNamed(this IDocuEntity entity)
        {
            var first = entity.Childs.FirstOrDefault();
            var nonNamedeitityTypes = new DocuEntityTypes[] {
                                            DocuEntityTypes.Date,
                                            //DocuEntityTypes.Event,
                                            DocuEntityTypes.List,
                                            DocuEntityTypes.ReturnValue,
                                            DocuEntityTypes.String,
                                            DocuEntityTypes.Text,
                                            DocuEntityTypes.Time,
                                            DocuEntityTypes.Version };

            return !nonNamedeitityTypes.Contains(entity.EntityType);

        }

        /// <summary>
        /// mko, ?
        /// Checks if Entity has Value
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static bool HasValue(this IDocuEntity entity)
            => entity.Childs.Count() > 1;



        public static IDocuEntity EntityValue(this IDocuEntity entity)
        {
            return entity.Childs.Skip(1)?.FirstOrDefault();
        }

        public static IDocuEntity EntityValue(this IDocuEntity entity, int no)
        {
            return entity.Childs.Skip(1 + no)?.FirstOrDefault();
        }

        /// <summary>
        /// mko, 18.4.2018
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string GetText(this IDocuEntity entity)
        {
            TraceHlp.ThrowArgExIfNot(entity.EntityType == DocuEntityTypes.Text, "doc entity is not a text!");

            return string.Join(" ", entity.Childs.Select(r => r.Value));

        }


        /// <summary>
        /// mko, 23.4.2018
        /// Returns the value of Version Entity, contained in a Entity. 
        /// </summary>
        /// <param name="ElemWithVersion"></param>
        /// <returns></returns>
        public static RCV2<string> GetVersion(this IDocuEntity ElemWithVersion)
        {
            RCV2<string> rc = RCV2<string>.Failed();

            if (ElemWithVersion.EntityType != DocuEntityTypes.Instance || ElemWithVersion.EntityType != DocuEntityTypes.Method)
            {
                rc = RCV2<string>.Failed(ErrorDescription: "Only instances (#i) or methods (#m) can contains a version element");
            }

            foreach (var child in ElemWithVersion.Childs.Skip(1).First().Childs)
            {
                if (child.EntityType == DocuEntityTypes.Version)
                {
                    var ver = child.Name();
                    rc = RCV2<string>.Ok(ver);
                    break;
                }
            }

            return rc;
        }



        public enum EventTypes
        {
            info,
            warn,
            fails,
            start,
            end,
            succeded,
        }


        public static Dictionary<string, EventTypes> MapStringToEventType = new Dictionary<string, EventTypes>()
        {
            {"info", EventTypes.info },
            {"warn", EventTypes.warn },
            {"fails", EventTypes.fails },
            {"start", EventTypes.start },
            {"end", EventTypes.end },
            {"succeeded", EventTypes.succeded }
        };

        /// <summary>
        /// mko, 2.7.2018
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static bool IsCommonEventType(this IDocuEntity entity)
        {
            return entity.EntityType == DocuEntityTypes.Event && MapStringToEventType.ContainsKey(entity.Name());
        }

        public static EventTypes GetEventType(this IDocuEntity entity)
        {
            TraceHlp.ThrowArgExIfNot(entity.EntityType == DocuEntityTypes.Event, "Entity is not a event");
            return MapStringToEventType[entity.Name()];
        }

    }

}
