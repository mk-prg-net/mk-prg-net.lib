using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ekd = mko.Euklid;
using Css = mkoIt.Xhtml.Css;
using System.Diagnostics;

namespace mko.Graphic
{
    public class Block
    {
        LinkedList<ContextCmd> _contextCommands = new LinkedList<ContextCmd>();
        public LinkedList<ContextCmd> ContextCommands
        {
            get
            {
                return _contextCommands;
            }
        }

        public ContextCmd[] SetContextCommands
        {
            get
            {                
                return ContextCommands.Select(cmd => cmd).ToArray();                
            }
            set
            {
                value.Select(cmd => ContextCommands.AddLast(cmd));
            }
        }

        LinkedList<Block> _subBlocks = new LinkedList<Block>();
        LinkedList<Shape> _Shapes = new LinkedList<Shape>();
        public Shape[] Shapes
        {
            get
            {
                return _Shapes.ToArray();
            }
        }

        public bool AddShape(Shape S)
        {
            _Shapes.AddLast(S);
            return true;
        }

        

        //public IEnumerable<Shape> AllShapes
        //{
        //    get
        //    {
        //        foreach (Block b in _subBlocks)
        //            for (int i = 0; i < b.Shapes.Length; i++)
        //                yield return b.Shapes[i];

        //    }
        //}


        public bool CollectAllShapes(Block block, LinkedList<Shape> allShapes)
        {
            bool suc = true;
            foreach (Shape s in block.Shapes)
            {
                allShapes.AddLast(s);
            }

            foreach (Block b in block._subBlocks)
            {
                suc &= CollectAllShapes(b, allShapes);
            }

            return suc;
        }

        public bool Clear()
        {
            _Shapes.Clear();
            return true;
        }
        


        public Block(params Shape[] shapes)
        {
            foreach (var s in shapes)
                _Shapes.AddLast(s);
        }

        public Block(params Block[] blocks)
        {

            foreach (var b in blocks)
                _subBlocks.AddLast(b);
        }

        public Block(Shape[] shapes, Block[] blocks)
        {
            foreach (var s in shapes)
                _Shapes.AddLast(s);

            foreach (var b in blocks)
                _subBlocks.AddLast(b);
        }

        public bool draw(IPlotter plotter)
        {
            // Kontextkommandos ausführen (z.B. Transformationen einstellen
            if (_contextCommands.Count > 0 && _contextCommands.Select(cmd => cmd.exec(plotter)).Any(success => !success))
                return false;

            // Alle Unterblöcke zeichnen
            if (_subBlocks.Count > 0 && _subBlocks.Select(b => b.draw(plotter)).Any(success => !success))
                return false;

            // Alle Figuren zeichnen
            if(_Shapes.Count > 0 && _Shapes.Select(s => s.draw(plotter)).Any(success => !success))
                return false;                

            return true;
        }

    }
}
