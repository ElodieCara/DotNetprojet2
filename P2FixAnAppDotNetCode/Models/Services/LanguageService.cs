using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using System;
using System.Globalization;

namespace P2FixAnAppDotNetCode.Models.Services
{
    /// <summary>
    /// Provides services method to manage the application language
    /// </summary>
    public class LanguageService : ILanguageService
    {
        /// <summary>
        /// Set the UI language
        /// </summary>
        public void ChangeUiLanguage(HttpContext context, string language)
        {
            string culture = SetCulture(language);

            // Appliquer la culture à la requête courante
            var cultureInfo = new CultureInfo(culture);
            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.CurrentUICulture = cultureInfo;

            UpdateCultureCookie(context, culture);
        }

        /// <summary>
        /// Set the culture
        /// </summary>
        public string SetCulture(string language)
        {
            string culture = "";
            // TODO complete the code 
            // Default language is "en", french is "fr" and spanish is "es".
            switch (language)
            {
                case "fr":
                    culture = "fr-FR"; // French culture
                    break;
                case "es":
                    culture = "es-ES"; // Spanish culture
                    break;
                default:
                    culture = "en-US"; // Default to English culture
                    break;
            }
            return culture;
        }

        /// <summary>
        /// Update the culture cookie
        /// </summary>
        public void UpdateCultureCookie(HttpContext context, string culture)
        {
            var cookieOptions = new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }; // Créer les options de cookie avec une date d'expiration
            context.Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                cookieOptions); // Passer les options de cookie
        }

    }
}
