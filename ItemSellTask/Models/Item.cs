using System.ComponentModel.DataAnnotations;

namespace ItemSellTask.Models;

public class Item : Base
{
    [Display(Name = "Item Name")]
    [MaxLength(100)]
    public string ItemName { get; set; }
    [Display(Name ="Item Discription")]
    public string? ItemDescription { get; set; }
    [Display(Name = "Item Price")]
    public double ItemPrice { get; set; }
    [Display(Name = "Item Count")]
    public int ItemCount { get; set; }
    public long CategoryId { get; set; }
    public Category Category { get; set; }

    [Display(Name = "Quantity Sold")]
    public int QuantitySold { get; set; }
}
