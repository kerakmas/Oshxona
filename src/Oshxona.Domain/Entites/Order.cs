using Oshxona.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oshxona.Domain.Entites
{
    public class Order : Auditable
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int MealId { get; set; }
        public Meal Meal { get; set; }
    }
}
