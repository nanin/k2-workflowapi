using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DianPing.WorkFlow.Repositories.Interface.DianpingK2SQLUM;
using DianPing.WorkFlow.Repositories.Interface.DianpingK2SQLUM.Entity;

namespace DianPing.WorkFlow.Repositories.Implementation.DianpingK2SQLUM
{
    public class K2CityRepostories : IK2CityRepostories
    {
        public K2CityPO GetK2CityByCityID(int cityID)
        {
            var edm = new DianpingK2SQLUMContext();
            return edm.K2City.Where<K2CityPO>(c=>c.CityID == cityID).FirstOrDefault<K2CityPO>();
        }
    }
}
