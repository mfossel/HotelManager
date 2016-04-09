﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManager.Data.Infrastructure
{
    public interface IDatabaseFactory : IDisposable
    {
        HotelManagerDataContext GetDataContext();
    }
}
