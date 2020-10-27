# Setting up

## Configure services
```
public void ConfigureServices(IServiceCollection services)
{
    services.AddControllers();

    services.AddTransient<WeatherForecast>();

    //Services used are scoped in order to allow you to change it at runtime if needed
    services.AddCL7Logger(options =>
    {
        options.ApplicationName = Configuration["YourApplicationName"];
        options.ConnectionString = Configuration["ConnectionString"];
    });
}
```

## Configure
```
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    ...
    app.UseCL7Logger();
    ...
}
```
