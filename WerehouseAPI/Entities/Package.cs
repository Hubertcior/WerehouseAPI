using System.ComponentModel.DataAnnotations;

namespace WerehouseAPI.Entities
{
    public class Package
    {
        public int Id { get; set; }

        public string SerialNumber { get; set; } = Guid.NewGuid().ToString();

        public double Weight { get; set; }
        public double Height { get; set; }
        public decimal Price { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public int SenderId { get; set; }
        public virtual Sender Sender { get; set; } = null!;

        public int ReceiverId { get; set; }
        public virtual Receiver Receiver { get; set; } = null!;

        public int StatusId { get; set; } = 1;
        public virtual PackageStatus Status { get; set; } = null!;

    }
}
