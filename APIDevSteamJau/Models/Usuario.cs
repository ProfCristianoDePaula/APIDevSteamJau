using Microsoft.AspNetCore.Identity;

namespace APIDevSteamJau.Models
{
    public class Usuario : IdentityUser
    {

        public string? NomeCompleto { get; set; }
        public DateOnly DataNascimento { get; set; }


        public Usuario() : base()
        {
        }
    }
}
