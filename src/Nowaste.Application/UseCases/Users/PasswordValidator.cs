using FluentValidation;
using FluentValidation.Validators;
using System.Text.RegularExpressions;

namespace Nowaste.Application.UseCases.Users;

public partial class PasswordValidator<T> : PropertyValidator<T, string> {
    private const string ERROR_MESSAGE_KEY = "ErrorMessage";

    public override string Name => "PasswordValidator";

    protected override string GetDefaultMessageTemplate(string errorCode) {
        return $"{{{ERROR_MESSAGE_KEY}}}";
    }

    public override bool IsValid(ValidationContext<T> context, string password) {
        if(string.IsNullOrEmpty(password)) {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, "A senha deve ter no mínimo 8 " +
                "caracteres contendo pelo menos uma letra maiúscula, uma letra minúscula, um número " +
                "e um caractere especial (por exemplo, @, *, -, ?).");

            return false;
        }

        if(password.Length < 8) {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, "A senha deve ter no mínimo 8 caracteres.");

            return false;
        }

        var hasAtLeastOneUpperCaseCharacter = UpperCaseLetter().IsMatch(password);

        if (!hasAtLeastOneUpperCaseCharacter) {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, "A senha deve ter pelo menos uma letra maiúscula.");

            return false;
        }

        var hasAtLeastOneLowerCaseCharacter = LowerCaseLetter().IsMatch(password);

        if (!hasAtLeastOneLowerCaseCharacter) {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, "A senha deve ter pelo menos uma letra minúscula.");
            return false;
        }

        var hasAtLeastOneNumber = Number().IsMatch(password);

        if (!hasAtLeastOneNumber) {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, "A senha deve ter pelo menos um número.");
            return false;
        }

        var hasAtLeastOneSpecialCharacter = SpecialCharacterLetter().IsMatch(password);

        if (!hasAtLeastOneSpecialCharacter) {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, "A senha deve ter pelo menos um caracter especial.");
            return false;
        }

        return true;
    }

    [GeneratedRegex(@"[A-Z]+")]
    private static partial Regex UpperCaseLetter();

    [GeneratedRegex(@"[a-z]+")]
    private static partial Regex LowerCaseLetter();

    [GeneratedRegex(@"[0-9]+")]
    private static partial Regex Number();

    [GeneratedRegex(@"[\!\?\.\@\*\-\&\(\)]+")]
    private static partial Regex SpecialCharacterLetter();
}
