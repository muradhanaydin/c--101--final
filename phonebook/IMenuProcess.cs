using System;

namespace phonebook{
    interface IMenuProcess{
        void MainMenu();
        protected bool AddNewContact(Person p);
        protected bool DeleteContactByNameOrSurname(Person person);
        protected void UpdateContact(string nameOrSurname , string number);
        protected void ListContacts();
        protected void ListContactsByNameOrSurname(string nameOrSurname);
        protected void ListContactsByPhoneNumber(string number);
        protected void SearchInContacts();
    }
}