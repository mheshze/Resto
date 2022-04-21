//
// namespace Resto;
// public class getMenu
// {
//     public void Menus(int a)
//     {
//         switch (a)
//         {
//             case 1 :
//                 Console.WriteLine("---------REST 1 MENU--------");
//                 break;
//             default :
//                 Console.WriteLine("Invalid option");
//                 break;
//         }
//     }
// }
// public class Select
// {
//     public int Restaurant()
//     {
//         int n = 4;
//         Console.WriteLine("1. Chinese Restaurant\n2. Indian Restaurent\n3.Italian Restaurant\n4.Mexican Restaurant\n");
//         Console.WriteLine("Enter the option : ");
//         int option = Convert.ToInt32(Console.Read());
//         return option;
//     }
// }
// public class FacadeClass
// {
//     getMenu menu;
//     Select rest;
//     public FacadeClass()
//     {
//         menu = new getMenu();
//         rest = new Select();
//     }
//     int choice = 0;
//     public void Select_rest()
//     {
//         Console.WriteLine("------- LIST OF RESTAURANTS ------");
//         choice = rest.Restaurant();
//     }
//     public void Showmenu()
//     {
//         Console.WriteLine("---SHOWING MENU----");
//         menu.Menus(choice);
//     }
// }

namespace Resto;

public class RestrauntDetails
{
    public void createRestraunt() // back end entry about new restaurant
    {
        Console.WriteLine("Entered details about the restaurant...");
        Console.WriteLine("Created Restaurant!!");
    }

    public void createMenu()
    {
        Console.WriteLine("Menu details entered...");
        Console.WriteLine("Created Menu for the restaurant!!");
    }
}

public class ShopDetails
{
    public void createShop() // back end entry about new shop
    {
        Console.WriteLine("Entered details about the Shop...");
        Console.WriteLine("Created Shop!!");
    }
    public void createItems()
    {
        Console.WriteLine("Item details entered...");
        Console.WriteLine("Created list of items for the shop!!");
    }
}

public class RestoFacade
{
    private RestrauntDetails rd;
    private ShopDetails sd;

    public RestoFacade()
    {
        rd = new RestrauntDetails();
        sd = new ShopDetails();
    }

    public void CreateRestaurantOnly()
    {
        Console.WriteLine("\n");
        rd.createRestraunt();
        rd.createMenu();
    }

    public void CreateShopOnly()
    {
        Console.WriteLine("\n");
        sd.createShop();
        sd.createItems();
    }

    public void CreateRestaurantWithShop()
    {
        Console.WriteLine("\n");
        rd.createRestraunt();
        rd.createMenu();
        Console.WriteLine("Restaurant contains shop!!");
        sd.createItems();
    }

    public void CreateShopWithRestaurant()
    {
        Console.WriteLine("\n");
        sd.createShop();
        sd.createItems();
        Console.WriteLine("Shop provides food services too...");
        rd.createMenu();
    }
}
