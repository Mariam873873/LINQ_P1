public class Loan
{
    public int LoanId { get; set; }
    public int BookId { get; set; }
    public override string ToString() => $"Loan {LoanId} (Book {BookId})";
}

public class Member
{
    public int MemberId { get; set; }
    public string Name { get; set; }
    public List<Loan> Loans { get; set; } = new();
}

public class Book
{
    public int BookId { get; set; }
    public string Title { get; set; }
    public string Genre { get; set; }
    public int CopiesInStock { get; set; }
    public override string ToString() => $"[{BookId}] {Title} - {Genre} ({CopiesInStock})";
}

public static class LibraryData
{
    public static List<Book> BookList = new()
    {
        new Book { BookId = 1, Title = "C# Basics", Genre = "Programming", CopiesInStock = 3 },
        new Book { BookId = 2, Title = "Clean Code", Genre = "Programming", CopiesInStock = 0 },
        new Book { BookId = 3, Title = "Dune", Genre = "Fiction", CopiesInStock = 2 },
        new Book { BookId = 4, Title = "Cosmos", Genre = "Science", CopiesInStock = 1 },
    };

    public static List<Member> MemberList = new()
    {
        new Member { MemberId = 1, Name = "Ahmed", Loans = new() { new Loan { LoanId = 1, BookId = 1 }, new Loan { LoanId = 2, BookId = 3 } } },
        new Member { MemberId = 2, Name = "Mona", Loans = new() { new Loan { LoanId = 3, BookId = 4 } } },
    };
}