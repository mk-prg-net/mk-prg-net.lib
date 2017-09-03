using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.BI.Repositories.Addresses.InMem
{
    public partial class CompanyAddresses : Addresses.CompanyAddresses
    {
        List<Bo.Addresses.MailingAddressCompany> _all = new List<Bo.Addresses.MailingAddressCompany>();

        Queue<Action> _cudActions = new Queue<Action>();

        public override void CreateBoAndAdd(string id)
        {
            if (_all.Any(r => r.CompanyName == id))
            {
                throw new Exception(mko.TraceHlp.FormatErrMsg(this, "CreateBoAndAddToCollection", "Es existiert bereis eine Firmenanschrift für ", id));
            }
            else
            {
                 _cudActions.Enqueue(() => _all.Add(new Bo.Addresses.MailingAddressCompany() { CompanyName = id }));
            }
        }

        public override Bo.Addresses.IMailingAddressCompany GetBo(string id)
        {
            if (_all.Any(r => r.CompanyName == id))
            {
                return _all.Single(r => r.CompanyName == id);
            }
            else
            {
                throw new ArgumentOutOfRangeException("CompanyName", mko.TraceHlp.FormatErrMsg(this, "Get", "Kein Eintrag gefunden für ", id));
            }
        }

        public override bool ExistsBo(string id)
        {
            return _all.Any(r => r.CompanyName == id);
        }

        public override void RemoveBo(string id)
        {
            if (_all.Any(r => r.CompanyName == id))
            {
                var entity = _all.Single(r => r.CompanyName == id);
                _cudActions.Enqueue(() => _all.Remove(entity));
            }
        }

        public override void RemoveAllBo()
        {
            _cudActions.Enqueue(() => _all.Clear());
        }

        public override void SubmitChanges()
        {
            try { 
                foreach (var action in _cudActions)
                {
                    action();
                }
                _cudActions.Clear();
            }
            catch (Exception ex)
            {
                throw new Exception(mko.TraceHlp.FormatErrMsg(this, "SubmitChanges"), ex);
            }
        }


        public override Addresses.CompanyAddresses.IFilteredSortedSetBuilder getFilteredSortedSetBuilder()
        {
            return new CompanyAddresses.FilteredSortedSetBuilder(_all);
        }
    }
}
