using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BirthdayLibrary.BLL.Models;
using BirthdayLibrary.DAL;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;

namespace BirthdayLibraryMVC.Controllers
{
    public class BooksController : Controller
    {

        // GET: Books
        //Hosted web API REST Book base url 

        string Baseurl = "http://localhost:8888/";
        public async Task<ActionResult> Index()
        {
            List<Book> BookInfo = new List<Book>();

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("api/Books/");

                if (Res.IsSuccessStatusCode)
                {
                    var BookResponse = Res.Content.ReadAsStringAsync().Result;

                    BookInfo = JsonConvert.DeserializeObject<List<Book>>(BookResponse);

                }
                return View(BookInfo);
            }
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Book book = new Book();

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("api/Books/" + id);

                if (Res.IsSuccessStatusCode)
                {
                    var BookResponse = Res.Content.ReadAsStringAsync().Result;
                    book = JsonConvert.DeserializeObject<Book>(BookResponse);

                }
                return View(book);
            }

        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                StringContent content = new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json");
                using (var response = await client.PostAsync($"api/Books/", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    book = JsonConvert.DeserializeObject<Book>(apiResponse);
                }
                PopulateAuthorsDropDownList(book.BookAuthors);
                return RedirectToAction("Index");
            }
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Book book = new Book();

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("api/Books/" + id);

                if (Res.IsSuccessStatusCode)
                {
                    var BookResponse = Res.Content.ReadAsStringAsync().Result;
                    book = JsonConvert.DeserializeObject<Book>(BookResponse);

                }
                PopulateAuthorsDropDownList(book.BookAuthors);
                return View(book);
            }
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Book book)
        {
            if (id != book.Id)
                return NotFound();

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(Baseurl);
                //client.DefaultRequestHeaders.Clear();
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                StringContent content = new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json");

                using (var response = await client.PutAsync($"api/Books/" + id, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    book = JsonConvert.DeserializeObject<Book>(apiResponse);
                    return RedirectToAction("Index");
                }
            }

        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(Baseurl);
                //client.DefaultRequestHeaders.Clear();
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using (var response = await client.DeleteAsync($"api/Books/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var book = JsonConvert.DeserializeObject<Book>(apiResponse);
                    return RedirectToAction("Index");
                }

            }
        }

        private async void PopulateAuthorsDropDownList(object selectedAuthor = null)
        {

            List<Author> AuthorInfo = new List<Author>();

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("api/Authors/");

                if (Res.IsSuccessStatusCode)
                {
                    var AuthorResponse = Res.Content.ReadAsStringAsync().Result;

                    AuthorInfo = JsonConvert.DeserializeObject<List<Author>>(AuthorResponse);

                }
                Console.WriteLine("_____________________");
                ViewBag.AuthorId = new SelectList(AuthorInfo, "Id", "Name", selectedAuthor);
            }
        }
    }
}
