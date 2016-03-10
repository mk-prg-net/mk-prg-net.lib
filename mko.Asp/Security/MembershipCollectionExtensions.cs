using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;
using System.Web.Security;


namespace mkoIt.Asp
{
    public static class MembershipCollectionExtensions
    {
        public static IEnumerable<MembershipUser> AsEnumarable(this MembershipUserCollection userCollection)
        {
            foreach (MembershipUser member in userCollection)
            {
                yield return member;
            }
        }

    }
}
