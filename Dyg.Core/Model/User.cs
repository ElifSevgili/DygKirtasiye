using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;



namespace Dyg.Core.Model
{
    public class User
    {
        public string Id { get; set; }

        [EmailAddress]
        [StringLength(200)]
        [Required]
        public string Email { get; set; }

        [StringLength(256)]
        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Şifre")]
        public string Password { get; set; }

        [NotMapped]
        [StringLength(256)]
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Display(Name ="Oluşturulma Tarihi")]
        public DateTime CreateDate { get; set; }
        [StringLength(200)]
        [Display(Name ="Oluşturan Kişi")]
        public string CreatedBy { get; set; }
        [Display(Name = "Güncelleme Tarihi")]
        public DateTime UpdateDate { get; set; }
        [StringLength(200)]
        [Display(Name = "Güncelleyen Kişi")]
        public string UpdatedBy { get; set; }
    }
}
