using Fieldr.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Fieldr.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<TodoList> TodoLists { get; set; }

        DbSet<TodoItem> TodoItems { get; set; }

        DbSet<Field> Fields { get; set; }

        DbSet<FieldRecord> FieldRecords { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
