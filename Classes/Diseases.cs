using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiseaseCalculator.Classes
{
    abstract class Disease_abstraction
    {
        public readonly string name;
        public readonly float infection_chance;

        protected Disease_abstraction(string name, float infection_chance)
        {
            this.name = name;
            this.infection_chance = infection_chance;
        }

        public override string ToString()
        {
            return name;
        }
    }
    class Hemophilia : Disease_abstraction
    {
        static Hemophilia singleInstance = new Hemophilia();

        private Hemophilia() : base("Гемофилия",0.5f)
        {

        }

        public static Hemophilia GetHemophiliaInstance() 
        {
            return singleInstance;
        }
    }
    class PersonalDisease
    {
        public readonly Disease_abstraction disease_type;
        public readonly bool is_ill;
        public float calculated_probability;

        public PersonalDisease(Disease_abstraction disease_type, bool is_ill)
        {
            this.disease_type = disease_type;
            this.is_ill = is_ill;
        }

        public override string ToString()
        {
            return disease_type.ToString() + " " + (is_ill ? "болен" : "предрасположен");
        }
    }
}
