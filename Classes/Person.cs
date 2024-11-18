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
        public List<PersonalDisease> diseases = new List<PersonalDisease>();

        public Person(string name, bool gender)
        {
            this.name = name;
            this.gender = gender;
        }
    }
}
