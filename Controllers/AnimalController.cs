using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


[ApiController]
[Route("[controller]")]
public class AnimalController : ControllerBase
{
    private readonly AnimalDbContext _context;
    private readonly IAnimalEnclosureService _service;

    public AnimalController(AnimalDbContext context, IAnimalEnclosureService service)
    {
        _context = context;
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create(AnimalDto animalDto)
    {
        try {
            await CreateAnimal(animalDto);
            await _service.AnimalsToEnclosures();

            return Ok();
        }
        catch(Exception e) {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("/multiple")]
    public async Task<IActionResult> CreateMultiple(List<AnimalDto> animalsDto)
    {
        foreach(var animal in animalsDto) {
            

            try {
                await CreateAnimal(animal);
                await _service.AnimalsToEnclosures();
            }
            catch(Exception e) {
                
            }
        }
        return Ok();
    }

    private async Task CreateAnimal(AnimalDto animal)
    {
        await _context.Animals.AddAsync(new Animal{
            Species = animal.Species,
            Food = animal.Food,
            Amount = animal.Amount
        });
        await _context.SaveChangesAsync();
    }
}