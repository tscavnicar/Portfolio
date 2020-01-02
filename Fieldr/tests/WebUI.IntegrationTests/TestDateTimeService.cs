using Fieldr.Application.Common.Interfaces;
using System;

namespace Fieldr.WebUI.IntegrationTests
{
    public class TestDateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
