using System;

namespace ATMO.mko.Logging
{
    /// <summary>
    /// mko, 20.12.2017
    /// Interface of structured return codes
    /// 
    /// mko, 5.6.2018
    /// Extendes with MessageEntity and toPlx function. Was nessesary after removing inheritance of IRCV2(T) from IRCV2
    /// </summary>
    public interface IRCV2 : ISucceeded, ITraceInfo
    {

        /// <summary>
        /// Additional Information in PLX (=property list expressions)
        /// </summary>
        PNDocuTerms.DocuEntities.IDocuEntity MessageEntity { get; }


        /// <summary>
        /// mko, 25.7.2018
        /// This property is only for DI scenarios. If serialization of content is needed, a extra property 
        /// in interface implementing class is needed.
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        IRCV2 InnerRCV2 { get; }


        /// <summary>
        /// converts all information in object in a property list expression (plx)
        /// </summary>
        /// <returns></returns>
        PNDocuTerms.DocuEntities.IDocuEntity ToPlx();


    }

    public interface IRCV2<out T> : IRCV2
    {
        T Value { get; }
    }

}