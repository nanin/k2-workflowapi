using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DianPing.WorkFlow.Domain.Interface;
using DianPing.WorkFlow.Repositories.Interface.DianPingK2Sln.Entity;
using Microsoft.Practices.Unity;
using DianPing.WorkFlow.Repositories.Interface.DianPingK2Sln;

namespace DianPing.WorkFlow.Domain.Implementation
{
    public class K2ParticipateDomain : IK2ParticipateDomain
    {
        [Dependency]
        public virtual IK2ParticipateRepostories k2ParticipateRepostories { get; set; }

        public void Save(K2ParticipatePO k2Participate)
        {
            k2ParticipateRepostories.Save(k2Participate);
        }
    }
}
