using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePerceptron
{
    class Network
    {
        public int[,] weight;
        public int[,] mul;
        public int[,] input;
        public int sum;
        public int limit = 9;
    }
}
