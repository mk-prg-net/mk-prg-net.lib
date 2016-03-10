using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mko.Graphic
{
    public abstract class Shape
    {
        public Shape(Brush brush, Pen pen)
        {
            Fill = true;
            Outline = true;
            Pen = pen;
            Brush = brush;
        }

        public Shape(Pen pen, Brush brush, bool fill, bool outline)
        {
            Fill = fill;
            Outline = outline;
            Pen = pen;
            Brush = brush;
        }

        public Pen Pen { get; set; }
        public Brush Brush { get; set; }

        public bool Outline { get; set; }
        public bool Fill { get; set; }


        public abstract bool draw(IPlotter plotter);
    }
}
