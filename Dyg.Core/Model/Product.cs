using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dyg.Core.Model
{
    public class Product
    {
        public string Id {get; set; }

        [StringLength(200)]
        [Display(Name="Ürün Adı")]
        public string Name { get; set; }


        
        [Display(Name = "Kategori Adı")]
        public string CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }


        
        [Display(Name = "Marka Adı")]
        public string BrandId { get; set; }
        [ForeignKey("BrandId")]
        public Brand Brand { get; set; }

        [StringLength(200)]
        [Display(Name = "Fotoğraf")]
        public string Photo { get; set; }

        
        [Display(Name = "Fiyat")]
        public string Price { get; set; }

        [Display(Name = "Adet")]
        public string Piece { get; set; }

        [Display(Name = "Stok")]
        public string Stock { get; set; }

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



    }
}
