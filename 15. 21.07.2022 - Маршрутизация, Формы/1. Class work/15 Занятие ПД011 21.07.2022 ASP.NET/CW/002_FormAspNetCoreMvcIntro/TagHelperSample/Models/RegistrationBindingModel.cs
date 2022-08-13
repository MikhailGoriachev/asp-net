namespace TagHelperSample.Models;

// Модель для привязки к форме
public record RegistrationBindingModel(
    string Login, string Email, string Password, string PasswordConfirm, 
    bool TermsAccepted
);

