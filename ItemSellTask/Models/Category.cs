using System.ComponentModel.DataAnnotations;

namespace ItemSellTask.Models;

    public class Category : Base
    {
    [Required]
    [Display(Name = "Category Name")]
    [MaxLength(100)]
    public string CategoryName { get; set; }

    public ICollection<Item> Items { get; set; } = new HashSet<Item>();

    }

