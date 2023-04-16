using Oshxona.Domain.Commons;
using Oshxona.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oshxona.Domain.Entites
{
    public class Meal : Auditable
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
        public MealType MealType { get; set; }
    }
}
