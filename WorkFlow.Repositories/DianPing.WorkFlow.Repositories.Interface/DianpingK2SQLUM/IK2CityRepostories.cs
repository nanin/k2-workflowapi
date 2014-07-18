using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DianPing.WorkFlow.Repositories.Interface.DianpingK2SQLUM.Entity;

namespace DianPing.WorkFlow.Repositories.Interface.DianpingK2SQLUM
{
    public interface IK2CityRepostories
    {
        K2CityPO GetK2CityByCityID(int cityID);
    }
}
