namespace WebApi.Auth
{
    public class TokenModelDto
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public string? errorMSG { get; set; }
        public int? errorCode { get; set; }
    }
}
