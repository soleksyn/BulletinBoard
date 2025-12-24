namespace BulletinBoard.Core.Constants
{
    public static class ErrorConstants
    {
        public const string InternalServerError = "Internal server error: {0}";
        public const string NotFound = "Announcement not found.";
        public const string IdMismatch = "ID mismatch.";
        public const string InvalidModelState = "Invalid model state.";
        public const string DatabaseError = "A database error occurred while processing your request.";
        public const string InvalidUserIdInToken = "Invalid user ID in token: {0}. Please logout and login again.";
        public const string UserAlreadyExists = "User with this email already exists.";
        public const string InvalidEmailOrPassword = "Invalid email or password.";
        public const string LoginFailed = "Login failed";
        public const string RegistrationFailed = "Registration failed";
    }
}
