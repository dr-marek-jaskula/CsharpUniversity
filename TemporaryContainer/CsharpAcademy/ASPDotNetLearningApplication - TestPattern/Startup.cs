using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: ApiController]
namespace ASPDotNetLearningApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Autentykacja

            //tworzymy obiekt ktory posluzy do autentykacji
            AuthenticationSettings authenticationSettings = new();
            //odwo�ujemy sie do sekcji Authentication w pliku appsettings.json i bindujemy to utworzonej zmiennej
            Configuration.GetSection("Authentication").Bind(authenticationSettings);
            //dodajemy singleton aby m�c wstrzykn�� do AccountService informacje z appsettings.json o kluczu itp
            services.AddSingleton(authenticationSettings);

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = "Bearer";
                option.DefaultScheme = "Bearer";
                option.DefaultChallengeScheme = "Bearer";

            }).AddJwtBearer(cfg => 
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = authenticationSettings.JwtIssuer,
                    ValidAudience = authenticationSettings.JwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),
                };
            });

            //teraz autoryzacja customowa na podstawie customowego claima (tj u nas narodowosci)
            //dodajemy opcje polityki. Nazwa polityki to "HasNationality", a dzia�a tak, �e claim "Nationality" jest wymagany (tylko tyle), po do�o�eniu po przecinku warto�� sprawdza czy warto�ci s� takie jak jedna z wymienionych
            services.AddAuthorization(option =>
            {
                option.AddPolicy("HasNationality", builder => builder.RequireClaim("Nationality", "German", "Poland", "Home"));
                option.AddPolicy("Atleast20", builder => builder.AddRequirements(new MinimumAgeRequirement(20)));
                //rejestrujemy polityke ze stworzyl przynajmniej dwie restauracje
                option.AddPolicy("CreatedAtLeast2Restaurants", builder => builder.AddRequirements(new CreatedMultipleRestaurantRequirement(2)));
            });

            //rejestrujemy handler do naszego requirementa
            services.AddScoped<IAuthorizationHandler, MinimumAgeRequirementHandler>();

            //rejestrujemy handler zwiazany z autoryzacj� po zasobach
            services.AddScoped<IAuthorizationHandler, ResourceOperationRequirementHandler>(); //tutaj nie b�dziemy dawali konkretnej polityki bo nasze wymaganie jest bardziej dynamiczne. Zatem chcemy wywo�a� ten hendler przy odniesieniu sie do odpowiedniego zasobu restauracji

            services.AddScoped<IAuthorizationHandler, CreatedMultipleRestaurantRequirementHandler>();
            #endregion

            //Fluent validation (customowa i lepsza), na podtawie paczki FluentValidators
            services.AddControllers().AddFluentValidation();

            //versionowanie (aby bylo par� versji)
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true; // to robi, �e defaultowa wersja b�dzie ok
                options.DefaultApiVersion = ApiVersion.Default; //to robi, �e defaultowa wersja jest 1.0
                //options.DefaultApiVersion = new ApiVersion(1, 1); //to robi, �e defaultowa wersja jest 1.1
                //options.ApiVersionReader = new MediaTypeApiVersionReader("version"); //to sprawie, �e zamiast jako parametr, b�dzie w headerze "Accept" i trzeba napis�c "application/json; version=2.0"
                options.ApiVersionReader = new HeaderApiVersionReader("CustomHeaderVersion"); // to tworzy ze nie b�dzie w headerze Accept, tylko w "CustomHeaderVersion". Wtedy nie trzeba pisa� "version=2.0" ale po porstu "2.0"

                /*mo�na te� oba naraz zrobi� 
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new MediaTypeApiVersionReader("version"),
                    new HeaderApiVersionReader("CustomHeaderVersion")
                    ); */

                options.ReportApiVersions = true; //to robi, �e daje odpowiedz, gdzie w headerze info o supportowanych versjach
            });

            //rejestrowanie kontekstu bazy danych (do entity framework), konfiguracja jest tutaj: connection string z appsettings
            services.AddDbContext<RestaurantDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("RestaurantDbConnection")));
            //rejestrowanie RestaurantSeeder (do entity framework, ale moze tutaj do dappera te�)
            services.AddScoped<RestaurantSeeder>();

            //dodajemy do serwisu AutoMaper (z namespace AutoMapper), gdzie trzeba doda� Assembly, czyli �r�d�o projektu w kt�rym AutoMapper przeszuka wszystkie typy i znajdzie profile
            services.AddAutoMapper(this.GetType().Assembly); // tak pozaujemy, �e to ten Assembly

            //dodaje MediatoRa, to automatycznie przeskanuje po hanldery i zarejestruje je
            services.AddMediatR(typeof(Startup));

            services.AddScoped<IRestaurantService, RestaurantService>(); //rejesterujemy service z interface'em aby przerzucic logik� z kontrolera do serviceu
            services.AddScoped<IAccountService, AccountService>(); 

            //testy
            services.AddScoped<IRestaurantRepository, RestaurantRepository>(); //rejesterujemy pod MediatoR

            //rejestracja middleware
            services.AddScoped<ErrorHandlingMiddleware>();
            services.AddScoped<RequestTimeMiddleware>();

            //dodajemy hashowanie hase�
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

            //rejestracje walidatora, ale trzeba te� doda�rejestracj� FluentValidations.
            //aby to zrobi� nale�y na pocz�tku po AddController() doda� metode AddFluentValidation().
            services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();
            services.AddScoped<IValidator<RestaurantQuery>, RestaurantQueryValidator>();

            //dodajemy servis zwi�zany z wstrzykiwaniem info o kontekscie usera
            services.AddScoped<IUserContextService, UserContextService>();
            services.AddHttpContextAccessor();

            //dodanie swaggera
            services.AddSwaggerGen();

            //dodanie polityki Corse, aby spe�ni� polityk� przegl�darkow� "same origin policy" 
            //od razu j�konfigurujemy
            services.AddCors(option =>
            {
                //pierwsza to nazwa polityki, drugie to builder gdzie ustala sie wymagania
                option.AddPolicy("FrontEndClient", builder => 
                {
                    //dopuszczamy wszystkie metody (tj zapytania GET, PUT, DELETE itp. mozna zaw�zi� jak si� chce
                    //potem dopuszcamy ka�dy header
                    //mo�naby dopu�ci� wszystkie aplikacje, ale lepiej specyfikowa� aby dopu�cic tylko ten na konkretnym ho�cie, czyli u nas localhoscie
                    builder.AllowAnyMethod().AllowAnyHeader().WithOrigins(Configuration["AllowedOrigins"]); 
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // Wstrzykni�cie seeder, aby przy ka�dym zapytaniu (i przy ju� pierwszym) uzupe�ni� baz� danych - chyba przesada)
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, RestaurantSeeder seeder)
        {
            //wazne zeby to by�o tutaj. Chaschowanie odpowiedzi, aby zwiekszyc wydajnosc
            app.UseResponseCaching();

            //wazne zeby by�o tutaj. Wazne zeby sciezka by�a r�na od sciezki api
            app.UseStaticFiles();
            app.UseCors("FrontEndClient");

            //kazda metoda przep�ywu wywo�ywana na aplication builderze jest nazwana middlewere (czyli IApplicationbuilder)
            //midlleware to warsty ktore sa po kolei odpalane przed tym jak request pojdzie dalej

            //uzupe�nianie danych 
            seeder.Seed();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //tutaj trzeba middleware dodawa�, nie dalej
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseMiddleware<RequestTimeMiddleware>();

            app.UseAuthentication();

            app.UseHttpsRedirection(); //robi to to, �e jesli uzytkownik wysle zapytanie bez protoko�u https to od razu przekieruje go na protoko� https

            if (env.IsDevelopment())
            {
                //w tym miejscu jest to konieczne zarejestrwoanie swaggera. Generuje plik swagger.json
                app.UseSwagger();
                //teraz ustawiamy endpoint swaggera, czyli gdzie robi dokumentacje i jak sie ona nazywa
                app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Restaurant API"); });
            }
            app.UseRouting(); // to i te useEndpoints mapuej zapytanie do kontroler�w poprzez atrybut [Route]

            app.UseAuthorization(); //to musi by� pomi�dzy Rounting a Endpoints. Rounting przypisuje do akcji, a Endpoints wykonuje.
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
