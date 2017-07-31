using LD39.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace LD39.Game1
{
    class Level
    {
        private static bool _mustReload;

        private int _width, _height;

        private Player _player;

        private List<PowerUp> _powerUps;
        private float _nextSpawn = 5f;

        private List<PowerDown> _powerDowns;
        private float _nextSpawn2 = 7f;

        private List<GameObject> _objects;

        private List<AnimatedSprite> _monsters;
        private float _nextSpawn3 = 2f;

        private Text _textScore;

        public Level(int pWidth, int pHeight)
        {
            _width = pWidth;
            _height = pHeight;
            _powerUps = new List<PowerUp>();
            _powerDowns = new List<PowerDown>();
            _monsters = new List<AnimatedSprite>();
        }

        public void Load(ContentManager pContent)
        {
            _player = new Player(new Vector2(50, 50));
            _objects = LevelLoader.GenerateLevel(LevelLoader.TexToArray(Ressources.Images["Level"]));
            _textScore = new Text("Score: ", new Vector2(10, 10));
        }   

        public void Update(GameTime pGameTime)
        {
            if (_mustReload)
            {
                _powerUps = new List<PowerUp>();
                _powerDowns = new List<PowerDown>();
                _player = new Player(new Vector2(50, 50));
                _mustReload = false;
            }

            _player.Update(pGameTime);
            _textScore.UpdateText("Score: " + (int)_player.GetScore());
            _textScore.SetPosition(new Vector2((int)_player.GetPosition().X - 250, (int)_player.GetPosition().Y - 180));
            SpawnPowerUps(pGameTime);
            SpawnPowerDowns(pGameTime);
            HandleDestructions(pGameTime);
            SpawnPowerMonsters(pGameTime);
        }

        public void Draw(SpriteBatch pSpriteBatch)
        {
            foreach (GameObject obj in _objects)
            {
                if (Math.Abs(obj.GetPosition().X - _player.GetPosition().X+16) < 5*32 &&
                    Math.Abs(obj.GetPosition().Y - _player.GetPosition().Y+16) < 5*32)
                    obj.Draw(pSpriteBatch);
            }

            foreach (PowerUp powerUp in _powerUps)
            {
                if (Math.Abs(powerUp.GetPosition().X - _player.GetPosition().X + 16) < 5*32 &&
                    Math.Abs(powerUp.GetPosition().Y - _player.GetPosition().Y + 16) < 5*32)
                    powerUp.Draw(pSpriteBatch);
            }

            foreach (PowerDown powerDown in _powerDowns)
            {
                if (Math.Abs(powerDown.GetPosition().X - _player.GetPosition().X + 16) < 5*32 &&
                    Math.Abs(powerDown.GetPosition().Y - _player.GetPosition().Y + 16) < 5*32)
                    powerDown.Draw(pSpriteBatch);
            }

            _player.Draw(pSpriteBatch);

            foreach (Sprite monster in _monsters)
            {
                    monster.Draw(pSpriteBatch);
            }

            _textScore.Draw(pSpriteBatch, Color.White, Ressources.LittleFont);
        }

        private void SpawnPowerUps(GameTime pGameTime)
        {
            Random rand = new Random();
            float gameTime = (float)pGameTime.ElapsedGameTime.TotalSeconds;
            _nextSpawn -= gameTime;

            if (_nextSpawn <= 0)
            {
                _nextSpawn = 5f;
                _powerUps.Add(new PowerUp(RandomSpawnVector()));
            }
        }

        private void SpawnPowerDowns(GameTime pGameTime)
        {
            Random rand = new Random();
            float gameTime = (float)pGameTime.ElapsedGameTime.TotalSeconds;
            _nextSpawn2 -= gameTime;

            if (_nextSpawn2 <= 0)
            {
                _nextSpawn2 = 7f;
                _powerDowns.Add(new PowerDown(RandomSpawnVector()));
            }
        }

        private void SpawnPowerMonsters(GameTime pGameTime)
        {
            Random rand = new Random();
            float gameTime = (float)pGameTime.ElapsedGameTime.TotalSeconds;
            _nextSpawn3 -= gameTime;

            if (_nextSpawn3 <= 0)
            {
                _nextSpawn3 = 2f;
                _monsters.Add(new AnimatedSprite(Ressources.Images["Evil"], RandomSpawnVector(), 32, 32, 5));
            }
        }

        private Vector2 RandomSpawnVector()
        {
            Random rand = new Random();

            Vector2[] randomVectors = new Vector2[4];
            randomVectors[0] = new Vector2(rand.Next(32, 11 * 32), rand.Next(64, 6 * 32));
            randomVectors[1] = new Vector2(rand.Next(13 * 32, 23 * 32), rand.Next(64, 6 * 32));
            randomVectors[2] = new Vector2(rand.Next(32, 16 * 32), rand.Next(9 * 32, 17 * 32));
            randomVectors[3] = new Vector2(rand.Next(18 * 32, 23 * 32), rand.Next(9 * 32, 17 * 32));

            return randomVectors[rand.Next(0, 3)];
        }

        private void HandleDestructions(GameTime pGameTime)
        {
            for (int i = _powerUps.Count - 1; i > -1; i--)
            {
                if (_player.CollideWith(_powerUps[i]))
                {
                    SoundEffect powerUp = Ressources.Sounds["PowerUp"];
                    powerUp.Play();

                    if (_player.GetBattery() < 100)
                        _player.SetBattery(_player.GetBattery() + Constants.Heal);
                    _player.AddToScore(Constants.BatteryBonus);
                    _powerUps.RemoveAt(i);
                    return;
                }

                _powerUps[i].DecreaseLifeSpan((float)pGameTime.ElapsedGameTime.TotalSeconds);

                if (_powerUps[i].GetLifeSpan() <= 0)
                    _powerUps.RemoveAt(i);
            }

            for (int i = _powerDowns.Count - 1; i > -1; i--)
            {
                if (_player.CollideWith(_powerDowns[i]))
                {
                    _player.SetBattery(_player.GetBattery() + Constants.Hurt);
                    _player.Immobilize(pGameTime, 3f);
                    _player.AddToScore(Constants.BatteryMalus);
                    _powerDowns.RemoveAt(i);
                    return;
                }

                _powerDowns[i].DecreaseLifeSpan((float)pGameTime.ElapsedGameTime.TotalSeconds);

                if (_powerDowns[i].GetLifeSpan() <= 0)
                    _powerDowns.RemoveAt(i);
            }

            foreach (GameObject obj in _objects)
            {
                if (obj.IsSolid())
                {
                    if (_player.CollideWith(obj))
                    {
                        _player.HandleCollision(obj);
                    }
                }
            }

            for (int i = _monsters.Count - 1; i > -1; i--)
            {
                _monsters[i].Update(pGameTime);

                if (Math.Abs(_monsters[i].GetPosition().X - _player.GetPosition().X + 16) < 4 * 32 &&
                    Math.Abs(_monsters[i].GetPosition().Y - _player.GetPosition().Y + 16) < 4 * 32)
                    _monsters.RemoveAt(i);
            }
        }

        public void HandleGameOver(SceneManager pSceneManager)
        {
            if (_player.GetBattery() <= 0)
            {
                Score.SetFinalScore(_player.GetScore());
                _player.ResetSound();
                pSceneManager.SetGameState(GameState.Score);
            }
        }

        public Player GetPlayer()
        {
            return _player;
        }

        public static void MustReload()
        {
            _mustReload = true;
        }
    }
}
