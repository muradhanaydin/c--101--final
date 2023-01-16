using System;

public class Board
{
    List<Card> root = new List<Card>();
    string title;

    public string Title { get=> this.title; set=> this.title = value; }
    public Board(string title){
        this.title = title;
    }

    public bool Add(Card c){
        try{
            root.Add(c);
            return true;
        }catch(Exception err){
            return false;
        }
    }
    public bool Remove(Card c){
        try{
            for(int i=0;i<root.Count;i++){
                if(root[i].Title == c.Title){
                    root.Remove(root[i]);
                }
            }
            return true;
        }catch(Exception err){
            Console.WriteLine(err.Message);
            return false;
        }
    }
    
    public Card GetCardByTitle(string title){
        foreach(var item in root){
            if(item.Title == title){
                return item;
            }
        }
        return null;
    }
    public bool CheckCard(string title){
        foreach(var card in root){
            if(card.Title == title){
                return true;
            }
        }
        return false;
    }
    public void List(){
        Console.Write($"\n\n{this.title} Line\n************************");
        if(root.Count > 0)
            foreach(Card card in root){
                Console.Write($"\nBaşlık      : {card.Title}\n");
                Console.Write($"İçerik      : {card.Content}\n");
                Console.Write($"Atanan Kişi : {card.AppointedPerson}\n");
                Console.Write($"Büyüklük    : {GetSizeWithText(card.Size)}\n");
                if(card != root.Last()){
                    Console.Write($"\n-\n");
                }
            }
        else Console.WriteLine("\n\n~ BOŞ ~");
    }
    public void PrintCardData(Card c , string title){
        Console.WriteLine($"Bulunan Kart Bilgileri:\n**************************************\nBaşlık      : {c.Title}\nİçerik      : {c.Content}\nAtanan Kişi : {c.AppointedPerson}\nBüyüklük    : {GetSizeWithText(c.Size)}\nLine        : {title}\n");
    }
    string GetSizeWithText(int idx){
        switch(idx){
            case (int)CardSize.XS:{
                return "XS";
            }
            case (int)CardSize.S:{
                return "S";
            }
            case (int)CardSize.M:{
                return "M";
            }
            case (int)CardSize.L:{
                return "L";
            }
            case (int)CardSize.XL:{
                return "XL";
            }
            default:{
                return "null";
            }
        }
    }
}
enum CardSize{
    XS=1,
    S,
    M,
    L,
    XL
}

