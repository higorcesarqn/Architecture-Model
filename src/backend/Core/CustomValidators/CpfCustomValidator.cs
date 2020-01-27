using FluentValidation;

namespace Core.CustomValidators
{
    public static class CpfCustomValidator
    {
        public static IRuleBuilderInitial<T, string> Cpf<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Custom((cpf, context) =>
           {
               if (cpf == null)
               {
                   context.AddFailure("CPF Inválido.");
                   return;
               }

               cpf = cpf.Trim();
               cpf = cpf.Replace(".", "").Replace("-", "");
               if (cpf.Length != 11)
               {
                   context.AddFailure("CPF Inválido.");
                   return;
               }

               var multiplicador1 = new[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
               var multiplicador2 = new[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

               var tempCpf = cpf.Substring(0, 9);
               var soma = 0;

               for (var i = 0; i < 9; i++)
               {
                   soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
               }

               var resto = soma % 11;
               if (resto < 2)
               {
                   resto = 0;
               }
               else
               {
                   resto = 11 - resto;
               }

               var digito = resto.ToString();
               tempCpf += digito;
               soma = 0;
               for (var i = 0; i < 10; i++)
               {
                   soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
               }

               resto = soma % 11;
               if (resto < 2)
               {
                   resto = 0;
               }
               else
               {
                   resto = 11 - resto;
               }

               digito = digito + resto.ToString();
               if (!cpf.EndsWith(digito))
               {
                   context.AddFailure("CPF Inválido.");
               }
           });
        }
    }
}