using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Common.Infrasturucture;

public class PasswordEncryptor
{
    public static string Encrpt(string password)
    {
        using var md5 = MD5.Create();

        byte[] inputBytes = Encoding.UTF8.GetBytes(password);
        byte[] hassBytes = md5.ComputeHash(inputBytes);

        return Convert.ToHexString(hassBytes);
    }
}
