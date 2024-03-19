namespace OjoREGED.BLL.DTOs
{
    public class CustomerCreateDTO
    {
        public string First_Name { get; set; }
        public string Middle_Name { get; set; }
        public string Last_Name { get; set; }
        public string Telephone { get; set; }
        public string Email_Address { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public string RePassword { get; set; }
    }
}
