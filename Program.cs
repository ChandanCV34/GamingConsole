using GamingConsole.Models;
using System;
using System.Collections.Generic;

namespace GamingConsole
{
    class Program
    {

       readonly GamingConsoleContext context;
       readonly UserDetail user;
       readonly SomeWord words;

        readonly FriendUser friend;
        public List<SomeWord> someword;
       public  List<FriendUser> friends;
        string playername;
        int id;

        public Program()
        {
            words = new SomeWord();
            user = new UserDetail();
            friend = new FriendUser();
            context = new GamingConsoleContext();
            someword = new List<SomeWord>();
            friends = new List<FriendUser>();
        }
        /// <summary>
        /// Where it display the Menu  List
        /// </summary>
        public void Menu()
        {
            string Userchoice;

            do
            {
                Console.WriteLine(" -- Welcome To Word Play----");
                Console.WriteLine(" Please Press A to Login ");
                Console.WriteLine(" Please Press B to Register");
                Console.WriteLine(" Please Press E to Exit");
                Console.WriteLine(" Please Enter Your Choice");
                Userchoice = Console.ReadLine().ToLower();
                switch (Userchoice)
                {
                    case "a":
                        UserLogin();
                        break;
                    case "b":
                        UserRegistration();
                        break;

                    default:
                        Console.WriteLine(" TATA BYE");
                        break;
                }

            } while (Userchoice != "e");


        }
        /// <summary>
        /// This function involves in checking the credential of user login
        /// </summary>

        public void UserLogin()
        {
            bool status = false;
            Console.WriteLine(" Please Enter The Player Name To Login");
            playername = Console.ReadLine();
            Console.WriteLine(" Please Enter The Password");
            string playerpassword = Console.ReadLine();
            foreach (var item in context.UserDetails)
            {
                if (item.Username == playername && item.Password == playerpassword)
                {
                    Console.WriteLine($" Welcome {item.Username} For the Word of Play");
                    status = true;
                    words.Username = item.Username;
                }

            }
            if (status == true)
            {
                Game();
            }
            if (status == false)
            {

                Console.WriteLine("Invalid Credential");
                Console.WriteLine("Please Register befor you login");


            }

        }

        /// <summary>
        /// This function involoves in registration process that accept the deatils from user
        /// </summary>

