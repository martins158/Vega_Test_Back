using Back_End.Models;

namespace Back_End.Services.Material
{
    public interface IMaterialService
    {
        Task<List<MaterialModel>> GetAllMaterials();
        Task<MaterialModel> CreateMaterial(MaterialModel material);
        Task UpdateMaterial(MaterialModel oldMaterial, MaterialModel newMaterial);
        Task DeleteMaterial(MaterialModel material);

    }
}
