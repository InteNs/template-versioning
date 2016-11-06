using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using entities;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new Entities())
            {
                Console.WriteLine("type question description");
                var desc = Console.ReadLine();
                var question = new Question { Description = desc };
                context.Questions.Add(question);
                context.SaveChanges();

                var questions = from q in context.Questions select q;
                foreach (var q in questions)
                {
                    Console.WriteLine(q.Description);
                }
                Console.WriteLine("press any key to exit...");
                Console.ReadKey();

            }
        }
    }
}
