using Fieldr.Application.FieldLists.Commands.CreateFieldList;
using Fieldr.Application.FieldLists.Commands.DeleteFieldList;
using Fieldr.Application.FieldLists.Commands.UpdateFieldList;
using Fieldr.Application.FieldLists.Queries.GetFields;
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

        [HttpPost]
        public async Task<ActionResult<long>> Create(CreateFieldListCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(long id, UpdateFieldListCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteFieldListCommand { Id = id });

            return NoContent();
        }
    }
}
