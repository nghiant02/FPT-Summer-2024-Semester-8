namespace Repository
{
    public interface ITokenRepository
    {
        string GenerateAccessToken(string email, int role);
    }
}
