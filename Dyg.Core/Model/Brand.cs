using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dyg.Core.Model
{
    public class Brand
    {
        public string Id { get; set; }
        [Required]
        [Display(Name = "Marka")]
        public string Name { get; set; }

        [Display(Name = "Oluşturulma Tarihi")]
        public DateTime CreateDate { get; set; }
        [StringLength(200)]
        [Display(Name = "Oluşturan Kişi")]
        public string CreatedBy { get; set; }

        [Display(Name = "Yayın Tarihi")]
        public DateTime PublishDate { get; set; }
        
        [Display(Name = "Yayında mı?")]
        public bool IsPublished { get; set; }

        [Display(Name = "Güncelleme Tarihi")]
        public DateTime UpdateDate { get; set; }
        [StringLength(200)]
        [Display(Name = "Güncelleyen Kişi")]
        public string UpdatedBy { get; set; }


        public ICollection<BrandCategory> BrandCategories { get; set; }

        [NotMapped]
        public string CategoryId { get; set; }
    }
}
