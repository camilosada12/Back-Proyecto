namespace Entity.DTOs.Default.LoginDto
{
    public class LoginDto
    {
        // Modo 1: email + password (Hacienda / Inspectora)
        public string? Email { get; set; }
        public string? Password { get; set; }

        // Modo 2: documento (PersonaNormal)
        public int? DocumentTypeId { get; set; }
        public string? DocumentNumber { get; set; }

    }
}
