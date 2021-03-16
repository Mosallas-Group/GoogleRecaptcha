using GoogleRecaptcha.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace GoogleRecaptcha.Models
{
   public class RecaptchaModelBase
   {
      [BindProperty(Name = "g-recaptcha-response")]
      [VerifyRecaptcha]
      public string Response { get; set; }
   }
}
