namespace PublicApi.DTO.v1.Mappers
{
    public class PaintingCategoryMapper : ApiBaseMapper<BLL.App.DTO.PaintingCategory, PaintingCategory>
    {
        public PaintingCategoryView MapPaintingCategoryView(BLL.App.DTO.PaintingCategory inObject)
        {
            return new PaintingCategoryView()
            {
                CategoryId = inObject.CategoryId,
                CategoryName = inObject.Category!.CategoryName,
                Id = inObject.Id,
                PaintingId = inObject.PaintingId
            };
        }
    }
}