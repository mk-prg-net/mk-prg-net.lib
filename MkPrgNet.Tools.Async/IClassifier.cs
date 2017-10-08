using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MkPrgNet.Tools.Async
{
    /// <summary>
    /// Classfactory for Classifiers
    /// </summary>
    public interface IClassifierBuilder
    {       
        IClassifier Create();
    }

    /// <summary>
    /// Classfactory for special classifiers.
    /// 
    /// </summary>
    public interface IClassifyByComponentsBuilder : IClassifierBuilder
    {
        /// <summary>
        /// Defines a set of equality- operators for each component of a vector
        /// </summary>
        /// <param name="eqFuncs">Equal operator. Returns true if param 1 and param 2 are equal.</param>
        void DefineEqualFunctions(params Func<int, int, bool>[] eqFuncs);
    }

    /// <summary>
    /// Abstract structure of a classifier
    /// </summary>
    public interface IClassifier
    {
        /// <summary>
        /// Maps vector to a category, based on implemented categorization algorithm and
        /// defined equality operators for each component.
        /// The input parameter, called vector, is an abstraction of object with n 
        /// properties. In a previous step, the properties of the object were mapped 
        /// to a list of integer values.
        /// </summary>
        /// <param name="vector"></param>
        /// <returns>category to which the vector is assigned</returns>
        int CategoryOf(params int[] vector);

        /// <summary>
        /// Trains the classification algorithm.
        /// </summary>
        /// <param name="vector"></param>
        void Classify(params int[] vector);

    }
}
