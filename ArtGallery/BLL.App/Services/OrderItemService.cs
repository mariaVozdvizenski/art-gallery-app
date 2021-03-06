﻿using System.Threading.Tasks;
using BLL.App.Mappers;
using ee.itcollege.mavozd.BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using OrderItem = BLL.App.DTO.OrderItem;

namespace BLL.App.Services
{
    public class OrderItemService : BaseEntityService<IAppUnitOfWork, IOrderItemRepository, IOrderItemServiceMapper, DAL.App.DTO.OrderItem,
    OrderItem>, IOrderItemService
    {
        public OrderItemService(IAppUnitOfWork uow) : base(uow, uow.OrderItems, new OrderItemServiceMapper())
        {
        }

        public async Task ReducePaintingQuantityAsync(OrderItem orderItem)
        {
            var painting = await UOW.Paintings.FirstOrDefaultAsync(orderItem.PaintingId);
            painting.Quantity -= orderItem.Quantity;
            
            await UOW.Paintings.UpdateAsync(painting);
        }
        
    }
}