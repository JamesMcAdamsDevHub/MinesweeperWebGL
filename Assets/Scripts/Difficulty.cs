using Color = UnityEngine.Color;

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

    public static int GetBombCountByDifficulty(Difficulty difficulty) => difficulty switch
    {
        Difficulty.Easy => 8,
        Difficulty.Normal => 16,
        Difficulty.Challenge => 30,
        _ => 16
    };

    public static int GetBoardWidthByDifficulty(Difficulty difficulty) => difficulty switch
    {
        Difficulty.Easy => 8,
        Difficulty.Normal => 10,
        Difficulty.Challenge => 12,
        _ => 10
    };

    public static Color GetTextColorByDifficulty(Difficulty difficulty) => difficulty switch
    {
        Difficulty.Easy => Color.forestGreen,
        Difficulty.Normal => Color.cornflowerBlue,
        Difficulty.Challenge => Color.mediumPurple,
        _ => Color.cornflowerBlue
    };
}


