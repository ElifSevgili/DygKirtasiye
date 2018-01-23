using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dyg.Core.Model
{
   public class Category
    {
        public string Id{ get; set; }

        
        [Display(Name="Kategori Adı")]
        public string Name { get; set; }

       
        [Display(Name="Üst Kategori")]
    
        public string ParentCategoryId { get; set; }
        [ForeignKey("ParentCategoryId")]
        public Category ParentCategory { get; set; }

     
       public ICollection<Category> SubCategories{ get; set; }

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

    }
}
