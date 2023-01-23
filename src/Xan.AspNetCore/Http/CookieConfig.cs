using Microsoft.AspNetCore.Http;

namespace Xan.AspNetCore.Http;

public sealed record CookieConfig(
      string Key
    , CookieOptions CookieOptions
);
