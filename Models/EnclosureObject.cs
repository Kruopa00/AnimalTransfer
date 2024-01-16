using System.ComponentModel.DataAnnotations.Schema;

public class EnclosureObject 
{
    [Column("EnclosureId")]
    public int EnclosureId { get; set; }
    public string ObjectName { get; set; }

    public Enclosure Enclosure { get; set; }
}
