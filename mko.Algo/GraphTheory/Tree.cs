using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Algo.GraphTheory
{
    public class Tree
    {
        /// <summary>
        /// Wendet eine Operation auf jeden Knoten eines BAumes an. Baum wird in einer Tiefensuche durchlaufen
        /// (Depth First)
        /// </summary>
        /// <typeparam name="TNode">Typ der Baumknoten</typeparam>
        /// <param name="node">Wurzelknoten</param>
        /// <param name="GetChilds">Funktion, die zu einem Baumkonten alle unmittelbaren Nachfolger liefert</param>
        /// <param name="OP">Operation, die auf jeden Knoten angewendet werden soll</param>
        public static void ForEachDF<TNode>(TNode node, Func<TNode, IEnumerable<TNode>> GetChilds, Action<TNode> OP)
        {
            OP(node);
            IEnumerable<TNode> childs = GetChilds(node);
            foreach (var child in childs)
                ForEachDF(child, GetChilds, OP);
        }

        /// <summary>
        /// Wendet eine Operation auf jeden Knoten eines BAumes an. . Baum wird in einer Tiefensuche durchlaufen
        /// (Depth First).
        /// </summary>
        /// <typeparam name="TNode">Typ der Baumknoten</typeparam>
        /// <param name="node">Wurzelknoten</param>
        /// <param name="GetChilds">Funktion, die zu einem Baumkonten alle unmittelbaren Nachfolger liefert</param>
        /// <param name="OP">Operation, die auf jeden Knoten angewendet werden soll</param>
        /// <param name="CancelIf">Abbruchkriterium</param>
        public static void ForEachDF<TNode>(TNode node, Func<TNode, IEnumerable<TNode>> GetChilds, Action<TNode> OP, Func<TNode, bool> CancelIf)
        {
            OP(node);
            IEnumerable<TNode> childs = GetChilds(node);
            foreach (var child in childs)
                ForEachDF(child, GetChilds, OP);
        }

        /// <summary>
        /// Liefert die Maximale Tiefe eines Baumes
        /// </summary>
        /// <typeparam name="TNode"></typeparam>
        /// <param name="node"></param>
        /// <param name="GetChilds"></param>
        /// <returns></returns>
        public static long GetDepth<TNode>(TNode node, Func<TNode, IEnumerable<TNode>> GetChilds)
        {
            long DephtSubtree = 0;
            foreach (var child in GetChilds(node))
                DephtSubtree = Math.Max(DephtSubtree, GetDepth(child, GetChilds));

            return 1 + DephtSubtree;
        }

        /// <summary>
        /// Liefert alle Elemente auf einer Ebene
        /// </summary>
        /// <typeparam name="TNode"></typeparam>
        /// <param name="node"></param>
        /// <param name="GetChilds"></param>
        /// <param name="Level"></param>
        /// <returns></returns>
        public static IEnumerable<TNode> GetLevel<TNode>(TNode node, Func<TNode, IEnumerable<TNode>> GetChilds, long Level)
        {
            Level -= 1;
            if (Level > 0)
            {
                var AllNodesAtLevel = new TNode[] { };

                foreach (var child in GetChilds(node))
                    AllNodesAtLevel = AllNodesAtLevel.Concat(GetLevel(child, GetChilds, Level)).ToArray();

                return AllNodesAtLevel;

            }
            else
            {
                return new TNode[] { node };
            }

        }


        /// <summary>
        /// Liefert zu einem Knote im Baum den Pfad von der Wurzel bis zum Knoten. Der Pfad ist eine Liste aller 
        /// Knoten, die beim Abstieg von der Wurzel bis zum gegebenen Knoten passiert werden.
        /// </summary>
        /// <typeparam name="TNode"></typeparam>
        /// <param name="node"></param>
        /// <param name="GetParent"></param>
        /// <param name="IsTreeRoot"></param>
        /// <returns></returns>
        public static IEnumerable<TNode> GetPath<TNode>(TNode node, Func<TNode, TNode> GetParent, Func<TNode, bool> IsTreeRoot)
        {
            if (IsTreeRoot(node))
                return new TNode[] { node };
            else
            {
                return GetPath(GetParent(node), GetParent, IsTreeRoot).Concat(new TNode[] { node });
            }
        }


        /// <summary>
        /// Fügt alle Knoten eines Pfades in einen bestehenden Baum ein. Ein Pfad ist eine Liste, die alle Knoten von der Wurzel abwärts bis zu einem 
        /// Blatt aufzählt.
        /// </summary>
        /// <typeparam name="TNode"></typeparam>
        /// <param name="Path">Pfad</param>
        /// <param name="TreeNode">Wurzel des Baumes </param>
        /// <param name="GetChilds">Accessor für die Childs eines Baumknotens</param>
        /// <param name="EqualNodes">gibt true zurück, wenn ein Baumknoten und einem Pfadknoten entspricht</param>
        /// <param name="AddChildNode">Operation, die einem Baumknoten ein Pfadknoten als Kind hinzufügt</param>
        public static void InsertPath<TNode>(IEnumerable<TNode> Path, TNode TreeNode, Func<TNode, IEnumerable<TNode>> GetChilds, Func<TNode, TNode, bool> EqualNodes, Action<TNode, TNode> AddChildNode)
        {

            if (Path.Any())
            {
                var currentNode = Path.First();

                bool newNode = true;

                var childs = GetChilds(TreeNode);

                // Alle Kindknoten mit dem Knoten aus dem Pfad vergleichen. 
                foreach (var child in childs)
                {
                    // Ist ein Kindknoten gleich dem Pfadknoten, dann ist der Pfadknoten
                    // bereits eingefügt. Das Einfügen wird dann mit dem nächsten Pfadknoten fortgesetzt
                    if (EqualNodes(currentNode, child))
                    {
                        newNode = false;
                        InsertPath(Path.Skip(1), child, GetChilds, EqualNodes, AddChildNode);
                        break;
                    }
                }

                if (newNode)
                {
                    // Der Pfadknoten ist neu. er wird dem Baum hinzugefügt
                    AddChildNode(TreeNode, currentNode);
                }
                else
                {

                }
            }

        }



    }
}
