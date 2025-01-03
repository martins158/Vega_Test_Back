using Back_End.Data;
using Back_End.Models;
using Back_End.Services.SupplierService;
using Microsoft.EntityFrameworkCore;

namespace Back_End.Services.Supplier
{
    public class SupplierService : ISupplierService
    {
        private readonly AppDbContext _context;

        public SupplierService(AppDbContext context) 
        {
            _context = context;
        }

        public async Task<SupplierModel> CreateSupplier(SupplierModel supplier)
        {

            GenerateQrCode(supplier);
            await _context.Supplier.AddAsync(supplier);
            await _context.SaveChangesAsync();
            return supplier;
        }

        public async Task DeleteSupplier(SupplierModel supplier)
        {
            _context.Supplier.Remove(supplier);
            await _context.SaveChangesAsync();
            //return $"The Supplier with the id:{supplier.Id} has been remove successfully";
        }

        public async Task<List<SupplierModel>> GetAllSuppliers()
        {
            return await _context.Supplier.ToListAsync();
        }

        public async Task UpdateSupplier(SupplierModel oldSupplier, SupplierModel updatedSupplier)
        {

            oldSupplier.Name = updatedSupplier.Name;
            
            oldSupplier.Cnpj = updatedSupplier.Cnpj;
            oldSupplier.Cep = updatedSupplier.Cep;
            oldSupplier.Country = updatedSupplier.Country;
            oldSupplier.RegistDate = updatedSupplier.RegistDate;

            GenerateQrCode(updatedSupplier);
            oldSupplier.QrCode = updatedSupplier.QrCode;
            await _context.SaveChangesAsync();

        }

        private string GenerateQrCode(SupplierModel supplier)
        {
            string formattedDate = supplier.RegistDate.ToString("dd/MM/yyyy");
            string qrCode = "%" + supplier.Cnpj + "% - %" + supplier.Cep + "%" + " / CAD.%" + formattedDate + "%";
            supplier.QrCode = qrCode;
            return qrCode;
        }







    }
}
