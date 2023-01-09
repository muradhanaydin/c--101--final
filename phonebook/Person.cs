using System;

namespace phonebook{
    public class Person{
        string name , surname ,number;

        public Person(string name , string surname , string number){
            Number = number;
            Name = name;
            Surname = surname;
        }
        public string Number {get=> this.number; set=> this.number = value;}
        public string  Name {get=> this.name; set=> this.name = value;}
        public string Surname {get=> this.surname; set=> this.surname = value;}
    }
}