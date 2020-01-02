using Fieldr.Application.Common.Mappings;
using Fieldr.Domain.Entities;

namespace Fieldr.Application.TodoLists.Queries.ExportTodos
{
    public class TodoItemRecord : IMapFrom<TodoItem>
    {
        public string Title { get; set; }

        public bool Done { get; set; }
    }
}
