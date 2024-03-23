using System;                                   
using GXPEngine;                                


public class MyGame : Game {

	LevelManager levelManager;
	Player player;
    private float offSet = 490;

	public MyGame() : base(320, 240, false, false, 980, 720, true)  
	{
		levelManager = new LevelManager(this);
		levelManager.CreateLevel();
        Console.WriteLine("Hello World!");

        EasyDraw canvas = new EasyDraw(800, 600);
        //SoundHandler.test.play(1, 0);

		player = new Player(levelManager);
		AddChild(player);
		player.SetXY(width / 2, height / 2);
	}

	void Update() {
		Scroll();
    }

    void Scroll()
    {
        Console.WriteLine("I hate this");
		float boundarySize = 36;

        if (player.x + player.width + boundarySize >= levelManager.LevelPosition.x && Input.GetKey(Key.D))
        {
			levelManager.Move(-1, 0);
        }

		if(player.x - boundarySize <= levelManager.LevelPosition.x + 490 && Input.GetKey(Key.A))
		{
			levelManager.Move(1, 0);
		}
    }
    static void Main()                          
	{
		new MyGame().Start();                   
	}
}
