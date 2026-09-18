using IAM_CORE.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IAM_CORE.Interfaces
{
    public interface IUserReposiotry
    {

        Task<User?> GetByEmailAsync(string email,CancellationToken cancellationToken = default);

    }
}
