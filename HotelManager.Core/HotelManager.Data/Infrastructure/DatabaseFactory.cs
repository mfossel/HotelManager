using HotelManager.Core.Infranstructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManager.Data.Infrastructure
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private readonly HotelManagerDataContext _dataContext;

        public HotelManagerDataContext GetDataContext()
        {
            return _dataContext ?? new HotelManagerDataContext();
        }

        public DatabaseFactory()
        {
            _dataContext = new HotelManagerDataContext();
        }

        protected override void DisposeCore()
        {
            if (_dataContext != null) _dataContext.Dispose();
        }
    }
}
