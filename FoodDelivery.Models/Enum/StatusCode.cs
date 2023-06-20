namespace FoodDelivery.Models.Enum
{
    public enum StatusCode
    {
        ProfileNotFound = 0,
        UserNotFound = 1,

        UserAlreadyExist = 20,
        DishAlreadyExist = 21,

        OK = 200,
        InternalServerError = 500
    }
}
