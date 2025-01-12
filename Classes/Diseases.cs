using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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

    class PersonalDisease : IEquatable<PersonalDisease>, IComparable<PersonalDisease>
    {
        public readonly Disease_abstraction disease_type;
        public readonly bool is_ill;
        public float calculated_probability;

        public PersonalDisease(Disease_abstraction disease_type, bool is_ill)
        {
            this.disease_type = disease_type;
            this.is_ill = is_ill;
            calculated_probability = 1;
        }

        public PersonalDisease(Disease_abstraction disease_type, float calculated_prob)
        {
            this.disease_type = disease_type;
            this.is_ill = false;
            this.calculated_probability = calculated_prob;
        }

        public override string ToString()
        {
            return disease_type.ToString() + " : " + (is_ill ? "болен" : "пред.");
        }

        public bool Equals(PersonalDisease obj)
        {
            return obj.disease_type.name == disease_type.name;
        }

        public int CompareTo(PersonalDisease obj)
        {
            if (obj.calculated_probability == calculated_probability)
            {
                return 0;
            }
            else if (obj.calculated_probability > calculated_probability)
            {
                return -1;
            }
            else if (obj.calculated_probability < calculated_probability)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public override int GetHashCode()
        {
            return disease_type.name.GetHashCode();
        }
    }
}
