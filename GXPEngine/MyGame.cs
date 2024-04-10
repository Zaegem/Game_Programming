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

    public MyGame() : base(320, 240, false, false, 980, 720, true)
    {
        EasyDraw canvas = new EasyDraw(800, 600);
        levelManager = new LevelManager(this);
        levelManager.CreateLevel();
        levelManager.SpawnPlayer();
        levelManager.SpawnPlantEnemy();
        levelManager.SpawnTrunkEnemy();
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
