namespace Core.Entities;

public class PersonalInfo : BaseEntity
{
    public string About { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime BirthDate { get; set; }

}
