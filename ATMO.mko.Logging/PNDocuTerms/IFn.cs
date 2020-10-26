using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMO.mko.Logging.PNDocuTerms
{
    public interface IFn : global::mko.RPN.IFunctionNames
    {

        /// <summary>
        /// Instance
        /// A instance defines a block, that decribes a business object.
        /// It has a name and contains a list with properties, methods and events or a version number.
        /// </summary>
        string Instance { get; }

        /// <summary>
        /// Method
        /// A method documents a method- or function call an the results of them.
        /// It contains instances, properties and events
        /// </summary>
        string Method { get; }


        string Function { get; }


        /// <summary>
        /// Return 
        /// A return block describes the result of a function- or method call. 
        /// </summary>
        string Return { get; }


        /// <summary>
        /// Property
        /// Assignes a name to a portion of information.
        /// A portion of information can be a text, a list or a instance.
        /// </summary>
        string Property { get; }

        string PropertySet { get; }


        /// <summary>
        /// Version
        /// Defines a version numeber for a business object like instances.
        /// The version number consists of thre parts: main, sub and build- number.
        /// The parts are separated with points (i.e. 1.2.3).
        /// </summary>
        string Version { get; }


        /// <summary>
        /// Event
        /// An Event can indicate the success of an operation on an business object. 
        /// The structure of an event is equivalent to the structure of a property: #e name value.
        /// The name is often an indicator for success: succeded, failed, warn, ... se DocuEntityHlp.MapStringToEventType
        /// </summary>
        string Event { get; }


        /// <summary>
        /// Date
        /// Prefix for date literal
        /// </summary>
        string Date { get; }

        /// <summary>
        /// Time
        /// Prefix for time literal
        /// </summary>
        string Time { get; }

        /// <summary>
        /// Prefix for list literal
        /// </summary>
        string List { get; }


        /// <summary>
        /// Prefix for text literal
        /// </summary>
        string Txt { get; }

    }
}
