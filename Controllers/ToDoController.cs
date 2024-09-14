using Microsoft.AspNetCore.Mvc;
using ToDoApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace ToDoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private static List<ToDo> ToDos = new List<ToDo>
        {
            new ToDo { Id = 1, Title = "Learn .NET 8", IsCompleted = false },
            new ToDo { Id = 2, Title = "Build an API", IsCompleted = false }
        };

        [HttpGet]
        public ActionResult<IEnumerable<ToDo>> GetToDos()
        {
            return ToDos;
        }

        [HttpGet("{id}")]
        public ActionResult<ToDo> GetToDoById(int id)
        {
            var todo = ToDos.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return NotFound();
            }
            return todo;
        }

        [HttpPost]
        public ActionResult<ToDo> CreateToDo(ToDo todo)
        {
            todo.Id = ToDos.Max(t => t.Id) + 1;
            ToDos.Add(todo);
            return CreatedAtAction(nameof(GetToDoById), new { id = todo.Id }, todo);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateToDo(int id, ToDo todo)
        {
            var existingToDo = ToDos.FirstOrDefault(t => t.Id == id);
            if (existingToDo == null)
            {
                return NotFound();
            }
            existingToDo.Title = todo.Title;
            existingToDo.IsCompleted = todo.IsCompleted;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteToDo(int id)
        {
            var todo = ToDos.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return NotFound();
            }
            ToDos.Remove(todo);
            return NoContent();
        }
    }
}
