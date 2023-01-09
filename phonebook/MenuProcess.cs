using System;

namespace phonebook{
    public class Menu : IMenuProcess{
        List<Person> Contacts = new List<Person>();

        public void MainMenu(){
            string selection;
            while(true){
                Console.WriteLine("\n==========> ANA MENU <==========\n\n(1) Yeni Numara Kaydetmek\n(2) Varolan Numarayı Silmek\n(3) Varolan Numarayı Güncelleme\n(4) Rehberi Listelemek\n(5) Rehberde Arama Yapmak﻿\n");
                string process = Console.ReadLine();
                switch(process){
                    case "1":{
                        string name , surname , number;
                        Console.WriteLine("\n==========> REHBERE NUMARA EKLEME <==========");
                        NameControl:   
                        Console.Write("\nLütfen isim giriniz             : ");
                        if(!string.IsNullOrEmpty(name = Console.ReadLine())){
                            SurnameControl:
                            Console.Write("\nLütfen soyisim giriniz          : ");
                            if(!string.IsNullOrEmpty(surname = Console.ReadLine())){
                                NumberControl:
                                Console.Write("\nLütfen telefon numarası giriniz : ");
                                if(!string.IsNullOrEmpty(number = Console.ReadLine())){
                                    if(AddNewContact(new Person(name.ToUpper() , surname.ToUpper() , number.ToUpper()))){
                                        Console.WriteLine("\n =====> Kisi Basariyla Kaydedildi!\n\n* Ana Menuye Don (1)\n* Yeni Kayit Ekle (2)");
                                        Selection:
                                        selection = Console.ReadLine();
                                        if(string.IsNullOrEmpty(selection)){
                                            goto Selection;
                                        }else{
                                            if(selection == "1"){
                                                MainMenu();
                                            }else if(selection == "2"){
                                                goto NameControl;
                                            }else{
                                                goto Selection;
                                            }
                                        }
                                    }else{
                                        Console.WriteLine("\n =====> Kisi Kaydedilemedi!\n\n* Ana Menuye Don (1)\n* Tekrar Dene (2)");
                                        Selection:
                                        selection = Console.ReadLine();
                                        if(string.IsNullOrEmpty(selection)){
                                            goto Selection;
                                        }else{
                                            if(selection == "1"){
                                                MainMenu();
                                            }else if(selection == "2"){
                                                goto NameControl;
                                            }else{
                                                goto Selection;
                                            }
                                        }
                                    }
                                }else{
                                    goto NumberControl;
                                }
                            }else{
                                goto SurnameControl;
                            }
                        }else{
                            goto NameControl;
                        }
                        break;
                    }
                    case "2":{
                        Delete:
                        Console.WriteLine("\n==========> REHBERDEN NUMARA SILME <==========\n");
                        Console.Write($"Lütfen numarasını silmek istediğiniz kişinin adını ya da soyadını giriniz: ");
                        string val = Console.ReadLine();
                        Person person = null;
                        if(GetPersonByNameOrSurname(val.ToUpper()) is null){
                            Selection:
                            Console.WriteLine("Aradığınız krtiterlere uygun veri rehberde bulunamadı. Lütfen bir seçim yapınız.\n* Silmeyi sonlandırmak için : (1)\n* Yeniden denemek için      : (2)");
                            selection = Console.ReadLine();
                            switch(selection){
                                case "1":{
                                    MainMenu();
                                    break;
                                }
                                case "2":{
                                    goto Delete;
                                    break;
                                }
                                default:{
                                    Console.WriteLine("Gecersiz islem girdiniz!");
                                    goto Selection;
                                    break;
                                }
                            }
                        }else{
                            person = GetPersonByNameOrSurname(val.ToUpper());
                            Confirmation:
                            Console.WriteLine($"`{person.Name}` isimli kişi rehberden silinmek üzere, onaylıyor musunuz ?(y/n)");
                            selection = Console.ReadLine();
                            switch(selection){
                                case "y":{
                                    bool status = DeleteContactByNameOrSurname(person);
                                    if(status){
                                        Console.WriteLine($"\n ====> {person.Name} Basariyla silindi!\n\n");
                                    }else{
                                        Console.WriteLine($"\n ====> {person.Name} Basariyla silinemedi!\n\n");
                                    }
                                    MainMenu();
                                    break;
                                }
                                case "n":{
                                    Selection:
                                    Console.WriteLine("\n\n* Ana Menuye Don (1)\n* Tekrar Dene (2)");
                                    selection = Console.ReadLine();
                                    if(string.IsNullOrEmpty(selection)){
                                        goto Selection;
                                    }else{
                                        if(selection == "1"){
                                            MainMenu();
                                        }else if(selection == "2"){
                                            goto Delete;
                                        }else{
                                            goto Selection;
                                        }
                                    }
                                    break;
                                }
                                default:{
                                    goto Confirmation;
                                    break;
                                }
                            }
                        }
                        break;
                    }
                    case "3":{
                        Console.WriteLine("\n==========> NUMARA GUNCELLEME <==========\n");
                        Update:
                        Console.Write("\nLütfen numarasını silmek istediğiniz kişinin adını ya da soyadını giriniz:");
                        string nameOrSurname = Console.ReadLine().ToUpper();
                        Person person = GetPersonByNameOrSurname(nameOrSurname);
                        if(person is null){
                            Selection:
                            Console.WriteLine("Aradığınız krtiterlere uygun veri rehberde bulunamadı. Lütfen bir seçim yapınız.\n* Güncellemeyi sonlandırmak için    : (1)\n* Yeniden denemek için              : (2)");
                            selection = Console.ReadLine();
                            switch(selection){
                                case "1":{
                                    MainMenu();
                                    break;
                                }
                                case "2":{
                                    goto Update;
                                    break;
                                }
                                default:{
                                    Console.WriteLine("Gecersiz islem girdiniz!");
                                    goto Selection;
                                    break;
                                }
                            }
                        }else{
                            ReplyUpdate:
                            Console.Write("Numara Giriniz: ");
                            string number = Console.ReadLine();
                            UpdateContact(nameOrSurname , number);
                            if(person.Number == number){
                                Console.WriteLine($"\n ====> `{person.Name}` adli kullanicinin numarasi => {person.Number} olarak guncellendi!\n\n* Ana Menuye Don (1)\n* Tekrar guncelle (2)");
                                Selection:
                                selection = Console.ReadLine();
                                if(string.IsNullOrEmpty(selection)){
                                    goto Selection;
                                }else{
                                    if(selection == "1"){
                                        MainMenu();
                                    }else if(selection == "2"){
                                        goto ReplyUpdate;
                                    }else{
                                        goto Selection;
                                    }
                                }
                            }
                           
                        }

                        break;
                    }
                    case "4":{
                        ListContacts();
                        break;
                    }
                    case "5":{
                        Console.WriteLine("\n==========> REHBERDEN ARAMA <==========\n");
                        Confirmation:
                        Console.WriteLine("Arama yapmak istediğiniz tipi seçiniz.\n**********************************************\n\nİsim veya soyisime göre arama yapmak için: (1)\nTelefon numarasına göre arama yapmak için: (2)\n");
                        string searchType = Console.ReadLine();
                        switch(searchType){
                            case "1":{
                                Console.Write("\nIsim Yada Soyisim Giriniz: ");
                                string nameOrSurname = Console.ReadLine().ToUpper();
                                ListContactsByNameOrSurname(nameOrSurname);
                                break;
                            }
                            case "2":{
                                Console.Write("\nTelefon Numarasi Giriniz: ");
                                string phoneNumber = Console.ReadLine();
                                ListContactsByPhoneNumber(phoneNumber);
                                break;
                            }
                            default:{
                                goto Confirmation;
                                break;
                            }
                        }
                        break;
                    }
                    default:{
                        Console.WriteLine("\n *** Gecersiz islem girdiniz ***");
                        break;
                    }
                }
            }
        }
        
