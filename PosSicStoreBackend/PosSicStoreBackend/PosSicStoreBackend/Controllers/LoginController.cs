using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http.Cors;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Inspinia_MVC5_SeedProject.Controllers
{
    [EnableCors("*", "*", "*")]
    public class LoginController : Controller
    {
        private JavaScriptSerializer _objSerializer = new JavaScriptSerializer();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult IniciarSesion(string Username, string Password)
        {
            try
            {
                Request_ModelStateUsuario Usuario = new Request_ModelStateUsuario()
                {
                    grant_type = "password",
                    userName = Username,
                    password = Password
                };

                var postData = "grant_type=password&userName=" + Usuario.userName + "&password=" + Usuario.password;

                var _DataContentLenght = System.Text.Encoding.ASCII.GetBytes(postData);

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(UtileriasController.UriApi + "CheckInAPI/Token");
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = _DataContentLenght.Length;

                using (var sr = request.GetRequestStream())
                {
                    sr.Write(_DataContentLenght, 0, _DataContentLenght.Length);
                }
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                Response_ModalStateUsuario Res = _objSerializer.Deserialize<Response_ModalStateUsuario>(responseString);
                if (Res == null)
                    return Json(0, JsonRequestBehavior.AllowGet);
                else
                {
                    IniciaVariablesSesion(Res);
                    return Json(_objSerializer.Serialize(Res), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                UtileriasController.EscribirLog(ex.ToString());
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }

        private void IniciaVariablesSesion(Response_ModalStateUsuario Usuario)
        {
            System.Web.HttpContext.Current.Session["objUsuario"] = _objSerializer.Serialize(Usuario);
        }

        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("Index", "Login");
        }

        #region PRIVATE
        private class Request_ModelStateUsuario
        {
            public string grant_type { get; set; }
            public string userName { get; set; }
            public string password { get; set; }
        }

        private class Response_ModalStateUsuario
        {
            public string access_token { get; set; }
            public string token_type { get; set; }
            public int expires_in { get; set; }
            public string userName { get; set; }
            public string issued { get; set; }
            public string expires { get; set; }
        }
        #endregion

    }
}
