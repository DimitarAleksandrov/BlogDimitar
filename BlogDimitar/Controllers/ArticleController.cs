using BlogDimitar.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BlogDimitar.Controllers
{
    public class ArticleController : Controller
    {
        // GET: Article
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        // GET: Article/List
        public ActionResult List()
        {
            using (var database = new BlogDbContext())
            {
                // Get articles from database
                var articles = database.Articles
                    .Include(a => a.Author)
                    .ToList();

                return View(articles);
            }
        }

        //GET: Article/Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var database = new BlogDbContext())
            {
                // Get the article from database
                var article = database.Articles
                    .Where(a => a.Id == id)
                    .Include(a => a.Author)
                    .First();

                if (article == null)
                {
                    return HttpNotFound();
                }
                return View(article);
            }
        }

        //GET: Article/Create
        public ActionResult Create()
        {
            return View();
        }

        //POS: Article/Create
        [HttpPost]
        public ActionResult Create(Article article)
        {
            if (ModelState.IsValid)
            {
                // Insert article in database
                using (var database = new BlogDbContext())
                {
                    // Get author id
                    var authorId = database.Users
                        .Where(u => u.UserName == this.User.Identity.Name)
                        .First()
                        .Id;
                    // Set article author
                    article.AutorId = authorId;
                    // Save article in databasa
                    database.Articles.Add(article);
                    database.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        // GET: Article/Edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var database = new BlogDbContext())
            {
                // Get article from database
                var artticle = database.Articles
                    .Where(a => a.Id == id)
                    .First();
                // Check if article exist
                if (artticle == null)
                {
                    return HttpNotFound();
                }
                // Create the view model
                var model = new ArticleViewModel();
                model.Id = artticle.Id;
                model.Title = artticle.Title;
                model.Content = artticle.Content;
                // Pass the view model to view
                return View(model);
            }
        }

        // POS: Article/Edit
        [HttpPost]
        public ActionResult Edit(ArticleViewModel model)
        {
            // Check if model state is valid
            if (ModelState.IsValid)
            {
                using (var database = new BlogDbContext())
                {
                    // Get article from database
                    var article = database.Articles
                        .FirstOrDefault(a => a.Id == model.Id);
                    // Set article properties
                    article.Title = model.Title;
                    article.Content = model.Content;
                    // Save article state in database
                    database.Entry(article).State = EntityState.Modified;
                    database.SaveChanges();
                    // Redirect to the index page
                    return RedirectToAction("Index");
                }
            }
            // If model state is invalid, return the same view
            return View(model);
        }

        // GET: Article/Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var database = new BlogDbContext())
            {
                // Get article from database
                var artticle = database.Articles
                    .Where(a => a.Id == id)
                    .Include(a => a.Author)
                    .First();
                // Check if article exist
                if (artticle == null)
                {
                    return HttpNotFound();
                }
                // Pass article to view
                return View(artticle);
            }
        }

        // POS: Artticle/Delete
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var database = new BlogDbContext())
            {
                // Get article from database
                var article = database.Articles
                    .Where(a => a.Id == id)
                    .Include(a => a.Author)
                    .First();
                // Check if article exist
                if (article == null)
                {
                    return HttpNotFound();
                }
                // Delete article from database
                database.Articles.Remove(article);
                database.SaveChanges();
                // Redirect to index page
                return RedirectToAction("Index");
            }
        }
    }
}