using System.Web.Mvc;

namespace ProvaCandidato.Helper
{
    public static class MessageHelper
    {
        public static void DisplaySuccessMessage(Controller controller, string message)
        {
            var userMessage = new { CssClassName = "", Title = "Sucesso", Message = message };
            controller.TempData["UserMessage"] = message;
        }
    }
}