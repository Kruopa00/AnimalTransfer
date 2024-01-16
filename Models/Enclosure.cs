public class Enclosure 
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Size { get; set; }
    public string Location { get; set; }
    public List<EnclosureObject> Objects { get; set; } = new List<EnclosureObject>();
    public List<AnimalEnclosure> AnimalEnclosures { get; set; } = new List<AnimalEnclosure>();
}
