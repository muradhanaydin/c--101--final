using System;

namespace todoapp{
    internal class Program{
       static Dictionary<int , string> users = new Dictionary<int, string>();
        static Board todo = new Board("TODO"), inProgress , done;

        public Program(){
            if(users.Count == 0){
                users.Add(234124 , "MURADHAN");
                users.Add(421312 , "NERIMAN");
                users.Add(412334 , "SELIM");
                users.Add(313123 , "FATMA");
                users.Add(789773 , "SELDA");
            }
        }
        
        public static void Main(){
            Console.Clear();
            Program app = new Program();
            Console.Write("Lütfen yapmak istediğiniz işlemi seçiniz :) \n*******************************************\n\n");
            Console.Write("(1) Board listelemek\n(2) Board'a Kart Eklemek\n(3) Board'dan Kart Silmek\n(4) Kart Taşımak\n(5) Cikis\n");
            int selection;
            while(int.TryParse(Console.ReadLine() ,out selection)){
                switch(selection){
                    case 1:{
                        app.ListBoard();
                        break;
                    }
                    case 2:{
                        app.AddCard();
                        break;
                    }
                    case 3:{
                        app.RemoveCard();
                        break;
                    }
                    case 4:{
                        app.MoveCard();
                        break;
                    }
                    case 5:{
                        Environment.Exit(0);
                        break;
                    }
                    default:{
                        Console.Write("GECERSIZ ISLEM SECTINIZ!\n");
                        break;
                    }
                }
            }
        }

        void AddCard(){
            Console.Clear();
            string title , content;
            int size , appointedPerson;
            CheckTitle:
            Console.Write("Başlık Giriniz                                  : ");
            title = Console.ReadLine();
            if(string.IsNullOrEmpty(title)){
                Console.WriteLine("\n --------> Baslik Bos Olamaz!\n");
                goto CheckTitle;
            }else{
                CheckContent:
                Console.Write("İçerik Giriniz                                  : ");
                content = Console.ReadLine();
                if(string.IsNullOrEmpty(content)){
                    Console.WriteLine("\n --------> Icerik Bos Olamaz!\n");
                    goto CheckContent;
                }else{
                    CheckSize:
                    Console.Write("Büyüklük Seçiniz -> XS(1),S(2),M(3),L(4),XL(5)  : ");
                    size = int.Parse(Console.ReadLine());
                    if(size >=1 || size <=5){
                        CheckUser:
                        Console.Write("Kişi Seçiniz                                    : ");
                        appointedPerson = CheckUserWithName(Console.ReadLine());
                        if(appointedPerson == 0){
                            Console.WriteLine("\n --------> Kullanici Bulunamadi.\n");
                            goto CheckUser;
                        }else{
                            if(todo.Add(new Card(title , content , appointedPerson, size))){
                            Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write("\nEKLEME BASARILI");
                            }else{
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("\nX EKLEME BASARISIZ");
                            }
                            Console.ForegroundColor = ConsoleColor.White;
                            ReturnHome();
                        }
                    }else{
                        Console.WriteLine("\nBoyut 1-5 (dahil) arasinda olmalidir!");
                        goto CheckSize;
                    }
                }
            }
        }
        
        void RemoveCard(){
            Console.Clear();
            Console.Write("Öncelikle silmek istediğiniz kartı seçmeniz gerekiyor.\nLütfen kart başlığını yazınız: ");
            string cardTitle = Console.ReadLine();
            Card card = todo.GetCardByTitle(cardTitle);
            if(card is null){
                Console.WriteLine("Aradığınız krtiterlere uygun kart board'da bulunamadı. Lütfen bir seçim yapınız.\n* İşlemi sonlandırmak için : (1)\n* Yeniden denemek için : (2)");
                int selection = int.Parse(Console.ReadLine());
                if(selection == 1){
                    Main();
                }else{
                    RemoveCard();
                }

            }else{
                bool status = todo.Remove(card);
                if(status){
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("KART SILINDI!\n");
                }else{
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("KART SILINEMEDI!\n");
                }
                ReturnHome();
            }
        }

