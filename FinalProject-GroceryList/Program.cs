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
    // Description:     This application adds up the cost of all the items and gives the user a Grand Total.
    // Date Created:    4/1/2021
    // Date Revised:    4/10/2021
    // *************************************************************

    public class Program
    {
        /// <summary>
        /// first method run when the app starts up
        /// </summary>
        /// <param name="args"></param>

        
        public int itemQuantity1;
        public int itemQuantity2;
        public int itemQuantity3;
        public int itemQuantity4;
        public int itemQuantity5;
        public int itemQuantity6;
        public int itemQuantity7;
        public int itemQuantity8;
        public int itemQuantity9;
        public int itemQuantity10;

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
            var p = new Program();

            for (p.itemQuantity1 = 0; p.itemQuantity1 <1;p.itemQuantity1++)
            {
                Console.WriteLine("\n\nEnter Gallons of Milk Needed:\t");
                p.itemQuantity1 = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("\n\n\t ******************************");
                Console.WriteLine("\tGallons Milk ordered: " + p.itemQuantity1);
                Console.WriteLine("\t ******************************");
                Console.WriteLine("\tCost:  $" + (p.itemQuantity1 * 1));
                

            }
            for (p.itemQuantity2 = 0; p.itemQuantity2 < 1; p.itemQuantity2++)
            {
                Console.WriteLine("\n\nEnter how many Dozen Eggs you Need:\t");
                p.itemQuantity2 = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("\n\n\t ******************************");
                Console.WriteLine("\tDozen Eggs Ordered: " + p.itemQuantity2);
                Console.WriteLine("\t ******************************");
                Console.WriteLine("\tCost:  $" + (p.itemQuantity2 * 2));
            }
            
            for (p.itemQuantity3 = 0; p.itemQuantity3 < 1; p.itemQuantity3++)
            {
                Console.WriteLine("\n\nEnter how many Avocados you Need:\t");
                p.itemQuantity3 = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("\n\n\t ******************************");
                Console.WriteLine("\tAvocados Ordered: " + p.itemQuantity3);
                Console.WriteLine("\t ******************************");
                Console.WriteLine("\tCost:  $" + (p.itemQuantity3 * 3));
            }

            for (p.itemQuantity4 = 0; p.itemQuantity4 < 1; p.itemQuantity4++)
            {
                Console.WriteLine("\n\nEnter how many bags of chicken Nuggets you Need:\t");
                p.itemQuantity4 = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("\n\n\t ******************************");
                Console.WriteLine("\tBags of Chicken Nuggets Ordered: " + p.itemQuantity4);
                Console.WriteLine("\t ******************************");
                Console.WriteLine("\tCost:  $" + (p.itemQuantity4 * 4));
            }
            
            for (p.itemQuantity5 = 0; p.itemQuantity5 < 1; p.itemQuantity5++)
            {
                Console.WriteLine("\n\nEnter how many Two Liters of Soda you Need:\t");
                p.itemQuantity5 = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("\n\n\t ******************************");
                Console.WriteLine("\tTwo Liters of Soda Ordered: " + p.itemQuantity5);
                Console.WriteLine("\t ******************************");
                Console.WriteLine("\tCost:  $" + (p.itemQuantity5* 5));
            }

            for (p.itemQuantity6 = 0; p.itemQuantity6 < 1; p.itemQuantity6++)
            {
                Console.WriteLine("\n\nEnter how many Pepperoni Pizzas you Need:\t");
                p.itemQuantity6 = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("\n\n\t ******************************");
                Console.WriteLine("\tPepperoni Pizzas Ordered: " + p.itemQuantity6);
                Console.WriteLine("\t ******************************");
                Console.WriteLine("\tCost:  $" + (p.itemQuantity6 * 6));
            }

            for (p.itemQuantity7 = 0; p.itemQuantity7 < 1; p.itemQuantity7++)
            {
                Console.WriteLine("\n\nEnter how many tubs of IceCream do you Need:\t");
                p.itemQuantity7 = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("\n\n\t ******************************");
                Console.WriteLine("\tIceCream Tubs Ordered: " + p.itemQuantity7);
                Console.WriteLine("\t ******************************");
                Console.WriteLine("\tCost:  $" + (p.itemQuantity7 * 7));
            }

            for (p.itemQuantity8 = 0; p.itemQuantity8 < 1; p.itemQuantity8++)
            {
                Console.WriteLine("\n\nEnter how many bags of Popcorn you Need:\t");
                p.itemQuantity8 = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("\n\n\t ******************************");
                Console.WriteLine("\tBags of Popcorn Ordered: " + p.itemQuantity8);
                Console.WriteLine("\t ******************************");
                Console.WriteLine("\tCost:  $" + (p.itemQuantity8 * 8));
            }

            for (p.itemQuantity9 = 0; p.itemQuantity9 < 1; p.itemQuantity9++)
            {
                Console.WriteLine("\n\nEnter how much pounds of Beef you Need:\t");
                p.itemQuantity9 = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("\n\n\t ******************************");
                Console.WriteLine("\tPound of Beef Ordered: " + p.itemQuantity9);
                Console.WriteLine("\t ******************************");
                Console.WriteLine("\tCost:  $" + (p.itemQuantity9 * 9));
            }

            for (p.itemQuantity10 = 0; p.itemQuantity10 < 1; p.itemQuantity10++)
            {
                Console.WriteLine("\n\nEnter how many filets of Salmon you Need:\t");
                p.itemQuantity10 = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("\n\n\t ******************************");
                Console.WriteLine("\tFilets of Salmon Ordered: " + p.itemQuantity10);
                Console.WriteLine("\t ******************************");
                Console.WriteLine("\tCost:  $" + (p.itemQuantity10 * 10));
            }
            
            DisplayContinuePrompt();





            /// <summary>
            /// ************************************************
            /// *              Order Products                  *
            /// ************************************************
            /// </summary>

            Console.WriteLine("Order Review & Cost Summary:");
            Console.WriteLine("\ty - Place Order");
            Console.WriteLine("\tn - Cancel Order");
            Console.Write("Choose: ");

            switch (Console.ReadLine())
            {
                case "y":

                    Console.WriteLine("\t\t Total Cost: ");
                    break;
                case "n":
                    Console.WriteLine("Sorry to hear that...");
                    break;
            }

            Console.WriteLine("\n\tSuccess! Your order has been placed!");
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
