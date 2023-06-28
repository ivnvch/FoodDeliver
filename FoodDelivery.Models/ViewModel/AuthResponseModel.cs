namespace FoodDelivery.Models.ViewModel
{
    public class AuthResponseModel
    {
        public string AccessToken { get; set; }
        //public string RefreshToken { get; set; }
        public bool IsOnboarded { get; set; }
        public Guid UserToken { get; set; }
        public DateTime ValidTo { get; set; }
        //public DateTime RefreshValidTo { get; set; }
        public DateTime ValidFrom { get; set; }
    }
}
