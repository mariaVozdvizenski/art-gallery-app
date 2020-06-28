using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using ee.itcollege.mavozd.BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Guid = System.Guid;
using Order = DAL.App.DTO.Order;

namespace BLL.App.Services
{
    public class OrderService : BaseEntityService<IAppUnitOfWork, IOrderRepository, IOrderServiceMapper, Order, DTO.Order>,
        IOrderService
    {
        public OrderService(IAppUnitOfWork uow) : base(uow, uow.Orders, new OrderServiceMapper())
        {
            
        }
        
        public IEnumerable<DTO.Order> ApplyAllFilters(IEnumerable<DTO.Order> query, string? condition,
            string? statusCodes)
        {
            if (statusCodes != null)
            {
                query =  FilterByStatusCodes(query, statusCodes);
            }

            if (condition != null)
            {
                query =  FilterByDate(query, condition);
            }

            return query;
        }
        
        private static IEnumerable<DTO.Order> FilterByDate(IEnumerable<DTO.Order> query, string condition)
        {
            query = condition switch
            {
                "descending" when condition != null => query.OrderByDescending(e => e.OrderDate),
                "ascending" when condition != null => query.OrderBy(e => e.OrderDate),
                _ => query
            };
            return query;
        }

        private static IEnumerable<DTO.Order> FilterByStatusCodes(IEnumerable<DTO.Order> query,
            string statusCodes)
        {
            var list = statusCodes.Split('_');
            return query.Where(o => list.Contains(o.OrderStatusCode!.Code));
        }

        public async Task PaintingQuantityOnCodeChange(DTO.Order oldOrder, Guid newOrderStatusCodeId, DTO.Order newOrder)
        {
            var cancelledId = Guid.Parse("00000000-0000-0000-0000-000000000004");
            
            var orderStatusCodes = await UOW.OrderStatusCodes.GetAllAsync();
            orderStatusCodes = orderStatusCodes.Where(e => e.Code != "Cancelled");
            
            if (oldOrder.OrderStatusCodeId == cancelledId)
            {
                foreach (OrderItem orderItem in oldOrder.OrderItems!)
                {
                    orderItem.Painting!.Quantity -= orderItem.Quantity;
                }
                
            } else if (orderStatusCodes.Any(e => e.Id == oldOrder.OrderStatusCodeId) 
                       && newOrderStatusCodeId == cancelledId) {
                foreach (OrderItem orderItem in oldOrder.OrderItems!)
                {
                    orderItem.Painting!.Quantity += orderItem.Quantity;
                }
            }
            newOrder.OrderItems = oldOrder.OrderItems;
        }
    }
}