using Back_End.Models;
using Back_End.Services.Supplier;
using Back_End.Services.SupplierService;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Back_End.Controllers
{
    [ApiController]
    [Route("api/supplier")]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;
        private readonly IValidator<SupplierModel> _validator;

        public SupplierController (ISupplierService supplierService, IValidator<SupplierModel> validator) 
        { 
            _supplierService = supplierService;
            _validator = validator;
        }


        [HttpGet("get")]
        public async Task<IActionResult> GetAllSuppliers()
        {
            var supplier = await _supplierService.GetAllSuppliers();

            if (supplier == null)
            {
                return NotFound();//404
            }
            
            return Ok(supplier);//200
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateSupplier(SupplierModel supplier)
        {
            if (supplier.RegistDate == default(DateTime))
            {
                supplier.RegistDate = DateTime.Now;
            }

            var validationResult = await _validator.ValidateAsync(supplier);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return BadRequest(ModelState);
            }

            var newSupplier = await _supplierService.CreateSupplier(supplier);
            return CreatedAtAction(nameof(CreateSupplier), newSupplier); // 201
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateSupplier(SupplierModel updatedSupplier)
        {
            if (updatedSupplier.RegistDate == default(DateTime))
            {
                updatedSupplier.RegistDate = DateTime.Now;
            }

            var validationResult = await _validator.ValidateAsync(updatedSupplier);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return BadRequest(ModelState);
            }


            
            var allExistingSupplier = await _supplierService.GetAllSuppliers();
            var supplier = allExistingSupplier.Where(x => x.Id == updatedSupplier.Id).FirstOrDefault();

            if(supplier == null)
            {
                return NotFound();
            }

            await _supplierService.UpdateSupplier(supplier, updatedSupplier);
            
            return NoContent();
          
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteSupplier(int id)
        {

            var allExistingSupplier = await _supplierService.GetAllSuppliers();
            var supplier = allExistingSupplier.Where(x => x.Id == id).FirstOrDefault();

            if (supplier == null)
            {
                return NotFound();
            }

            await _supplierService.DeleteSupplier(supplier);

            return Ok($"The Supplier with the id:{id} has been remove successfully");
        }


    }
}


