using DNIC.AccountCenter.Core.Domain.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DNIC.AccountCenter.Services.Messages
{
    public interface IEmailAccountService : IBaseService<EmailAccount>
    {
        IList<EmailAccount> GetAllEmailAccounts();
    }
}
