using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DianPing.WorkFlow.Repositories.Interface.DianpingK2SQLUM;
using DianPing.WorkFlow.Repositories.Interface.DianpingK2SQLUM.Entity;

namespace DianPing.WorkFlow.Repositories.Implementation.DianpingK2SQLUM
{
    public class K2UserRepostories : IK2UserRepostories
    {
        public K2UserPO GetK2User(int loginID)
        {
            var edm = new DianpingK2SQLUMContext();
            var loginIdStr = loginID.ToString();
            return edm.K2User.
                Where(u => u.UserName == loginIdStr).FirstOrDefault<K2UserPO>();
        }
    }
}
