using CMS.Models;
using Microsoft.EntityFrameworkCore;
using CMS.Data;
using CMS.Repositories;
using CMS.Services;
using Azure.Storage.Blobs;
using CMS.Factories;
using CloudinaryDotNet;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<CmsDbContext>(options =>
                    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton(new BlobServiceClient(builder.Configuration.GetValue<string>("AzureBlobStorage:ConnectionString")));

var cloudinary = new Cloudinary(new Account(
    builder.Configuration["Cloudinary:CloudName"],
    builder.Configuration["Cloudinary:ApiKey"],
    builder.Configuration["Cloudinary:ApiSecret"]
));
builder.Services.AddSingleton(cloudinary);

builder.Services.AddSingleton<AzureBlobService>();

builder.Services.AddScoped<IContentItemRepository, ContentItemRepository>();
builder.Services.AddScoped<IContentItemService, ContentItemService>();

builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();
builder.Services.AddScoped<IPlayerService, PlayerService>();

// builder.Services.AddScoped<IPlaylistRepository, PlaylistRepository>();
// builder.Services.AddScoped<IPlaylistService, PlaylistService>();

builder.Services.AddScoped<ILabelRepository, LabelRepository>();
builder.Services.AddScoped<ILabelService, LabelService>();

builder.Services.AddScoped<AzureBlobStorageService>();
builder.Services.AddScoped<CloudinaryStorageService>();
builder.Services.AddScoped<IStorageServiceFactory, StorageServiceFactory>();

builder.Services.AddHostedService<ScheduleBackgroundService>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
