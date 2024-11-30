namespace PresentationApi.Auth.DTOs
{

    public sealed record LoginUserResponse
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string Email { get; set; }
      
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime Expiration { get; set; }
        public string Status { get; set; }
        public string? Role { get; set; }
        public string ErrorMessage { get; set; }
        public int ErrorCode { get; set; }

        public object? MerchantObj { get; set; } = null;





    }
}
