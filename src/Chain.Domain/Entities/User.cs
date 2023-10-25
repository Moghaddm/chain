namespace Chain.Domain.Entities;
public class User : Entity
{
    public string Username { get; }
    public string FullName { get; private set; }
    public string PhoneNumber { get; private set; }
    private User()
    {

    }

    public User(string username, string fullName, string phoneNumber)
    {
        ValidateProperties(username, fullName, phoneNumber);

        Username = username;
        FullName = fullName;
        PhoneNumber = phoneNumber;
    }

    private void ValidateProperties(string username, string fullName, string phoneNumber)
    {
        if (String.IsNullOrWhiteSpace(username))
            throw new ArgumentNullException($"{username} cannot be null or empty.");
        if (String.IsNullOrWhiteSpace(fullName))
            throw new ArgumentNullException($"{fullName} cannot be null or empty.");
        if (String.IsNullOrWhiteSpace(phoneNumber))
            throw new ArgumentNullException($"{phoneNumber} cannot be null or empty.");
    }
}