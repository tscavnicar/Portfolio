﻿using Fieldr.Application.TodoLists.Commands.CreateTodoList;
using Fieldr.Application.UnitTests.Common;
using Fieldr.Domain.Entities;
using Shouldly;
using Xunit;

namespace Fieldr.Application.UnitTests.TodoLists.Commands.CreateTodoList
{
    public class UpdateTodoListCommandValidatorTests : CommandTestBase
    {
        [Fact]
        public void IsValid_ShouldBeTrue_WhenListTitleIsUnique()
        {
            var command = new CreateTodoListCommand
            {
                Title = "Bucket List"
            };

            var validator = new CreateTodoListCommandValidator(Context);

            var result = validator.Validate(command);

            result.IsValid.ShouldBe(true);
        }

        [Fact]
        public void IsValid_ShouldBeFalse_WhenListTitleIsNotUnique()
        {
            Context.TodoLists.Add(new TodoList { Title = "Shopping" });
            Context.SaveChanges();

            var command = new CreateTodoListCommand
            {
                Title = "Shopping"
            };

            var validator = new CreateTodoListCommandValidator(Context);

            var result = validator.Validate(command);

            result.IsValid.ShouldBe(false);
        }
    }
}
