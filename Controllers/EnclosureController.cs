using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


[ApiController]
[Route("[controller]")]
public class EnclosureController : ControllerBase
{
    private readonly AnimalDbContext _context;
    private readonly IAnimalEnclosureService _service;

    public EnclosureController(AnimalDbContext context, IAnimalEnclosureService service)
    {
        _context = context;
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateEnclosure(EnclosureDto enclosureDto)
    {
        try {
            var enclosure = new Enclosure{
                Name = enclosureDto.Name,
                Size = enclosureDto.Size,
                Location = enclosureDto.Location,
                Objects = enclosureDto.Objects.Select(x => new EnclosureObject {
                    //EnclosureId = enclosureDto.Name,
                    ObjectName = x,
                }).ToList()
            };

            await _context.Enclosures.AddAsync(enclosure);
            await _context.SaveChangesAsync();

            await _service.AnimalsToEnclosures();

            return Ok();
        }
        catch(Exception e) {
            return BadRequest(e.Message);
        }
    }
}