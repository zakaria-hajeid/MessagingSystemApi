using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Task.percestance.Models;
using Task.Percestance.Abstractions;

namespace Task.percestance
{
     public interface IAdmin: IRepstory<Organizations>
    {
        Task<IEnumerable<Organizations>> GetOrg();
        Task<Organizations> GetOrg(int id);

    }
}
