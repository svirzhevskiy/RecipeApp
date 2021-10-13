using System;

namespace Application.Exceptions
{
    public class AppException : Exception
    {
        public short StatusCode { get; set; }
        
        public AppException(short statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }

        public static AppException BadAuth => new(400, "Wrong login or password");
        public static AppException UserExist => new(400, "User already registered");
        public static AppException UserNotFound => new(404, "User not found");
        public static AppException Bug(string message = "BUG occured!") => new(520, message);
        public static AppException RecipeNotFound => new(404, "Recipe not found");
        public static AppException NotFound => new(404, "Entity not found");
        public static AppException InvalidToken => new(401, "invalid token");
        public static AppException InvalidModel => new(400, "invalid model");
    }
}