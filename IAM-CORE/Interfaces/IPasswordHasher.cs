using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAM_CORE.Interfaces
{
    public interface IPasswordHasher
    {

        string Hash(string password);

        bool Verify(string to_check, string db_pass);

    }
}
