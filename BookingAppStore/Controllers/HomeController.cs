using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookingAppStore.Models;

namespace BookingAppStore.Controllers
{
    public class HomeController : Controller
    {
        BookContext db = new BookContext();

        public ActionResult Index()
        {
            var books = db.Books;
   
            //ViewBag.Books = books;

            SelectList authors = new SelectList(db.Books, "Author", "Name");
            ViewBag.Authors = authors;

            return View(books);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Book book)
        {
            db.Books.Add(book); //аналог INSERT в SQL - додає нову книгу в таблицю
            db.SaveChanges(); //SaveChanges - для зберігання всіх змін в базі даних

            return RedirectToAction("Index");
        }

        //public ActionResult Delete(int id)
        //{
        //    Book b = db.Books.Find(id);
        //    if(b != null)
        //    {
        //        db.Books.Remove(b); // DELETE in SQL
        //        db.SaveChanges();
        //    }
        //    return RedirectToAction("Index");
        //}
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Book b = db.Books.Find(id);
            if (b == null)
            {
                return HttpNotFound();
            }
            return View(b);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Book b = db.Books.Find(id);
            if (b == null)
            {
                return HttpNotFound();
            }
            db.Books.Remove(b);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return HttpNotFound();
            }
            Book book = db.Books.Find(id);
            if(book != null)
            {
                return View(book);
            }
            return HttpNotFound();
        }
        [HttpPost]
        public ActionResult Edit(Book book)
        {
            db.Entry(book).State = EntityState.Modified; //UPDATE in SQL
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult GetBook(int id)
        {
            Book b = db.Books.Find(id);
            if (b == null)
                return HttpNotFound();
            return View(b);
        }

        [HttpPost]
        public string GetForm(string author)
        {
            return author;
        }
        //Цей метод для часткового представлення
        public ActionResult GetList()
        {
            string[] states = new string[] { "Russia", "USA", "Canada", "France" };
            return PartialView(states);
            
        }
        [HttpGet]
        public ActionResult Buy(int id) 
        {
            ViewBag.BookId = id;
            Purchase purchase = new Purchase { BookId = id, Person = "Невідомо" };
            return View(purchase);
        }
        [HttpPost]
        public string Buy(Purchase purchase)                 
        {
            purchase.Date = DateTime.Now;
            // добавляємо інформацію про покупку в БД
            db.Purchases.Add(purchase);
            //Save in data based all changes
            db.SaveChanges();
            return "Дякуємо," + purchase.Person + "за покупку!";
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your disccription page.";
            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }


    }
}