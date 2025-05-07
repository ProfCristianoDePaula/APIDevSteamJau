using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace APIDevSteamJau.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CpfController : ControllerBase
    {
        [HttpPost("validar")]
        public IActionResult ValidarCpf([FromBody] string cpf)
        {
            // Verificar e formatar o CPF
            var cpfFormatado = ValidarEFormatarCpf(cpf);
            if (cpfFormatado == null)
            {
                return BadRequest("CPF inválido.");
            }
            return Ok(cpfFormatado);
        }

        private string ValidarEFormatarCpf(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
                return null;

            // Remover caracteres não numéricos
            var cpfLimpo = RemoverCaracteresNaoNumericos(cpf);

            // Validar o CPF
            if (!ValidarCpfNumerico(cpfLimpo))
                return null;

            // Aplicar a máscara
            return AplicarMascara(cpfLimpo);
        }

        private string RemoverCaracteresNaoNumericos(string cpf)
        {
            return Regex.Replace(cpf, @"\D", "");
        }

        private bool ValidarCpfNumerico(string cpf)
        {
            // Verificar se o CPF tem 11 dígitos
            if (cpf.Length != 11)
                return false;

            // Cálculos de validação do CPF
            var numeros = cpf.Select(c => int.Parse(c.ToString())).ToArray();
            var primeiros9Digitos = numeros.Take(9).ToArray();

            // Validação do primeiro dígito
            var primeiroDigito = CalcularDigitoVerificador(primeiros9Digitos, 10);
            if (primeiroDigito != numeros[9])
                return false;

            // Validação do segundo dígito
            var segundoDigito = CalcularDigitoVerificador(primeiros9Digitos.Concat(new[] { primeiroDigito }).ToArray(), 11);
            return segundoDigito == numeros[10];
        }

        private int CalcularDigitoVerificador(int[] numeros, int peso)
        {
            int soma = 0;
            for (int i = 0; i < numeros.Length; i++)
            {
                soma += numeros[i] * peso--;
            }

            int resto = soma % 11;
            return resto < 2 ? 0 : 11 - resto;
        }

        private string AplicarMascara(string cpf)
        {
            return Regex.Replace(cpf, @"(\d{3})(\d{3})(\d{3})(\d{2})", "$1.$2.$3-$4");
        }
    }
}
