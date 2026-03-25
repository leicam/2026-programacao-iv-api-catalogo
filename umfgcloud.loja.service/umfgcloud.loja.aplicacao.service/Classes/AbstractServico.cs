using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;

namespace umfgcloud.loja.aplicacao.service.Classes;

public abstract class AbstractServico
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    protected string UserId { get; private set; } = string.Empty;
    protected string UserEmail { get; private set; } = string.Empty;

    private string Token { get; set; } = string.Empty;
    private JwtSecurityToken UserSecurityToken { get; set; }

    protected AbstractServico(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));

        Token = GetUserToken();
        UserSecurityToken = GetDecodedToken();
        UserId = GetUserId();
        UserEmail = GetUserEmail();
    }

    #region metodos privados

    private string GetUserToken() => _httpContextAccessor.HttpContext.Request.Headers[HeaderNames.Authorization].ToString().Split(" ").LastOrDefault() ?? string.Empty;

    private JwtSecurityToken GetDecodedToken() => new JwtSecurityToken(Token);    

    private string GetUserId()
        => IsPayloadContainsKey(JwtRegisteredClaimNames.NameId) ? GetPayloadValue(JwtRegisteredClaimNames.NameId).ToUpper() : throw new InvalidOperationException("Usuario nao possui um id valido");    

    private string GetUserEmail()
        => IsPayloadContainsKey(JwtRegisteredClaimNames.Email) ? GetPayloadValue(JwtRegisteredClaimNames.Email).ToUpper() : throw new InvalidOperationException("Usuario nao possui um email valido");

    private bool IsPayloadContainsKey(string key) => UserSecurityToken.Payload.ContainsKey(key);
    private string GetPayloadValue(string key) => UserSecurityToken.Payload[key].ToString() ?? string.Empty;

    #endregion metodos privados
}