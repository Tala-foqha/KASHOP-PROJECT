using KASHOP.DAL.Dto.Request;
using KASHOP.DAL.Dto.Response;
using KASHOP.DAL.Models;
using Mapster;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Mapping
{
    public static class MapsterConfig
    {
        public static void MapsterConfigRegister()
        {
            TypeAdapterConfig<Category, CategoryResponse>.NewConfig()
    .Map(dest => dest.Category_Id, src => src.Id)
    .Map(dest => dest.UserCreated, src => src.CreateBy)
    .Map(dest => dest.Name,
         src => src.translations != null
             ? src.translations
                 .Where(t => t.Language == MapContext.Current.Parameters["lang"].ToString())
                 .Select(t => t.Name)
                 .FirstOrDefault()
             : null);
            TypeAdapterConfig<Product, ProductResponse>.NewConfig()
    .Map(dest => dest.UserCreated, source => source.CreateBy.UserName)
    .Map(dest => dest.Name, source =>
        source.Translations
            .Where(t => t.Language == CultureInfo.CurrentCulture.Name)
            .Select(t => t.Name)
            .FirstOrDefault()

    ).Map(dest => dest.MainImage, source => $"https://localhost:7075/images/${source.MainImage}");

            TypeAdapterConfig<Brand, BrandResponse>.NewConfig()
           .Map(dest => dest.UserCreated, source => source.CreateBy.UserName)
           .Map(dest => dest.Name, source =>
               source.Translations
                   .Where(t => t.Language == CultureInfo.CurrentCulture.Name)
                   .Select(t => t.Name)
                   .FirstOrDefault()

           ).Map(dest => dest.Logo, source => $"https://localhost:7075/images/${source.Logo}");
            TypeAdapterConfig<ProductUpdateRequest, Product>.NewConfig().IgnoreNullValues(true);
            //عشان بس نعمل ابديت لقيمة وحدة والباقي لا




        }
    }
}
