using DiseaseCalculator.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiseaseCalculator.Classes
{
    class Person2
    {
        public readonly string name;
        public readonly bool gender;//true = male
        public Person2 mother;
        public Person2 father;
        public List<PersonalDisease> diseases1 = new List<PersonalDisease>();//chromosome 1
        public List<PersonalDisease> diseases2 = new List<PersonalDisease>();//chromosome 2

        public Person2(string name, bool gender)
        {
            this.name = name;
            this.gender = gender;
        }

        public void Calculate() //-------------------------------------------------------main logic------------------------------------------------------//
        {
            float mutation_prob = 0.00002f;//спонтанная мутация 0.00002% по Холдейну
            float prob = 0.0f;
            Random rnd = new Random(10);
            Predicate<PersonalDisease> search = x => x.Equals(new PersonalDisease(Hemophilia.GetHemophiliaInstance(), true));

            if (gender == true)
            {
                if (mother != null)
                {
                    if (
                        mother.diseases1.FindAll(search).Count > 0 &&
                        mother.diseases2.FindAll(search).Count > 0
                    )
                    {
                        prob += 1 * mother.diseases1.Find(search).calculated_probability;
                    }
                    else if (mother.diseases1.FindAll(search).Count > 0)
                    {
                        prob += 0.5f * mother.diseases1.Find(search).calculated_probability;
                    }
                    else if (mother.diseases2.FindAll(search).Count > 0)
                    {
                        prob += 0.5f * mother.diseases1.Find(search).calculated_probability;
                    }
                }
            }
            else
            {
                if (father != null)
                {
                    if (father.diseases1.FindAll(search).Count > 0)
                    {
                        prob += 1 * father.diseases1.Find(search).calculated_probability;
                    }
                }
                if (mother != null)
                {
                    if (
                        mother.diseases1.FindAll(search).Count > 0 &&
                        mother.diseases2.FindAll(search).Count > 0
                    )
                    {
                        prob += 1 * mother.diseases1.Find(search).calculated_probability;
                    }
                    else if (mother.diseases1.FindAll(search).Count > 0)
                    {
                        prob += 0.5f * mother.diseases1.Find(search).calculated_probability;
                    }
                    else if (mother.diseases2.FindAll(search).Count > 0)
                    {
                        prob += 0.5f * mother.diseases1.Find(search).calculated_probability;
                    }
                }
            }

            if (prob > 0.0001f)
            {
                if (gender == true)
                {
                    diseases1.Add(new PersonalDisease(Hemophilia.GetHemophiliaInstance(), prob));
                }
                else
                {
                    if (rnd.Next(10) > 5)
                    {
                        diseases1.Add(new PersonalDisease(Hemophilia.GetHemophiliaInstance(), prob));
                    }
                    else
                    {
                        diseases2.Add(new PersonalDisease(Hemophilia.GetHemophiliaInstance(), prob));
                    }
                }
            }
        }

        public override string ToString()
        {
            string pdiseases = "";
            if (diseases1.Count > 0 || diseases2.Count > 0)
            {
                foreach (var item in diseases1)
                {
                    pdiseases += item.ToString();
                }
                foreach (var item in diseases2)
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
