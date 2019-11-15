using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Learning.Data;
using Learning.Filters;
using Learning.IRepository;
using Learning.IRepository.Base;
using Learning.IService;
using Learning.IService.Base;
using Learning.Provide;
using Learning.Repository;
using Learning.Repository.Base;
using Learning.Service;
using Learning.Service.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;

namespace Learning
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            JwtHelper._configs = Configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            #region Core自带DI

            //services.AddScoped<IUserServices, UserServices>();
            // services.AddScoped<IBaseServices, BaseServices>();
            //services.AddScoped<IBaseRepository, BaseRepository>();
            //services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            #endregion Core自带DI

            //添加swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "NetCoreAPI", Version = "v1" });
            });
            //添加jwt验证
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,//是否验证Issuer
                        ValidateAudience = true,//是否验证Audience
                        ValidateLifetime = true,//是否验证失效时间
                                                // ClockSkew = TimeSpan.FromSeconds(30),

                        ValidateIssuerSigningKey = true,//是否验证SecurityKey
                        ValidAudience = "jwttest",//Audience
                        ValidIssuer = "jwttest",//Issuer，这两项和前面签发jwt的设置一致
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtOptions:SecurityKey"]))//拿到SecurityKey
                    };
                });
            //配置跨域处理
            services.AddCors(options =>
            {
                options.AddPolicy("any", bui =>
                {
                    bui.AllowAnyOrigin() //允许任何来源的主机访问
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();//指定处理cookie
                });
            });
            //添加EFCore
            services.AddDbContext<EFCoreContext>(option => option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc(o =>
            {
                //添加过滤器
                o.Filters.Add<CustomExceptionFilter>();
            }).AddJsonOptions(op =>
            {
                op.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                op.SerializerSettings.MissingMemberHandling = Newtonsoft.Json.MissingMemberHandling.Ignore;
                op.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            #region AutoFac DI

            //实例化 AutoFac  容器
            var builder = new ContainerBuilder();
            var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;

            #region Service.dll 注入，有对应接口

            //获取项目绝对路径，请注意，这个是实现类的dll文件，不是接口 IService.dll ，注入容器当然是Activatore
            var servicesDllFile = Path.Combine(basePath, "Learning.Service.dll");
            var assemblysServices = Assembly.LoadFrom(servicesDllFile);//直接采用加载文件的方法  ※※★※※ 如果你是第一次下载项目，请先F6编译，然后再F5执行，※※★※※
            builder.RegisterAssemblyTypes(assemblysServices).AsImplementedInterfaces();

            #endregion Service.dll 注入，有对应接口

            #region Repository.dll 注入，有对应接口

            var repositoryDllFile = Path.Combine(basePath, "Learning.Repository.dll");
            var assemblysRepository = Assembly.LoadFrom(repositoryDllFile);
            builder.RegisterAssemblyTypes(assemblysRepository).AsImplementedInterfaces();

            #endregion Repository.dll 注入，有对应接口

            //将services填充到Autofac容器生成器中
            builder.Populate(services);
            //使用已进行的组件登记创建新容器
            var ApplicationContainer = builder.Build();

            #endregion AutoFac DI

            return new AutofacServiceProvider(ApplicationContainer);//第三方IOC接管 core内置DI容器
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseCors("any");
            //配置Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                //根据版本名称倒序 遍历展示
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "DemoAPI V1");
                // 将swagger首页，设置成我们自定义的页面，记得这个字符串的写法：解决方案名.index.html
                //c.IndexStream = () => GetType().GetTypeInfo().Assembly.GetManifestResourceStream("Learning.index.html");//这里是配合MiniProfiler进行性能监控的，《文章：完美基于AOP的接口性能分析》，如果你不需要，可以暂时先注释掉，不影响大局。
                c.RoutePrefix = ""; //路径配置，设置为空，表示直接在根域名（localhost:8001）访问该文件,注意localhost:8001/swagger是访问不到的，去launchSettings.json把launchUrl去掉
            });
            app.UseAuthentication();//注意添加这一句，启用验证
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}