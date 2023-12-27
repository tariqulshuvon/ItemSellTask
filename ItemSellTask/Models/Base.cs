namespace ItemSellTask.Models;

public class Base
{
    public long Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }= DateTimeOffset.Now;
    public DateTimeOffset UpdatedAt { get;set; }= DateTimeOffset.Now;
    public string CreatedBy { get; set; } = string.Empty;
    public string UpdatedBy { get; set; } = string.Empty;
}
