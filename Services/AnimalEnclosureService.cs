using Microsoft.EntityFrameworkCore;

public class AnimalEnclosureService : IAnimalEnclosureService
{
    private readonly AnimalDbContext _context;

    public AnimalEnclosureService(AnimalDbContext context)
    {
        _context = context;
    }

    public async Task AnimalsToEnclosures()
    {
        var animals = await _context.Animals
            .Include(x => x.AnimalEnclosures).ThenInclude(x => x.Enclosure)
            .Where(x => !x.AnimalEnclosures.Any() && x.Amount > 0)
            .ToListAsync();
        if (!animals.Any()) return;

        var enclosers = await _context.Enclosures
            .Include(x => x.AnimalEnclosures).ThenInclude(x => x.Animal)
            .ToListAsync();
        if (!enclosers.Any()) return;
        
        var saveChanges = false;
        foreach(var animal in animals)
        {
            if (animal.Food == "Herbivore")
            {
                var validEnclosures = enclosers
                    .Where(x => !x.AnimalEnclosures.Any() || x.AnimalEnclosures.Any(a => a.Animal.Food != "Carnivore"))
                    .ToList();
                
                if (!validEnclosures.Any()) continue;

                animal.AnimalEnclosures.Add(new AnimalEnclosure{
                    AnimalId = animal.Id,
                    EnclosureId = validEnclosures.First().Id
                });
                saveChanges = true;
            }
            else if (animal.Food == "Carnivore")
            {
                var validEnclosures = enclosers
                    .Where(x => !x.AnimalEnclosures.Any())
                    .ToList();
                if (!validEnclosures.Any()) continue;

                animal.AnimalEnclosures.Add(new AnimalEnclosure{
                    AnimalId = animal.Id,
                    EnclosureId = validEnclosures.First().Id
                });
                saveChanges = true;
            }
        }

        if (saveChanges) await _context.SaveChangesAsync();
    }
}