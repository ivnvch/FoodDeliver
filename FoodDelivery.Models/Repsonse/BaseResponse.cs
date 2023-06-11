using FoodDelivery.Models.Enum;

namespace FoodDelivery.Models.Repsonse
{
    public class BaseResponse<T> : IBaseResponse<T>
    {
        public T Data { get; set; }
        public StatusCode StatusCode { get; set; }
        public string Description { get; set; }
    }

    public interface IBaseResponse<T>
    {
        T Data { get; set; }
        StatusCode StatusCode { get; set; }
        string Description { get; set; }
    }
}
