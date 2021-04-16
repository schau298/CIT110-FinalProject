using System;
using System.Collections.Generic;
using System.IO;

using System.Linq;

namespace FinalProject_GroceryList
{

    public enum Product
    {
        
        Milk = 1,
        DozenEggs,
        Avocado,
        ChickenNuggets,
        Soda = 5,
        PepperoniPizza,
        IceCream,
        Popcorn = 8,
        Beef,
        Salmon = 10,
        
    }

    // *************************************************************
    // Application:     The Grocery Store
    // Author:          Schaub, Dylan
    // Description:     This application adds up the cost of all the items and gives the user a Grand Total with Tax.
    // Date Created:    4/1/2021
    // Date Revised:    4/15/2021
    // *************************************************************

    public class Program
    {
        /// <summary>
        /// first method run when the app starts up
        /// </summary>
        /// <param name="args"></param>

        
    
        


        static void Main(string[] args)
        {

            SetTheme();
            DisplayWelcomeScreen();

            DisplayLoginRegister();
            DisplayMenuScreen();

            DisplayClosingScreen();
        }


        /// console theme


        static void SetTheme()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.BackgroundColor = ConsoleColor.White;
        }


        static string userName;

        /// <summary>
        /// ****************
        /// *   Main Menu  *          
        /// ****************
        /// </summary>

