using InventorySystem.Application.Common.Dtos;
using InventorySystem.Application.Features.Categories.Dtos;

namespace InventorySystem.Application.Features.Products.Dtos
{


    public class ProductCreationDataDto
    {
        public List<EnumDto> UnitTypes { get; set; } = new();
        public List<CategoryDto> Categories { get; set; } = new();
    }




}
