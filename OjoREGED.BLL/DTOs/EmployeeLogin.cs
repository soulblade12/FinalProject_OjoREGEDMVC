using System.ComponentModel.DataAnnotations;

namespace OjoREGED.BLL.DTOs
{
    public class EmployeeLogin
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
