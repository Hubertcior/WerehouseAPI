namespace WerehouseAPI.Dtos
{
    public class GetSenderDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required string City { get; set; }
    }
}