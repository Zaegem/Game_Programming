using System;
using GXPEngine;


public class MyGame : Game
{
    LevelManager levelManager;
    static Player player;
    static MyGame game;

    
    
    public static MyGame GetGame()
    {
        return game;
    }

    public MyGame() : base(640, 480, false, false, 1024, 768, true)
    {
        levelManager = new LevelManager(this);
        levelManager.SpawnPlayer();
        levelManager.ChangeLevel(LevelType.Level1);
        //SoundHandler.test.play(1, 0);
    }

    void Update()
    {
        levelManager.Update();

    }
    static void Main()
    {
        new MyGame().Start();
    }
}
