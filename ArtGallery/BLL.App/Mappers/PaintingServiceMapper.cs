using AutoMapper;
using BLL.App.DTO;
using Contracts.BLL.App.Mappers;
using DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class PaintingServiceMapper :  AppServiceBaseMapper<DAL.App.DTO.Painting, DTO.Painting>, IPaintingServiceMapper
    {
        public PaintingServiceMapper()
        {
        }

        public BLLPaintingView MapPaintingView(DALPaintingView inObject)
        {
            return Mapper.Map<DALPaintingView, BLLPaintingView>(inObject);
        }

    }
}