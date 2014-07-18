using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DianPing.WorkFlow.Repositories.Interface.DianpingK2SQLUM.Entity
{
    [Table("K2Users", Schema = "dbo")]
    public class K2UserPO
    {
        [Key]
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string UserDescription { get; set; }
        public string UserEmail { get; set; }
        public int ManagerID { get; set; }
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public int CityID { get; set; }
        public bool FirstLogin { get; set; }
    }
}
