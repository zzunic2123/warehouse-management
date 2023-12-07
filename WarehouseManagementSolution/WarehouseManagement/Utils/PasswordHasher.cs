using Domain.Models.DomainModels;
using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Utils;

internal sealed class PasswordHasher : IPasswordHasher
{
    private readonly PasswordHasher<User> _passwordHasher = new();

    public bool VerifyPassword(string providedPassword, string hashedPassword) =>
        _passwordHasher.VerifyHashedPassword(null!, hashedPassword, providedPassword) ==
        PasswordVerificationResult.Success;

    public string Hash(string password) => _passwordHasher.HashPassword(null!, password);
}