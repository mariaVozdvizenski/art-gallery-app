namespace PublicApi.DTO.v1.Mappers
{
    public class BasketMapper : ApiBaseMapper<BLL.App.DTO.Basket, Basket>
    {
        public BasketView MapBasketView(BLL.App.DTO.Basket inObject)
        {
            return new BasketView()
            {
                AppUserId = inObject.AppUserId,
                DateCreated = inObject.DateCreated,
                Id = inObject.Id,
                UserName = inObject.AppUser!.UserName
            };
        }
    }
}