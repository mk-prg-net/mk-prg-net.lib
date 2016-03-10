using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;
using System.Web.Security;

namespace mkoIt.Asp
{
    public class BoUsers
    {


        // Die beiden folgenden Eigenschaften müssen in der ObjectDatasource_Created Ereignis
        // gesetzt werden

        public mko.Log.LogServer log
        {
            get;
            set;
        }


        public string UserId
        {
            get;
            set;
        }

        /// <summary>
        /// Definition einer  Filterkombination für das Resultset
        /// </summary>
        public mkoIt.Db.FiltersCombine<MembershipUser> FilterSys = new Db.FiltersCombine<MembershipUser>();


        public int selectCount(string sortType)
        {
            try
            {
                var all = Membership.GetAllUsers().AsEnumarable().AsQueryable();
                return FilterSys.filter(all).Count();
            }
            catch (Exception ex)
            {
                if (log != null)
                {
                    log.Log(UserId, mko.Log.RC.CreateError("BoUsers.selectCount :" + ex.Message));
                    return 0;
                }
                else
                    throw new Exception("BoUsers.selectCount :" + ex.Message, ex);

            }            
        }

        public IQueryable<MembershipUser> select(string sortType, int StartRowIndex, int PageSize)
        {
            try
            {
                int AnzUsers;
                var all = Membership.GetAllUsers(StartRowIndex, PageSize, out AnzUsers).AsEnumarable().AsQueryable();                
                return FilterSys.filter(all).OrderBy(r => r.UserName).Skip(StartRowIndex).Take(PageSize);
            }
            catch (Exception ex)
            {
                if (log != null)
                    log.Log(UserId, mko.Log.RC.CreateError("BoUsers.select :" + ex.Message));
                else
                    throw new Exception("BoUsers.select :" + ex.Message, ex);
            }
            return null;
        }

        public void update(MembershipUser user)
        {
            try
            {
                Membership.UpdateUser(user);
            }
            catch (Exception ex)
            {
                if (log != null)
                    log.Log(UserId, mko.Log.RC.CreateError("BoUsers.update :" + ex.Message));
                else
                    throw new Exception("BoUsers.update :" + ex.Message, ex);
            }
        }
    }
}
