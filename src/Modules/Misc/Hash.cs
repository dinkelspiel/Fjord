using System.Security.Cryptography;
using System;

public static class Hash {
    public static string HashString(string text)
    {
        var salt = "";
        
        if (String.IsNullOrEmpty(text))
        {
            return String.Empty;
        }
        
        // Uses SHA256 to create the hash
        var sha = SHA256.Create();
        using (sha)
        {
            // Convert the string to a byte array first, to be processed
            byte[] textBytes = System.Text.Encoding.UTF8.GetBytes(text + salt);
            byte[] hashBytes = sha.ComputeHash(textBytes);
            
            // Convert back to a string, removing the '-' that BitConverter adds
            string hash = BitConverter
                .ToString(hashBytes)
                .Replace("-", String.Empty);

            return hash;
        }
    }
}