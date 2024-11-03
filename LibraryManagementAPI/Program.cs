using Repository;
using Repository.interfaces;
using DataAccess.DAOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Author
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<AuthorDAO, AuthorDAO>();

// Book
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<BookDAO, BookDAO>();

//BookAccessForMemberGroup
builder.Services.AddScoped<IBookAccessForMemberGroupRepository, BookAccessForMemberGroupRepository>();
builder.Services.AddScoped<BookAccessForMemberGroupDAO, BookAccessForMemberGroupDAO>();

// Bookshelf
builder.Services.AddScoped<IBookshelfRepository, BookshelfRepository>();
builder.Services.AddScoped<BookshelfDAO, BookshelfDAO>();

// BookGroup
builder.Services.AddScoped<IBookGroupRepository, BookGroupRepository>();
builder.Services.AddScoped<BookGroupDAO, BookGroupDAO>();

// BookInGroup
builder.Services.AddScoped<IBookInGroupRepository, BookInGroupRepository>();
builder.Services.AddScoped<BookInGroupDAO, BookInGroupDAO>();

// Category
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<CategoryDAO, CategoryDAO>();

// Favorite
builder.Services.AddScoped<FavoritesListDAO, FavoritesListDAO>();

// LiquidatedBook
builder.Services.AddScoped<ILiquidatedBookRepository, LiquidatedBookRepository>();
builder.Services.AddScoped<LiquidatedBookDAO, LiquidatedBookDAO>();

// Member
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<MemberDAO, MemberDAO>();

//MemberGroup
builder.Services.AddScoped<IMemberGroupRepository, MemberGroupRepository>();
builder.Services.AddScoped<MemberGroupDAO, MemberGroupDAO>();

// Fees
builder.Services.AddScoped<IFeeRepository, FeeRepository>();
builder.Services.AddScoped<FeeDAO, FeeDAO>();

// Publisher
builder.Services.AddScoped<IPublisherRepository, PublisherRepository>();
builder.Services.AddScoped<PublisherDAO, PublisherDAO>();

// Supplier
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<SupplierDAO, SupplierDAO>();

//Staff
builder.Services.AddScoped<IStaffRepository, StaffRepository>();
builder.Services.AddScoped<StaffDAO, StaffDAO>();

//Jwt Token
builder.Services.AddSingleton<JwtTokenService>();

//IHttpContextAccessor
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
