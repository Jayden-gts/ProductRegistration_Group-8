using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProductRegistration_Group_8.Models;

public partial class Product
{
    public int ProductId { get; set; }

    [Required]
    [StringLength(100)]
    [Display(Name = "Product Name")]
    public string Name { get; set; } = null!;

    [Required]
    [Range(0.01, 999999)]
    [Display(Name = "Price")]
    public double Price { get; set; }

    [Required]
    [Display(Name = "Category")]
    public int CategoryId { get; set; }

    public virtual Category? Category { get; set; } = null!;
}
