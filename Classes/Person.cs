using DiseaseCalculator.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiseaseCalculator.Classes
{
    class Person//rewrite to work with any disease//pack into functions things in main file
    {

        public readonly bool gender;//true = male
        public readonly string name;
        public Person? mother;
        public Person? father;
        public List<PersonalDisease> diseases1 = new List<PersonalDisease>();//chromosome 1
        public List<PersonalDisease> diseases2 = new List<PersonalDisease>();//chromosome 2

        public Person(string name, bool gender)
        {
            this.name = name;
            this.gender = gender;
        }

        public void Calculate() //-------------------------------------------------------main logic------------------------------------------------------//
        {
            float mutation_prob = 0.00002f;//спонтанная мутация 0.00002% по Холдейну
            float prob = 0.0f;
            Random rnd = new Random(10);
            //make a loop through all diseases
            Predicate<PersonalDisease> search = x => x.Equals(new PersonalDisease(Hemophilia.GetHemophiliaInstance(), true));

            //Predicate<PersonalDisease>[] predicates = new Predicate<PersonalDisease>[6];
            //predicates[0] = x => x.Equals(new PersonalDisease(Hemophilia.GetHemophiliaInstance(), true));
            //foreach disease in predicates

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
                    /* //try adding prob to both chromos
					diseases1.Add(new PersonalDisease(Hemophilia.GetHemophiliaInstance(), prob / 2));
					diseases2.Add(new PersonalDisease(Hemophilia.GetHemophiliaInstance(), prob / 2));
					*/
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
                    pdiseases += "\n  " + item.ToString() + " (" + item.calculated_probability + ")";
                }
                foreach (var item in diseases2)
                {
                    pdiseases += "\n  " + item.ToString() + " (" + item.calculated_probability + ")";
                }
            }
            else
            {
                if (gender == true)
                {
                    pdiseases = "\n  Здоров";
                }
                else
                {
                    pdiseases = "\n  Здорова";
                }
            }

            return name + " " + (gender ? "M" : "F") + " " + pdiseases;
        }

        public void ChangeName(String s)
        {
            name = s;
        }

        // для проверки работоспособности
        public bool SearchHemophilia()
        {
            Predicate<PersonalDisease> search = x => x.Equals(new PersonalDisease(Hemophilia.GetHemophiliaInstance(), true));
            return diseases.FindAll(search).Count > 0;
        }

        // для проверки работоспособности
        public void AddHemophilia()
        {
            if (SearchHemophilia())
                throw new Exception("Болезнь уже добавлена");

            // 100%?
            diseases1.Add(new PersonalDisease(Hemophilia.GetHemophiliaInstance(), true));
        }

        // для проверки работоспособности
        public void RemoveHemophilia()
        {
            if (!SearchHemophilia())
                Console.WriteLine("Нет болезни");
                //throw new Exception("Болезнь не найдена");

            // 100%?
            diseases1.Clear();///------
            diseases2.Clear();///------
        }
    }
}