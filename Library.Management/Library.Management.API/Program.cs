using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//Log
builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .Enrich.FromLogContext()
        .Enrich.WithProperty("Application", "Library.Management.API")
        .WriteTo.Console()
        .WriteTo.Seq("http://localhost:5341");
});

//ConnectionString

builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));


//Services
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IBorrowService, BorrowService>();
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<IMemberFactory, MemberFactory>();

builder.Services.AddScoped<ILibraryDbContext>(sp =>
    sp.GetRequiredService<LibraryDbContext>());

//Exception
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

//AddRateLimiter
builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    options.AddPolicy("BorrowPolicy", HttpContext =>
    {
        var memberId = HttpContext.User?.Identity?.Name
               ?? HttpContext.Connection.RemoteIpAddress?.ToString()
               ?? "anonymous";

        return RateLimitPartition.GetFixedWindowLimiter(
    partitionKey: memberId,
    factory: _ => new FixedWindowRateLimiterOptions
    {
        PermitLimit = 3,          // max 3 borrow requests
        Window = TimeSpan.FromMinutes(1),
        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
        QueueLimit = 0
    });
    });
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
}); 

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Library Management API",
        Version = "v1",
        Description = "REST API for managing books, members, and borrowing operations",

    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Library Management API v1");
    });
}

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseRateLimiter();


app.UseAuthorization();

app.MapControllers();

app.Run();
