using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oshxona.Service.Exeptions
{
    public class OshxonaExeption : Exception
    {
        public int Code { get; set; }
        public OshxonaExeption(int code, string message)
            : base(message)
        {
            Code = code;
        }
    }
}
