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
        public Person? mother;
        public Person? father;
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
                float mutation_prob = 0.00002f;//спонтанная мутация 0.00002% по Холдейну
                float prob = 0.0f;
                Predicate<PersonalDisease> search = x => x.Equals(new PersonalDisease(Hemophilia.GetHemophiliaInstance(), true));

                if (gender == true)
                {
                    if (mother != null)
                    {
                        if (mother.diseases.FindAll(search).Count > 0)
                        {
                            prob += 0.5f * mother.diseases.Find(search).calculated_probability;
                        }
                    }
                }
                else
                {
                    if (father != null)
                    {
                        if (father.diseases.FindAll(search).Count > 0)
                        {
                            prob += 1 * father.diseases.Find(search).calculated_probability;
                        }
                    }
                    if (mother != null)
                    {
                        if (mother.diseases.FindAll(search).Count > 0)
                        {
                            prob += 0.5f * mother.diseases.Find(search).calculated_probability;
                        }
                    }
                }
                
                if (prob > 0.0001f)
                {
                    if (prob > 1)
                    {
                        prob = 0.99f;
                    }
                    diseases.Add(new PersonalDisease(Hemophilia.GetHemophiliaInstance(), prob));
                }
            }
        }

        // временное решение как затычка
        // для проверки работоспособности
        public bool SearchHemophilia()
        {
            return diseases.Count > 0;
        }

        // временное решение как затычка
        // для проверки работоспособности
        public void AddHemophilia()
        {
            if (SearchHemophilia())
                throw new Exception("Болезнь уже добавлена");

            // 100%?
            diseases.Add(new PersonalDisease(Hemophilia.GetHemophiliaInstance(), true));
        }
        
        // временное решение как затычка
        // для проверки работоспособности
        public void RemoveHemophilia()
        {
            if (!SearchHemophilia())
                throw new Exception("Болезнь не найдена");

            // 100%?
            diseases.Clear();
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
