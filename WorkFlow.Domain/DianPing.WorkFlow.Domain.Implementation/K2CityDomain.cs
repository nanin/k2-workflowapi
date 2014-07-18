using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DianPing.WorkFlow.Domain.Interface;
using DianPing.WorkFlow.Repositories.Interface.DianpingK2SQLUM.Entity;
using Microsoft.Practices.Unity;
using DianPing.WorkFlow.Repositories.Interface.DianpingK2SQLUM;

namespace DianPing.WorkFlow.Domain.Implementation
{
    public class K2CityDomain : IK2CityDomain
    {
        [Dependency]
        public virtual IK2CityRepostories k2CityRepostories { get; set; }

        public K2CityPO GetK2CityByCityID(int cityID)
        {
            return k2CityRepostories.GetK2CityByCityID(cityID);
        }
    }
}
