namespace Project.Application.Features.Categories.Commands.Update
{
    public class UpdateCategoryCommand
    {

        public string Name { get; set; } = default!;
        public string? Description { get; set; }
    }
}
