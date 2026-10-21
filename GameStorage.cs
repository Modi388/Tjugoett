namespace Tjugoett;

// aldrig console. Bara data.
public class GameStorage
{
    private readonly string _filePath; // player.txt skickas in

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
        catch (IOException) // låst eller trasig
        {
            return records;
        }
        catch (UnauthorizedAccessException) // ingen tillgång
        {
            return records;
        }
// Mohammed;3;2;1
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

// skriver till filen.
    public bool Save(IEnumerable<Player> players)
    {
        List<string> lines = new();

        foreach (Player player in players)
        {
            lines.Add($"{player.Name};{player.Wins};{player.Losses};{player.Pushes}");
        }
        try
        {
            File.WriteAllLines(_filePath, lines);
            return true;
        }
        catch (IOException)
        {
            return false;
        }
        catch (UnauthorizedAccessException)
        {
            return false;
        }
    }
}