using Autofac;
using Autofac.Integration.WebApi;
using AutoMapper;
using ShoeShopAPI.Mappings;
using ShoeShopAPI.Models;
using ShoeShopAPI.Repositories;
using ShoeShopAPI.Services.AccountService;
using ShoeShopAPI.Services.BlogService;
using ShoeShopAPI.Services.BrandService;
using ShoeShopAPI.Services.CategoryService;
using ShoeShopAPI.Services.OrderInfoService;
using ShoeShopAPI.Services.OrderService;
using ShoeShopAPI.Services.ProductAttributeService;
using ShoeShopAPI.Services.ProductService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ShoeShopAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            var container = ContainerConfig.Configure();
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            AutoMapperConfig.Initialize();
        }
    }

    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // INJECT REPOSITORY
            builder.RegisterType(typeof(BaseRepository<Account>)).As(typeof(IRepository<Account>));
            builder.RegisterType(typeof(BaseRepository<Category>)).As(typeof(IRepository<Category>));
            builder.RegisterType(typeof(BaseRepository<Brand>)).As(typeof(IRepository<Brand>));
            builder.RegisterType(typeof(BaseRepository<Blog>)).As(typeof(IRepository<Blog>));
            builder.RegisterType(typeof(BaseRepository<Product>)).As(typeof(IRepository<Product>));
            builder.RegisterType(typeof(BaseRepository<ProductAttribute>)).As(typeof(IRepository<ProductAttribute>));
            builder.RegisterType(typeof(BaseRepository<ProductDetail>)).As(typeof(IRepository<ProductDetail>));
            builder.RegisterType(typeof(BaseRepository<ProductImage>)).As(typeof(IRepository<ProductImage>));
            builder.RegisterType(typeof(BaseRepository<Order>)).As(typeof(IRepository<Order>));

            // INJECT SERVICE
            builder.RegisterType(typeof(AccountService)).As(typeof(IAccountService));
            builder.RegisterType(typeof(CategoryService)).As(typeof(ICategoryService));
            builder.RegisterType(typeof(BrandService)).As(typeof(IBrandService));
            builder.RegisterType(typeof(BlogService)).As(typeof(IBlogService));
            builder.RegisterType(typeof(ProductService)).As(typeof(IProductService));
            builder.RegisterType(typeof(ProductAttributeService)).As(typeof(IProductAttributeService));
            builder.RegisterType(typeof(OrderService)).As(typeof(IOrderService));
            builder.RegisterType(typeof(OrderInfoService)).As(typeof(IOrderInfoService));

            builder.RegisterType<LinqDataContext>().InstancePerRequest();

            builder.RegisterInstance<IMapper>(AutoMapperFactory.CreateMapper());
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                   .Where(t => t.Name.EndsWith("Repository"))
                   .AsImplementedInterfaces();
            return builder.Build();
        }
    }

    public class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<ToMapping>();
            });
        }
    }

    public static class AutoMapperFactory
    {
        public static IMapper CreateMapper()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ToMapping>(); // Your AutoMapper mapping profile
            });

            return configuration.CreateMapper();
        }
    }
}
