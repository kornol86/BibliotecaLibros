
namespace BibliotecaLibros
{
    internal static class Program
    {
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        var library = new LibraryService();

        SeedSampleData(library);

        bool running = true;
        while (running)
        {
            ShowMenu();
            Console.Write("Seleccione una opciòn:");
            string? option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    RegisterNewBook(library);
                    break;
                case "2":
                    SearchByIsbn(library);
                    break;
                case "3":
                    SearchByGenre(library);
                    break;
                case "4":
                    SearchByAuthor(library);
                    break;
                case "5":
                    ListAllBooks(library);
                    break;
                case "6":
                    library.ReportDictionaryByIsbn();
                    break;
                case "7":
                    library.ReportMapByGenre();
                    break;
                case "8":
                    library.ReportMapByAuthor();
                    break;
                case "9":
                    library.ReportSetGenres();
                    break;
                case "10":
                    library.ReportSetAuthors();
                    break;
                case "11":
                    library.ReportSummary();
                    break;
                case "0":
                    running = false;
                    Console.WriteLine("\n¡hasta luego!");
                    break;
                default:
                    Console.WriteLine("\nOpción inválida. Intente nuevamente.");
                    break;
            }

            if (running)
            {
                Console.WriteLine("\nPresione cualquier tecla para continuar...");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }

    static void ShowMenu()
    {
        Console.WriteLine("SISTEMA DE REGISTRO DE LIBROS");
        Console.WriteLine("***************************************");
        Console.WriteLine("1. Registrar un nuevo libro");
        Console.WriteLine("2. Buscar libro por ISBN");
        Console.WriteLine("3. Buscar libro por género");
        Console.WriteLine("4. Buscar libro por autor");
        Console.WriteLine("5. Listar todos los libros");
        Console.WriteLine("6. Reporte Diccionario (ISBN-Libro)");
        Console.WriteLine("7. Reporte Mapa (Género-Libros)");
        Console.WriteLine("8. Reporte Mapa (Autor-Libros)");
        Console.WriteLine("9. Reporte Conjunto de géneros");
        Console.WriteLine("10. Reporte Conjunto de autores");
        Console.WriteLine("11. Resumen de todas las estructuras");
        Console.WriteLine("                                                  ");
        Console.WriteLine("0. Salir");
    }

    static void RegisterNewBook(LibraryService library)
    {
        Console.WriteLine("\nRegistro de un nuevo libro");
        Console.WriteLine("**************************");
        var book = new Book();

        Console.Write("Ingrese el ISBN del libro: ");
        book.ISBN = Console.ReadLine();

        Console.Write("Ingrese el título del libro: ");
        book.Title = Console.ReadLine();

        Console.Write("Ingrese el autor del libro: ");
        book.Author = Console.ReadLine();

        Console.Write("Ingrese el género del libro: ");
        book.Genre = Console.ReadLine();

        Console.Write("Año de publicación del libro: ");
        if (int.TryParse(Console.ReadLine(), out int year))
           book.Year = year;
        
        Console.Write("Nùmero de Ejemplares: ");
        if (int.TryParse(Console.ReadLine(), out int copies))
            book.Copies = copies;
        
        library.RegisterBook(book);
    }

    static void SearchByIsbn(LibraryService library)
    {
        Console.WriteLine("\nBúsqueda de libro por ISBN");
        Console.WriteLine("**************************");
        Console.Write("Ingrese el ISBN del libro: ");
        string? isbn = Console.ReadLine();

        var book = library.SearchByIsbn(isbn);
        if (book != null)
        {
            Console.WriteLine("\nLibro encontrado:");
            Console.WriteLine(book);
        }
        else
        {
            Console.WriteLine("\nNo se encontró ningún libro con el ISBN proporcionado.");
        }
    }

    static void SearchByGenre(LibraryService library)
    {
        Console.WriteLine("\nBúsqueda de libro por género");
        Console.WriteLine("**************************");
        Console.Write("Ingrese el género del libro: ");
        string? genre = Console.ReadLine();

        var books = library.SearchByGenre(genre);
        if (books.Count > 0)
        {
            Console.WriteLine($"\nLibros encontrados en el género '{genre}':");
            foreach (var book in books)
            {
                Console.WriteLine(book);
            }
        }
        else
        {
            Console.WriteLine($"\nNo se encontraron libros en el género '{genre}'.");
        }
    }

    static void SearchByAuthor(LibraryService library)
    {
        Console.WriteLine("\nBúsqueda de libro por autor");
        Console.WriteLine("**************************");
        Console.Write("Ingrese el autor del libro: ");
        string? author = Console.ReadLine();

        var books = library.SearchByAuthor(author);
        if (books.Count > 0)
        {
            Console.WriteLine($"\nLibros encontrados del autor '{author}':");
            foreach (var book in books)
            {
                Console.WriteLine(book);
            }
        }
        else
        {
            Console.WriteLine($"\nNo se encontraron libros del autor '{author}'.");
        }
    }

    static void ListAllBooks(LibraryService library)
    { 
        Console.WriteLine($"\nTodos los libros registrados ({library.TotalBooks}):");
        foreach (var book in library.GetAllBooks())
        {
            Console.WriteLine($" {book}");
        }
    }

    static void SeedSampleData(LibraryService library)
    {
        library.RegisterBook(new Book { ISBN = "978-3-16-148410-0", Title = "Cien Años de Soledad", Author = "Gabriel García Márquez", Genre = "Realismo Mágico", Year = 1967, Copies = 5 });
        library.RegisterBook(new Book { ISBN = "978-0-7432-7356-5", Title = "El Código Da Vinci", Author = "Dan Brown", Genre = "Thriller", Year = 2003, Copies = 3 });
        library.RegisterBook(new Book { ISBN = "978-0-452-28423-4", Title = "1984", Author = "George Orwell", Genre = "Distopía", Year = 1949, Copies = 4 });
        library.RegisterBook(new Book { ISBN = "978-0-06-112008-4", Title = "Matar a un ruiseñor", Author = "Harper Lee", Genre = "Ficción Histórica", Year = 1960, Copies = 2 });
        library.RegisterBook(new Book { ISBN = "978-0-14-028333-4", Title = "El Gran Gatsby", Author = "F. Scott Fitzgerald", Genre = "Ficción Clásica", Year = 1925, Copies = 6 });
    }
    }
}
