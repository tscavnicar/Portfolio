using FluentValidation;

namespace Fieldr.Application.FieldRecords.Commands.UpdateFieldRecord
{
    public class UpdateFieldRecordCommandValidator : AbstractValidator<UpdateFieldRecordCommand>
    {
        public UpdateFieldRecordCommandValidator()
        {
            RuleFor(v => v.Note)
               .MaximumLength(1000)
               .NotEmpty();
        }
    }
    
}
