using System.ComponentModel.DataAnnotations;

namespace WerehouseAPI.Dtos
{
    public class CreateSenderDto
    {
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public required string City { get; set; }
        public required string Street { get; set; }
        public required string PostalCode { get; set; }
    }
}
