using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DianPing.WorkFlow.Repositories.Interface.DianPingK2Sln.Entity;

namespace DianPing.WorkFlow.Repositories.Interface.DianPingK2Sln
{
    public interface IK2ParticipateRepostories
    {
        void Save(K2ParticipatePO k2Participate);
    }
}
