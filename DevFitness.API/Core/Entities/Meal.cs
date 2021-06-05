using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevFitness.API.Core.Entities
{
    public class Meal : BaseEntity
    {
        public Meal(string descricao, int calories, DateTime date, int userId) : base()
        {
            Descricao = descricao;
            Calories = calories;
            Date = date;
            UserId = userId;
        }

        public string Descricao { get; private set; }
        public int Calories { get; private set; }
        public DateTime Date { get; private set; }

        // EF Core precisa indetificar de quem é a refeição 
        public int UserId { get; private set; }
    }
}
