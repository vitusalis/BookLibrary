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
    public class AuthorsController : Controller
    {

        // GET: Authors
        //Hosted web API REST Author base url 

        string Baseurl = "http://localhost:8888/";
        public async Task<ActionResult> Index()
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
                
                return View(AuthorInfo);
            }
        }

        // GET: Authors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Author author = new Author();

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("api/Authors/" + id);

                if (Res.IsSuccessStatusCode)
                {
                    var AuthorResponse = Res.Content.ReadAsStringAsync().Result;
                    author = JsonConvert.DeserializeObject<Author>(AuthorResponse);

                }
                return View(author);
            }

        }

        // GET: Authors/Create
        public IActionResult Create()
        {
            //PopulateBooksDropDownList();
            return View();
        }

        // POST: Authors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Author author)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                StringContent content = new StringContent(JsonConvert.SerializeObject(author), Encoding.UTF8, "application/json");


                using (var response = await client.PostAsync($"api/Authors/", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    author = JsonConvert.DeserializeObject<Author>(apiResponse);
                }
                //PopulateBooksDropDownList(author.BookId);
                return RedirectToAction("Index");
            }
        }

        // GET: Authors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Author author = new Author();

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("api/Authors/" + id);

                if (Res.IsSuccessStatusCode)
                {
                    var AuthorResponse = Res.Content.ReadAsStringAsync().Result;
                    author = JsonConvert.DeserializeObject<Author>(AuthorResponse);

                }
                return View(author);
            }
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, /*[Bind("Location,StartDate,EndDate,Id")]*/ Author author)
        {
            if (id != author.Id)
                return NotFound();

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri(Baseurl);
                //client.DefaultRequestHeaders.Clear();
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                StringContent content = new StringContent(JsonConvert.SerializeObject(author), Encoding.UTF8, "application/json");

                using (var response = await client.PutAsync($"api/Authors/" + id, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    author = JsonConvert.DeserializeObject<Author>(apiResponse);
                    return RedirectToAction("Index");
                }
            }

        }

        // GET: Authors/Delete/5
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

                using (var response = await client.DeleteAsync($"api/Authors/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var author = JsonConvert.DeserializeObject<Author>(apiResponse);
                    return RedirectToAction("Index");
                }

            }
        }

        //private async void PopulateBooksDropDownList(object selectedBook = null)
        //{

        //    List<Book> BookInfo = new List<Book>();

        //    using (var client = new HttpClient())
        //    {

        //        client.BaseAddress = new Uri(Baseurl);

        //        client.DefaultRequestHeaders.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //        HttpResponseMessage Res = await client.GetAsync("api/Books/");

        //        if (Res.IsSuccessStatusCode)
        //        {
        //            var BookResponse = Res.Content.ReadAsStringAsync().Result;

        //            BookInfo = JsonConvert.DeserializeObject<List<Book>>(BookResponse);

        //        }
        //        Console.WriteLine("_____________________");
        //        ViewBag.BookId = new SelectList(BookInfo, "Id", "Name", selectedBook);
        //    }
        //}
    }
}
