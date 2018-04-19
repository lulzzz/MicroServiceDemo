using System;
using System.Threading.Tasks;

namespace Interface
{
    public interface IAccountService
    {
	    Task LoginAsync(string userName, string password);
    }
}
