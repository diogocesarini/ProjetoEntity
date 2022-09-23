using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using ProvaCandidato.Data;
using ProvaCandidato.Data.Entidade;
using ProvaCandidato.Data.Interface;
using ProvaCandidato.Helper;
using ProvaCandidato.Models;

namespace ProvaCandidato.Controllers
{
    public class CidadesController : Controller
    {
        private ContextoPrincipal db = new ContextoPrincipal();
        private readonly ICidadeRepositorio _cidadeRepositorio;
        private readonly IMapper _mapper;

        public CidadesController(ICidadeRepositorio cidadeRepositorio, IMapper mapper)
        {
            _cidadeRepositorio = cidadeRepositorio;
            _mapper = mapper;
        }

        public ActionResult Index()
        {
            try
            {
                ViewBag.Empresas = ConfigurationManager.AppSettings["MensagemEmpresa"];
                var cidades = _mapper.Map<List<CidadeModel>>(_cidadeRepositorio.BuscarTodos());
                return View(cidades);
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public ActionResult Details(int id)
        {
            try
            {
                if (id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                CidadeModel cidade = _mapper.Map<CidadeModel>(_cidadeRepositorio.BuscarPorId(id));
                if (cidade == null)
                {
                    return HttpNotFound();
                }
                return View(cidade);
            }
            catch (System.Exception)
            {
                throw;
            }

        }

        public ActionResult Create()
        {
            try
            {
                ViewBag.Empresas = ConfigurationManager.AppSettings["MensagemEmpresa"];
                CidadeModel cidadeModel = new CidadeModel();

                return View(cidadeModel);
            }
            catch (System.Exception)
            {

                throw;
            }
          
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CidadeModel cidade)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _cidadeRepositorio.AdicionarOuAtualizarESalvar(_mapper.Map<Cidade>(cidade));
                    Helper.MessageHelper.DisplaySuccessMessage(this, "Criado com sucesso!");

                    return RedirectToAction("Index");
                }

                return View(cidade);
            }
            catch (System.Exception)
            {

                throw;
            }
            
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Empresas = ConfigurationManager.AppSettings["MensagemEmpresa"];

            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CidadeModel cidade = _mapper.Map<CidadeModel>(_cidadeRepositorio.BuscarPorId(id));
            if (cidade == null)
            {
                return HttpNotFound();
            }
            return View(cidade);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CidadeModel cidade)
        {
            if (ModelState.IsValid)
            {
                _cidadeRepositorio.AdicionarOuAtualizarESalvar(_mapper.Map<Cidade>(cidade));
                Helper.MessageHelper.DisplaySuccessMessage(this, "Editado com sucesso!");
                return RedirectToAction("Index");
            }
            return View(cidade);
        }

        public ActionResult Delete(int id)
        {
            ViewBag.Empresas = ConfigurationManager.AppSettings["MensagemEmpresa"];

            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CidadeModel cidade = _mapper.Map<CidadeModel>(_cidadeRepositorio.BuscarPorId(id));
            if (cidade == null)
            {
                return HttpNotFound();
            }
            return View(cidade);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cidade cidade = _cidadeRepositorio.BuscarPorId(id);
            _cidadeRepositorio.Deletar(cidade);
            _cidadeRepositorio.Salvar();

            Helper.MessageHelper.DisplaySuccessMessage(this, "Deletado com sucesso!");
            
            return RedirectToAction("Index");
        }
       
    }
}
