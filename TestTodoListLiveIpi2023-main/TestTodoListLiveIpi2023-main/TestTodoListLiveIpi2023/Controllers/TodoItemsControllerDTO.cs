using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestTodoListGamer.Models;
using TestTodoListGamer.Models.Context;

namespace TestTodoListGamer.Controllers
{
    [Route("api/TacheDTO")]
    [ApiController]
    public class TodoItemsControllerDTO : ControllerBase
    {
        private readonly TodoContext _contextTodo;

        public TodoItemsControllerDTO(TodoContext context)
        {
            _contextTodo = context;
        }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
          if (_contextTodo.TodoItems == null)
          {
              return NotFound();
          }
          
          return await _contextTodo.TodoItems
                .Select(item => item.ToDto())
                .ToListAsync();
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
        {
            //Vérfifier si la table existe
          if (_contextTodo.TodoItems == null)
          {
              return NotFound();
          }
          //Cherche l'objet
            var todoItem = await _contextTodo.TodoItems.FindAsync(id);
            
            //On vérifie si l'objet existe
            if (todoItem == null)
            {
                return NotFound();
            }

            //On retourne l'objet
            return todoItem.ToDto();
        }

    }
}
