using DiseaseCalculator.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiseaseCalculator.Classes
{
    class Person
    {
        public readonly string name;
        public readonly bool gender;//true = male
        public Person mother;
        public Person father;
        public List<PersonalDisease> diseases = new List<PersonalDisease>();

        public Person(string name, bool gender)
        {
            this.name = name;
            this.gender = gender;
        }

        public void Calculate() //-------------------------------------------------------main logic------------------------------------------------------//
        {
            if (!diseases.Contains(new PersonalDisease(Hemophilia.GetHemophiliaInstance(), true)))
            {
                float prob = 0.0f;
                if (father != null)
                {
                    if (father.diseases.Count > 0)
                    {
                        prob += 0.5f;
                    }
                }
                if (mother != null)
                {
                    if (mother.diseases.Count > 0)
                    {
                        prob += 0.5f;
                    }
                }
                if (prob > 0.0f)
                {
                    diseases.Add(new PersonalDisease(Hemophilia.GetHemophiliaInstance(), false));
                }
            }
        }

        public override string ToString()
        {
            string pdiseases = "";
            if (diseases.Count > 0)
            {
                foreach (var item in diseases)
                {
                    pdiseases += item.ToString();
                }
            }
            else
            {
                pdiseases = "здоров";
            }

            return name + " " + (gender ? "M" : "F") + " " + pdiseases;
        }
    }
}
