namespace StockManage.Services
{
    public interface IJwtServices
    {
        string GenerateToken(string username);
    }
}
