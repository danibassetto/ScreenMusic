using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using ScreenMusic.Arguments;
using ScreenMusic.Domain.Interfaces.Service;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ScreenMusic.Domain.Services;

public class AuthenticationService(IHttpContextAccessor httpContext /*, IUserService userService*/) : BaseService_0, IAuthenticationService
{
    private readonly HttpContext _httpContext = httpContext.HttpContext;
    //private readonly IUserService _userService = userService;

    public OutputAuthentication? Authenticate(InputAuthentication inputAuthentication)
    {
        return new OutputAuthentication("", "", DateTime.Now);
        //OutputUser? user = _userService.GetByIdentifier(new InputIdentifierUser(inputAuthentication.Username));
        //if (user is not null)
        //{
        //    if (inputAuthentication.Password == user.Password)
        //    {
        //        var token = GenerateJwtToken(user.Id, user.Username!);

        //        if (inputAuthentication.SaveFileToken)
        //        {
        //            try
        //            {
        //                var fileContent = token;
        //                var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        //                var filePath = Path.Combine(desktopPath, "ScreenMusic-BearerToken.txt");
        //                File.WriteAllText(filePath, fileContent);
        //            }
        //            catch (Exception ex)
        //            {
        //                return new OutputAuthentication($"Usúario autorizado, porém ocorreram problemas para salvar um arquivo do Token: '{ex.Message}'. Configure seu novo Bearer Token.", token, DateTime.UtcNow.AddDays(7));
        //            }
        //        }

        //        _userService.Update(user.Id, new InputUpdateUser(user.Username!, user.Password) { TokenExpirationDate = DateTime.UtcNow.AddDays(7) });

        //        return new OutputAuthentication("Usúario autorizado. Configure seu novo Bearer Token.", token, DateTime.UtcNow.AddDays(7));
        //    }
        //    else
        //        return new OutputAuthentication("Usuário não autorizado. Senha incorreta.", string.Empty, null);
        //}
        //else
        //    return new OutputAuthentication("Usuário não existe. Cadastre seu usuário no endpoint aberto POST '/api/User'", string.Empty, null);
    }

    private string GenerateJwtToken(long userId, string userName)
    {
        List<Claim> claims =
        [
            new(JwtRegisteredClaimNames.Iss, _httpContext.Request.Host.Value),
            new(JwtRegisteredClaimNames.Sub, userName),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        ];

        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes("63bcc5be-fa37-4290-b5a4-57a6a4e3afcb"));
        SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha256);
        var expireDate = DateTime.UtcNow.AddDays(7);

        JwtSecurityToken token = new(
            _httpContext.Request.Host.Value,
            userId.ToString(),
            claims,
            expires: expireDate,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}