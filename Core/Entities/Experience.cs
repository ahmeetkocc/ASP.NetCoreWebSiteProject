namespace Core.Entities;

public class Experience : BaseEntity
{
    public string Title { get; set; }
    public string Company { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Description { get; set; }

}
