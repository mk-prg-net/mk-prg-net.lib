using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.Euklid.Transformations
{
    public class CombinedTransformation : Transformation
    {
        public CombinedTransformation(int Dimensions)
            : base(Dimensions)
        {
        }

        LinkedList<Transformation> _trafos = new LinkedList<Transformation>();

        public void LeftMul(Transformation Trafo)
        {
            _trafos.AddLast(Trafo);
        }

        public void RightMul(Transformation Trafo)
        {
            _trafos.AddFirst(Trafo);
        }

        public override Vector apply(Vector v)
        {
            Vector vv = new Vector(v);
            foreach (var trafo in _trafos)
            {
                vv = trafo.apply(vv);
            }

            return vv;
        }

    }
}
