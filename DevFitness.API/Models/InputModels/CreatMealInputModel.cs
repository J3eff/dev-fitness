using System;

namespace DevFitness.API.Models.InputModels
{
    public class CreatMealInputModel
    {
        public string description { get; set; }
        public int Calories { get; set; }
        public DateTime Date { get; set; }
    }
}
