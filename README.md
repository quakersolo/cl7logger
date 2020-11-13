# Setting up

## Configure services
```
public void ConfigureServices(IServiceCollection services)
{
    ...

    //Services used are scoped in order to allow you to change it at runtime if needed
    services.AddCL7Logger(options =>
    {
        options.LogginInfo.ApplicationName = Configuration["YourApplicationName"];
        options.ConnectionString = Configuration["ConnectionString"];
    });
    
    ...
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
