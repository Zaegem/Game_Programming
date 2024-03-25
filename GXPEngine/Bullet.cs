using GXPEngine;

class Bullet : GameObject
{
    public Vec2 position;

    private int radius;

    public Bullet(Vec2 position, int radius = 5)
    {
        this.position = position;
        this.radius = radius;
    }

    void Update()
    {

    }

    public void SpawnBullet(float x, float y)
    {
        MyGame game = MyGame.GetGame();
        EasyDraw bullet = new EasyDraw(radius, radius);
        game.AddChild(bullet);
        bullet.SetOrigin(radius/2, radius/2);
        bullet.SetXY(x, y);
        bullet.Ellipse(0, 0, radius, radius);
        GameObject[] collisions = bullet.GetCollisions();
        foreach (GameObject obj in collisions)
        {

        }

        bullet.LateDestroy();
    }
}