/// <summary>
/// Representa un libro en la Biblioteca.
/// </summary>
public class Book
{
    public string ISBN { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public int Year { get; set; }
    public int Copies { get; set; }

    public override string ToString()
    {
        return $"ISBN: {ISBN}, Título: {Title}, Autor: {Author}, Género: {Genre}, Año: {Year}, Ejemplares: {Copies}";
    }

    public override bool Equals(object? obj)
    {
        if (obj is Book other)
            return ISBN == other.ISBN;
        return false;
    }

    public override int GetHashCode()
    {
        return ISBN.GetHashCode();
    }
}