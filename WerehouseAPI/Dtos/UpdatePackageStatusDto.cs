using Microsoft.AspNetCore.Routing.Constraints;
using System.ComponentModel.DataAnnotations;

namespace WerehouseAPI.Dtos
{
    public class UpdatePackageStatusDto
    {
        [Required]
        public int NewStatusId { get; set; }
    }
}
