namespace Sports_Nutrition_Core.DTOs;

public class ClientDTO
{
    public Id Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string MiddleName { get; set; }
    public int Age { get; set; }
    public Gender Gender { get; set; }
    public double Height { get; set; }
    public double Weight { get; set; }
    public ActivityLevel ActivityLevel { get; set; }

    public static implicit operator ClientDTO(Client other) =>
        new()
        {
            Id = other.Id,
            Name = other.Name,
            Surname = other.Surname,
            MiddleName = other.MiddleName,
            Age = other.Age,
            Gender = other.Gender,
            Height = other.Height,
            Weight = other.Weight,
            ActivityLevel = other.ActivityLevel
        };
}
