using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Fieldr.Application.FieldRecords.Commands.CreateFieldRecord
{
    public class CreateFieldRecordCommandValidator : AbstractValidator<CreateFieldRecordCommand>
    {
        public CreateFieldRecordCommandValidator()
        {
            RuleFor(v => v.Note)
               .MaximumLength(1000)
               .NotEmpty();
        }
    }
}
