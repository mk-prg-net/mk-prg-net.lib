using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using TechTerms = ATMO.mko.Logging.PNDocuTerms.DocuEntities.Composer.TechTerms;

namespace ATMO.mko.Logging.PNDocuTerms.DocuEntities
{
    /// <summary>
    /// mko, 28.2.2019
    /// Fasst Mehtoden zusammen, mit denen unterhalb eines DocuEntity nach Substrukturen gesucht werden kann
    /// </summary>
    public static partial class DocuEntityHlp
    {
        /// <summary>
        /// mko, 28.2.2019
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>true if a and b of same entity type and immediate content of a is the same as b</returns>
        public static bool IsEqualTo(this IDocuEntity a, IDocuEntity b)
        {
            if (a.EntityType == b.EntityType)
            {
                if (a.IsNamed())
                    // Value compare is not necessary because Value of named Docentity is EntityType.ToString()
                    return b.IsNamed() && a.Name() == b.Name();
                else
                    return !b.IsNamed() && a.Value == b.Value;
            }
            else return false;
        }

        /// <summary>
        /// mko, 28.2.2019
        /// Returns true, if a is an embeded subtree in b.
        /// Order of childs is impoertant but not the absolute position:
        /// e.g.
        ///     If X := a(b(d f)) and Y:= a(b(c d e f) g(h i)) are a trees,
        ///     Then X is subtree of Y!
        /// 
        /// e.g. (Subtree- Nodes are marked with *)
        /// 
        /// a*
        /// +-b*
        /// | +-c
        /// | +-d*  -+
        /// | +-e    +- d and f has an offset, but the order of d and f is preserved
        /// | +-f*  -+
        /// |
        /// +-g
        ///   +-h
        ///   +-i
        /// 
        /// </summary>
        /// <param name="subTreePattern">possible subtree</param>
        /// <param name="treeRoot">main tree</param>
        /// <returns>true if a is a subtree of b</returns>
        public static bool IsSubTreeOf(this IDocuEntity subTreePattern, IDocuEntity treeRoot, bool searchAnywhere = true)
        {
            bool isSubTree = false;

            //if (searchAnywhere && !subTreePattern.IsEqualTo(treeRoot))
            //{
            //    // move to first node in tree that matches the subTreeRoot
            //    treeRoot = subTreePattern.FindIn(treeRoot);
            //}

            if (subTreePattern.IsEqualTo(treeRoot))
            {
                if (!subTreePattern.Childs.Any())
                    // a hat keine weiteren Kinder- hier endet der strukturelle Vergleich mit b,
                    // uns a wird als in b enthalten betrachtet.
                    isSubTree = true;
                else
                {
                    var subTreeRootChilds = subTreePattern.IsNamed() ? subTreePattern.Childs.Skip(1) : subTreePattern.Childs;
                    var treeRootChildsEnum = treeRoot.IsNamed() ? treeRoot.Childs.Skip(1).GetEnumerator() : treeRoot.Childs.GetEnumerator();

                    // all subC must be contained in treeRootChilds
                    var foundSubTreeNodes = 0;
                    foreach (var subC in subTreeRootChilds)
                    {
                        bool isSub = false;

                        // compare subChild child by child with treeChilds until subChild matches or all treeChilds are checked.
                        while (treeRootChildsEnum.MoveNext())
                        {
                            var tc = treeRootChildsEnum.Current;

                            // hier false notwendig, damit sichergestellt wird, das Wurzel vom Subtree
                            // mit der Wurzel vom Tree übereinstimmen müssen.
                            isSub = subC.IsSubTreeOf(tc, false);

                            // break, if a subC is embedded in tree
                            if (isSub)
                                break;
                        }

                        // break, if current subTreeChild node does not match with any child node of tree
                        if (isSub)
                            foundSubTreeNodes++;
                    }

                    isSubTree = foundSubTreeNodes == subTreeRootChilds.Count();
                }
            }
            else if (searchAnywhere)
            {
                // Der Treenode selbst stimmt nicht mit dem subTreePattern überein.
                // Weiter in der Tiefe des Baumes nach einem Subtree suchen

                var TreeRootChilds = treeRoot.IsNamed() ? treeRoot.Childs.Skip(1) : treeRoot.Childs;

                if (TreeRootChilds.Any())
                {
                    foreach (var subC in TreeRootChilds)
                    {
                        isSubTree = IsSubTreeOf(subTreePattern, subC, true);

                        if (isSubTree)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    // Im Baum wurde keine Übereinstimmung mit der Teilbaumstruktur gefunden
                    isSubTree = false;
                }
            }
            else
            {
                isSubTree = false;
            }

            return isSubTree;
        }

        /// <summary>
        /// mko, 29.3.2019
        /// Eine Baumstruktur als Teilbaum (Muster) in einem anderen Baume suchen. Wenn das Muster auf einem Zweig im anderen Baum passt, die Wurzel dieses Zweiges
        /// zurückgeben.
        /// Die Suche erfolgt top-down.
        /// </summary>
        /// <param name="subTreePattern"></param>
        /// <param name="treeRoot"></param>
        /// <param name="searchAnywhere">Wenn false, dann muss der Baumn mit dem Teilbaumabschnitt beginnen. sonst wird nach dem ersten Teilbaum linksrekursiv gesucht</param>
        /// <returns></returns>
        public static RCV3sV<(IDocuEntity subTree, IDocuEntity subTreeParent, long depth)> AsSubTreeOf(this IDocuEntity subTreePattern, IDocuEntity treeRoot, IComposer pnL, bool searchAnywhere = true, long deepth = 0, IDocuEntity subTreeParent = null)
        {
            var ret = RCV3sV<(IDocuEntity, IDocuEntity, long)>.Failed(value: (null, null, -1), pnL.ReturnNotCompleted("SearchAsSubTreeOf"));

            bool res = false;

            IDocuEntity subTree = null;

            //if (searchAnywhere && !subTreePattern.IsEqualTo(treeRoot))
            //{
            //    // move to first node in tree that matches the subTreeRoot

            //    (IDocuEntity node, IDocuEntity parent, long d) = subTreePattern.FindNodeAndPositionIn(treeRoot);
            //    treeRoot = node;
            //    subTreeParent = parent;
            //    deepth = d;
            //}            

            if (subTreePattern.IsEqualTo(treeRoot))
            {
                var patternChilds = subTreePattern.IsNamed() ? subTreePattern.Childs.Skip(1) : subTreePattern.Childs;
                if (!patternChilds.Any())
                {
                    // a hat keine weiteren Kinder- hier endet der strukturelle Vergleich mit b,
                    // und a wird als in b enthalten betrachtet.
                    res = true;
                    subTree = treeRoot;

                    ret = RCV3sV<(IDocuEntity, IDocuEntity, long)>.Ok(value: (subTree, subTreeParent, deepth));
                }
                else
                {
                    // Prüfen, ob die Knoten des subTreePatterns in der Menge der Kindknoten vom Tree enthalten sind.
                    var treeRootChildsEnum = treeRoot.IsNamed() ? treeRoot.Childs.Skip(1).GetEnumerator() : treeRoot.Childs.GetEnumerator();

                    // all subC must be contained in treeRootChilds
                    var foundSubTreeNodes = 0;
                    foreach (var subC in patternChilds)
                    {
                        bool isSub = false;

                        // compare subChild child by child with treeChilds until subChild matches or all treeChilds are checked.
                        while (treeRootChildsEnum.MoveNext())
                        {
                            var tc = treeRootChildsEnum.Current;

                            // hier false notwendig, damit sichergestellt wird, das Wurzel vom Subtree
                            // mit der Wurzel vom Tree übereinstimmen müssen.
                            isSub = subC.IsSubTreeOf(tc, false);

                            // break, if a subC is embedded in tree
                            if (isSub)
                                break;
                        }

                        // break, if current subTreeChild node does not match with any child node of tree
                        if (isSub)
                            foundSubTreeNodes++;
                    }

                    res = foundSubTreeNodes == patternChilds.Count();
                    if (res)
                    {
                        subTree = treeRoot;
                        ret = RCV3sV<(IDocuEntity, IDocuEntity, long)>.Ok(value: (subTree, subTreeParent, deepth));
                    }
                    else if(searchAnywhere)
                    {
                        // Ein Knoten vom gesuchten Typ und gesuchten Name wurde gefunden, jedoch stimmen die Childnodes noch nicht überein.                        
                        // Weiter in der Tiefe des Baumes nach einem Subtree suchen

                        var TreeRootChilds = treeRoot.IsNamed() ? treeRoot.Childs.Skip(1) : treeRoot.Childs;

                        if (TreeRootChilds.Any())
                        {
                            foreach (var subC in TreeRootChilds)
                            {
                                ret = AsSubTreeOf(subTreePattern, subC, pnL, true, deepth + 1, treeRoot);

                                if (ret.Succeeded)
                                {
                                    break;
                                }
                            }
                        } else
                        {
                            // Im Baum wurde keine Übereinstimmung mit der Teilbaumstruktur gefunden
                            ret = RCV3sV<(IDocuEntity, IDocuEntity, long)>.Failed(value: (null, null, -1), pnL.ReturnSearchFailsEmptyResult(subTreePattern));
                        }
                    } else
                    {
                        // Im Baum wurde keine Übereinstimmung mit der Teilbaumstruktur gefunden
                        ret = RCV3sV<(IDocuEntity, IDocuEntity, long)>.Failed(value: (null, null, -1), pnL.ReturnSearchFailsEmptyResult(subTreePattern));
                    }
                }
            }
            else if (searchAnywhere)
            {
                // Der Treenode selbst stimmt nicht mit dem subTreePattern überein.
                // Weiter in der Tiefe des Baumes nach einem Subtree suchen

                var TreeRootChilds = treeRoot.IsNamed() ? treeRoot.Childs.Skip(1) : treeRoot.Childs;

                if (TreeRootChilds.Any())
                {
                    foreach (var subC in TreeRootChilds)
                    {
                        ret = AsSubTreeOf(subTreePattern, subC, pnL, true, deepth + 1, treeRoot);

                        if (ret.Succeeded)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    // Im Baum wurde keine Übereinstimmung mit der Teilbaumstruktur gefunden
                    ret = RCV3sV<(IDocuEntity, IDocuEntity, long)>.Failed(value: (null, null, -1), pnL.ReturnSearchFailsEmptyResult(subTreePattern));
                }

            }
            else
            {
                ret = RCV3sV<(IDocuEntity, IDocuEntity, long)>.Failed(value: (null, null, -1), pnL.ReturnSearchFailsEmptyResult(subTreePattern));
            }

            return ret;
        }

        /// <summary>
        /// mko, 1.4.2019
        /// Sucht alle Teilbäume in einem Baum mit gegebener Struktur.
        /// </summary>
        /// <param name="subTreePattern"></param>
        /// <param name="treeRoot"></param>
        /// <param name="pnL"></param>
        /// <param name="deepth"></param>
        /// <returns></returns>
        public static RCV3sV<IEnumerable<(IDocuEntity subTree, IDocuEntity subTreeParent, long depth)>> AsSubTreeOf_AllOccurrences
            (this IDocuEntity subTreePattern, 
            IDocuEntity treeRoot, 
            IComposer pnL, 
            long deepth = 0, 
            IDocuEntity parent = null)
        {            
            Debug.Assert(pnL != null);

            var matches = new List<(IDocuEntity subTree, IDocuEntity subTreeParent, long depth)>();

            var ret = RCV3sV<IEnumerable<(IDocuEntity subTree, IDocuEntity subTreeParent, long depth)>>.Failed(
                value: matches,
                pnL.ReturnNotCompleted(
                    "AsSubTreeOf_AllOccurrences",
                    pnL.p("subTreePattern", subTreePattern),
                    pnL.p("TreeRoot", treeRoot),
                    pnL.p("deepth", deepth)));

            try
            {

                // Prüfen von Vorbedingungen, die in der kopmplexen Umgebung des Aufrufes nicht notwendigerweise erfüllt sein müssen
                // da treeRoot und subTreePattern ergebnisse von vorausgegangenen Berechnungen sein können.
                // Deshalb ist Debug.Assert hier nicht ausreichend.
                if (subTreePattern == null)
                {
                    return RCV3sV<IEnumerable<(IDocuEntity subTree, IDocuEntity subTreeParent, long depth)>>.Failed(
                        value: matches,
                        pnL.ReturnValidatePreconditionNotNullFailed(pnL.p(TechTerms.MetaData.Arg, "subTreePattern")));
                }
                else if (treeRoot == null)
                {
                    return RCV3sV<IEnumerable<(IDocuEntity subTree, IDocuEntity subTreeParent, long depth)>>.Failed(
                        value: matches,
                        pnL.ReturnValidatePreconditionNotNullFailed(pnL.p(TechTerms.MetaData.Arg, "treeRoot")));
                }
                else
                {

                    var first = subTreePattern.AsSubTreeOf(treeRoot, pnL, false, deepth, parent);
                    if (first.Succeeded)
                    {
                        matches.Add(first.Value);
                    }

                    if(first.Succeeded || !first.Succeeded && pnL.ReturnSearchFailsEmptyResult().IsSubTreeOf(first.MessageEntity))
                    {
                        ret = RCV3sV<IEnumerable<(IDocuEntity subTree, IDocuEntity subTreeParent, long depth)>>.Ok(matches);

                        // Weitere Teilbäume innerhalb des aktuellen Teilbaumes suchen
                        foreach (var child in treeRoot.Childs)
                        {
                            var getAllSubtrees = subTreePattern.AsSubTreeOf_AllOccurrences(child, pnL, deepth + 1, treeRoot);
                            if (getAllSubtrees.Succeeded)
                            {
                                matches.AddRange(getAllSubtrees.Value);
                            } else
                            {
                                ret = RCV3sV<IEnumerable<(IDocuEntity subTree, IDocuEntity subTreeParent, long depth)>>.Failed(matches, getAllSubtrees.ToPlx());
                                break;
                            }
                        }

                        if (ret.Succeeded)
                        {
                            // Rückgabewert mit den allen Treffern aktualisieren
                            ret = RCV3sV<IEnumerable<(IDocuEntity subTree, IDocuEntity subTreeParent, long depth)>>.Ok(matches);
                        }                        
                    }
                    else
                    {
                        // Fall: In der Suche ging was schief
                        ret = RCV3sV<IEnumerable<(IDocuEntity subTree, IDocuEntity subTreeParent, long depth)>>.Failed(value: matches, first.ToPlx());
                    }
                
                }
            } catch(Exception ex)
            {
                ret = RCV3sV<IEnumerable<(IDocuEntity subTree, IDocuEntity subTreeParent, long depth)>>.Failed(value: matches, TraceHlp.FlattenExceptionMessagesPN(ex));
            }

            return ret;
        }




        /// <summary>
        /// mko, 28.2.2019
        /// Returns true, if e is named with given name, and is of given type
        /// </summary>
        /// <param name="e"></param>
        /// <param name="eType"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool IsEntityOfTypeWithName(this IDocuEntity e, DocuEntityTypes eType, string name)
            => e.EntityType == eType && e.IsNamed() && e.Name() == name;


        /// <summary>
        /// mko, 28.2.2019
        /// Finds a given docu entity in a given tree or not. Search is top/down. Returns the first matching entity.
        /// </summary>
        /// <param name="node">docuEntity to find</param>
        /// <param name="tree">tree where to find the docuEntitiy</param>
        /// <returns>docuEntity as embedded node in tree</returns>
        public static IDocuEntity FindIn(this IDocuEntity node, IDocuEntity tree)
        {
            IDocuEntity instance = null;

            if (node.IsEqualTo(tree))
                instance = node;
            else
            {
                foreach (var child in tree.Childs)
                {
                    // mko, 12.11.2018
                    // Now more robust in case of empty child lists
                    //if (child.EntityType == dType && child.Childs.FirstOrDefault()?.Value == name)
                    if (node.IsEqualTo(child))
                    {
                        instance = child;
                    }
                }

                if (instance == null)
                {
                    // Search for instance a level deeper
                    foreach (var child in tree.Childs)
                    {
                        instance = node.FindIn(child);
                        if (instance != null)
                        {
                            // Found an Instance
                            break;
                        }
                    }
                }
            }

            return instance;
        }

        /// <summary>
        /// mko, 28.2.2019
        /// Finds a given docu entity in a given tree or not. Search is top/down. Returns the first matching entity.
        /// </summary>
        /// <param name="node">docuEntity to find</param>
        /// <param name="tree">tree where to find the docuEntitiy</param>
        /// <returns>docuEntity as embedded node in tree</returns>
        public static (IDocuEntity treeNode, IDocuEntity treeNodeParent, long deepth) FindNodeAndPositionIn(this IDocuEntity node, IDocuEntity tree, long deepth = 0)
        {
            IDocuEntity instance = null;
            IDocuEntity treeNodeParent = null;

            long _deepth = deepth;

            if (node.IsEqualTo(tree))
            {
                instance = node;
            }
            else
            {
                foreach (var child in tree.Childs)
                {
                    // mko, 12.11.2018
                    // Now more robust in case of empty child lists
                    //if (child.EntityType == dType && child.Childs.FirstOrDefault()?.Value == name)
                    if (node.IsEqualTo(child))
                    {
                        treeNodeParent = tree;
                        instance = child;
                        deepth += 1;
                        break;
                    }
                }

                if (instance == null)
                {
                    // Search for instance a level deeper
                    foreach (var child in tree.Childs)
                    {
                        (instance, treeNodeParent, deepth) = node.FindNodeAndPositionIn(child, deepth + 1);
                        if (instance != null)
                        {
                            // Found an Instance
                            break;
                        }
                    }
                }
            }

            return (instance, treeNodeParent, deepth);
        }


        /// <summary>
        /// mko, 11.3.2019
        /// Returns all nodes matching entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tree"></param>
        /// <param name="matches"></param>
        /// <param name="depth">Rekursionstiefe</param>
        /// <returns>Liste aus (IDocEntity, depth) Wertepaare. Depth gibt an, wieviele Stufen im Baum unterhalb des 
        /// Entities das passende Element gefunden wurde</returns>
        public static List<(IDocuEntity node, int depth)> FindAllIn(this IDocuEntity entity, IDocuEntity tree, List<(IDocuEntity node, int depth)> matches = null, int depth = 0)
        {
            if (matches == null)
            {
                matches = new List<(IDocuEntity node, int depth)>();
            }

            if (entity.IsEqualTo(tree))
                matches.Add((tree, depth));

            foreach (var child in tree.Childs)
            {
                entity.FindAllIn(child, matches, depth + 1);
            }

            return matches;

        }


        /// <summary>
        /// mko, 18.4.2018
        /// Search for a farest descendant of an entity with defined type and name.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="root"></param>
        /// <returns></returns>
        public static IDocuEntity FindNamedEntity(this IDocuEntity root, DocuEntityTypes dType, string name)
        {
            IDocuEntity instance = null;
            foreach (var child in root.Childs)
            {
                // mko, 12.11.2018
                // Now more robust in case of empty child lists
                //if (child.EntityType == dType && child.Childs.FirstOrDefault()?.Value == name)
                if (child.IsEntityOfTypeWithName(dType, name))
                {
                    instance = child;
                }
            }

            if (instance == null)
            {
                // Search for instance a level deeper
                foreach (var child in root.Childs)
                {
                    instance = FindNamedEntity(child, dType, name);
                    if (instance != null)
                    {
                        // Found an Instance
                        break;
                    }
                }
            }

            return instance;
        }

        /// <summary>
        /// mko, 18.8.2018
        /// Search for a child of an entity with defined type and name. Depth of Search will be restricted.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="root"></param>
        /// <returns></returns>
        public static IDocuEntity FindNamedEntity(this IDocuEntity root, DocuEntityTypes dType, string name, int UpToLevel)
        {
            IDocuEntity instance = null;
            foreach (var child in root.Childs)
            {
                // mko, 12.11.2018
                // Now more robust in case of emptiy child lists
                //if (child.EntityType == dType && child.Childs.FirstOrDefault()?.Value == name)
                if (child.IsEntityOfTypeWithName(dType, name))
                {
                    instance = child;
                }
            }

            UpToLevel--;

            if (instance == null && UpToLevel > 0)
            {
                // Search for instance a level deeper
                foreach (var child in root.Childs)
                {
                    instance = FindNamedEntity(child, dType, name, UpToLevel);
                    if (instance != null)
                    {
                        // Found an Instance
                        break;
                    }
                }
            }

            return instance;
        }

        /// <summary>
        /// mko, 28.2.2019
        /// Search for all descendants of an entity with defined type and name.
        /// </summary>
        /// <param name="root"></param>
        /// <param name="dType"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IEnumerable<IDocuEntity> FindAllNamedEntities(this IDocuEntity root, DocuEntityTypes dType, string name)
        {
            var instances = new List<IDocuEntity>();
            foreach (var child in root.Childs)
            {
                // mko, 12.11.2018
                // Now more robust in case of emptiy child lists
                //if (child.EntityType == dType && child.Childs.FirstOrDefault()?.Value == name)
                if (child.IsEntityOfTypeWithName(dType, name))
                {
                    instances.Add(child);
                }
            }

            // Search for instance a level deeper
            foreach (var child in root.Childs)
            {
                instances.AddRange(FindAllNamedEntities(child, dType, name));
            }

            return instances;
        }
    }
}
