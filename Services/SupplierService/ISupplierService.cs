

using Back_End.Models;

namespace Back_End.Services.SupplierService
{
    public interface ISupplierService
    {
        Task<List<SupplierModel>> GetAllSuppliers();
        Task<SupplierModel> CreateSupplier(SupplierModel supplier);
        Task UpdateSupplier(SupplierModel oldSupplier, SupplierModel newSupplier);
        Task DeleteSupplier(SupplierModel supplier);

    }
}
