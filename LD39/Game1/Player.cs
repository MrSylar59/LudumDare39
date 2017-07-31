using LD39.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LD39.Game1
{
    class Player : GameObject
    {
        private float _speed;
        private float _score;
        private float _battery;
        private const float _maxBattery = 100f;
        private bool _immobilized;
        private float _immobileDuration;
        private bool _collide = false;

        private new AnimatedSprite _sprite;
        private Sprite _light;
        private SoundEffect _whispers, _music;
        SoundEffectInstance sound, music;

        public Player(Vector2 pPos)
        {
            _sprite = new AnimatedSprite(Ressources.Images["PlayerIdle"], new Vector2(pPos.X, pPos.Y), Constants.PlayerWidth, Constants.PlayerHeight, 3);
            _sprite.SetInterval(0.4f);
            _light = new Sprite(Ressources.Images["Lightmask"], new Vector2(pPos.X - 1000+16, pPos.Y - 1000+16), 2000, 2000);
            _position = pPos;
            _speed = 3f;
            _battery = _maxBattery;
            _score = 0;
            _immobilized = false;
            _velocity = Vector2.Zero;
            _whispers = Ressources.Sounds["Whispers"];
            _music = Ressources.Sounds["MainMusic"];

            music = _music.CreateInstance();
            music.Volume = 1f;
            music.IsLooped = true;
            music.Play();

            sound = _whispers.CreateInstance();
            sound.Volume = 0;
            sound.IsLooped = true;
            sound.Play();
        }

        public override void Update(GameTime pGameTime)
        {
            HandleInput();
            ClampPlayerToLevel();
            HandleLight();
            _sprite.Update(pGameTime);

            _battery -= 0.035f;

            if (_battery > _maxBattery)
            {
                _battery = _maxBattery;
            }

            if (_battery <= 0)
            {
                _battery = 0;
            }

            if (10 / _battery < 1f)
                sound.Volume = (10 / _battery);
            else
                sound.Volume = 1f;

            music.Volume = (_battery / 100);
            _score += 0.01f;

            if (_score < 0)
            {
                _score = 0;
            }

            if (_immobilized)
            {
                HandleImmobilization(pGameTime);
            }

            _sprite.SetPos(_position);
        }

        public override void Draw(SpriteBatch pSpriteBatch)
        {
            _sprite.Draw(pSpriteBatch);
            if (Constants.LightActive)
                _light.Draw(pSpriteBatch);
        }

        private void MovePlayer(Vector2 _newPos)
        {
            if (!_collide)
                _position += _newPos;
            _collide = false;
            _hitBox = new Rectangle((int)_position.X, (int)_position.Y, Constants.PlayerWidth, Constants.PlayerHeight);
        }

        private void HandleLight()
        {
            _light.SetPos(new Vector2(_position.X - 1000 * _light.GetScale() + Constants.PlayerWidth/2, _position.Y - 1000 * _light.GetScale() + Constants.PlayerHeight/2));
            _light.ChangeScale(_battery / 100);

            if (_light.GetScale() <= 0.19f)
                _light.ChangeScale(0.19f);
        }

        public void HandleCollision(GameObject other)
        {
            _collide = true;

            if (_velocity.X > 4)
            {
                _position.X = other.GetPosition().X - Constants.PlayerWidth;
            }
            else if (_velocity.X < -4)
            {
                _position.X = other.GetPosition().X + Constants.PlayerWidth;
            }
            else if (_velocity.Y > 4)
            {
                _position.Y = other.GetPosition().Y - Constants.PlayerHeight;
            }
            else if (_velocity.Y < -4)
            {
                _position.Y = other.GetPosition().Y + Constants.PlayerHeight;
            }
        }

        public void HandleInput()
        {
            KeyboardState ks = Keyboard.GetState();
            _velocity = Vector2.Zero;

            if (ks.IsKeyDown(Keys.W) || ks.IsKeyDown(Keys.Z))
            {
                _velocity.Y = -1;
                HandleAnimation("PlayerWalkUp");
            }
            else if (ks.IsKeyDown(Keys.S))
            {
                _velocity.Y = 1;
                HandleAnimation("PlayerWalkDown");
            }

            if (ks.IsKeyDown(Keys.A) || ks.IsKeyDown(Keys.Q))
            {
                _velocity.X = -1;
                HandleAnimation("PlayerWalkLeft");
            }
            else if (ks.IsKeyDown(Keys.D))
            {
                _velocity.X = 1;
                HandleAnimation("PlayerWalkRight");
            }

            if (_velocity != Vector2.Zero)
                _velocity.Normalize();
            else
                HandleAnimation("PlayerIdle");

            _velocity *= _speed;

            MovePlayer(_velocity);
        }

        private void HandleAnimation(string pAnim)
        {
            if (!_immobilized)
            {
                switch (pAnim)
                {
                    case "PlayerWalkDown":
                        _sprite.UpdateTexture(Ressources.Images["PlayerWalkDown"], 4, 0.15f);
                        break;
                    case "PlayerWalkUp":
                        _sprite.UpdateTexture(Ressources.Images["PlayerWalkUp"], 4, 0.15f);
                        break;
                    case "PlayerWalkRight":
                            if (_velocity.Y == 0)
                                _sprite.UpdateTexture(Ressources.Images["PlayerWalkRight"], 4, 0.15f);
                        break;
                    case "PlayerWalkLeft":
                        if (_velocity.Y == 0)
                            _sprite.UpdateTexture(Ressources.Images["PlayerWalkLeft"], 4, 0.15f);
                        break;
                    default:
                        _sprite.UpdateTexture(Ressources.Images["PlayerIdle"], 4, 0.2f);
                        break;
                }
            }
            else
            {
                _sprite.UpdateTexture(Ressources.Images["PlayerHurt"], 3, 0.1f);
            }
        }

        private void HandleImmobilization(GameTime pGameTime)
        {
            _immobileDuration -= (float)pGameTime.ElapsedGameTime.TotalSeconds;
            if (_immobileDuration <= 0)
            {
                SetSpeed(3f);
                _immobilized = false;
            }
        }

        private void ClampPlayerToLevel()
        {
            if (_position.X < 32){ _position.X = 32; }
            if (_position.Y < 64) { _position.Y = 64; }
            if (_position.X + Constants.PlayerWidth + 32 > Constants.GameWidth) { _position.X = Constants.GameWidth - Constants.PlayerWidth - 32; }
            if (_position.Y + Constants.PlayerHeight + 32> Constants.GameHeight) { _position.Y = Constants.GameHeight - Constants.PlayerHeight - 32; }

            // Hardcoded collisions because of shity physic :x
            if (_position.X > 11*32 && _position.X < 12*32 && _position.Y < 8*32) { _position.X = 11 * 32; }
            if (_position.X > 11*32 &&  _position.X < 13*32 && _position.Y < 8*32) { _position.X = 13 * 32; }
            if (_position.X > 16*32 && _position.X < 17*32 && ((_position.Y > 8*32 && _position.Y < 13*32) || _position.Y > 15*32)) { _position.X = 16 * 32; }
            if (_position.X > 16*32 && _position.X < 18*32 && ((_position.Y > 8*32 && _position.Y < 13*32) || _position.Y > 15*32)) { _position.X = 18 * 32; }
            if (_position.Y > 6*32 && _position.Y < 8*32 && ((_position.X < 5*32 || _position.X > 7*32) && (_position.X < 13*32 || _position.X > 16*32))) { _position.Y = 6 * 32; }
            if (_position.Y > 6*32 && _position.Y < 9*32 && ((_position.X < 5*32 || _position.X > 7*32) && (_position.X < 13*32 || _position.X > 16*32))) { _position.Y = 9 * 32; }
        }

        public void Immobilize(GameTime pGameTime, float pDuration)
        {
            SoundEffect playerHurt = Ressources.Sounds["PlayerHurt"];
            playerHurt.Play(0.6f, 0f, 0f);
            SetSpeed(0);
            _immobilized = true;
            _immobileDuration = Constants.ImmobileDuration;
        }

        public float GetSpeed()
        {
            return _speed;
        }

        public void SetSpeed(float pNewSpeed)
        {
            _speed = pNewSpeed;
        }

        public float GetBattery()
        {
            return _battery;
        }

        public void SetBattery(float pBattery)
        {
            _battery = pBattery;
        }

        public float GetScore()
        {
            return _score;
        }

        public void AddToScore(float pScore)
        {
            _score += pScore;
        }

        public Vector2 GetVelocity()
        {
            return _velocity;
        }

        public void SetVelocity(Vector2 pVelo)
        {
            _velocity = pVelo;
        }

        public void ResetSound()
        {
            sound.Stop();
            music.Stop();
        }
    }
}
