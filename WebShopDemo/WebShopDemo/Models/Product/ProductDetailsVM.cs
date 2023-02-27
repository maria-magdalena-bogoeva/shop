using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebShopDemo.Models.Product
{
    public class ProductDetailsVM
    {

        [Key]
        public int Id { get; set; }


        [Display(Name = "Product Name")]
        public string ProductName { get; set; }


        public int BrandId { get; set; }
        [Display(Name = "Brand")]
        public string BrandName { get; set; }

        public int CategoryId { get; set; }
        [Display(Name = "Category")]

        public string CategoryName { get; set; }


        [Display(Name = "Picture")]
        public string Picture { get; set; }



        [Display(Name = "Quantity")]
        public int Quantity { get; set; }



        [Display(Name = "Price")]
        public decimal Price { get; set; }


        [Display(Name = "Discount")]
        public decimal Discount { get; set; }
    }
}
