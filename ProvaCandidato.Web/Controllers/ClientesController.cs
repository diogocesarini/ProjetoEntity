using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using ProvaCandidato.Data;
using ProvaCandidato.Data.Entidade;
using ProvaCandidato.Data.Interface;
using ProvaCandidato.Models;

namespace ProvaCandidato.Controllers
{
    public class ClientesController : Controller
    {
        private ContextoPrincipal db = new ContextoPrincipal();
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly IMapper _mapper;
        public ClientesController(IClienteRepositorio clienteRepositorio, IMapper mapper)
        {
            _clienteRepositorio = clienteRepositorio;
            _mapper = mapper;
        }

        public ActionResult Index()
        {
            try
            {
                ViewBag.Empresas = ConfigurationManager.AppSettings["MensagemEmpresa"];
                var clienteEntidade = _clienteRepositorio.BuscarTodos().ToList();
                var clientes = _mapper.Map<List<ClienteModel>>(clienteEntidade);

                return View(clientes);
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
                ClienteModel cliente = _mapper.Map<ClienteModel>(_clienteRepositorio.BuscarPorId(id));
                if (cliente == null)
                {
                    return HttpNotFound();
                }
                return View(cliente);
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
                ClienteModel clienteModel = new ClienteModel();
                ViewBag.Empresas = ConfigurationManager.AppSettings["MensagemEmpresa"];
                ViewBag.CidadeId = new SelectList(db.Cidades, "Codigo", "Nome");
                return View(clienteModel);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClienteModel clienteModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _clienteRepositorio.AdicionarESalvar(_mapper.Map<Cliente>(clienteModel));
                    Helper.MessageHelper.DisplaySuccessMessage(this, "Adicionado com sucesso!");

                    return RedirectToAction("Index");
                }

                ViewBag.CidadeId = new SelectList(db.Cidades, "Codigo", "Nome", clienteModel.CidadeId);
                return View(clienteModel);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                if (id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ClienteModel cliente = _mapper.Map<ClienteModel>( _clienteRepositorio.BuscarPorId(id));
                if (cliente == null)
                {
                    return HttpNotFound();
                }
                ViewBag.CidadeId = new SelectList(db.Cidades, "Codigo", "Nome", cliente.CidadeId);
                ViewBag.Empresas = ConfigurationManager.AppSettings["MensagemEmpresa"];
                return View(cliente);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ClienteModel cliente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _clienteRepositorio.AdicionarOuAtualizarESalvar(_mapper.Map<Cliente>(cliente));
                    Helper.MessageHelper.DisplaySuccessMessage(this, "Editado com sucesso!");


                    return RedirectToAction("Index");
                }
                ViewBag.CidadeId = new SelectList(db.Cidades, "Codigo", "Nome", cliente.CidadeId);
                return View(cliente);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }

        public ActionResult Delete(int id)
        {
            try
            {
                if (id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ClienteModel cliente = _mapper.Map<ClienteModel> (_clienteRepositorio.BuscarPorId(id));

                if (cliente == null)
                {
                    return HttpNotFound();
                }
                return View(cliente);
            }
            catch (System.Exception)
            {
                throw;
            }

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Cliente cliente = _clienteRepositorio.BuscarPorId(id);
                _clienteRepositorio.Deletar(cliente);
                _clienteRepositorio.Salvar();

                Helper.MessageHelper.DisplaySuccessMessage(this, "Deletado com sucesso!");
                return RedirectToAction("Index");
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
