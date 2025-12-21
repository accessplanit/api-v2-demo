using System;

namespace Models;

public class LoginResultModel
{
    public bool Success { get; set; } = false;
    public string Message { get; set; } = string.Empty;
    public User User { get; set; } = null;
}