        private bool CheckPersonData(string name , string surname , string number){
            return string.IsNullOrEmpty(name)?false:string.IsNullOrEmpty(surname)?false:string.IsNullOrEmpty(number)?false:true;
        }
        private Person GetPersonByNameOrSurname(string val){
            foreach (var contact in Contacts)
            {
                if(contact.Name  == val || contact.Surname == val){
                   return contact;
                }
            }
            return null;
        }

        public bool AddNewContact(Person p){
            try{
                Contacts.Add(p);
                return true;
            }catch(Exception err){
                return false;
            }
        }

        public bool DeleteContactByNameOrSurname(Person person){
            try{
                Contacts.Remove(person);
                return true;
            }catch(Exception err){
                return false;
            }
            
        }
        public void UpdateContact(string nameOrSurname , string number){
            foreach(var person in Contacts){
                if(person.Name == nameOrSurname || person.Surname == nameOrSurname){
                    person.Number = number;
                    break;
                }
            }
        }
        public void ListContacts(){
            Console.WriteLine("\n==========> TELEFON REHBER LISTESI <==========\n");
            foreach (var person in Contacts)
            {
                Console.WriteLine($"isim: {person.Name}\nSoyisim: {person.Surname}\nTelefon Numarası: {person.Number}\n-\n");
            }
            ReturnHomeSelection();
        }
        public void ListContactsByNameOrSurname(string nameOrSurname){
            Console.WriteLine("");
            foreach (var person in Contacts)
            {
                if(person.Name == nameOrSurname || person.Surname == nameOrSurname){
                    Console.WriteLine($"isim: {person.Name}\nSoyisim: {person.Surname}\nTelefon Numarası: {person.Number}\n-\n");
                }
            }
            ReturnHomeSelection();
        }
        public void ListContactsByPhoneNumber(string number){
            Console.WriteLine("");
            foreach (var person in Contacts)
            {
                if(person.Number == number){
                    Console.WriteLine($"isim: {person.Name}\nSoyisim: {person.Surname}\nTelefon Numarası: {person.Number}\n-\n");
                }
            }
            ReturnHomeSelection();
        }
        public void SearchInContacts(){
            throw new Exception("HATRA");
        }

        private void ReturnHomeSelection(){
            Console.WriteLine("* Ana Menuye Don (1)");
            Selection:
            string selection = Console.ReadLine();
            if(string.IsNullOrEmpty(selection)){
                goto Selection;
            }else{
                if(selection == "1"){
                    MainMenu();
                }else{
                    goto Selection;
                }
            }
        }
        
        
    }
}