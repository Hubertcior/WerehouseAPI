using Microsoft.AspNetCore.Routing.Constraints;
using System.ComponentModel.DataAnnotations;

namespace WerehouseAPI.Dtos
{
    public class UpdatePackageStatusDto
    {
        public int NewStatusId { get; set; }
    }
}
