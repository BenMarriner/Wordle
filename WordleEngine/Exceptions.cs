using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordleEngine
{
    public class InvalidWordException : Exception
    {
        public InvalidWordException() : base() 
        {
        }
    }

    public class DictionaryNotFoundException : FileNotFoundException
    {
        public DictionaryNotFoundException() : base()
        {
        }
    }
}
