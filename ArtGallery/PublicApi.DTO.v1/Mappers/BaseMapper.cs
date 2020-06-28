using DAL = ee.itcollege.mavozd.DAL.Base.Mappers;
namespace PublicApi.DTO.v1.Mappers
{
    public class BaseMapper<TLeftObject, TRightObject> : DAL.BaseMapper<TLeftObject, TRightObject>
        where TRightObject : class?, new()
        where TLeftObject : class?, new()
    {
    }
}