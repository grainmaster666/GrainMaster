public static class PasswordHashing
{
    public static string Hash(string password, int workFactor)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, workFactor);
    }

    public static string Hash(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, 5);
    }

    public static bool Verify(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
}