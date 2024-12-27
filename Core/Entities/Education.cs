namespace Core.Entities;

public class Education : BaseEntity
{
    public int UserId { get; set; }
    public string Degree { get; set; }
    public string School { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    //public virtual User User { get; set; }
}
