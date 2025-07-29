using Domain.Entities;
using Domain.Specifications;

namespace Project.Application.Features.PurchaseOrders.Specifications
{
    public class PurchaseOrderWithItemsSpecifications : BaseSpecifications<PurchaseOrder>
    {



        public PurchaseOrderWithItemsSpecifications(int id) : base(p => p.Id == id)
        {

            AddInclude(p => p.Items);

        }

    }
}
