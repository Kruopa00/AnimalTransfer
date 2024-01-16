public class Animal 
{
    public int Id { get;set; }
    public string Species { get; set; }
    public string Food { get; set; }
    public int Amount { get; set; }
    public List<AnimalEnclosure> AnimalEnclosures { get; set; } = new List<AnimalEnclosure>();
}