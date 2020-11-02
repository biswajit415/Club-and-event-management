using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace University_Website.Models
{
    [Table("AdminTbl")]
    public class AdminDL
    {
        [Key]
        [Required(ErrorMessage = "* Username is required")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string UserId { get; set; }

        [Required(ErrorMessage = "* Password is required ")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
