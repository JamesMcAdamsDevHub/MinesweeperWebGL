public enum Difficulty
{
    Easy,
    Normal,
    Challenge
}

public static class DifficultyParser
{
    public static Difficulty GetDifficultyFromString(string str) => str switch
    {
        "Easy" => Difficulty.Easy,
        "Normal" => Difficulty.Normal,
        "Challenge" => Difficulty.Challenge,
        _ => Difficulty.Normal
    };

}


