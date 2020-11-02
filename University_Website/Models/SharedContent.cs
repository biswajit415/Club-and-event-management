using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
namespace University_Website.Models
{
    [Table("SharedContentTbl")]
    public class SharedContent
    {
        [Key]
        public int SharedCintentId { get; set; }
        public int UserID { get; set; }
        public int EventID { get; set; }

        public int SharedWithUserID { get; set; }

        public EventDL Events { get; set; }
        public UserDL Users { get; set; }


    }

}