        void MoveCard(){
            Console.Write("Öncelikle tasimak istediğiniz kartı seçmeniz gerekiyor.\nLütfen kart başlığını yazınız:");
            string title = Console.ReadLine();
            if(todo.CheckCard(title)){
                Card card =  todo.GetCardByTitle(title);
                todo.PrintCardData(card , todo.Title);
                Console.WriteLine("Lütfen taşımak istediğiniz Line'ı seçiniz:\n(1) IN PROGRESS\n(2) DONE\n");
                int selection = int.Parse(Console.ReadLine());
                if(selection == 1){
                    todo.Remove(card);
                    if(inProgress is object){
                        inProgress.Add(card);
                    }else{
                        inProgress = new Board("IN PROGRESS");
                        inProgress.Add(card);
                    }
                    ReturnHome();
                }else if(selection == 2){
                    todo.Remove(card);
                    if(done is object){
                        done.Add(card);
                    }else{
                        done = new Board("DONE");
                        done.Add(card);
                    }
                    ReturnHome();                
                }else{
                    MoveCard();
                }
            }else if(inProgress is object && inProgress.CheckCard(title)){
                Card card =  todo.GetCardByTitle(title);
                todo.PrintCardData(card , todo.Title);
                Console.WriteLine("Lütfen taşımak istediğiniz Line'ı seçiniz:\n(1) TODO\n(2) DONE\n");
                int selection = int.Parse(Console.ReadLine());
                if(selection == 1){
                    inProgress.Remove(card);
                    todo.Add(card);
                    ReturnHome();
                }else if(selection == 2){
                    inProgress.Remove(card);
                    if(done is object){
                        done.Add(card);
                    }else{
                        done = new Board("DONE");
                        done.Add(card);
                    }
                    ReturnHome();
                }else{
                    MoveCard();
                }
            }else if(done is object && done.CheckCard(title)){
                Card card =  todo.GetCardByTitle(title);
                todo.PrintCardData(card , todo.Title);
                Console.WriteLine("Lütfen taşımak istediğiniz Line'ı seçiniz:\n(1) TODO\n(2) IN PROGRESS\n");
                int selection = int.Parse(Console.ReadLine());
                if(selection == 1){
                    done.Remove(card);
                    todo.Add(card);
                    ReturnHome();
                }else if(selection == 2){
                    done.Remove(card);
                    if(inProgress is object){
                        inProgress.Add(card);
                    }else{
                        inProgress = new Board("IN PROGRESS");
                        inProgress.Add(card);
                    }
                    ReturnHome();
                }else{
                    MoveCard();
                }
            }else{
                TryMore:
                Console.Write("GECERSIZ ISLEM GIRILDI!\n(1) Tekrar Dene\n(2) Ana Menu");
                int selection = int.Parse(Console.ReadLine());
                if(selection == 1){
                    MoveCard();
                }else if(selection == 2){
                    Main();
                }else{
                    goto TryMore;
                }
                
            }
            
        }

        void ListBoard(){
            todo.List();
            if(inProgress is object){
                inProgress.List();
            }else{
                inProgress = new Board("IN PROGRESS");
                inProgress.List();
            }
            if(done is object){
                done.List();
            }else{
                done = new Board("DONE");
                done.List();
            }
            ReturnHome();
        }

        void ReturnHome(){
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\n\n(1) Ana Menuye Don\n");
            int selection = int.Parse(Console.ReadLine());
            if(selection == 1){
                Main();
            }else{
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n!-- Gecersiz Islem Sectiniz!\n");
                ReturnHome();
            }
        }

        int CheckUserWithName(string name){
            foreach(var user in users){
                if(user.Value == name){
                    return user.Key;
                }
            }
            return 0;
        }
    }
}