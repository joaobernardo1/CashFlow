﻿using System.Globalization;

namespace CashFlow.API.Middleware
{
    public class CultureMiddleware
    {
        private readonly RequestDelegate _next;

        public async Task Invoke(HttpContext context)
        {
            var supportedLanguages = CultureInfo.GetCultures(CultureTypes.AllCultures).ToList();
            var requestedCulture = context.Request.Headers.AcceptLanguage.FirstOrDefault();

            var cultureInfo = new CultureInfo("pt-BR");

            if (string.IsNullOrWhiteSpace(requestedCulture) == false
                && supportedLanguages.Exists(language => language.Name.Equals(requestedCulture)))
            {
                cultureInfo = new CultureInfo(requestedCulture);
            }

            CultureInfo.CurrentUICulture = cultureInfo;
            CultureInfo.CurrentCulture = cultureInfo;

            await _next(context); // Ensure the middleware pipeline continues and the Task is awaited
        }

        public CultureMiddleware(RequestDelegate next)
        {
            _next = next;
        }
    }
}

