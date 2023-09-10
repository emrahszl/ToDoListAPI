using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToDoListApi.Data;
using ToDoListApi.Dtos;

namespace ToDoListUI.Controllers
{
    public class ToDoItemsController : Controller
    {
        private readonly HttpClient _httpClient;

        public ToDoItemsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _httpClient.GetFromJsonAsync<List<ToDoItem>>("https://localhost:7197/api/ToDoItems"));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostToDoItem postToDoItem)
        {
            if (ModelState.IsValid)
            {
                await _httpClient.PostAsJsonAsync("https://localhost:7197/api/ToDoItems", postToDoItem);

                return RedirectToAction(nameof(Index));
            }
            return View(postToDoItem);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var toDoItem = await _httpClient.GetFromJsonAsync<PutToDoItem>($"https://localhost:7197/api/ToDoItems/{id}");

            if (toDoItem == null)
                return NotFound();

            return View(toDoItem);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PutToDoItem putToDoItem)
        {
            if (ModelState.IsValid)
            {
                await _httpClient.PutAsJsonAsync($"https://localhost:7197/api/ToDoItems/{id}", putToDoItem);

                return RedirectToAction(nameof(Index));
            }
            return View(putToDoItem);
        }

        public async Task<IActionResult> Delete(int id)
        {
           var toDoItem =  await _httpClient.GetFromJsonAsync<ToDoItem>($"https://localhost:7197/api/ToDoItems/{id}");

            if (toDoItem == null)
                return NotFound();

            return View(toDoItem);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _httpClient.DeleteAsync($"https://localhost:7197/api/ToDoItems/{id}");

            return RedirectToAction(nameof(Index));
        }
    }
}
