using Backend.DTOs;
using Backend.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BrandController : ControllerBase
{
    private readonly IValidator<BrandInsertDto> _brandInsertValidator;
    private readonly IValidator<BrandUpdateDto> _brandUpdateValidator;
    private readonly BrandService _brandService;

    public BrandController(
        IValidator<BrandInsertDto> brandInsertValidator,
        IValidator<BrandUpdateDto> brandUpdateValidator,
        [FromKeyedServices("brandService")] ICommonService<BrandDto, BrandInsertDto, BrandUpdateDto> brandService)
    {
        _brandInsertValidator = brandInsertValidator;
        _brandUpdateValidator = brandUpdateValidator;
        _brandService = (BrandService)brandService;
    }

    [HttpGet]
    public async Task<IEnumerable<BrandDto>> Get() =>
        await _brandService.Get();

    [HttpGet("{id}")]
    public async Task<ActionResult<BrandDto>> GetById(int id)
    {
        var brand = await _brandService.GetById(id);
        return brand == null ? NotFound() : Ok(brand);
    }

    // Reto 3: GET /api/Brand/{id}/beers
    [HttpGet("{id}/beers")]
    public async Task<ActionResult<IEnumerable<BeerDto>>> GetBeers(int id)
    {
        var beers = await _brandService.GetBeersByBrandId(id);
        return beers == null ? NotFound("Marca no encontrada") : Ok(beers);
    }

    [HttpPost]
    public async Task<ActionResult<BrandDto>> Add(BrandInsertDto brandInsertDto)
    {
        var validationResult = await _brandInsertValidator.ValidateAsync(brandInsertDto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        // Reto 1: Validar nombre duplicado
        if (await _brandService.ExistsByName(brandInsertDto.Name))
        {
            return BadRequest(new { message = "Ya existe una marca con ese nombre." });
        }

        var brandDto = await _brandService.Add(brandInsertDto);
        return CreatedAtAction(nameof(GetById), new { id = brandDto.Id }, brandDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<BrandDto>> Update(int id, BrandUpdateDto brandUpdateDto)
    {
        var validationResult = await _brandUpdateValidator.ValidateAsync(brandUpdateDto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        if (id != brandUpdateDto.Id)
        {
            return BadRequest(new { message = "El ID de la URL no coincide con el ID del body." });
        }

        // Reto 1: Validar nombre duplicado excluyendo la marca actual
        if (await _brandService.ExistsByName(brandUpdateDto.Name, id))
        {
            return BadRequest(new { message = "Ya existe otra marca con ese nombre." });
        }

        var brand = await _brandService.Update(id, brandUpdateDto);
        return brand == null ? NotFound() : Ok(brand);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<BrandDto>> Delete(int id)
    {
        // Reto 2: Validar si la marca tiene cervezas asociadas antes de eliminar
        if (await _brandService.HasBeers(id))
        {
            return Conflict(new { message = "No se puede eliminar la marca porque tiene cervezas asociadas." });
        }

        var brand = await _brandService.Delete(id);
        if (brand == null)
        {
            return NotFound();
        }

        return Ok(brand);
    }
}