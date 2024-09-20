using System;
using System.Collections.Generic;

public class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    public int Copies { get; set; }

    public Book(string title, string author, string isbn, int copies)
    {
        Title = title;
        Author = author;
        ISBN = isbn;
        Copies = copies;
    }

    public bool IsAvailable()
    {
        return Copies > 0;
    }
}

public class Reader
{
    public string Name { get; set; }
    public int ReaderId { get; set; }

    public Reader(string name, int readerId)
    {
        Name = name;
        ReaderId = readerId;
    }
}

public class Library
{
    private List<Book> books = new List<Book>();
    private List<Reader> readers = new List<Reader>();
    private Dictionary<int, List<Book>> borrowedBooks = new Dictionary<int, List<Book>>();

    public void AddBook(Book book)
    {
        books.Add(book);
        Console.WriteLine($"Книга '{book.Title}' добавлена в библиотеку.");
    }

    public void RemoveBook(string isbn)
    {
        Book bookToRemove = books.Find(b => b.ISBN == isbn);
        if (bookToRemove != null)
        {
            books.Remove(bookToRemove);
            Console.WriteLine($"Книга '{bookToRemove.Title}' удалена из библиотеки.");
        }
        else
        {
            Console.WriteLine("Книга не найдена.");
        }
    }

    public void RegisterReader(Reader reader)
    {
        readers.Add(reader);
        Console.WriteLine($"Читатель '{reader.Name}' зарегистрирован.");
    }

    public void RemoveReader(int readerId)
    {
        Reader readerToRemove = readers.Find(r => r.ReaderId == readerId);
        if (readerToRemove != null)
        {
            readers.Remove(readerToRemove);
            Console.WriteLine($"Читатель '{readerToRemove.Name}' удален.");
        }
        else
        {
            Console.WriteLine("Читатель не найден.");
        }
    }

    public void LendBook(int readerId, string isbn)
    {
        Reader reader = readers.Find(r => r.ReaderId == readerId);
        Book book = books.Find(b => b.ISBN == isbn);

        if (reader != null && book != null && book.IsAvailable())
        {
            if (!borrowedBooks.ContainsKey(readerId))
            {
                borrowedBooks[readerId] = new List<Book>();
            }

            borrowedBooks[readerId].Add(book);
            book.Copies--;

            Console.WriteLine($"Читатель '{reader.Name}' взял книгу '{book.Title}'.");
        }
        else if (book == null || !book.IsAvailable())
        {
            Console.WriteLine("Книга недоступна.");
        }
        else
        {
            Console.WriteLine("Читатель не найден.");
        }
    }

    public void ReturnBook(int readerId, string isbn)
    {
        if (borrowedBooks.ContainsKey(readerId))
        {
            Book bookToReturn = borrowedBooks[readerId].Find(b => b.ISBN == isbn);
            if (bookToReturn != null)
            {
                borrowedBooks[readerId].Remove(bookToReturn);
                bookToReturn.Copies++;
                Console.WriteLine($"Читатель вернул книгу '{bookToReturn.Title}'.");
            }
            else
            {
                Console.WriteLine("Эта книга не была взята.");
            }
        }
        else
        {
            Console.WriteLine("Читатель не взял книги.");
        }
    }
}

class Program
{
    static void Main()
    {
        Library library = new Library();

        Book book1 = new Book("Путь Ауэзова", "Абай Кунанбаев", "1234567890", 3);
        Book book2 = new Book("Беспредел", "Генадий Бендитский", "0987654321", 2);

        library.AddBook(book1);
        library.AddBook(book2);

        Reader reader1 = new Reader("Саша", 1);
        Reader reader2 = new Reader("Петя", 2);

        library.RegisterReader(reader1);
        library.RegisterReader(reader2);

        library.LendBook(1, "1234567890");
        library.LendBook(2, "0987654321");


        library.LendBook(2, "0987654321");


        library.ReturnBook(1, "1234567890");

        library.RemoveBook("1234567890");

        library.RemoveReader(1);
    }
}