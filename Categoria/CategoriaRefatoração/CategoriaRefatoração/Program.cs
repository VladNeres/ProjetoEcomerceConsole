using CategoriaRefatoracao;
using System;

public class Program
{
    static void Main(String[] args)
    {
        try
        {
            Menu menu = new Menu();
            menu.ExbirMenu();

        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message); 
        }


    }
}