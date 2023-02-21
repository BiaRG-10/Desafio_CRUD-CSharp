using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Desafio_CRUD_CSharp.Models;
using PagedList;

namespace Desafio_CRUD_CSharp.Controllers
{
    public class UsuariosController : Controller
    {
        private CadastroContatoDBEntities db = new CadastroContatoDBEntities();

        public object Nome { get; private set; }

        // original substituído

        // GET: Usuarios
        //public ActionResult Index() 
        //{
        //    return View(db.Usuarios.ToList()); // converte em comando sql select
        //}

        // novo classificação
        public ViewResult Index(string Ordenacao, string searchString, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = Ordenacao;
            ViewBag.NomeOrdenacaoParm = string.IsNullOrEmpty(Ordenacao) ? "Nome desc" : "";
            ViewBag.EmpresaOrdenacaoParm = Ordenacao == "Empresa" ? "Empresa desc" : "Empresa";
            ViewBag.EmailOrdenacaoParm = Ordenacao == "Email desc" ? "Email desc" : "Email";

            //ViewBag.TelefonePessoalOrdenacaoParm = Ordenacao == "Telefone Pessoal desc" ? "Telefone Pessoal desc" : "Telefone Pessoal";
            //ViewBag.TelefoneComercialOrdenacaoParm = Ordenacao == "Telefone Comercial desc" ? "Telefone Comercial desc" : "Telefone Comercial";

            if (searchString != null)
            {
                page = 1; 
            } else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var contatos = from est in db.Usuarios
                             select est;

            if (!String.IsNullOrEmpty(searchString))
            {
                contatos = contatos.Where(est => est.Nome.ToUpper().Contains(searchString.ToUpper()) || est.Empresa.ToLower().Contains(searchString.ToUpper())
                   || est.Email.ToLower().Contains(searchString.ToUpper()) || est.TelefonePessoal.ToString().Contains(searchString) 
                   || est.TelefoneComercial.ToString().Contains(searchString)); 
            }

            switch (Ordenacao)
            {
                case "Nome desc":
                    contatos = contatos.OrderByDescending(est => est.Nome);
                    break;     

                case "Empresa":
                    contatos = contatos.OrderBy(est => est.Empresa);
                    break;

                case "Empresa desc":

                    contatos = contatos.OrderByDescending(est => est.Empresa);
                    break;

                case "Email":
                    contatos = contatos.OrderBy(est => est.Email);
                    break;

                case "Email desc":
                     contatos = contatos.OrderByDescending(est => est.Email);
                     break;

                //case "Telefone Pessoal":
                //    {
                //        contatos = contatos.OrderBy(est => est.TelefonePessoal);
                //        break;
                //    }

                //case "Telefone Pessoal desc":
                //    {
                //        contatos = contatos.OrderByDescending(est => est.TelefonePessoal);
                //        break;
                //    }

                //case "Telefone Comercial":
                //    {
                //        contatos = contatos.OrderBy(est => est.TelefoneComercial);
                //        break;
                //    }

                //case "Telefone Comercial desc":
                //    {
                //        contatos = contatos.OrderByDescending(est => est.TelefoneComercial);
                //        break;
                //    }

                default:
                        contatos = contatos.OrderBy(est => est.Nome);
                        break;
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(contatos.ToPagedList(pageNumber, pageSize));
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nome,Empresa,Email,TelefonePessoal,TelefoneComercial,Created,Modified")] Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                db.Usuarios.Add(usuarios);

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(usuarios);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // POST: Usuarios/Edit/5
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Nome,Empresa,Email,TelefonePessoal,TelefoneComercial,Created,Modified")] Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuarios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usuarios);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuarios usuarios = db.Usuarios.Find(id);
            db.Usuarios.Remove(usuarios);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
