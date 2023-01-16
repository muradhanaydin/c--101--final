using System;

public class Card{
    string title;
    string content;
    int appointedPerson;
    int size; 

    public string Title { get => this.title; set=> this.title = value; }
    public string Content { get=> this.content; set=> this.content = value; }
    public int AppointedPerson { get=> this.appointedPerson; set=> this.appointedPerson = value; }
    public int Size { get=> this.size; set=> this.size = value;} 

    public Card(string title , string content , int appointedPerson , int size){
        this.title = title;
        this.content = content;
        this.size = size;
        this.appointedPerson = appointedPerson;
    }

}
