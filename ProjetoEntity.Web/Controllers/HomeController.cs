﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProvaCandidato.Controllers
{
  public class HomeController : Controller
  {
    public ActionResult Index()
    {

      ViewBag.Empresas = ConfigurationManager.AppSettings["MensagemEmpresa"];


      return View();
    }
  }
}