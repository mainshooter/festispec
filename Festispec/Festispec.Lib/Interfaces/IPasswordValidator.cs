namespace Festispec.Lib.Interfaces
{
    public interface IPasswordValidator
    {
        string StringToPassword(string toPassword);
        bool PasswordsCompare(string unHashed, string hashed);
    }
}
