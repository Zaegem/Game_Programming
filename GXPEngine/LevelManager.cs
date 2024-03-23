﻿using System.Collections.Generic;
using System.Drawing;
using GXPEngine.Core;

namespace GXPEngine
{
    class LevelManager
    {
        public Vector2 LevelPosition = new Vector2(0, 0);

        int tileSize = 18;
        List<Sprite> tiles = new List<Sprite>();

        public List<Sprite> levelSprites = new List<Sprite>();

        int[,] level = new int[15, 26]
        {
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            { 47, 47, 48, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 46, 47, },
            { 157, 157, 157, 47, 47, 48, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 46, 47, 47, 157, 157, },
            { 157, 157, 157, 157, 157, 157, 47, 47, 47, 47, 47, 47, 47, 47, 47, 47, 47, 47, 47, 47, 47, 157, 157, 157, 157, 157, },
            { 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, },
            { 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, },
            { 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, },
            { 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, },
            {  157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157, 157 }
        };

        private MyGame game;

        public LevelManager(MyGame game)
        {
            float levelWidth = level.GetLength(1) * tileSize;
            float levelHeight = level.GetLength(0) * tileSize;
            
            this.game = game;

            Sprite blankSprite = new Sprite(new Bitmap(tileSize, tileSize));
            tiles.Add(blankSprite);

            Sprite tileMap = new Sprite("assets/GameTileMap.png");
            int numColumns = tileMap.width / tileSize;
            int numRows = tileMap.height / tileSize;

            for (int y = 0; y < numRows; y++)
            {
                for (int x = 0; x < numColumns; x++)
                {
                    Bitmap bitmap = new Bitmap(tileSize, tileSize);

                    for (int j = 0; j < tileSize; j++)
                    {
                        for (int i = 0; i < tileSize; i++)
                        {
                            int spriteX = x * tileSize + i;
                            int spriteY = y * tileSize + j;

                            Color pixel = tileMap.texture.bitmap.GetPixel(spriteX, spriteY);
                            bitmap.SetPixel(i, j, pixel);
                        }
                    }

                    Sprite tile = new Sprite(bitmap);
                    tiles.Add(tile);
                }
            }
        }

        public void CreateLevel()
        {
            for (int y = 0; y < level.GetLength(0); y++)
            {
                for (int x = 0; x < level.GetLength(1); x++)
                {
                    int tileIdx = level[y, x];
                    Sprite tile = tiles[tileIdx];

                    int tileX = x * tileSize;
                    int tileY = y * tileSize;
                    Sprite sprite = new Sprite(tile.texture.bitmap);

                    if (tileIdx == 0 || tileIdx == 1)
                    {
                        sprite.collider.isTrigger = true;
                    }

                    game.AddChild(sprite);
                    sprite.SetXY(tileX, tileY);
                    levelSprites.Add(sprite);
                }
            }
        }

        public void Move(float x, float y)
        {
            for(int i = 0; i < levelSprites.Count; i++)
            {
                Sprite sprite = levelSprites[i];

                sprite.Move(x, y);
            }

            LevelPosition.x += x;
            LevelPosition.y += y;
        }
    }
}