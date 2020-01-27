using FluentValidation;

namespace Core.CustomValidators
{
    public static class CnpjCustomValidator
    {
        public static IRuleBuilderInitial<T, string> Cnpj<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Custom((cnpj, context) =>
            {
                cnpj = cnpj.Trim();
                cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
                if (cnpj.Length != 14)
                {
                    context.AddFailure("CNPJ Inválido.");
                    return;
                }

                var multiplicador1 = new[] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                var multiplicador2 = new[] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                int soma;
                int resto;
                string digito;
                string tempCnpj;

                tempCnpj = cnpj.Substring(0, 12);
                soma = 0;
                for (var i = 0; i < 12; i++)
                    soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
                resto = (soma % 11);
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;
                digito = resto.ToString();
                tempCnpj += digito;
                soma = 0;
                for (var i = 0; i < 13; i++)
                    soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
                resto = (soma % 11);
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;
                digito += resto.ToString();

                if (!cnpj.EndsWith(digito))
                {
                    context.AddFailure("CNPJ Inválido.");
                }
            });
        }
    }
}