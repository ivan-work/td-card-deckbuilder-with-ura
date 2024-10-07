public interface Card
{
  string name { get; }
}


// x
public class SlashCard : Card
{
  public string name { get => "SlashCard"; }
  // public Matrix getTargetingCells() {
  //   // return new NotImplemented
  // }
}

// xxxxx
public class PierceCard : Card
{
  public string name { get => "PierceCard"; }
}

// xx
// xx
public class ArcherTowerCard : Card
{
  public string name { get => "ArcherTowerCard"; }
  // public string name = "ArcherTowerCard";
}

// xx
// xx
public class SwordsmanTowerCard : Card
{
  public string name { get => "SwordsmanTowerCard"; }
  // public string name = "SwordsmanTowerCard";
}