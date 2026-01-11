using System.ComponentModel.DataAnnotations;

namespace WerehouseAPI.Dtos
{
    public class CreateOrderDto
    {
        public int SenderId { get; set; } 
        public double Weight { get; set; }
        public double Height { get; set; }
        public decimal Price { get; set; }
        public required CreateReceiverDto Receiver { get; set; }
    }
    public class CreateReceiverDto : CreateSenderDto
    {

    }
}
