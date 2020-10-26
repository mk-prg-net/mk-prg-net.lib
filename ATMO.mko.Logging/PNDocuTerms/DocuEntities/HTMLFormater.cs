using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static ATMO.mko.Logging.PNDocuTerms.DocuEntities.DocuEntityHlp;

namespace ATMO.mko.Logging.PNDocuTerms.DocuEntities
{
    /// <summary>
    /// mko, 23.3.2018
    /// </summary>
    public class HTMLFormater : IFormater
    {
        public string Print(IDocuEntity entity)
        {
            var fn = Fn._;

            return _Print(1, entity);
        }

        string NormalizeName(string name)
        {
            return name.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;").Replace("'", "&apos;"); //.Replace(';', ' ').Replace('#', ' ');
        }

        private string _Print(int Level, IDocuEntity entity)
        {
            string res = "";
            switch (entity.EntityType)
            {
                case DocuEntityTypes.Date:
                    res = $"<time date='{entity.Childs.First().ToString()}'>{entity.Childs.First().ToString()}</time>";
                    break;
                case DocuEntityTypes.Event:
                    {
                        var name = NormalizeName(entity.Name());

                        var color = "#000000";
                        if (entity.IsCommonEventType())
                        {
                            switch (entity.GetEventType())
                            {
                                case EventTypes.start:
                                case EventTypes.end:
                                    color = "#0000ff";
                                    break;
                                case EventTypes.fails:
                                    color = "#ff0000";
                                    break;
                                case EventTypes.info:
                                    color = "#888888";
                                    break;
                                case EventTypes.succeded:
                                    color = "#00A00";
                                    break;
                                case EventTypes.warn:
                                    color = "orange";
                                    break;
                                default:
                                    color = "black";
                                    break;
                            }
                        } else
                        {
                            color = "blue";
                        }

                        if (entity.HasValue())
                        {
                            res = $"<div style='color: {color}'><em>{name}!</em><br/>{_Print(Level + 1, entity.EntityValue())}</div>";
                        }
                        else
                        {
                            res = $"<em style='color: {color}'>{name}!</em>";
                        }
                    }
                    break;
                case DocuEntityTypes.Instance:
                    {
                        var name = NormalizeName(entity.Name());
                        var hLevel = Level > 6 ? 6 : Level;

                        if (entity.HasValue())
                        {
                            // mko, 18.4.2018
                            // xTab <=> pivot table processing
                            if (name == "xTab")
                            {
                                res = xTabFormating(entity);
                            }
                            else
                            {
                                res = $"<h{hLevel}>{name}</h{hLevel}><p>{_Print(Level + 1, entity.EntityValue())}</p>";
                            }
                        }
                        else
                        {
                            res = $"<h{hLevel}>{name}</h{hLevel}>";
                        }
                    }
                    break;
                case DocuEntityTypes.List:
                    {
                        var bld = new StringBuilder();
                        bld.Append("<ol>");
                        foreach (var child in entity.Childs)
                        {
                            bld.Append("<li>");
                            bld.Append(_Print(Level + 1, child));
                            bld.Append("</li>");
                        }
                        bld.Append("</ol>");

                        res = bld.ToString();
                    }
                    break;
                case DocuEntityTypes.Method:
                    {
                        var bld = new StringBuilder();

                        if (entity.HasValue())
                        {
                            bld.Append("<p>");
                            bld.Append($"<dfn><code>{NormalizeName(entity.Name())}</code></dfn><br/>");
                            bld.Append(_Print(Level + 1, entity.EntityValue()));
                            bld.Append("<p>");
                        }
                        else
                        {
                            bld.Append($"<code>{NormalizeName(entity.Name())}()</code><br/>");
                        }
                        res = bld.ToString();
                    }
                    break;
                case DocuEntityTypes.Property:
                    {
                        var bld = new StringBuilder();

                        bld.Append("<p>");
                        bld.Append($"<dfn>{NormalizeName(entity.Name())}</dfn><br/>");
                        bld.Append(_Print(Level + 1, entity.EntityValue()));
                        bld.Append("<p>");

                        res = bld.ToString();
                    }
                    break;
                case DocuEntityTypes.PropertySet:
                    {
                        var bld = new StringBuilder();

                        bld.Append("<p>");
                        bld.Append($"<dfn>{NormalizeName(entity.Name())} :=</dfn><br/>");
                        bld.Append(_Print(Level + 1, entity.EntityValue()));
                        bld.Append("<p>");

                        res = bld.ToString();
                    }
                    break;
                case DocuEntityTypes.String:
                    {
                        res = NormalizeName(entity.Value);
                    }
                    break;
                case DocuEntityTypes.Text:
                    {
                        var bld = new StringBuilder();
                        foreach (var str in entity.Childs)
                        {
                            bld.Append($" {NormalizeName(str.Value)}");
                        }
                        res = bld.ToString();
                    }
                    break;
                case DocuEntityTypes.Version:
                    // 15.11.2018
                    // Bei einer Versionsdefinition ist das erste Kind der Wert und nicht wie bei einer Eigenschaft erst der zweite
                    res = $"<Dfn>Version</Dfn> {_Print(Level + 1, entity.Childs.First())}</br>";
                    break;
                case DocuEntityTypes.ReturnValue:
                    {
                        var bld = new StringBuilder();

                        bld.Append("<p>");

                        // mko, 8.10.2018
                        // Zugriff auf Rückgabewerte robuster gemacht.
                        if (entity.Childs.Any())
                        {
                            bld.Append($"<dfn>&#8599;</dfn><br/>");
                            //bld.Append(_Print(Level + 1, entity.EntityValue()));
                            bld.Append(_Print(Level + 1, entity.Childs.First()));
                        } else
                        {
                            bld.Append($"<dfn>&#8599;; </dfn>");
                        }
                        
                        bld.Append("</p>");

                        res = bld.ToString();

                    }
                    break;
            }

            return res;
        }

