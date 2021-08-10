using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Entity.Att;
using AttApi;

namespace Coldairarrow.Business.Att
{
    public static class BuildAttDayReturnEntiry
    {
        public static void SetStaffInfo(this AttDayReturnEntiry t, Base_Staff staff)
        {
            t.AttId = staff.AttId;
            t.Name = staff.StaffName;
            t.DeptName = staff.DeptName;
            t.StaffId = staff.Id;
        }

        public static void SetDayRecord(this AttDayReturnEntiry t, AttDayRecord adr)
        {
            t.CheckInTime = adr.FirstCheck;
            t.CheckOutTime = adr.LastCheck;
            t.AttRecord = adr.AttRecordString;
            t.OverTime = adr.OverTime;
            t.AttDate = adr.AttDay;
        }

        public static void SetAttDay(this AttDayReturnEntiry t, Att_Day ad)
        {
            t.Id = ad.Id;
            t.BeginOverTime = ad.BeginOverTime;
            t.EndOverTime = ad.EndOverTime;
            t.DeclareOverTime = ad.DeclareOverTime;
        }


    }
}
