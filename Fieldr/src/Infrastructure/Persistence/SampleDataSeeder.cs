using Fieldr.Application;
using Fieldr.Application.Common.Interfaces;
using Fieldr.Application.Common.Models;
using Fieldr.Domain.Entities;
using Fieldr.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Fieldr.Infrastructure.Persistence
{
    public class SampleDataSeeder
    {
        private readonly IApplicationDbContext _context;
        private readonly IIdentityService _userManager;

        private readonly Dictionary<int, Field> Fields = new Dictionary<int, Field>();
        private readonly Dictionary<int, FieldRecord> FieldNotes = new Dictionary<int, FieldRecord>();

        public SampleDataSeeder(IApplicationDbContext context, IIdentityService userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task SeedAllAsync(CancellationToken cancellationToken)
        {
            if (_context.Fields.Any())
            {
                return;
            }

            await SeedFieldAsync(cancellationToken);
            await SeedUsersAsync(cancellationToken);

        }


        private async Task SeedFieldAsync(CancellationToken cancellationToken)
        {
            Fields.Add(1,
              new Field()
              {
                  Lat = 16.1963533229,
                  Lng = 46.6419890882,
                  Name = "Grmovec"
              }.AddFieldRecords(
                  new FieldRecord { Note = "Health check 1", Created = DateTime.Parse("01-08-2018") },
                  new FieldRecord { Note = "Health check 2", Created = DateTime.Parse("14-08-2018") }));

            Fields.Add(2,
              new Field()
              {
                  Lat = 16.1963533229,
                  Lng = 46.6419890882,
                  Name = "Muzge"
              }.AddFieldRecords(
                  new FieldRecord { Note = "Health check 1", Created = DateTime.Parse("05-06-2018") },
                  new FieldRecord { Note = "Health check 2", Created = DateTime.Parse("11-06-2018") }));


            foreach (var field in Fields.Values)
            {
                _context.Fields.Add(field);
            }

            await _context.SaveChangesAsync(cancellationToken);

        }

        private async Task SeedUsersAsync(CancellationToken cancellationToken)
        {
            //var defaultUser = new ApplicationUser { UserName = "jason@clean-architecture", Email = "jason@clean-architecture" };
            await _userManager.CreateUserAsync("admin@clean-architecture", "Clean_arhitecture!2");
        }

        ///<param name="hex">base64 image format</param>
        private static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                .ToArray();
        }
    }

    internal static class FieldExtensions
    {
        public static Field AddFieldRecords(this Field field, params FieldRecord[] fieldrecords)
        {
            foreach (var fieldrecord in fieldrecords)
            {
                field.FieldRecords.Add(fieldrecord);
            }

            return field;
        }
    }
}
