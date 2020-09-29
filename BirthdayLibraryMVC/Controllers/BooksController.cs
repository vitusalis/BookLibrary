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

        //Hosted web API REST Book base url 
        string Baseurl = "http://localhost:8888/";
        
        // GET: Books
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
                return NotFound();
            
            if (TempData["message"] != null)
                ViewBag.Message = TempData["message"];

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
        public async Task<IActionResult> Create()
        {
            if (TempData["message"] != null)
                ViewBag.Message = TempData["message"];

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
                    Dictionary<int, string> authorList = new Dictionary<int, string>();
                    foreach (var author in AuthorInfo)
                        authorList.Add(author.Id, author.Name + " " + author.LastName);
                    ViewBag.AuthorList = authorList;
                }
                
            }

            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookDTO book)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                StringContent content = new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json");
                using (var response = await client.PostAsync($"api/Books/", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                        return RedirectToAction("Index");
                }
                TempData["message"] = "Livro nao pode ser criado";
                return RedirectToAction("Create");
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
                return View(book);
            }
        }

        // POST: Books/Edit/5
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
                    return RedirectToAction("Index");
                }
            }

        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                using (var response = await client.DeleteAsync($"api/Books/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    return RedirectToAction("Index");
                }
            }
        }
    }
}
