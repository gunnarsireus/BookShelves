using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;

namespace Api.Data
{
    public interface IShelfRepository:IRepository<Shelf>
    {
	    //Todo, if more advanced filtering is needed
	}
}
