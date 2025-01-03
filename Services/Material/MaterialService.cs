using Back_End.Data;
using Back_End.Models;
using Microsoft.EntityFrameworkCore;

namespace Back_End.Services.Material
{
    public class MaterialService : IMaterialService
    {
        private readonly AppDbContext _context;

        public MaterialService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<MaterialModel> CreateMaterial(MaterialModel material)
        {
            await _context.Material.AddAsync(material);
            await _context.SaveChangesAsync();
            return material;
        }

        public async Task DeleteMaterial(MaterialModel material)
        {
            _context.Material.Remove(material);
            await _context.SaveChangesAsync();
            
        }
        
        public async Task<List<MaterialModel>> GetAllMaterials()
        {
            return await _context.Material.ToListAsync();

        }
        

        public async Task UpdateMaterial(MaterialModel oldMaterial, MaterialModel updateModel)
        {
            oldMaterial.Code = updateModel.Code;
            oldMaterial.Name = updateModel.Name;
            oldMaterial.Description = updateModel.Description;
            oldMaterial.FiscalCode = updateModel.FiscalCode;
            oldMaterial.Specie = updateModel.Specie;
            oldMaterial.UpdatedAt = DateTime.Now;
            oldMaterial.UpdatedBy = updateModel.UpdatedBy;

            await _context.SaveChangesAsync();
        }

    }
}
