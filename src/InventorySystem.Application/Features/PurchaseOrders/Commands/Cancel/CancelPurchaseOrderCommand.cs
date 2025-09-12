using MediatR;

namespace Project.Application.Features.PurchaseOrders.Commands.Cancel
{
    public class CancelPurchaseOrderCommand : IRequest<string>
    {

        public int Id { get; set; }
        public string RowVersion { get; set; }

        public CancelPurchaseOrderCommand(int id, string rowVersion)
        {
            Id = id;
            RowVersion = rowVersion;
        }
    }
}
