using ErrorOr;

namespace JobConnectApi.Services.ErrorService;

public class UserErrors
{
    public static Error InvalidCredentials => Error.NotFound(
        code: "Auth.InvalidCredentials", description: "Invalid username or password");
    public static Error UserNotFound => Error.NotFound(
        code: "Auth.UserNotFound", description: "User not found with the provided credentials");
    public static Error AccountLocked => Error.NotFound(
        code: "Auth.AccountLocked", description: "Account is locked");
    public static Error AccountDisabled => Error.NotFound(
        code: "Auth.AccountDisabled", description: "Account has been disabled");
    public static Error InsufficientPermissions => Error.NotFound(
        code: "Auth.InsufficientPermissions", description: "Insufficient permissions to perform this action");
    public static Error TooManyRequests => Error.Conflict(
        code: "Auth.TooManyRequests", description: "Too many requests in a short period. Please try again later");
    public static Error InvalidUsername => Error.Validation(
        code: "Auth.InvalidUsername", description: "Username does not meet requirements");
    public static Error UsernameAlreadyUsed => Error.Validation(
        code: "Auth.InvalidUsername", description: "Username Already Used");
    public static Error InvalidEmail => Error.Validation(
        code: "Auth.InvalidEmail", description: "Email format is invalid");
    public static Error InvalidPassword => Error.Validation(
        code: "Auth.InvalidPassword", description: "Password does not meet complexity requirements");

}