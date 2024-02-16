using Newtonsoft.Json;
using pirveli_etapis_proeqti;
using System;
using System.Collections.Specialized;
using System.ComponentModel.Design;


internal class Program
{
    private static void Main(string[] args)
    {
        StreamReader sr = new StreamReader(@"C:\Users\zuka\source\repos\pirveli etapis proeqti\jsconfig1.json");
        var info = sr.ReadToEnd();
        sr.Close();
        Person personObj = JsonConvert.DeserializeObject<Person>(info);
        while (true) {
            Console.WriteLine(" Press 1 to end program! Or Press any Number to Enter Card Details:");
            int end = Convert.ToInt32(Console.ReadLine());
            if (end == 1) 
            {
                break;
            }
            CardDetails detailsObj = new CardDetails();
            detailsObj = personObj.Carddetails;
            Console.WriteLine("CardNumber: ");
            string cardNumber = Console.ReadLine();
            Console.WriteLine("ExpirationDate: ");
            string expirationDate = Console.ReadLine();
            Console.WriteLine("CVC: ");
            string CVC = Console.ReadLine();
            if (detailsObj.cardNumber == cardNumber && detailsObj.expirationDate == expirationDate && detailsObj.CVC == CVC)
            {
                Console.WriteLine("Enter Pin:");
                string pin = Console.ReadLine();
                if (personObj.pin == pin)
                {
                    List<Transaction> transactions = personObj.transactions;
                    Console.WriteLine($"Choose an operation: \n 1.Check Balance \n 2.Get Amount \n 3.Get Last 5 Transactions \n" +
                        $"4.Add Amount \n  5.Change Pin");
                    Transaction transactionsObj = new Transaction();
                    var aftertransaction = 0;
                    string transactiontype = "";
                    int operation = Convert.ToInt32(Console.ReadLine());
                    switch (operation)
                    {
                        case 1:
                            transactiontype = "Check balance";
                            if (transactions.Count == 0)
                            {
                                Console.WriteLine("Your balance is: 0 ");
                            }
                            else
                            {
                                aftertransaction = transactions.ElementAt(transactions.Count - 1).amountGEL;
                                Console.WriteLine($"Your Balance Is:{transactions.ElementAt(transactions.Count - 1).amountGEL} ");
                            }
                            break;
                        case 2:
                            transactiontype = "Get amount";
                            if (transactions.Count == 0)
                            {
                                Console.WriteLine("Your balance is: 0, therefore you can'c get any amount");
                            }
                            else
                            {   
                               
                                Console.WriteLine($"Get amount: You can only take  money multiple of 5. Maximum amount you can take is 200 GEL");
                                Console.WriteLine($"Choose an amount you want to take: Your balance is: {transactions.ElementAt(transactions.Count - 1).amountGEL}");
                                int a = Convert.ToInt32(Console.ReadLine());
                                while (a >= transactions.ElementAt(transactions.Count - 1).amountGEL || a < 0 || a % 5 != 0 || a >= 200)
                                {
                                    Console.WriteLine("Enter a valid amount");
                                    a = Convert.ToInt32(Console.ReadLine());

                                }
                                aftertransaction = (transactions.ElementAt(transactions.Count - 1).amountGEL) - a;
                                Console.WriteLine($" You successfully took your desired amount.\nYour balance is : {aftertransaction}");
                            }
                            
                            break;
                        case 3:
                            if (transactions.Count != 0)
                            {
                                aftertransaction = transactions.ElementAt(transactions.Count - 1).amountGEL;
                            }
                            transactiontype = "Last 5 operations";
                            if (transactions.Count < 5)
                            {
                                break;
                            }
                            Console.WriteLine("Last 5 transactions are: ");
                            for (int i = transactions.Count - 1; i > transactions.Count - 5; i--)
                            {
                                Console.WriteLine(transactions.ElementAt(i).transactionDate);
                                Console.WriteLine(transactions.ElementAt(i).transactionType);
                                Console.WriteLine(transactions.ElementAt(i).amountGEL);
                                Console.WriteLine("-------------------------");
                            }
                            break;

                        case 4:
                            
                            transactiontype = "Add amount";
                            Console.WriteLine($"Add amount: \n You only can add amount that is multiple of 5.");
                            int b = Convert.ToInt32(Console.ReadLine());
                            while (b <= 0 || b % 5 != 0)
                            {
                                Console.WriteLine("Enter a valid amount");
                                b = Convert.ToInt32(Console.ReadLine());
                            }
                            if (transactions.Count == 0)
                            {
                                aftertransaction = b;
                            }
                            else
                            {
                                aftertransaction = (transactions.ElementAt(transactions.Count - 1).amountGEL) + b;
                            }
                            Console.WriteLine($" Money successfully added to your account.\nYour balance is {aftertransaction}");
                            break;
                        case 5:
                            if (transactions.Count != 0)
                            {
                                aftertransaction = transactions.ElementAt(transactions.Count -1).amountGEL;
                            }
                            transactiontype = "Change pin";
                            Console.WriteLine("New Pin must contain 4 numbers! ");
                            string p = Console.ReadLine();
                            while (p == personObj.pin || p.Length != 4)
                            {
                                Console.WriteLine("Enter a correct Pin");
                                p = Console.ReadLine();
                            }
                            Console.WriteLine("Pin was changed successfully");
                            personObj.pin = p;
                            break;
                       
                    }
                    transactionsObj.transactionType = transactiontype;
                    transactionsObj.amountGEL = aftertransaction;
                    transactionsObj.transactionDate = DateTime.Now.ToString();
                    personObj.transactions.Add(transactionsObj);
                    var ser = JsonConvert.SerializeObject(personObj, Formatting.Indented);
                    File.WriteAllText(@"C:\Users\zuka\source\repos\pirveli etapis proeqti\jsconfig1.json", ser);

                }
                else
                {
                    Console.WriteLine("Please Enter Correct Pin");
                }
            }
            else { Console.WriteLine("Invalid Card. Try again!\n"); }
        }
    }
}
