﻿using Fieldr.Application.Common.Interfaces;
using System;

namespace Fieldr.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
