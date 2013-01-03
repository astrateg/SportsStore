using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SportsStore.Domain.Entities
{
    [Table(Name = "Products")]
    public class Product
    {
        [HiddenInput(DisplayValue = false)] // скрытое поле передается в форму
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public int ProductID { get; set; }

        [Required(ErrorMessage = "Please enter a product name")]
        [Column] public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a description")]
        [DataType(DataType.MultilineText)]
        [Column] public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        [Column] public decimal Price { get; set; }

        [Required(ErrorMessage = "Please specify a category")]
        [Column] public string Category { get; set; }

        [HiddenInput(DisplayValue = false)]
        [Column] public byte[] ImageData { get; set; }

        [HiddenInput(DisplayValue = false)]
        [Column] public string ImageMimeType { get; set; } 
    }
}
