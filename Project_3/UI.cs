using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DbManager;
using System.Text.RegularExpressions;

namespace Project_3
{
    public delegate void MinHandler(string Message);
    class UI
    {
        //Creating Logic Search enginge  
        Searching SearchEngine = new Searching();
        //Creating Database Manager 
        DBManager db = new DBManager();
        // event declaration
        public static event MinHandler View;
        public UI()
        {
            //delegate to connect the event to the function EventMsg
            UI.View += EventMsg;
        }
        // function for the event
        static void EventMsg(string msg)
        {
            Console.WriteLine(msg);
        }

        //Function to show the currect error msg and restart the program
        void ErrorMsg(string msg)
        {
            Console.WriteLine(msg);
            Console.WriteLine("\n---------Press enter to clear text and try again :)---------");
            Console.ReadLine();
            Console.Clear();
            Start();
        }
        //Function to check if user enter a valid string
        void CheckString(string StringName)
        {
            if (string.IsNullOrWhiteSpace(StringName))

            {
                throw new ArgumentException();
            }
        }
        //Function to start the program
        public void Start()
        {



            //Count the number of rows in Table SearchDetails
            int DetailsCount = db.DetailsCounter() + 1;
            //Count the number of rows in Table SearchResults
            int ReasultCount = db.ResultCounter() + 1;
            char choice;
            try
            {
                do
                {

                    Console.Write("Please select a search option-\n");
                    Console.Write("1.Enter file name to search in C:// drive.\n2.Enter file name to search + path to search in.\n3.Exit");
                    Console.Write("\nSelection: ");
                    choice = Console.ReadKey().KeyChar;
                    //Check if user enter a valid selection
                    if (!(choice == '1' || choice == '2' || choice == '3'))
                    {
                        throw new Exception("You Have to choose number between 1-3---------");

                    }
                    Console.WriteLine();
                    switch (choice)
                    {
                        case '1':

                            Console.Write("Please enter file name to search:");
                            string FileName = Console.ReadLine();
                            //Check if user enter a valid string to search
                            CheckString(FileName);
                            db.AddSearchDetails(FileName, DetailsCount);
                            Console.WriteLine("Please wait, search in progress...");
                            List<string> arr = SearchEngine.GetFiles(FileName);
                            //Check if the list is empty and there is no results, also show a msg
                            bool isEmpty = !arr.Any();
                            if (isEmpty)
                            {
                                Console.WriteLine();
                                Console.WriteLine("--------------Nothing Found-----------");
                                Console.WriteLine();
                                db.AddSearchResults(DetailsCount, ReasultCount);
                                ReasultCount++;
                                DetailsCount++;

                            }
                            else
                            {
                                Console.WriteLine("Here is the results:\n");
                                foreach (var i in arr)
                                {

                                    //***  raise the event
                                    View(i);
                                    db.AddSearchResults(DetailsCount, ReasultCount, i);
                                    ReasultCount++;
                                }
                                DetailsCount++;
                            }
                            db.SaveChanges();
                            arr.Clear();
                            Console.WriteLine("\n---------Press enter to clear text and search again :)---------");
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        case '2':
                            Console.Write("Please enter file name to search:");
                            string FileName2 = Console.ReadLine();
                            //Check if user enter a valid string to search
                            CheckString(FileName2);
                            Console.Write("Please write full path,\nFor example to search xxx foleder in drive c, please write - c://xxx :");
                            string root = Console.ReadLine();
                            List<string> arr2 = SearchEngine.GetFiles(FileName2, root);
                            db.AddSearchDetails(FileName2, DetailsCount, root);

                            //Check if the list is empty and there is no results, also show a msg
                            bool isEmpty2 = !arr2.Any();
                            if (isEmpty2)
                            {
                                Console.WriteLine();
                                Console.WriteLine("--------------Nothing Found-----------");
                                Console.WriteLine();
                                db.AddSearchResults(DetailsCount, ReasultCount);
                                ReasultCount++;
                                DetailsCount++;

                            }
                            else
                            {
                                Console.WriteLine("Here is the results:\n");
                                foreach (var i in arr2)
                                {

                                    //***  raise the event
                                    View(i);
                                    db.AddSearchResults(DetailsCount, ReasultCount, i);
                                    ReasultCount++;

                                }
                                DetailsCount++;
                            }
                            db.SaveChanges();
                            arr2.Clear();
                            Console.WriteLine("\n---------Press enter to clear text and search again :)---------");
                            Console.ReadLine();
                            Console.Clear();
                            break;



                        default:
                            break;
                    }



                } while (choice != '3');

            }
            //Catch and throw error msg if user didnt enter a file name to Directory to search in
            catch (ArgumentException)
            {
                ErrorMsg("\n--------- ERROR!! You have to type something ---------");

            }
            //Catch and throw error msg if user didnt enter a valid directory to search in
            catch (DirectoryNotFoundException)
            {
                ErrorMsg("\n---------ERROR!! Please Type a valid full path to search in---------");

            }

            //Catch and throw error msg if user did somthing wrong (make sure the program wont crush)
            catch (Exception ex)
            {
                ErrorMsg("\n---------ERROR!! Please Try again: " + ex.Message + "---------");

            }

        }
    }
}
