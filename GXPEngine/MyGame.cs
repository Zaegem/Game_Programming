using System;
using GXPEngine;


public class MyGame : Game
{

    LevelManager levelManager;
    Player player;
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
        //SoundHandler.test.play(1, 0);

        player = new Player(levelManager, new Vec2(width / 2f, 0f));
        AddChild(player);
    }

    void Update()
    {
        Scroll();
    }

    void Scroll()
    {
        float boundarySize = 36;

        //if (player.x + player.width + boundarySize >= levelManager.LevelPosition.x && Input.GetKey(Key.D))
        //{
        //    levelManager.Move(-1, 0);
        //}

        //if (player.x - boundarySize <= levelManager.LevelPosition.x + 490 && Input.GetKey(Key.A))
        //{
        //    levelManager.Move(1, 0);
        //}
    }
    static void Main()
    {
        new MyGame().Start();
    }
}
