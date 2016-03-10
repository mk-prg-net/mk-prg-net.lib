using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ekd = mko.Euklid;
using Nwt = mko.Newton;

namespace mko.Graphic
{
    /// <summary>
    /// IPath definiert einen Pfad mittelst graphischer Operationen. Nachdem die vom Pfad umschlossene Fläche gefüllt
    /// oder wahlweise umrissen wurde (stroke), werden alle Resourcen durch die implementierung von IDisposable 
    /// wieder freigegeben
    /// </summary>
    public interface IPath : IDisposable
    {
        void moveTo(Ekd.Vector p);

        void lineTo(Ekd.Vector p);

        void circle(Ekd.Vector center, double radius);
        void arc(Ekd.Vector center, double radius, Nwt.Angle startAngle, Nwt.Angle endAngleRad);
        void arcAnticlockwise(Ekd.Vector center, double radius, Nwt.Angle startAngle, Nwt.Angle endAngleRad);


        /// <summary>
        /// Füllt den durch die Pfadoperationen umrissenen Inhalt
        /// </summary>
        void fill();

        /// <summary>
        /// Umschließt die durch die Pafdoperationen umrissenen Inhalt
        /// </summary>
        void stroke();       
        
    }
}
