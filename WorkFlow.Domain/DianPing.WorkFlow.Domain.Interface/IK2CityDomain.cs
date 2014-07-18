using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DianPing.WorkFlow.Repositories.Interface.DianpingK2SQLUM.Entity;

namespace DianPing.WorkFlow.Domain.Interface
{
    public interface IK2CityDomain
    {
        K2CityPO GetK2CityByCityID(int cityID);
    }
}
