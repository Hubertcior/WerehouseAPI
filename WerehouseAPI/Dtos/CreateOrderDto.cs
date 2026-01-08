using System.ComponentModel.DataAnnotations;

namespace WerehouseAPI.Dtos
{
    public class CreateOrderDto
    {
        [Required]
        public int SenderId { get; set; } 

        [Required]
        public double Weight { get; set; }
        [Required]
        public double Height { get; set; }
        public decimal Price { get; set; }

        [Required]
        public required CreateReceiverDto Receiver { get; set; }
    }
    public class CreateReceiverDto : CreateSenderDto
    {

    }
}
