using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevFitness.API.Core.Entities
{
    public class User : BaseEntity
    {
        public User(string fullName, decimal height, decimal weight, DateTime birthDate) : base()
        {
            FullName = fullName;
            Height = height;
            Weight = weight;
            BirthDate = birthDate;

            Maels = new List<Meal>();
        }

        public string FullName { get; private set; }
        public decimal Height { get; private set; }
        public decimal Weight { get; private set; }
        public DateTime BirthDate { get; private set; }

        public IEnumerable<Meal> Maels { get; private set; }
    }
}
