using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DianPing.WorkFlow.Repositories.Interface.DianPingK2Sln.Entity;

namespace DianPing.WorkFlow.Domain.Interface
{
    public interface IK2ParticipateDomain
    {
        void Save(K2ParticipatePO k2Participate);
    }
}
