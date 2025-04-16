using Microsoft.AspNetCore.Identity;

namespace APIDevSteamJau.Models
{
    public class Usuario : IdentityUser
    {
        public Usuario() : base()
        { }

        public string? NomeCompleto { get; set; }
        public DateOnly DataNascimento { get; set; }
    }
}