        /// <summary>
        /// mko, 18.4.2018
        /// Formats a xTab- property as a cross table
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        string xTabFormating(IDocuEntity entity)
        {
            var res = $"<table>";

            var dim1 = entity.FindNamedEntity(DocuEntityTypes.Property, "dim1");
            if (dim1 != null && dim1.HasValue())
            {
                // First dimension as table header
                res += "<tr><th>&nbsp;</th>";
                var dim1List = dim1.EntityValue();
                var dim1Str = dim1List.Childs.Select(r => r.EntityValue().GetText()).ToArray();

                foreach (var c in dim1Str)
                {
                    res += $"<th>{c}</th>";
                }

                res += "</tr>";

                // Second dimension as table rows

                var dim2 = entity.FindNamedEntity(DocuEntityTypes.Property, "dim2");
                var dim2List = dim2.EntityValue();

                var dim2Str = dim2List.Childs.Select(r => r.EntityValue().GetText()).ToArray();

                var values = entity.FindNamedEntity(DocuEntityTypes.Property, "values"); //.EntityValue().Childs;

                foreach (var c2 in dim2Str)
                {
                    res += $"<tr><td>{c2}</td>";

                    foreach (var c1 in dim1Str)
                    {
                        res += "<td>";

                        // i.e.: #i c1 #_ #p c2 #$ read #. #.

                        var _1 = values.FindNamedEntity(DocuEntityTypes.Instance, c1);
                        if (_1 != null)
                        {
                            var _2 = _1.FindNamedEntity(DocuEntityTypes.Property, c2);

                            if (_2 != null)
                            {
                                res += _2.EntityValue().GetText();
                            } else
                            {
                                res += "&nbsp;";
                            }                           

                        }
                        else
                        {
                            res += "&nbsp;";
                        }

                        res += "</td>";
                    }
                    res += "<tr>";
                }
                res += "</table>";
            }
            else
            {
                res = $"Cross table structure invalid";
            }

            return res;
        }
    }
}
