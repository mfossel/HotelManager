﻿using HotelManager.Core.Domain;
using HotelManager.Core.Repository;
using HotelManager.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManager.Data.Repository
{
    public class WorkorderRepository : Repository<Workorder>, IWorkorderRepository
    {
        public WorkorderRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
