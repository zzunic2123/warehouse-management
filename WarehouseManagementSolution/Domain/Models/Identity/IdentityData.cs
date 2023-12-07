namespace Domain.Models.Identity;

public class IdentityData
{
    public const string ClaimType = "Roles";
    public const string AdminClaimValue = "ADMIN";
    public const string AdminPolicyName = "Admin";
    public const string UserClaimValue = "USER";
    public const string UserPolicyName = "User";
    public const string OperatorClaimValue = "OPERATOR";
    public const string OperatorPolicyName = "Operator";
    public const string BackOfficeClaimValue = "BACKOFFICEUSER";
    public const string BackOfficePolicyName = "BackOfficeUser";
}