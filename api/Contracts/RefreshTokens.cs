
public class RefreshTokenRequest
{
    public string UserId { get; set; } = default!;
}

public class RevokeTokenRequest
{
    public string UserId{ get; set; } = default!;
}