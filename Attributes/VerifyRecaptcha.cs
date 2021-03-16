using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using GoogleRecaptcha.Models;
using Newtonsoft.Json.Linq;

namespace GoogleRecaptcha.Attributes
{
   public class VerifyRecaptcha : ValidationAttribute
   {
      protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
      {
         var warningMessage = new ValidationResult("Please Complete Recaptcha");

         if(value == null) {
            return warningMessage;
         }

         var secrets = (RecaptchaSecrets)validationContext.GetService(typeof(RecaptchaSecrets));

         if(secrets == null) {
            return new ValidationResult("Secrets can not be null");
         }

         var httpRes = new HttpClient()
            .GetAsync(
               $"https://www.google.com/recaptcha/api/siteverify?secret={secrets.SecretKey}&response={value}").Result;

         if(httpRes.StatusCode != HttpStatusCode.OK) {
            return warningMessage;
         }

         var jsonRes = httpRes.Content.ReadAsStringAsync().Result;
         dynamic jsonData = JObject.Parse(jsonRes);

         if(jsonData.success != "true") {
            return warningMessage;
         }

         return ValidationResult.Success;
      }
   }
}
