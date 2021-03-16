using System.Threading.Tasks;
using GoogleRecaptcha.Models;
using Microsoft.AspNetCore.Mvc;

namespace GoogleRecaptcha.Controllers
{
   public class RecaptchaController : Controller
   {
      private readonly RecaptchaSecrets _secrets;

      public RecaptchaController(RecaptchaSecrets secrets)
      {
         _secrets = secrets;
      }

      public IActionResult Index()
      {
         return View();
      }

      //public async Task<IActionResult> Verify(RecaptchaModelBase modelBase)
      //{
      //   var warningMessage = "Please Complete Recaptcha";

      //   if(string.IsNullOrEmpty(modelBase.Response)) {
      //      ModelState.AddModelError(string.Empty, warningMessage);
      //      return View("Index");
      //   }

      //   var httpRes = await new HttpClient()
      //      .GetAsync(
      //         $"https://www.google.com/recaptcha/api/siteverify?secret={_secrets.SecretKey}&response={modelBase.Response}");

      //   if (httpRes.StatusCode != HttpStatusCode.OK)
      //   {
      //      ModelState.AddModelError(string.Empty, warningMessage);
      //      return View("Index");
      //   }

      //   var jsonRes = await httpRes.Content.ReadAsStringAsync();
      //   dynamic jsonData = JObject.Parse(jsonRes);

      //   if (jsonData.success != "true")
      //   {
      //      ModelState.AddModelError(string.Empty, warningMessage);
      //      return Redirect(Url.Action("Index", "Home", new {message = "You are human :)"}));
      //   }

      //   return Redirect(Url.Action("Index", "Home", new {message = "You are human :)"}));
      //}

      public async Task<IActionResult> Verify(RecaptchaModelBase modelBase)
      {
         if(ModelState.IsValid) {
            return Redirect(Url.Action("Index", "Home", new { message = "You are human :)" }));
         }

         return View("Index");
      }
   }
}
