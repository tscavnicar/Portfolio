using Fieldr.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using FluentValidation;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace Fieldr.Application.FieldLists.Commands.UpdateFieldList
{
    public class UpdateFieldListCommandValidator : AbstractValidator<UpdateFieldListCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateFieldListCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(200).WithMessage("Name must not exceed 200 characters.")
                .MustAsync(BeUniqueTitle).WithMessage("The specified name is already exists.");
        }

        public async Task<bool> BeUniqueTitle(UpdateFieldListCommand model, string name, CancellationToken cancellationToken)
        {
            return await _context.Fields
                .Where(l => l.Id != model.Id)
                .AllAsync(l => l.Name != name);
        }
    }
}
