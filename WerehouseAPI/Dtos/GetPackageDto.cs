namespace WerehouseAPI.Dtos
{
    public class GetPackageDto
    {
        public int Id { get; set; }
        public double Weight { get; set; }
        public required string Serial { get; set; }
        public double Height { get; set; }
        public decimal Price { get; set; }
        public required string Status { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public GetSenderDto Sender { get; set; } = null!;
        public GetSenderDto Receiver { get; set; } = null!;
    }
}
