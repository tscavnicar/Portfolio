using Fieldr.Application.TodoLists.Queries.ExportTodos;
using System.Collections.Generic;

namespace Fieldr.Application.Common.Interfaces
{
    public interface ICsvFileBuilder
    {
        byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
    }
}
