var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

// ��� ������� � HTML, CSS � ����� wwwroot
app.UseStaticFiles();

// ���������� ������ � ��������� /Index /Privacy 
app.UseRouting();

// ��� �����������
app.UseAuthorization();

// ���������� ������������ ��� Razor Pages
app.MapRazorPages();

app.Run();
