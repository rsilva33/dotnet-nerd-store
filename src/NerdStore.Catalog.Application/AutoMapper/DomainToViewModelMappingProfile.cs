using NerdStore.Catalog.Application.ViewModels;
using NerdStore.Catalog.Domain;

namespace NerdStore.Catalog.Application.AutoMapper;

public class DomainToViewModelMappingProfile : Profile
{
    public DomainToViewModelMappingProfile()
    {
        CreateMap<Category, CategoryViewModel>();

        CreateMap<Product, ProductViewModel>()
            .ForMember(d => d.Width, o => o.MapFrom(s => s.Dimensions.Width))
            .ForMember(d => d.Height, o => o.MapFrom(s => s.Dimensions.Height))
            .ForMember(d => d.Depth, o => o.MapFrom(s => s.Dimensions.Depth));
    }
}

public class ViewModelToDomainMappingProfile : Profile
{
    public ViewModelToDomainMappingProfile()
    {
        CreateMap<ProductViewModel, Product>()
            .ConstructUsing(p => 
            new Product(p.Name, p.Description, p.Active, p.Value, p.Image, p.Created_At, p.CategoryId, 
                new Dimensions(p.Height, p.Width, p.Depth)));

        CreateMap<CategoryViewModel, Category>()
            .ConstructUsing(c => new Category(c.Name, c.Code));
    }
}
