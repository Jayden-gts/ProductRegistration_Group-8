using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProductRegistration_Group_8.Models;

public partial class Category
{
    public int CategoryId { get; set; }
    [Required]
    [StringLength(100)]
    [Display(Name = "Category Name")]
    public string Name { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
