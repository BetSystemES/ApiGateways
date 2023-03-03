namespace WebApiGateway.Configuration
{
    public static class SwaggerConfiguration
    {
        public static WebApplication SwaggerConfig(this WebApplication app)
        {
            var isEnableSwaggerString = app.Configuration["AppSettings:EnableSwagger"];

            if (bool.TryParse(isEnableSwaggerString, out bool isEnableSwagger))
            {
                if (isEnableSwagger)
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }
            }

            return app;
        }
    }
}