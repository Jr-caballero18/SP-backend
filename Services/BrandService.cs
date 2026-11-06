using Backend.DTOs;
using Backend.Models;
using Backend.Repository;

namespace Backend.Services;

public class BrandService : ICommonService<BrandDto, BrandInsertDto, BrandUpdateDto>
{
    private readonly IRepository<Brand> _brandRepository;
    private readonly IRepository<Beer> _beerRepository;

    public BrandService(
        IRepository<Brand> brandRepository,
        IRepository<Beer> beerRepository)
    {
        _brandRepository = brandRepository;
        _beerRepository = beerRepository;
    }

    public async Task<IEnumerable<BrandDto>> Get()
    {
        var brands = await _brandRepository.Get();
        return brands.Select(b => new BrandDto
        {
            Id = b.BrandId,
            Name = b.Name
        });
    }

    public async Task<BrandDto?> GetById(int id)
    {
        var brand = await _brandRepository.GetByTd(id);
        if (brand == null) return null;

        return new BrandDto
        {
            Id = brand.BrandId,
            Name = brand.Name
        };
    }

    public async Task<BrandDto> Add(BrandInsertDto brandInsertDto)
    {
        var brand = new Brand
        {
            Name = brandInsertDto.Name
        };

        await _brandRepository.Add(brand);
        await _brandRepository.Save();

        return new BrandDto
        {
            Id = brand.BrandId,
            Name = brand.Name
        };
    }

    public async Task<BrandDto?> Update(int id, BrandUpdateDto brandUpdateDto)
    {
        var brand = await _brandRepository.GetByTd(id);
        if (brand == null) return null;

        brand.Name = brandUpdateDto.Name;

        _brandRepository.Update(brand);
        await _brandRepository.Save();

        return new BrandDto
        {
            Id = brand.BrandId,
            Name = brand.Name
        };
    }

    public async Task<BrandDto?> Delete(int id)
    {
        var brand = await _brandRepository.GetByTd(id);
        if (brand == null) return null;

        _brandRepository.Delete(brand);
        await _brandRepository.Save();

        return new BrandDto
        {
            Id = brand.BrandId,
            Name = brand.Name
        };
    }

    // Reto 1: Validar si existe un nombre duplicado
    public async Task<bool> ExistsByName(string name, int? excludeId = null)
    {
        var brands = await _brandRepository.Get();
        return brands.Any(b => b.Name.Equals(name, StringComparison.OrdinalIgnoreCase) 
                               && (!excludeId.HasValue || b.BrandId != excludeId.Value));
    }

    // Reto 2: Validar si la marca tiene cervezas asociadas antes de eliminar
    public async Task<bool> HasBeers(int brandId)
    {
        var beers = await _beerRepository.Get();
        return beers.Any(b => b.BrandId == brandId);
    }

    // Reto 3: Obtener las cervezas de una marca específica
    public async Task<IEnumerable<BeerDto>?> GetBeersByBrandId(int brandId)
    {
        var brand = await _brandRepository.GetByTd(brandId);
        if (brand == null) return null;

        var beers = await _beerRepository.Get();
        return beers
            .Where(b => b.BrandId == brandId)
            .Select(b => new BeerDto
            {
                Id = b.BeerId,
                Name = b.Name,
                BrandId = b.BrandId,
                Alcohol = b.Alcohol
            });
    }
}