        static void DisplayMenuScreen()
        {
            Console.CursorVisible = true;
            
            bool quitApplication = false;
            string menuChoice;
            do
            {
                DisplayScreenHeader("Main Menu");
                //
                //
                Console.WriteLine("\ta) Shop for food");
                Console.WriteLine("\tq) Quit");
                Console.Write("\t\tEnter Choice:");

                menuChoice = Console.ReadLine().ToLower();

                Product allProducts = default;
                //
                // process user menu choice
                //
                switch (menuChoice)
                {

                    case "a":
                        GrocerySelectionScreen(allProducts);
                        break;

                    case "q":
                        quitApplication = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitApplication);
        }

        #region LOGIN REGISTER
        /// <summary>
        /// *****************************************************************
        /// *                 Login OR Register Screen                      *
        /// *****************************************************************
        /// </summary>
        static void DisplayLoginRegister()
        {
            DisplayScreenHeader("Login/Register");

            Console.Write("\tAre you a registered user [ yes | no ]?");
            if (Console.ReadLine().ToLower() == "yes")
            {
                DisplayLogin();
            }
            else
            {
                DisplayRegisterUser();
                DisplayLogin();
            }
        }

        /// <summary>
        /// *****************************************************************
        /// *                          Login Screen                         *
        /// *****************************************************************
        /// </summary>
        static void DisplayLogin()
        {
            //string userName;
            string password;

            bool validLogin;

            do
            {
                DisplayScreenHeader("Login");

                Console.WriteLine();
                Console.Write("\tEnter your user name:");
                userName = Console.ReadLine();
                Console.Write("\tEnter your password:");
                password = Console.ReadLine();

                validLogin = IsValidLoginInfo(userName, password);

                Console.WriteLine();
                if (validLogin)
                {
                    Console.WriteLine("\tYou are now logged in.");
                }
                else
                {
                    Console.WriteLine("\tIt appears either the user name or password is incorrect.");
                    Console.WriteLine("\tPlease try again.");
                }

                DisplayContinuePrompt();
            } while (!validLogin);
        }

        /// <summary>
        /// check user login
        /// </summary>
        /// <param name="userName">user name entered</param>
        /// <param name="password">password entered</param>
        /// <returns>true if valid user</returns>
        static bool IsValidLoginInfo(string userName, string password)
        {
            List<(string userName, string password)> registeredUserLoginInfo = new List<(string userName, string password)>();
            bool validUser = false;

            registeredUserLoginInfo = ReadLoginInfoData();


            foreach ((string userName, string password) userLoginInfo in registeredUserLoginInfo)
            {
                if ((userLoginInfo.userName == userName) && (userLoginInfo.password == password))
                {
                    validUser = true;
                    break;
                }
            }

            return validUser;
        }

        /// <summary>
        /// *****************************************************************
        /// *                       Register Screen                         *
        /// *****************************************************************
        /// </summary>
        static void DisplayRegisterUser()
        {
            string userName;
            string password;

            DisplayScreenHeader("Register");

            Console.Write("\tEnter your user name:");
            userName = Console.ReadLine();
            Console.Write("\tEnter your password:");
            password = Console.ReadLine();

            WriteLoginInfoData(userName, password);

            Console.WriteLine();
            Console.WriteLine("\tYou entered the following information and it has be saved.");
            Console.WriteLine($"\tUser name: {userName}");
            Console.WriteLine($"\tPassword: {password}");

            DisplayContinuePrompt();
        }


        static List<(string userName, string password)> ReadLoginInfoData()
        {
            string dataPath = @"Data/UserData.txt";

            string[] loginInfoArray;
            (string userName, string password) loginInfoTuple;

            List<(string userName, string password)> registeredUserLoginInfo = new List<(string userName, string password)>();

            loginInfoArray = File.ReadAllLines(dataPath);


            foreach (string loginInfoText in loginInfoArray)
            {

                loginInfoArray = loginInfoText.Split(',');

                loginInfoTuple.userName = loginInfoArray[0];
                loginInfoTuple.password = loginInfoArray[1];
                registeredUserLoginInfo.Add(loginInfoTuple);

            }

            return registeredUserLoginInfo;
        }


        static void WriteLoginInfoData(string userName, string password)
        {
            string dataPath = @"Data/UserData.txt";
            string loginInfoText;

            loginInfoText = userName + "," + password + "," + "\n";


            File.AppendAllText(dataPath, loginInfoText);
        }


        #endregion LOGIN REGISTER




        #region PLACING ORDER

        ///<summary>
        ///**********************
        ///*    Store Menu      *
        ///**********************
        ///</summary>

        static void GrocerySelectionScreen(Product allProducts)
        {
            Console.Clear();
            Console.CursorVisible = true;
            string menuChoice;
            bool quitMenu = false;

            List<Product> Products = new List<Product>();


            do
            {


                DisplayScreenHeader("Get Groceries Now");
                //
                // menu choices
                //


                Console.WriteLine("\ta) View Products");
                Console.WriteLine("\tb) Buy Products");
                Console.WriteLine("\tq) Stop Shopping");
                Console.Write("\t\t Enter Choice:");
                menuChoice = Console.ReadLine().ToLower();
                switch (menuChoice)
                {

                    case "a":
                        DisplayGroceryStoreProducts(Products);
                        break;

                    case "b":
                        SelectProducts(Products);
                        break;


                    case "q":
                        quitMenu = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }
            } while (!quitMenu);

        }



        /// <summary>
        /// ************************************************
        /// *            Display Products                  *
        /// ************************************************
        /// </summary>
        /// <param name="display Products"></param>


        static void DisplayGroceryStoreProducts(List<Product> products)
        {
            DisplayScreenHeader("The Grocery Store's Top Products");
            int ProductCount = 1;
            foreach (string ProductName in Enum.GetNames(typeof(Product)))
            {
                Console.Write($"- {ProductName.ToLower()}  -");
                if (ProductCount % 5 == 0) Console.Write("-\n\t-");
                ProductCount++;
            }

            DisplayMenuPrompt("Order Products");
        }


        /// <summary>
        /// ************************************************
        /// *          Get Products From User              *
        /// ************************************************
        /// </summary>
        /// <param name="Products"></param>


   
        



        public static void SelectProducts(List<Product> products)
        {
         int itemQuantity1;
         int itemQuantity2;
         int itemQuantity3;
        int itemQuantity4;
        int itemQuantity5;
         int itemQuantity6;
         int itemQuantity7;
         int itemQuantity8;
        int itemQuantity9;
         int itemQuantity10;
            
                for (itemQuantity1 = 0; itemQuantity1 < 1; itemQuantity1++)
                {
                    Console.WriteLine("------------------------------------------------------------");
                    Console.WriteLine("\n\nEnter Gallons of Milk Needed:   ");
                    itemQuantity1 = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("\n\n\t ******************************");
                    Console.WriteLine("\tGallons Milk ordered: " + itemQuantity1);
                    Console.WriteLine("\t ******************************");
                    Console.WriteLine("\n\t ******************************");
                    Console.WriteLine("\tCost:  $" + (itemQuantity1 * 1));
                    Console.WriteLine("\t ******************************");

                }
                for (itemQuantity2 = 0; itemQuantity2 < 1; itemQuantity2++)
                {
                    Console.WriteLine("------------------------------------------------------------");
                    Console.WriteLine("\n\nEnter how many Dozen Eggs you Need:  ");
                    itemQuantity2 = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("\n\n\t ******************************");
                    Console.WriteLine("\tDozen Eggs Ordered: " + itemQuantity2);
                    Console.WriteLine("\t ******************************");
                    Console.WriteLine("\n\t ******************************");
                    Console.WriteLine("\tCost:  $" + (itemQuantity2 * 2));
                    Console.WriteLine("\t ******************************");
                }

                for (itemQuantity3 = 0; itemQuantity3 < 1; itemQuantity3++)
                {
                    Console.WriteLine("------------------------------------------------------------");
                    Console.WriteLine("\n\nEnter how many Avocados you Need:  ");
                    itemQuantity3 = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("\n\n\t ******************************");
                    Console.WriteLine("\tAvocados Ordered: " + itemQuantity3);
                    Console.WriteLine("\t ******************************");
                    Console.WriteLine("\n\t ******************************");
                    Console.WriteLine("\tCost:  $" + (itemQuantity3 * 3));
                    Console.WriteLine("\t ******************************");
                }

                for (itemQuantity4 = 0; itemQuantity4 < 1; itemQuantity4++)
                {
                    Console.WriteLine("------------------------------------------------------------");
                    Console.WriteLine("\n\nEnter how many bags of chicken Nuggets you Need:  ");
                    itemQuantity4 = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("\n\n\t ******************************");
                    Console.WriteLine("\tBags of Chicken Nuggets Ordered: " + itemQuantity4);
                    Console.WriteLine("\t ******************************");
                    Console.WriteLine("\n\t ******************************");
                    Console.WriteLine("\tCost:  $" + (itemQuantity4 * 4));
                    Console.WriteLine("\t ******************************");
                }

                for (itemQuantity5 = 0; itemQuantity5 < 1; itemQuantity5++)
                {
                    Console.WriteLine("------------------------------------------------------------");
                    Console.WriteLine("\n\nEnter how many Two Liters of Soda you Need:  ");
                    itemQuantity5 = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("\n\n\t ******************************");
                    Console.WriteLine("\tTwo Liters of Soda Ordered: " + itemQuantity5);
                    Console.WriteLine("\t ******************************");
                    Console.WriteLine("\n\t ******************************");
                    Console.WriteLine("\tCost:  $" + (itemQuantity5 * 5));
                    Console.WriteLine("\t ******************************");
                }

                for (itemQuantity6 = 0; itemQuantity6 < 1; itemQuantity6++)
                {
                    Console.WriteLine("------------------------------------------------------------");
                    Console.WriteLine("\n\nEnter how many Pepperoni Pizzas you Need:  ");
                    itemQuantity6 = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("\n\n\t ******************************");
                    Console.WriteLine("\tPepperoni Pizzas Ordered: " + itemQuantity6);
                    Console.WriteLine("\t ******************************");
                    Console.WriteLine("\n\t ******************************");
                    Console.WriteLine("\tCost:  $" + (itemQuantity6 * 6));
                    Console.WriteLine("\t ******************************");
                }

                for (itemQuantity7 = 0; itemQuantity7 < 1; itemQuantity7++)
                {
                    Console.WriteLine("------------------------------------------------------------");
                    Console.WriteLine("\n\nEnter how many tubs of IceCream do you Need:  ");
                    itemQuantity7 = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("\n\n\t ******************************");
                    Console.WriteLine("\tIceCream Tubs Ordered: " + itemQuantity7);
                    Console.WriteLine("\t ******************************");
                    Console.WriteLine("\n\t ******************************");
                    Console.WriteLine("\tCost:  $" + (itemQuantity7 * 7));
                    Console.WriteLine("\t ******************************");
                }

                for (itemQuantity8 = 0; itemQuantity8 < 1; itemQuantity8++)
                {
                    Console.WriteLine("------------------------------------------------------------");
                    Console.WriteLine("\n\nEnter how many bags of Popcorn you Need:  ");
                    itemQuantity8 = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("\n\n\t ******************************");
                    Console.WriteLine("\tBags of Popcorn Ordered: " + itemQuantity8);
                    Console.WriteLine("\t ******************************");
                    Console.WriteLine("\n\t ******************************");
                    Console.WriteLine("\tCost:  $" + (itemQuantity8 * 8));
                    Console.WriteLine("\t ******************************");
                }

                for (itemQuantity9 = 0; itemQuantity9 < 1; itemQuantity9++)
                {
                    Console.WriteLine("------------------------------------------------------------");
                    Console.WriteLine("\n\nEnter how much pounds of Beef you Need:  ");
                    itemQuantity9 = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("\n\n\t ******************************");
                    Console.WriteLine("\tPound of Beef Ordered: " + itemQuantity9);
                    Console.WriteLine("\t ******************************");
                    Console.WriteLine("\n\t ******************************");
                    Console.WriteLine("\tCost:  $" + (itemQuantity9 * 9));
                    Console.WriteLine("\t ******************************");
                }

                for (itemQuantity10 = 0; itemQuantity10 < 1; itemQuantity10++)
                {
                    Console.WriteLine("------------------------------------------------------------");
                    Console.WriteLine("\n\nEnter how many filets of Salmon you Need:  ");
                    itemQuantity10 = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("\n\n\t ******************************");
                    Console.WriteLine("\tFilets of Salmon Ordered: " + itemQuantity10);
                    Console.WriteLine("\t ******************************");
                    Console.WriteLine("\n\t ******************************");
                    Console.WriteLine("\tCost:  $" + (itemQuantity10 * 10));
                    Console.WriteLine("\t ******************************");
                }
              


            DisplayContinuePrompt();





            /// <summary>
            /// ************************************************
            /// *              Order Products                  *
            /// ************************************************
            /// </summary>
            Console.WriteLine("\n\n*************************************");
            Console.WriteLine("Order Review & Cost Summary:");
            Console.WriteLine();
            Console.WriteLine("\ty - Place Order");
            Console.WriteLine();
            Console.WriteLine("\tn - Cancel Order");
            Console.Write("Choose: ");
            switch (Console.ReadLine())
            {
                case "y":
                    double totalCost = (itemQuantity1 * 1 + itemQuantity2 * 2 + itemQuantity3 * 3 + itemQuantity4 * 4 + itemQuantity5 * 5 + itemQuantity6 * 6 + itemQuantity7 * 7 + itemQuantity8 * 8 + itemQuantity9 * 9 + itemQuantity10 * 10);
                    Console.WriteLine("\n\n\t\t*************************************");
                    Console.WriteLine("\t\t Total Cost  $:" + totalCost);
                    double addTax = (totalCost * 0.06);
                    double CostwithTax = (totalCost + addTax);
                    Console.WriteLine("\t\t Total Cost w/Tax: " + CostwithTax);
                    Console.WriteLine("\n\t\t*************************************");

                    break;
                case "n":
                    Console.WriteLine("Thanks for shopping anyway...");
                    break;
            }
            Console.WriteLine("\n\n\t\t*************************************");
            Console.WriteLine("\n\t\tSuccess! Your order has been placed!");
            Console.WriteLine("\t\tIt will be ready for pick up in 30 minutes...");
            Console.WriteLine("\n\t\t*************************************");
            Console.WriteLine();
            DisplayMenuPrompt("Store Menu");
        }


        #endregion PLACING ORDER
        #region USER INTERFACE

        /// <summary>
        /// *****************************************************************
        /// *                     Welcome Screen                            *
        /// *****************************************************************
        /// </summary>
        static void DisplayWelcomeScreen()
        {
            Console.CursorVisible = false;
            

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tThe Grocery Store");
            Console.WriteLine();

            DisplayContinuePrompt();
        }


        /// <summary>
        /// *****************************************************************
        /// *                     Closing Screen                            *
        /// *****************************************************************
        /// </summary>
        static void DisplayClosingScreen()
        {
            Console.CursorVisible = false;
            
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tThank you for shopping with us!");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display continue prompt
        /// </summary>
        static void DisplayContinuePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("\tPress any key to continue.");
            Console.ReadKey();
        }

        /// <summary>
        /// display menu prompt
        /// </summary>
        static void DisplayMenuPrompt(string menuName)
        {
            Console.WriteLine();
            Console.WriteLine($"\tPress any key to return to the {menuName} Menu.");
            Console.ReadKey();
        }

        /// <summary>
        /// display screen header
        /// </summary>
        static void DisplayScreenHeader(string headerText)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t" + headerText);
            Console.WriteLine();
        }



    }
}

#endregion USER INTERFACE
