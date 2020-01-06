using Fieldr.Application.FieldLists.Queries.GetFields;
using Fieldr.Application.TodoLists.Commands.CreateTodoList;
using Fieldr.Application.TodoLists.Commands.DeleteTodoList;
using Fieldr.Application.TodoLists.Commands.UpdateTodoList;
using Fieldr.Application.TodoLists.Queries.ExportTodos;
using Fieldr.Application.TodoLists.Queries.GetTodos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Fieldr.WebUI.Controllers
{
    [Authorize]
    public class FieldListsController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<FieldVm>> Get()
        {
            return await Mediator.Send(new GetFieldsQuery());
        }

        //[HttpPost]
        //public async Task<ActionResult<long>> Create(CreateTodoListCommand command)
        //{
        //    return await Mediator.Send(command);
        //}

        //[HttpPut("{id}")]
        //public async Task<ActionResult> Update(long id, UpdateTodoListCommand command)
        //{
        //    if (id != command.Id)
        //    {
        //        return BadRequest();
        //    }

        //    await Mediator.Send(command);

        //    return NoContent();
        //}

        //[HttpDelete("{id}")]
        //public async Task<ActionResult> Delete(int id)
        //{
        //    await Mediator.Send(new DeleteTodoListCommand { Id = id });

        //    return NoContent();
        //}
    }
}
