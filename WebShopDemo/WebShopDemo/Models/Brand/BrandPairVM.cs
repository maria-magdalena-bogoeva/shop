using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebShopDemo.Models.Brand
{
    public class BrandPairVM
    {
        public int Id { get; set; }
        [Display(Name="Brand")]
        public string Name { get; set; }
    }
}
