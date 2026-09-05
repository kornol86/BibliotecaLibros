
///Dictionary(Diccionario/Mapa) para acceso ràpido por ISBN, Género y Autor
public class ServicioLibreria
    {
        ///Diccionario (Mapa) para almacenar libros por ISBN
        private readonly Dictionary<string, Book> _booksByIsbn = new Dictionary<string, Book>();

        ///Diccionario (Mapa) para almacenar libros por Género
        private readonly Dictionary<string, List<Book>> _booksByGenre = new Dictionary<string, List<Book>>();

        ///Diccionario (Mapa) para almacenar libros por Autor    
        private readonly Dictionary<string, List<Book>> _booksByAuthor = new Dictionary<string, List<Book>>();

        ///Conjuntos (HashSet) para almacenar géneros únicos.
        private readonly HashSet<string> _genres = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        ///Conjuntos (HashSet) para almacenar autores únicos.
        private readonly HashSet<string> _authors = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        ///Conjuntos (HashSet) todos los ISBNs registrados. 
        private readonly HashSet<string> _isbns = new HashSet<string>();

        /// <summary>
        /// Registra un nuevo libro en la biblioteca, asegurando que el ISBN sea único y que los campos obligatorios estén completos.
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        public bool RegisterBook(Book book)
        {

            if (string.IsNullOrWhiteSpace(book.ISBN) || string.IsNullOrWhiteSpace(book.Title))
            {
                Console.WriteLine("Error: ISBN y Título son obligatorios.");
                return false;
            }

            if (_booksByIsbn.ContainsKey(book.ISBN))
            {
                Console.WriteLine($"Error: Ya existe un libro con ISBN {book.ISBN}.");
                return false;
            }

            ///Registra en diccionario principal.
            _booksByIsbn[book.ISBN] = book;
            _isbns.Add(book.ISBN);

            ///Agregar a conjunto de géneros.
            if (!string.IsNullOrWhiteSpace(book.Genre))
            {
                if (!_booksByGenre.ContainsKey(book.Genre))
                    _booksByGenre[book.Genre] = new List<Book>();

                _booksByGenre[book.Genre].Add(book);
                _genres.Add(book.Genre);
            }

            if (!string.IsNullOrWhiteSpace(book.Author))
            {
                ///Agregar a conjunto de autores.
                _authors.Add(book.Author);
                
                if (!_booksByAuthor.ContainsKey(book.Author))
                    _booksByAuthor[book.Author] = new List<Book>();
                
                _booksByAuthor[book.Author].Add(book);
            }

            Console.WriteLine($"Libro registrado exitosamente: {book.Title}");
            return true;
        }
    ///Buscar un libro por ISBN.
    public Book? GetBookByIsbn(string isbn)
    {
        if (_booksByIsbn.TryGetValue(isbn, out var book))
            return book;
        return null;
    }
    /// <summary>
    /// Obtiene todos los libros de un género (usando el mapa por género).
    /// </summary>
    /// <param name="genre"></param>
    /// <returns></returns>
    public List<Book> GetBooksByGenre(string genre)
    {
        if (_booksByGenre.TryGetValue(genre, out var books))
            return books;
        return new List<Book>();
    }
    /// <summary>
    /// Obtiene todos los libros de un autor (usando el mapa por autor).
    /// </summary>
    /// <param name="author"></param>
    /// <returns></returns>
    public List<Book> GetBooksByAuthor(string author)
    {
        if (_booksByAuthor.TryGetValue(author, out var books))
            return books;
        return new List<Book>();
    }

    ///Verifica si un género existe.
    public bool GenreExists(string genre) => _genres.Contains(genre);

    ///Verifica si un autor existe.
    public bool AuthorExists(string author) => _authors.Contains(author);

    //Obtiene todos los géneros únicos.
    public IEnumerable<string> GetAllGenres() => _genres.OrderBy(g => g);

    //Obtiene todos los autores únicos.
    public IEnumerable<string> GetAllAuthors() => _authors.OrderBy(a => a);

    //Obtiene todos los libros registrados.
    public IEnumerable<Book> GetAllBooks() => _booksByIsbn.Values.OrderBy(b => b.Title);

    public int TotalBooks() => _booksByIsbn.Count;
    public int TotalGenres() => _genres.Count;
    public int TotalAuthors() => _authors.Count;

    public void ReportSummary()
    {
        Console.WriteLine("\nResumen de la Biblioteca");
        Console.WriteLine("**************************");
        Console.WriteLine($"Total de libros registrados: {_booksByIsbn.Count}");
        Console.WriteLine($"Total de géneros registrados: {_genres.Count}");
        Console.WriteLine($"Total de autores registrados: {_authors.Count}");
    }

    //REPORTERIA
    public void ReportDictionaryByIsbn()
    {
        Console.WriteLine("\nReporte de libros por ISBN");
        Console.WriteLine("**************************");
        Console.WriteLine("Estructura: Dictionary<string, Book>");
        Console.WriteLine($"Total de libros registrados: {_booksByIsbn.Count}\n");

        if (_booksByIsbn.Count == 0)
        {
            Console.WriteLine("No hay libros registrados.");
            return;
        }
        foreach (var kvp in _booksByIsbn.OrderBy(x => x.Key))
        {
            Console.WriteLine($"Clave (ISBN): {kvp.Key}");
            Console.WriteLine($" Valor: {kvp.Value}");
            Console.WriteLine("-----------------------------");

        }
    }

    public void ReportMapByGenre()
    {
        Console.WriteLine("\nReporte de libros por género");
        Console.WriteLine("**************************");
        Console.WriteLine("Estructura: Dictionary<string, List<Book>>");
        Console.WriteLine($"Total de géneros registrados: {_booksByGenre.Count}\n");

        if (_booksByGenre.Count == 0)
        {
            Console.WriteLine("No hay géneros registrados.");
            return;
        }

        foreach (var kvp in _booksByGenre.OrderBy(x => x.Key))
        {
            Console.WriteLine($"Clave (Género): {kvp.Key}");
            Console.WriteLine($" Valor: {kvp.Value.Count} libros");
            Console.WriteLine("-----------------------------");
            foreach (var book in kvp.Value)
            {
                Console.WriteLine($"  - {book.Title} (ISBN: {book.ISBN})");
            }
            Console.WriteLine();
        }
    }
    
    ///Muestra el Mapa de libros por autor.
    public void ReportMapByAuthor()
    {
        Console.WriteLine("\nReporte de libros por autor");
        Console.WriteLine("**************************");
        Console.WriteLine("Estructura: Dictionary<string, List<Book>>");
        Console.WriteLine($"Total de autores registrados: {_booksByAuthor.Count}\n");

        if (_booksByAuthor.Count == 0)
        {
            Console.WriteLine("No hay autores registrados.");
            return;
        }

        foreach (var kvp in _booksByAuthor.OrderBy(x => x.Key))
        {
            Console.WriteLine($"Clave (Autor): {kvp.Key}");
            Console.WriteLine($" Valor: {kvp.Value.Count} libros");
            Console.WriteLine("-----------------------------");
            foreach (var book in kvp.Value)
            {
                Console.WriteLine($"  - {book.Title} (ISBN: {book.ISBN})");
            }
            Console.WriteLine();
        }
    }

    ///Muestra el conjunto de géneros únicos.    
    public void ReportSetGenres()
    {
        Console.WriteLine("\nReporte de géneros registrados");
        Console.WriteLine("**************************");
        Console.WriteLine("Estructura: HashSet<string>");
        Console.WriteLine($"Total de géneros registrados: {_genres.Count}\n");

        if (_genres.Count == 0)
        {
            Console.WriteLine("No hay géneros registrados.");
            return;
        }

        int i = 1;
        foreach (var genre in _genres.OrderBy(g => g))
        {
            Console.WriteLine($"- {i++}. {genre}");
        }
    }
    
    ///Muestra el conjunto de autores únicos.
    public void ReportSetAuthors()
    {
        Console.WriteLine("\nReporte de autores registrados");
        Console.WriteLine("**************************");
        Console.WriteLine("Estructura: HashSet<string>");
        Console.WriteLine($"Total de autores registrados: {_authors.Count}\n");

        if (_authors.Count == 0)
        {
            Console.WriteLine("No hay autores registrados.");
            return;
        }

        int i = 1;
        foreach (var author in _authors.OrderBy(a => a))
        {
            Console.WriteLine($"- {i++}. {author}");
        }
    }
}