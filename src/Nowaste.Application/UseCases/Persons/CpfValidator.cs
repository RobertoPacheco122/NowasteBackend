using FluentValidation;
using FluentValidation.Validators;
using System.Text.RegularExpressions;

namespace Nowaste.Application.UseCases.Persons;

public partial class CpfValidator<T> : PropertyValidator<T, string> {
    private const string ERROR_MESSAGE_KEY = "ErrorMessage";
    public override string Name => "CpfValidator";

    protected override string GetDefaultMessageTemplate(string errorCode) {
        return $"{{{ERROR_MESSAGE_KEY}}}";
    }

    public override bool IsValid(ValidationContext<T> context, string cpf) {
        cpf = CleanCpf().Replace(cpf, "");

        if (string.IsNullOrEmpty(cpf)) {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, "O CPF é obrigatório.");

            return false;
        }

        if(cpf.Length != 11) {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, "O CPF deve ter 11 números.");

            return false;
        }

        string temporaryCpf = cpf[..9];

        int[] firstDigitMultipliers = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] secondDigitMultipliers = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        int firstDigit;
        int firstDigitSum = 0;
        int firstDigitReminder;

        int secondDigit;
        int secondDigitSum = 0;
        int secondDigitReminder;

        for(int i = 0; i < 9; i++)
            firstDigitSum += int.Parse(temporaryCpf[i].ToString()) * firstDigitMultipliers[i];
        
        firstDigitReminder = firstDigitSum % 11;
        firstDigit = firstDigitReminder < 2 ? 0 : 11 - firstDigitReminder;
        temporaryCpf += firstDigit.ToString();

        for (int i = 0; i < 10; i++)
            secondDigitSum += int.Parse(temporaryCpf[i].ToString()) * secondDigitMultipliers[i];
        
        secondDigitReminder = secondDigitSum % 11;
        secondDigit = secondDigitReminder < 2 ? 0 : 11 - secondDigitReminder;

        string verifierDigits = $"{firstDigit}{secondDigit}";

        if(!cpf.EndsWith(verifierDigits)) {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, "O CPF é inválido.");

            return false;
        }

        return true;
    }

    [GeneratedRegex("[^0-9]")]
    private static partial Regex CleanCpf();
}
