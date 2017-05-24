//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 11.4.2017
//
//  Projekt.......: mko.RPN.Filter
//  Name..........: BasicfilterFunctionNames.cs
//  Aufgabe/Fkt...: Standardimplementierung der IFilterFunctionNameFactories.cs
//                  Schnittstelle
//
//
//
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows 7 mit .NET 4.5
//  Werkzeuge.....: Visual Studio 2013
//  Autor.........: Martin Korneffel (mko)
//  Version 1.0...: 
//
// </unit_environment>
//
//<unit_history>
//------------------------------------------------------------------
//
//  Version.......: 1.1
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 
//  Änderungen....: 
//
//</unit_history>
//</unit_header>        

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FltClass = mko.BI.Repositories.FilterClassification;

namespace mko.RPN.Filter
{
    // Standardimplementierung der IFilterFunctionNames.cs  Schnittstelle
    public class BasicFilterFunctionNameFactories : IFilterFunctionNameFactories
    {

        public const string Prefix = ".";

        IFilterFunctionNamePrefixes fn;

        readonly int lenLikePrefix;
        readonly int lenKeyPrefix;
        readonly int lenSetPrefix;
        readonly int lenRngPrefix;
        readonly int lenPredPrefix;
        readonly int lenSortPrefix;

        public BasicFilterFunctionNameFactories(IFilterFunctionNamePrefixes fnPrefix)
        {
            this.fn = fnPrefix;

            lenKeyPrefix = fn.keyFltPrefix.Length;
            lenLikePrefix = fn.likeFltPrefix.Length;
            lenPredPrefix = fn.predFltPrefix.Length;
            lenRngPrefix = fn.rngFltPrefix.Length;
            lenSetPrefix = fn.setFltPrefix.Length;
            lenSortPrefix = fn.sortFltPrefix.Length;
        }

        public string createKeyFltName(string FunctionName)
        {
            return fn.keyFltPrefix + Prefix + FunctionName;
        }

        public string createLikeFltName(string FunctionName)
        {
            return fn.likeFltPrefix + Prefix + FunctionName;
        }

        public string createPredFltName(string FltName)
        {
            return fn.predFltPrefix + Prefix + FltName;
        }

        public string createRngFltName(string FunctionName)
        {
            return fn.rngFltPrefix + Prefix + FunctionName;
        }

        public string createSetFltName(string FunctionName)
        {
            return fn.setFltPrefix + Prefix + FunctionName;
        }

        public string createSortFltName(string FunctionName)
        {
            return fn.sortFltPrefix + Prefix + FunctionName;
        }

        public bool IsKeyFilter(string name)
        {
            return name.StartsWith(fn.keyFltPrefix);
        }

        public bool IsLikeFilter(string name)
        {
            return name.StartsWith(fn.likeFltPrefix);
        }

        public bool IsPredFilter(string name)
        {
            return name.StartsWith(fn.predFltPrefix);
        }

        public bool IsRngFilter(string name)
        {
            return name.StartsWith(fn.rngFltPrefix);
        }

        public bool IsSetFilter(string name)
        {
            return name.StartsWith(fn.setFltPrefix);
        }

        public bool IsSortFilter(string name)
        {
            return name.StartsWith(fn.sortFltPrefix);
        }

        public string reduceKeyFilterName(string FltName)
        {
            return FltName.Remove(0, lenKeyPrefix +1);
        }

        public string reduceLikeFilterName(string FltName)
        {
            return FltName.Remove(0, lenLikePrefix + 1);
        }

        public string reducePredFilterName(string FltName)
        {
            return FltName.Remove(0, lenPredPrefix + 1);
        }

        public string reduceRngFilterName(string FltName)
        {
            return FltName.Remove(0, lenRngPrefix + 1);
        }

        public string reduceSetFilterName(string FltName)
        {
            return FltName.Remove(0, lenSetPrefix + 1);
        }

        public string reduceSortFilterName(string FltName)
        {
            return FltName.Remove(0, lenSortPrefix + 1);
        }
    }
}