        public void UserRegistration()
        {
            Console.WriteLine("Please Enter Your Username");
            user.Username = Console.ReadLine();
            Console.WriteLine("Please Enter Your Name");
            user.Name = Console.ReadLine();
            Console.WriteLine("Please Set Your Password");
            user.Password = Console.ReadLine();
            Console.WriteLine("Please Enter Your Phone number");
            user.Phone = Console.ReadLine();
            context.UserDetails.Add(user);
            context.SaveChanges();
            Console.WriteLine(" Player Registered Successfully");
        }
        /// <summary>
        /// Displays the number of gaming option which he can play
        /// </summary>
        public void Game()
        {
            int option;
            do
            {

                Console.WriteLine(" Please Choose An Option ");
                Console.WriteLine(" 1) Words ready for you to play");
                Console.WriteLine(" 2) Assign word for another player");
                Console.WriteLine(" 3) See score board ");
                Console.WriteLine(" 0 To Exit ");
                try
                {
                    option = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {


                    Console.WriteLine(" Please Choose An Option ");
                    option = Convert.ToInt32(Console.ReadLine());
                }
                switch (option)
                {
                    case 1:
                        WordPlay();
                        break;
                    case 2:
                        Wordassign();
                        break;

                    case 3:
                        Scorecard();
                        break;

                    default:
                        break;
                }



            } while (option != 0);


        }
        /// <summary>
        /// The entire Logic of word split is taken care here
        /// </summary>
        public void WordPlay()
        {

          
            bool status = false;
            Console.WriteLine("Caution : If Your Key is Generated Please Apply the Same Key To play with your Word");

            foreach (var item in context.SomeWords)
            {

                someword.Add(item);
              
                 if (item.Username == playername)
                {
                    Console.WriteLine(" Your  Generated Key is!! ");
                    Console.WriteLine("__________________________");
                    Console.WriteLine(item.Id);
                   
                    Console.WriteLine("__________________________");
                    status = true;
                }
               
               
                
              

            }

            if (status == true)
            {
                Console.WriteLine("Please Enter Your Id");
                try
                {
                    id = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {

                    Console.WriteLine("Please Enter The number form");
                    Console.WriteLine("Please Enter Your Id");
                    id = Convert.ToInt32(Console.ReadLine());

                }

                Console.WriteLine("The Word Given To you Are");

                foreach (var items in someword)
                {
                    if (items.Id == id)
                    {
                        try
                        {
                            for (int i = 0; i < items.Word.Length; i++)
                            {

                                Console.Write("X");

                            }

                        }
                        catch (Exception e)
                        {

                            Console.WriteLine(e.Message);
                        }

                    }


                }
                Console.WriteLine();
                Split();

            }

            
            

            if (status == false)
            {
                Console.WriteLine("new user !! Dont have id please assign the word to your friend and get id");
            }
           
        }

        public void Split()
        {
            int score ;
            string word;
            int cow = 0;
            int bull = 0;
            int count = 0;

            bool status = false;
            char[] array;
            Console.WriteLine("Enter Your Guess");
            string choi = Console.ReadLine();
            char[] guess = choi.ToCharArray();

            foreach (var item in context.SomeWords)
            {

                if (item.Id == id)
                {
                   

                    word = item.Word;

                array = word.ToCharArray();




                for (int i = 0; i < array.Length; i++)
                {


                    for (int j = 0; j < guess.Length; j++)
                    {

                        if (array[i] == guess[j] && i == j)
                        {
                            cow++;
                            count++;
                            if (choi == word && cow == count)
                            {
                                status = true;
                                words.Word = word;

                            }


                        }
                        else if (array[i] == guess[j] && i != j)
                        {

                            bull++;


                        }
                        else
                        {

                            status = false;
                        }
                    }


                }

            }
            }

            Console.WriteLine($"The Word you have entered is {choi} cow - {cow},Bull - { bull}");



            if (status == false)
            {
                Split();
            }
            if (status == true)
            {
                Console.WriteLine( "Congo You Have Guessed The Right Word");
                score = 10;
                words.Score = score;

                try
                {
                    context.SomeWords.Add(words);
                    context.SaveChanges();

                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message);
                }
               
                



            }


        }
        /// <summary>
        /// Here we just assign a word to our friend
        /// </summary>
        public void Wordassign()
        {
            bool flag = false;
            Console.WriteLine( "Enter The Word To assign");
            friend.Word = Console.ReadLine();
            words.Word = friend.Word;
            foreach (var item in context.UserDetails)
            {
                Console.WriteLine(item.Username);

                      

            }
            Console.WriteLine("Please Select your friend by entering his name");
            friend.Username = Console.ReadLine();
            words.Username = friend.Username;
            foreach (var items  in context.UserDetails)
            {
                
                if ( friend.Username == items.Username)
                {
                    flag = true;
                }
               

            }

            if (flag == true)
            {
                try
                {
                    context.FriendUsers.Add(friend);
                    context.SaveChanges();

                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message);
                }
                
                try
                {
                    context.SomeWords.Add(words);
                    context.SaveChanges();
                }
                catch (Exception )
                {
                    Console.WriteLine("Word is added");

                }

                

            }
            if (flag == false)
            {
                Console.WriteLine( " Please Verify the name");
            }


        }

        void Scorecard()
        {
            int points=0;

            foreach (var item in context.SomeWords)
            {
                if (item.Username == playername)
                {
                    if (item.Score >= 10)
                    {
                        points += (int)item.Score;
                        
                    }

                    
                }

            }

            
            Console.WriteLine(" Your Score Upto Here is :" + points);
            Console.WriteLine("The overall Score Board is");
            foreach (var item in context.SomeWords)
            {
                if (item.Score >= 10)
                {
                   
                    Console.WriteLine($" {item.Username}-> {item.Score}");
                }

            }

        }



        static void Main()
        {
            new Program().Menu();
        }
    }
}
