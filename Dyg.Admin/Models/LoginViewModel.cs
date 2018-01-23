using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dyg.Admin.Models
{
    public class LoginViewModel
    {
        [EmailAddress]
        [StringLength(200)]
        [Required]
        [Display(Name ="Kullanıcı Adı")]
        public string UserName { get; set; }
        [StringLength(256)]
        [DataType(DataType.Password)]
        [Required]
        [Display(Name = "Şifre")]
        public string Password { get; set; }

    }
}
