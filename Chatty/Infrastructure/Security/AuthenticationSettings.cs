namespace Infrastructure.Security
{
    public class AuthenticationSettings
    {
        public string Key { get; set; } = default!;
        public int ExpireDays { get; set; }
    }
}