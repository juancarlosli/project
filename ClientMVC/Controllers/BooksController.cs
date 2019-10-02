using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookStore.Models;
using Newtonsoft.Json;

namespace ClientMVC.Controllers
{
    public class BooksController : Controller
    {

        // GET: Books
        public async Task<IActionResult> Index()
        {
            IEnumerable<Books> Books = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5002/api/");
                //HTTP GET
                var responseTask = client.GetAsync("Books");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Books>>();
                    readTask.Wait();

                    Books = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    Books = Enumerable.Empty<Books>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(Books);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Books Books = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5002/api/");
                //HTTP GET
                var responseTask = client.GetAsync("Books" + $"/{id}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Books>();
                    readTask.Wait();

                    Books = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    Books = null;

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            if (Books == null)
            {
                return NotFound();
            }

            return View(Books);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            string token = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5002/api/");
                //HTTP GET
                object data = new
                {
                    userName = "admin",
                    password = "article"
                };

                var myContent = JsonConvert.SerializeObject(data);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var responseTask = client.PostAsync("Auth", byteContent);
                responseTask.Wait();

               

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    token = readTask.Result.Split('"')[3].Replace("Bearer ", "");
                }
            }

            IEnumerable<Authors> authors = null;
            using (var client = new HttpClient())
            {
                
                client.BaseAddress = new Uri("http://localhost:5002/api/");
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
                //HTTP GET
                var responseTask = client.GetAsync("Authors");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Authors>>();
                    readTask.Wait();

                    authors = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    authors = Enumerable.Empty<Authors>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            IEnumerable<Categories> categories = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5002/api/");
                //HTTP GET
                var responseTask = client.GetAsync("Categories");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Categories>>();
                    readTask.Wait();

                    categories = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    categories = Enumerable.Empty<Categories>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            ViewData["AuthorId"] = new SelectList(authors, "Id", "FirstName");
            ViewData["CategoryId"] = new SelectList(categories, "Id", "Name");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,InsertDate,AuthorId,CategoryId")] Books books)
        {
            
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5002/api/");
                    //HTTP GET

                    var myContent = JsonConvert.SerializeObject(books);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var responseTask = client.PostAsync("Books", byteContent);
                    responseTask.Wait();

                    var result = responseTask.Result;
                }

                return RedirectToAction(nameof(Index));
            }
            return View(books);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Books Books = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5002/api/");
                //HTTP GET
                var responseTask = client.GetAsync("Books" + $"/{id}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Books>();
                    readTask.Wait();

                    Books = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    Books = null;

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            string token = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5002/api/");
                //HTTP GET
                object data = new
                {
                    userName = "admin",
                    password = "article"
                };

                var myContent = JsonConvert.SerializeObject(data);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var responseTask = client.PostAsync("Auth", byteContent);
                responseTask.Wait();



                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    token = readTask.Result.Split('"')[3].Replace("Bearer ", "");
                }
            }

            IEnumerable<Authors> authors = null;
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:5002/api/");
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
                //HTTP GET
                var responseTask = client.GetAsync("Authors");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Authors>>();
                    readTask.Wait();

                    authors = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    authors = Enumerable.Empty<Authors>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            IEnumerable<Categories> categories = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5002/api/");
                //HTTP GET
                var responseTask = client.GetAsync("Categories");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Categories>>();
                    readTask.Wait();

                    categories = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    categories = Enumerable.Empty<Categories>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            if (Books == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(authors, "Id", "FirstName", Books.Author);
            ViewData["CategoryId"] = new SelectList(categories, "Id", "Name", Books.Category);
            return View(Books);
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,InsertDate,AuthorId,CategoryId")] Books books)
        {
            if (id != books.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://localhost:5002/api/");
                        //HTTP GET

                        object data = new
                        {
                            id = books.Id,
                            Books = books
                        };

                        var myContent = JsonConvert.SerializeObject(books);
                        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                        var byteContent = new ByteArrayContent(buffer);
                        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                        var responseTask = client.PutAsync("Books" + $"/{id}", byteContent);
                        responseTask.Wait();

                        var result = responseTask.Result;
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(books);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Books Books = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5002/api/");
                //HTTP GET
                var responseTask = client.GetAsync("Books" + $"/{id}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Books>();
                    readTask.Wait();

                    Books = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    Books = null;

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            if (Books == null)
            {
                return NotFound();
            }

            return View(Books);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5002/api/");
                //HTTP GET
                var responseTask = client.DeleteAsync("Books" + $"/{id}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Books>();
                    readTask.Wait();

                    var Books = readTask.Result;
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
