using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookStore.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ClientMVC.Controllers
{
    public class CategoriesController : Controller
    {
        public async Task<IActionResult> Index()
        {
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
            return View(categories);
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Categories categories = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5002/api/");
                //HTTP GET
                var responseTask = client.GetAsync("Categories" + $"/{id}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Categories>();
                    readTask.Wait();

                    categories = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    categories = null;

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            if (categories == null)
            {
                return NotFound();
            }

            return View(categories);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,InsertDate")] Categories categories)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5002/api/");
                    //HTTP GET

                    var myContent = JsonConvert.SerializeObject(categories);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var responseTask = client.PostAsync("Categories", byteContent);
                    responseTask.Wait();

                    var result = responseTask.Result;
                }
            }
            return View(categories);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Categories categories = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5002/api/");
                //HTTP GET
                var responseTask = client.GetAsync("Categories" + $"/{id}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Categories>();
                    readTask.Wait();

                    categories = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    categories = null;

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            if (categories == null)
            {
                return NotFound();
            }
            return View(categories);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,InsertDate")] Categories categories)
        {
            if (id != categories.Id)
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
                            id = categories.Id,
                            Books = categories
                        };

                        var myContent = JsonConvert.SerializeObject(categories);
                        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                        var byteContent = new ByteArrayContent(buffer);
                        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                        var responseTask = client.PutAsync("Categories" + $"/{id}", byteContent);
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
            return View(categories);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Categories categories = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5002/api/");
                //HTTP GET
                var responseTask = client.GetAsync("Categories" + $"/{id}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Categories>();
                    readTask.Wait();

                    categories = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    categories = null;

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            if (categories == null)
            {
                return NotFound();
            }

            return View(categories);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5002/api/");
                //HTTP GET
                var responseTask = client.DeleteAsync("Categories" + $"/{id}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Categories>();
                    readTask.Wait();

                    var Categories = readTask.Result;
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
