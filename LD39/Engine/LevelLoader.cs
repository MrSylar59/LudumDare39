using LD39.Game1;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace LD39.Engine
{
    abstract class LevelLoader
    {
        private static List<GameObject> _objects = new List<GameObject>();

        public static Color[,] TexToArray(Texture2D pTex)
        {
            Color[] rawData = new Color[pTex.Width * pTex.Height];
            pTex.GetData(rawData, 0, rawData.Length);

            Color[,] data = new Color[pTex.Height, pTex.Width];
            for (int x = 0; x < pTex.Height; x++)
            {
                for (int y = 0; y < pTex.Width; y++)
                {
                    data[x, y] = rawData[x * pTex.Width + y];
                }
            }

            return data;
        }

        public static List<GameObject> GenerateLevel(Color[,] data)
        {
            int blockID;

            for (int x = 0; x < Constants.GameWidth / Constants.TextureWidth; x++)
            {
                for (int y = 0; y < Constants.GameHeight / Constants.TextureHeight; y++)
                {
                    blockID = data[y, x].R;
                    _objects.Add(SpawnGameObject(blockID, x, y));
                }
            }

            return _objects;
        }

        private static GameObject SpawnGameObject(int pBlockID, int x, int y)
        {
            Vector2 pos = new Vector2(x * Constants.TextureWidth, y * Constants.TextureHeight);
            switch (pBlockID)
            {
                case 0:
                    return new Wall(pos);
                case 1:
                    GameObject wall = new Wall(pos);
                    wall.SetTexture(Ressources.Images["Wall2"]);
                    return wall;
                case 25:
                    GameObject floor = new Floor(pos);
                    floor.SetTexture(Ressources.Images["Floor2"]);
                    return floor;
                case 70:
                    GameObject floor1 = new Floor(pos);
                    floor1.SetTexture(Ressources.Images["Floor3"]);
                    return floor1;
                default:
                    return new Floor(pos);
            }
        }
    }
}
