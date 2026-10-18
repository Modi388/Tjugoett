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

        string[] lines;
        try
        {
            lines = File.ReadAllLines(_filePath);
        }
        catch (IOException)
        {
            return records;
        }
        catch (UnauthorizedAccessException)
        {
            return records;
        }

        foreach (string line in lines)
        {
            string[] parts = line.Split(';');

            if (parts.Length != 4)
            {
                continue;
            }

            if (!int.TryParse(parts[1], out int wins) ||
                !int.TryParse(parts[2], out int losses) ||
                !int.TryParse(parts[3], out int pushes))
            {
                continue;
            }

            records.Add(new PlayerRecord(parts[0], wins, losses, pushes));
        }

        return records;
    }
}