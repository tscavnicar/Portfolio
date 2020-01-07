using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fieldr.Application.FieldRecords.Commands.CreateFieldRecord;
using Fieldr.Application.FieldRecords.Commands.DeleteFieldRecord;
using Fieldr.Application.FieldRecords.Commands.UpdateFieldRecord;
using Microsoft.AspNetCore.Mvc;

namespace Fieldr.WebUI.Controllers
{
    public class FieldRecordsController : ApiController
    {
        [HttpPost]
        public async Task<ActionResult<long>> Create(CreateFieldRecordCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(long id, UpdateFieldRecordCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            await Mediator.Send(new DeleteFieldRecordCommand { Id = id });

            return NoContent();
        }
    }
}