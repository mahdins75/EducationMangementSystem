using System;
using System.IdentityModel.Tokens.Jwt;

public class JwtHelper
{
    public static bool IsTokenExpired(string token)
    {
        try
        {
            var jwtToken = new JwtSecurityToken(token);

            var exp = jwtToken.Payload.Exp;

            var expDateTime = DateTimeOffset.FromUnixTimeSeconds((long)exp).UtcDateTime;
            return DateTime.UtcNow >= expDateTime;
        }
        catch (ArgumentException)
        {
            return true;
        }
        catch (Exception)
        {
            return true;
        }
    }
}
