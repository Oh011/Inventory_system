using Project.Application.Features.Categories.Dtos;

namespace Project.Application.Features.Products.Dtos
{


    public class ProductCreationDataDto
    {
        public List<EnumDto> UnitTypes { get; set; } = new();
        public List<CategoryDto> Categories { get; set; } = new();
    }

    public class EnumDto
    {
        public string Name { get; set; } = default!;
        public int Value { get; set; }
    }


}
