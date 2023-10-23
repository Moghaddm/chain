namespace Chain.Domain.Entities;
public class User : Entity
{
    public string Username { get; set; }
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
}