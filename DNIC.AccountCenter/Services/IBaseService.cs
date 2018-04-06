using DNIC.AccountCenter.Core;
using DNIC.AccountCenter.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DNIC.AccountCenter.Services
{
    public interface IBaseService<T> where T : BaseEntity
    {
        void Insert(T entitiy);

        T GetById(int id);

        void Update(T entitiy);

        void Delete(T entitiy);

        IPagedList<T> GetListPageable(int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
