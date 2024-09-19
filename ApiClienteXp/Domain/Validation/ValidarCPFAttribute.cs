using System.ComponentModel.DataAnnotations;

namespace ApiClienteXp.Domain.Validation
{
    public class ValidarCPFAttribute : ValidationAttribute
    {

        //cpf 54721450583
        protected override ValidationResult IsValid(object value,
          ValidationContext validationContext)
        {
            var cpf = value.ToString();
            if (cpf.Length != 11 || !cpf.All(char.IsDigit))
            {
                return new ValidationResult("O CPF deve conter exatamente 11 dígitos numéricos.");
            }
            int[] somar = new int[2] { 0, 0 };
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string digito;
            for (int i = 0; i < 10; i++)
            {
                if (i < 9) somar[0] += int.Parse(cpf[i].ToString()) * multiplicador1[i];
                somar[1] += int.Parse(cpf[i].ToString()) * multiplicador2[i];
            }
            digito = ((somar[0] % 11) < 2 ? 0 : 11 - somar[0] % 11).ToString();
            digito += ((somar[1] % 11) < 2 ? 0 : 11 - somar[1] % 11).ToString();
            var confirmacao = cpf.EndsWith(digito);
            if (!confirmacao)
            {
                return new ValidationResult("Insira um CPF valido.");
            }
            return ValidationResult.Success;
        }
    }
}
