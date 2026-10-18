namespace Tjugoett;

public class GameStorage
{
    private readonly string _filePath;

    public GameStorage(string filePath)
    {
        _filePath = filePath;
    }

    public List<PlayerRecord> Load()
    {
        List<PlayerRecord> records = new();
        if (!File.Exists(_filePath))
        {
            return records;
        }
        
        return records;
    }

}