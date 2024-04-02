using GXPEngine;

class Bullet : GameObject
{
    private int radius;

    private EasyDraw sprite;

    public Bullet(int radius = 5)
    {
        this.radius = radius;

        sprite = new EasyDraw(radius, radius);
        sprite.SetOrigin(radius / 2f, radius / 2f);
        sprite.SetXY(x, y);
        AddChild(sprite);
    }

    void Update()
    {
        sprite.Ellipse(0, 0, radius, radius);
        // write movement code
        // write collision check
    }

    public bool IsColliding()
    {
        //GameObject[] collisions = bullet.GetCollisions();
        //foreach (GameObject obj in collisions)
        //{

        //}

        return false;
    }
}
