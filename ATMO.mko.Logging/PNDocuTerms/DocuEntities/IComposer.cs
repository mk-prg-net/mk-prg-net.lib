using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMO.mko.Logging.PNDocuTerms.DocuEntities
{
    /// <summary>
    /// mko, 26.3.2018
    /// Create Document entity trees
    /// </summary>
    public interface IComposer
    {
        IDocuEntity List(params IDocuEntity[] entities);

        IDocuEntity i(string name, params IDocuEntity[] pn);

        /// <summary>
        /// Defines a vrsion number of an object or method
        /// </summary>
        /// <param name="versionStr"></param>
        /// <returns></returns>
        IDocuEntity ver(string versionStr);


        /// <summary>
        /// Decribes a method/action call
        /// </summary>
        /// <param name="pn"></param>
        /// <returns></returns>
        IDocuEntity m(string name, params IDocuEntity[] pn);

        /// <summary>
        /// mko, 10.4.2018
        /// Returnvalue
        /// </summary>
        /// <param name="pn"></param>
        /// <returns></returns>
        IDocuEntity ret(params IDocuEntity[] pn);

        /// <summary>
        /// mko, 27.02.2019
        /// Rückgabe einer Entscheidung
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        IDocuEntity ret(bool res);

        /// <summary>
        /// mko, 27.2.2019
        /// Rückgabe eines Integers
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        IDocuEntity ret(int res);

        /// <summary>
        /// mko, 27.2.2019
        /// Rückgabe eines Longs
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        IDocuEntity ret(long res);

        /// <summary>
        /// mko, 27.2.2019
        /// Rückgabe einer Textmeldung
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        IDocuEntity ret(string res);


        /// <summary>
        /// Reports the value of a property 
        /// </summary>
        /// <param name="pn"></param>
        /// <returns></returns>
        IDocuEntity p(string Name, IDocuEntity Value);

        /// <summary>
        /// mko, 22.2.2019
        /// Eigenschaften mit Zeichenkettenwert
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        IDocuEntity p(string Name, string Value);


        /// <summary>
        /// mko, 22.2.2019
        /// Eigenschaft mit int- Wert
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        IDocuEntity p(string Name, int Value);

        /// <summary>
        /// mko, 22.02.2019
        /// Eigenschaft mit long- Wert
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        IDocuEntity p(string Name, long Value);

        /// <summary>
        /// mko, 27.2.2019
        /// Eigenschaft mit bool- Wert
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        IDocuEntity p(string Name, bool Value);

        /// <summary>
        /// mko, 22.2.2019
        /// Eigenschaft mit float- Wert
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        IDocuEntity p(string Name, double Value);

        /// <summary>
        /// Reports the value, property was set
        /// </summary>
        /// <param name="pn"></param>
        /// <returns></returns>
        IDocuEntity pSet(string Name, IDocuEntity Value);

        /// <summary>
        /// Defines a fired event with parameters
        /// </summary>
        /// <param name="pn"></param>
        /// <returns></returns>
        IDocuEntity e(string name, IDocuEntity value);

        /// <summary>
        /// Describes an event, that fires, when a process starts
        /// </summary>        
        /// <param name="pn"></param>
        /// <returns></returns>
        IDocuEntity eStart(IDocuEntity value);

        /// <summary>
        /// mko, 22.2.2019
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        IDocuEntity eStart(string info);

        IDocuEntity eStart();

        /// <summary>
        /// Describes an event, that fires, when a process ends
        /// </summary>
        /// <param name="nameOfProcess"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        IDocuEntity eEnd(IDocuEntity value = null);

        /// <summary>
        /// mko, 22.2.2019
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        IDocuEntity eEnd(string value);

        IDocuEntity eEnd();

        /// <summary>
        /// Signals interrupted or aborted functions etc.
        /// </summary>
        /// <returns></returns>
        IDocuEntity eNotCompleted();

        IDocuEntity eNotCompleted(IDocuEntity value);



        /// <summary>
        /// Describes an event, that fires, when a process fails
        /// </summary>
        /// <param name="nameOfProcess"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        IDocuEntity eFails(IDocuEntity value);

        /// <summary>
        /// mko, 22.2.2019
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        IDocuEntity eFails(string value);

        /// <summary>
        /// mko, 19.9.2018
        /// </summary>
        /// <returns></returns>
        IDocuEntity eFails();

        IDocuEntity eWarn(IDocuEntity value);

        /// <summary>
        /// mko, 22.2.2019
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        IDocuEntity eWarn(string value);

        /// <summary>
        /// mko, 19.9.2018
        /// </summary>
        /// <returns></returns>
        IDocuEntity eWarn();

        IDocuEntity eInfo(IDocuEntity value);

        /// <summary>
        /// mko, 22.2.2019
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        IDocuEntity eInfo(string value);

        IDocuEntity eSucceeded(IDocuEntity value);

        /// <summary>
        /// mko, 22.2.2019
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        IDocuEntity eSucceeded(string value);

        IDocuEntity eSucceeded();

        /// <summary>
        /// mko, 19.9.2018
        /// </summary>
        /// <returns></returns>
        IDocuEntity eInfo();

        IDocuEntity ePrms(string name, params IDocuEntity[] pn);


        /// <summary>
        /// Defines a textvalue
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        IDocuEntity txt(string text);


        /// <summary>
        /// Defines a date constant
        /// </summary>
        /// <param name="dat"></param>
        /// <returns></returns>
        IDocuEntity date(DateTime dat);

        /// <summary>
        /// Defines a time constant
        /// </summary>
        /// <param name="dat"></param>
        /// <param name="showMilliseconds"></param>
        /// <returns></returns>
        IDocuEntity time(TimeSpan dat, bool showMilliseconds = false);

        IDocuEntity KillIfNot(bool Condition, Func<IDocuEntity> docuEntityFactory);

        IDocuEntity KillIf(bool Condition, Func<IDocuEntity> docuEntityFactory);

        /// <summary>
        /// Embeds entities as Child in current Entity.
        /// Note that thisEntity(.., embed(entities), ..) it is not the same like thisEntity(.., List(entities), ..).
        /// List then will be a child of this entity, and entities are childs of list:
        /// thisEntity             
        ///    +--> List
        ///           +-->> entities
        /// Instead of, after calling thisEntity(.., embed(entities), ..), entities are childs of thisEntity.
        /// thisEntity             
        ///     +-->> entities
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        IDocuEntity EmbedMembers(params IDocuEntity[] entities);

        /// <summary>
        /// mko, 31.1.2019
        /// Führ ein KillIfNot- Kommando aus, wenn der übergebene Parameter ein solches ist.
        /// Als Ergebnis wird dann das eingekapselte IDocuEntity, falls die Bedingung nicht zutraf,
        /// oder null zurückgegeben. 
        /// Liegt kein KillIfNot- Kommando vor, dann wird dieses zurückgegeben
        /// </summary>
        /// <param name="docuEntity"></param>
        /// <returns></returns>
        IDocuEntity ExecuteKillCommand(IDocuEntity docuEntity);
    }
}
