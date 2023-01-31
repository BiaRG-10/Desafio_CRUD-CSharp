using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Desafio_CRUD_CSharp.Models;

namespace Desafio_CRUD_CSharp.Controllers
{
    public class UsuariosController : Controller
    {
        private CadastroContatoDBEntities db = new CadastroContatoDBEntities();

        public object Nome { get; private set; }

        // original substituido

        // GET: Usuarios
        //public ActionResult Index() 
        //{
        //    return View(db.Usuarios.ToList()); // converte em comando sql select
        //}


        // novo ordenação
        public ViewResult Index(string Ordenacao, string searchString)
        {

            ViewBag.NomeOrdenacaoParm = string.IsNullOrEmpty(Ordenacao) ? "Nome desc" : "";
            ViewBag.EmpresaOrdenacaoParm = Ordenacao == "Empresa desc" ? "Empresa desc" : "Empresa";
            ViewBag.EmailOrdenacaoParm = Ordenacao == "Email desc" ? "Email desc" : "Email";
            ViewBag.TelefonePessoalOrdenacaoParm = Ordenacao == "Telefone Pessoal desc" ? "Telefone Pessoal desc" : "Telefone Pessoal";
            ViewBag.TelefoneComercialOrdenacaoParm = Ordenacao == "Telefone Comercial desc" ? "Telefone Comercial desc" : "Telefone Comercial";

            var contatos = from est in db.Usuarios
                             select est;

            if (!String.IsNullOrEmpty(searchString))
            {
                contatos = Usuarios.Where(est => est.Nome.ToUpper().Contains(searchString.ToUpper) || est.Nome.Contains(searchString)); // arrumar
            }

            switch (Ordenacao)
            {
                case "Nome desc":
                    {
                        contatos = contatos.OrderByDescending(est => est.Nome);
                        break;
                        break;
                    }

                case "Empresa":
                    {
                        contatos = contatos.OrderBy(est => est.Empresa);
                        break;
                        break;
                    }

                case "Empresa desc":
                    {
                        contatos = contatos.OrderByDescending(est => est.Empresa);
                        break;
                        break;
                    }

                case "Email":
                    {
                        contatos = contatos.OrderBy(est => est.Email);
                        break;
                        break;
                    }

                case "Email desc":
                    {
                        contatos = contatos.OrderByDescending(est => est.Email);
                        break;
                        break;
                    }

                case "Telefone Pessoal":
                    {
                        contatos = contatos.OrderBy(est => est.TelefonePessoal);
                        break;
                        break;
                    }

                case "Telefone Pessoal desc":
                    {
                        contatos = contatos.OrderByDescending(est => est.TelefonePessoal);
                        break;
                        break;
                    }

                case "Telefone Comercial":
                    {
                        contatos = contatos.OrderBy(est => est.TelefoneComercial);
                        break;
                        break;
                    }

                case "Telefone Comercial desc":
                    {
                        contatos = contatos.OrderByDescending(est => est.TelefoneComercial);
                        break;
                        break;
                    }

                default:
                    {
                        contatos = contatos.OrderBy(est => est.Nome);
                        break;
                        break;
                    }
            }
            return View(contatos.ToList());
        }


        // arrumar pesquisa

        //public ViewResult Index(string searchString, object LastName ) 

        //{
        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        Nome = Usuarios.Where(s => s.LastName.Contains(searchString) || s.FirstMidName.Contains(searchString));
        //    }
        //}

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
