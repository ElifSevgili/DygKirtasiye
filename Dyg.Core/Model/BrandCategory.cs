using System;
using System.Collections.Generic;
using System.Text;

namespace Dyg.Core.Model
{
   public class BrandCategory
    {
        public string CategoryId { get; set; }
        public Category Category { get; set; }

        public string BrandId { get; set; }
        public Brand Brand { get; set; }
    }
}
