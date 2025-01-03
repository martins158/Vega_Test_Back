using System.ComponentModel.DataAnnotations;

namespace Back_End.Models
{
    public class SupplierModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? QrCode { get; set; }
        public string Cnpj { get; set; }
        public string Cep { get; set; }
        public string Country { get; set; }
        public DateTime RegistDate { get; set; }


    }
}
