namespace ee.itcollege.mavozd.Contracts.BLL.Base.Mappers
{
    public interface IBaseMapper<TLeftObject, TRightObject> : ee.itcollege.mavozd.Contracts.DAL.Base.Mappers.IBaseMapper<TLeftObject, TRightObject>
        where TLeftObject: class?, new()
        where TRightObject: class?, new()
    {
        
    }
}