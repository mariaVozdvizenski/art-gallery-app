﻿using ee.itcollege.mavozd.Contracts.BLL.Base.Mappers;
using DAL.App.DTO;
using BLLAppDTO = BLL.App.DTO;


namespace Contracts.BLL.App.Mappers
{
    public interface ICommentServiceMapper : IBaseMapper<Comment, BLLAppDTO.Comment>
    {
        BLLAppDTO.BLLCommentView MapCommentView(DALCommentView inObject);
    }
}