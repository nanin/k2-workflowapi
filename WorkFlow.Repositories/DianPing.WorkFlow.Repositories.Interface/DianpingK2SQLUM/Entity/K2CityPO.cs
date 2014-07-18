using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DianPing.WorkFlow.Repositories.Interface.DianpingK2SQLUM.Entity
{
    [Table("City", Schema = "dbo")]
    public class K2CityPO
    {
        [Key]
        public int CityID { get; set; }
        public string CityName { get; set; }
        public string CityCode { get; set; }
        public string CityAbbrCode { get; set; }
    }
}
