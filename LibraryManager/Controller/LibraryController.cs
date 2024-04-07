using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.JavaScript;
using System.Threading;
using LibraryManager.Data;
using LibraryManager.Model;
using LibraryManager.Shared;

namespace LibraryManager.Controller;

public class LibraryController
{
    private Thread _issueBooksThread;
    private Thread _returnBooksThread;
    private Thread _addBookThread;

    public event EventHandler<EventArgs> LogCreated; 

    public LibraryController()
    {
        BookRepository.Initialize(new List<Book>
        {
            new() { Title = "To Kill a Mockingbird", Author = "Harper Lee" },
            new() { Title = "1984", Author = "George Orwell" },
            new() { Title = "The Great Gatsby", Author = "F. Scott Fitzgerald" },
            new() { Title = "Pride and Prejudice", Author = "Jane Austen" },
            new() { Title = "The Catcher in the Rye", Author = "J.D. Salinger" },
            new() { Title = "The Hobbit", Author = "J.R.R. Tolkien" },
            new() { Title = "Harry Potter and the Philosopher's Stone", Author = "J.K. Rowling" },
            new() { Title = "To the Lighthouse", Author = "Virginia Woolf" },
            new() { Title = "Brave New World", Author = "Aldous Huxley" },
            new() { Title = "Moby-Dick", Author = "Herman Melville" },
            new() { Title = "The Lord of the Rings", Author = "J.R.R. Tolkien" },
            new() { Title = "Jane Eyre", Author = "Charlotte Brontë" },
            new() { Title = "The Picture of Dorian Gray", Author = "Oscar Wilde" },
            new() { Title = "Frankenstein", Author = "Mary Shelley" },
            new() { Title = "The Odyssey", Author = "Homer" },
            new() { Title = "Hamlet", Author = "William Shakespeare" },
            new() { Title = "The Adventures of Huckleberry Finn", Author = "Mark Twain" },
            new() { Title = "The Road", Author = "Cormac McCarthy" },
        });
        CustomerRepository.Initialize(new List<Customer>
        {
            new() { Name = "John Smith" },
            new() { Name = "Emily Johnson" },
            new() { Name = "Michael Brown" },
            new() { Name = "Emma Davis" },
            new() { Name = "Christopher Wilson" }
        }); 
        TakenBookRepository.Initialize(new List<TakenBook>());


        _issueBooksThread = new Thread(_issueBooks);
        _returnBooksThread = new Thread(_returnBooks);
        _addBookThread = new Thread(_addBook);

    }

    public void Start()
    {
        _issueBooksThread.Start();
        _returnBooksThread.Start();
        _addBookThread.Start();
    }

    public void Stop()
    {
        _issueBooksThread.Interrupt();
        _returnBooksThread.Interrupt();
        _addBookThread.Interrupt();
    }

    private void _addBook()
    {
        Random random = new Random();
        while (true)
        {
            var newBook = new Book
            {
                Author = $"New Author #{random.Next(0, 1000)}",
                Title = $"New Title #{random.Next(0, 1000)}"
            };
            BookRepository.Add(newBook);
            LogRepository.Add(new Log
            {
                Title = "New book",
                Message = $"{newBook} added!"
            });
            LogCreated?.Invoke(this,EventArgs.Empty);
            Thread.Sleep(TimeSpan.FromSeconds(30));
        }
    }

    private void _issueBooks()
    {
        Random random = new Random();
        while (true)
        {
            Book bookToIssue = BookRepository.GetAll()[random.Next(0, BookRepository.GetAll().Count - 1)];
            while (bookToIssue.Count == 0)
            {
                bookToIssue = BookRepository.GetAll()[random.Next(0, BookRepository.GetAll().Count - 1)];
            }
            
            var takenBook = new TakenBook
            {
                Book = bookToIssue,
                Customer = CustomerRepository.GetAll()[random.Next(0, CustomerRepository.GetAll().Count - 1)],
                TimeTaken = DateTime.Now,
                TimeToReturn = DateTime.Now.AddSeconds(25)
            };
            TakenBookRepository.Add(takenBook);
            bookToIssue.Count--;
            LogRepository.Add(new Log
            {
                Title = "Book taken!",
                Message = $"{takenBook.Book} taken by {takenBook.Customer.Name}!"
            });
            LogCreated?.Invoke(this, EventArgs.Empty);
            Thread.Sleep(TimeSpan.FromSeconds(1));
        }
    }

    private void _returnBooks()
    {
        Random random = new Random();
        while (true)
        {
            var allTakenBooks = TakenBookRepository.GetAll();
            if (allTakenBooks != null && allTakenBooks.Count > 0)
            {
                TakenBook? bookToReturn = allTakenBooks.Find(b => b.TimeToReturn < DateTime.Now);
                if (bookToReturn != null)
                {
                    TakenBookRepository.Delete(bookToReturn);
                    bookToReturn.Book.Count++;
                    LogRepository.Add(new Log
                    {
                        Title = "Book returned!",
                        Message = $"{bookToReturn.Book} returned by {bookToReturn.Customer.Name}!"
                    });
                    LogCreated?.Invoke(this, EventArgs.Empty);
                }
            }
            Thread.Sleep(TimeSpan.FromSeconds(3));
        }
    }
}