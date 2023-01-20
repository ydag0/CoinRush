using System;

public class Settings : Singleton<Settings>
{
    public static Action gameOver;
    public static Action gameEnd;
    public GameSettings setting;

}
