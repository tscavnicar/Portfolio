using Fieldr.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fieldr.Application.FieldLists.Commands.CreateFieldList
{
    class CreateFieldListCommandValidator : AbstractValidator<CreateFieldListCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateFieldListCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(200).WithMessage("Name must not exceed 200 characters.")
                .MustAsync(BeUniqueTitle).WithMessage("The specified name already exists.");
        }

        public async Task<bool> BeUniqueTitle(string name, CancellationToken cancellationToken)
        {
            return await _context.Fields
                .AllAsync(l => l.Name != name);
        }
    }
}
