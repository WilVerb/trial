using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace WebAppEFsql.Models
{
    public class PersonsUtil
    {
        private myDBEntities dbcontext;
        public PersonsUtil()
        {
            dbcontext = new myDBEntities();
        }
        public class PersonVM
        {
            public string FName;
            public string LName;
            public int? Age;
            //public string Address;
        }
        public class PersonVMa
        {
            public string FName;
            public string LName;
            public int? Age;
            public string Address;
        }

        public List<PersonVMa> GetPeople(string city)
        {
            var query =
            from persons in dbcontext.People
            where persons.City == city
            select new PersonVMa { FName = persons.FirstName, LName = persons.LastName
            , Age = persons.Age
            , Address = persons.Address
            };
            return query.ToList<PersonVMa>();
        }

        public List<PersonVM> GetPeople(string city, int age)
        {
            int age1, age2;
            age1 = age - 1;
            age2 = age + 1;
            var query =
            from persons in dbcontext.People
            where persons.City == city && persons.Age>=age1 && persons.Age<=age2
            select new PersonVM { FName = persons.FirstName, LName = persons.LastName,  Age = persons.Age
            };
            return query.ToList<PersonVM>();
        }

        public List<PersonVMa> GetPeopleAndAddress(string city, int age)
        {
            int age1, age2;
            age1 = age - 1;
            age2 = age + 1;
            var query =
            from persons in dbcontext.People
            where persons.City == city && persons.Age >= age1 && persons.Age <= age2
            select new PersonVMa
            {
                FName = persons.FirstName,
                LName = persons.LastName,
                Age = persons.Age,
                Address = persons.Address
            };
            return query.ToList<PersonVMa>();
        }

    }

}