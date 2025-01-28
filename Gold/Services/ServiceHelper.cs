using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;


namespace Gold.Services;

public static class ServiceHelper
{
    public static IServiceProvider Services { get; private set; }

    public static void Initialize(IServiceProvider serviceProvider) => Services = serviceProvider;

    public static T GetService<T>() => Services.GetService<T>();
}

public static class HashHelper
{
    public static string ComputeHash512(string input)
    {
        // Convert the input string to a byte array
        byte[] bytes = Encoding.UTF8.GetBytes(input);

        // Create a SHA512 instance and compute the hash
        using (SHA512 sha512 = SHA512.Create())
        {
            byte[] hashBytes = sha512.ComputeHash(bytes);

            // Convert the byte array to a hexadecimal string
            StringBuilder sb = new StringBuilder();
            foreach (byte b in hashBytes)
            {
                sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }
    }
}




