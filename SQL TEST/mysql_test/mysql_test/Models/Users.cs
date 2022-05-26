using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mysql_test.Models
{
    class Users
    {
        [Key]
        [Required]
        [MaxLength(30)]
        public string userid { get; set; }

        [Required]
        [MaxLength(30)]
        public string userPW { get; set; }

        [Required]
        [MaxLength(30)]
        public string userName { get; set; }
    }
}
