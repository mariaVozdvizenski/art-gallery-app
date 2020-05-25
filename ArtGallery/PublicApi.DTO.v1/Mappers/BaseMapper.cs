﻿namespace PublicApi.DTO.v1.Mappers
{
    public class BaseMapper<TLeftObject, TRightObject> : DAL.Base.Mappers.BaseMapper<TLeftObject, TRightObject>
        where TRightObject : class?, new()
        where TLeftObject : class?, new()
    {
    }
}