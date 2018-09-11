using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Data
{
    interface IBookUnitOfWork: IDisposable
    {
	    IBookRepository Books { get; }
		int Complete();
    }
}
