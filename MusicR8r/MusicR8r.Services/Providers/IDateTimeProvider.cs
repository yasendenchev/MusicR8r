﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicR8r.Services.Providers
{
    public interface IDateTimeProvider
    {
       DateTime? Now();
    }
}
