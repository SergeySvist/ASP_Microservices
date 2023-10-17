namespace UsersMS.Services
{
    public interface IPasswordService
    {
        byte[] CreatePasswordHash(string openTextPassword);
        bool ValidatePasswordAgainstHash(byte[] hashToCheck, string password);

    }
}
