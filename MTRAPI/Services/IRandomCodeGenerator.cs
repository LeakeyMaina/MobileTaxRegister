using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MTR.Services
{
    public interface IRandomCodeGenerator
    {
        string GenerateConfirmationCode();

    }
}
