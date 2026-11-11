namespace LINQ_P1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Part 1 Filteration

            //Q1: Books with CopiesInStock > 0
            //Where keeps only the books whose condition is true, so we keep books that still have copies

            #region 
            //Fluent one
            var booksInStock = LibraryData.BookList.Where(b => b.CopiesInStock > 0);
            foreach (var item in booksInStock)
            {
                Console.WriteLine(item);
            }
            //Query one
            var q = from b in LibraryData.BookList
                    where b.CopiesInStock > 0
                    select b;
            foreach (var item in q)
            {
                Console.WriteLine(item);
            }
            #endregion




            #region Q2: Programming books that are in stock
            // two conditions must both be true, and i will join them with one where

            //fluent one
            var q2 = LibraryData.BookList.Where(b => b.Genre == "Programming" && b.CopiesInStock > 0);
            foreach (var item in q2)
            {
                Console.WriteLine(item);
            }

            // Query one
            var q2b = from b in LibraryData.BookList
                      where b.Genre == "Programming" && b.CopiesInStock > 0
                      select b;
            foreach (var item in q2b)
            {
                Console.WriteLine(item);
            }
            #endregion



            #region Q3:Titles that start with C
            //checks the first characters of the string so we keep titles beginning with C.

            //fluent
            var q3 = LibraryData.BookList.Where(b => b.Title.StartsWith("C"));
            foreach (var item in q3)
            {
                Console.WriteLine(item);
            }

            // Query one
            var q3b = from b in LibraryData.BookList
                      where b.Title.StartsWith("C")
                      select b;
            foreach (var item in q3b)
            {
                Console.WriteLine(item);
            }
            #endregion






            //Part 2: Projection (Select)
            #region Q4: to print Only book titles
            //Select changes the shape of each element, so here each Book becomes just its Title string.

            //fluent part
            var q4 = LibraryData.BookList.Select(b => b.Title);
            foreach (var item in q4)
            {
                Console.WriteLine(item);
            }

            //query
            var q4b = from b in LibraryData.BookList
                      select b.Title;
            foreach (var item in q4b)
            {
                Console.WriteLine(item);
            }
            #endregion


            #region Q5:BookId and Title with an anonymous type
            //using new { } builds a small object with only the properties we want.

            //fluent
            var q5 = LibraryData.BookList.Select(b => new { ID = b.BookId, Title = b.Title });
            foreach (var item in q5)
            {
                Console.WriteLine($"{item.ID} {item.Title}");
            }

            //query
            var q5b = from b in LibraryData.BookList
                      select new { ID = b.BookId, Title = b.Title };
            foreach (var item in q5b)
            {
                Console.WriteLine($"{item.ID} {item.Title}");
            }
            #endregion

            #region Q6:[ID] Title - Genre
            //select can return a formatted string so each Book becomes one ready to print line.

            //fluent
            var q6 = LibraryData.BookList.Select(b => $"[{b.BookId}] {b.Title} - {b.Genre}");
            foreach (var item in q6)
            {
                Console.WriteLine(item);
            }

            // Query
            var q6b = from b in LibraryData.BookList
                      select $"[{b.BookId}] {b.Title} - {b.Genre}";
            foreach (var item in q6b)
            {
                Console.WriteLine(item);
            }
            #endregion






            //part 3: Select Many

            #region Q7 All loans of all members flattened
            //each member has a list of loans. SelectMany takes all those lists and merges them into ONE sequence of Loan.

            //fluent
            var q7 = LibraryData.MemberList.SelectMany(m => m.Loans);
            foreach (var loan in q7)
            {
                Console.WriteLine(loan);
            }

            // Query
            var q7b = from m in LibraryData.MemberList
                      from loan in m.Loans
                      select loan;
            foreach (var loan in q7b)
            {
                Console.WriteLine(loan);
            }
            #endregion



            #region Q8: Loans of certain members only
            //first filter the members with Where, then flatten only their loans with SelectMany.

            //fluent one
            var q8 = LibraryData.MemberList
                                .Where(m => m.Name == "Ahmed")
                                .SelectMany(m => m.Loans);
            foreach (var loan in q8)
            {
                Console.WriteLine(loan);
            }

            //query
            var q8b = from m in LibraryData.MemberList
                      where m.Name == "Ahmed"
                      from loan in m.Loans
                      select loan;
            foreach (var loan in q8b)
            {
                Console.WriteLine(loan);
            }
            #endregion



            #region Q9: Q7 using a regular Select
            //Since select does NOT flatten and it returns one loans-list per member, we get a collection of collections.
            // Select     -> IEnumerable<IEnumerable<Loan>> (a list of lists, so we need a nested foreach)
            // SelectMany -> IEnumerable<Loan>              (one flat list, so one foreach is enough)

            //fluent sol
            var q9 = LibraryData.MemberList.Select(m => m.Loans);
            foreach (var memberLoans in q9)
            {
                foreach (var loan in memberLoans) //this is the inner loop
                {
                    Console.WriteLine(loan);
                }
            }

            //query sol
            var q9b = from m in LibraryData.MemberList
                      select m.Loans;
            foreach (var memberLoans in q9b)
            {
                foreach (var loan in memberLoans)
                {
                    Console.WriteLine(loan);
                }
            }
            #endregion


            // PART 4 finally

            #region Q10: Where, then add a book, then print
            //The query doesn't run when written, it runs when the foreach starts.
            // but in Deferred, the new book WILL appear because the query executes after the Add.

            var newBook = new Book { BookId = 999, Title = "Clean Code", Genre = "Programming", CopiesInStock = 5 };

            //fluent
            var q10 = LibraryData.BookList.Where(b => b.CopiesInStock > 0);

            //query
            var q10b = from b in LibraryData.BookList
                       where b.CopiesInStock > 0
                       select b;

            LibraryData.BookList.Add(newBook); //added AFTER both queries were written

            foreach (var item in q10)
            {
                Console.WriteLine(item); //here the boook appears
            }

            foreach (var item in q10b)
            {
                Console.WriteLine(item);
            }





            //now i will reset to make it clean before going to the next question
            LibraryData.BookList.Remove(newBook);
            #endregion


            #region Q11 Same steps with .ToList()
            //ToList() forces the query to run immediately and stores the results in a new list
            //Here, the new book will not appear because the query already ran before the Add

            // Fluent
            var q11 = LibraryData.BookList.Where(b => b.CopiesInStock > 0).ToList();

            // Query
            var q11b = (from b in LibraryData.BookList
                        where b.CopiesInStock > 0
                        select b).ToList();

            LibraryData.BookList.Add(newBook); //added AFTER both lists were already filled

            foreach (var item in q11)
            {
                Console.WriteLine(item);   // this time it wont appear
            }

            foreach (var item in q11b)
            {
                Console.WriteLine(item);   //it will not appear too
            }

            LibraryData.BookList.Remove(newBook);
            #endregion


            //Q12: Deferred or Eager ?
            // in case of question 10 its Deferred because Where only builds the query. It runs at the foreach, so it sees the book added before it.
            // Q11 its the case of Eager because the ToList() runs the query right away and saves the result, so later changes to BookList are not seen.





            //BONUS part
            //flueent
            var b1 = LibraryData.BookList.Where(b => (b.Genre == "Programming" && b.CopiesInStock > 0) || b.Title.StartsWith("C"));
            foreach (var item in b1)
            {
                Console.WriteLine(item);
            }

            //quuery
            var b1b = from b in LibraryData.BookList
                      where (b.Genre == "Programming" && b.CopiesInStock > 0) || b.Title.StartsWith("C")
                      select b;
            foreach (var item in b1b)
            {
                Console.WriteLine(item);
            }

            //last question of bonus Select with an index
            //This Select overload gives us the position of each element starting at 0 along with the element.
            var b2 = LibraryData.BookList.Select((book, index) => new { index, book.Title });
            foreach (var item in b2)
            {
                Console.WriteLine($"{item.index} - {item.Title}");
            }
            Console.ReadLine();

        }
    }

}