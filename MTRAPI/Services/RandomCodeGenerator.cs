using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MTR.Services
{
    public class RandomCodeGenerator : IRandomCodeGenerator
    {
        private readonly Random _rdm = new Random();
        public string GenerateConfirmationCode()
        {
            int _min = 100000;
            int _max = 999999;
            return _rdm.Next(_min, _max).ToString();
        }
    }
}
