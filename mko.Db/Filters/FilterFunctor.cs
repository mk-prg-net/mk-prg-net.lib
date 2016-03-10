using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mkoIt.Db
{
    [Serializable]
    public abstract class Filter<TEntity>
    {

        public abstract IQueryable<TEntity> filterImpl(IQueryable<TEntity> srcTab);

        // Klartextbeschreibung des Filters für Dokumetationszwecke
        string _descr;
        public virtual string Description { 
            get{
                return _descr;
            }  
            set
            {
                _descr = GetType().Name + "=" + value;
            }
        }

        
    }

    [Serializable]
    public abstract class FilterFunctor<TEntity, TRvalue> : Filter<TEntity>       
    {
        // Default Konstruktor, um Instanzierung in Templates zu ermöglichen
        public FilterFunctor() { }

        public FilterFunctor(TRvalue rValueParam)
        {
            RValue = rValueParam;
        }

        public TRvalue RValue { get; set; }

        /// <summary>
        /// Rechtswert aus einer Zeichenkette einlesen
        /// </summary>
        /// <param name="RValueTxt"></param>
        public virtual void RValueParse(string RValueTxt)
        {
            throw new NotImplementedException("mkoIt.Asp.FilterFunctor");
        }

        protected virtual string MakeFilterDescription(string Filtername)
        {
            return Filtername + "= " + RValue.ToString();
        }
       
    }
}
