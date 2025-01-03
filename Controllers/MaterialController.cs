
using System.ComponentModel.DataAnnotations;
using Back_End.Models;
using Back_End.Services.Material;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Back_End.Validator;

namespace Back_End.Controllers
{
    [ApiController]
    [Route("api/material")]
    public class MaterialController : ControllerBase
    {
        private readonly IMaterialService _materialService;
        private readonly IValidator<MaterialModel> _validator;

        public MaterialController(IMaterialService materialService, IValidator<MaterialModel> validator)
        {
            _materialService = materialService;
            _validator = validator;
        }


        [HttpGet("get")]
        public async Task<IActionResult> GetAllMaterials()
        {
            var material = await _materialService.GetAllMaterials();

            if (material == null)
            {
                return NotFound();//404
            }

            return Ok(material);//200
        }
       


        [HttpPost("create")]
        public async Task<IActionResult> CreateMaterial(MaterialModel material)
        {
            if (material.CreatedAt == default(DateTime))
            {
                material.CreatedAt = DateTime.Now;
            }

            var validationResult = await _validator.ValidateAsync(material);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return BadRequest(ModelState);
            }

            var newMaterial = await _materialService.CreateMaterial(material);
            return CreatedAtAction(nameof(CreateMaterial), newMaterial);
        }


        [HttpPut("update")]
        public async Task<IActionResult> UpdateMaterial(MaterialModel updatedMaterial)
        {


            if (updatedMaterial.UpdatedAt == default(DateTime))
            {
                updatedMaterial.UpdatedAt = DateTime.Now;
            }

            var result = await _validator.ValidateAsync(updatedMaterial);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            var validationResult = await _validator.ValidateAsync(updatedMaterial);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return BadRequest(ModelState);
            }


            var allExistingMaterial = await _materialService.GetAllMaterials();
            var material = allExistingMaterial.Where(x => x.Id == updatedMaterial.Id).FirstOrDefault();

            if (material == null)
            {
                return NotFound();
            }

            await _materialService.UpdateMaterial(material, updatedMaterial);

            return NoContent();

        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteMaterial(int id)
        {

            var allExistingMaterial = await _materialService.GetAllMaterials();
            var material = allExistingMaterial.Where(x => x.Id == id).FirstOrDefault();

            if (material == null)
            {
                return NotFound("");
            }

            await _materialService.DeleteMaterial(material);

            return Ok($"The Material with the id:{id} has been removed successfully");

        }

    }
}
