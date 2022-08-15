// System : C

using System;

// MonoGame : D

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
// les classes 
using learnmonogame.entities;
using learnmonogame.drawing;

namespace learnmonogame.entities
{
    public class Projectile : Entity
    {

        static public int score = 0 ; 


        public Projectile(string name, Texture2D texture, float velocity)
        {
            _entityName = name;
            _entityPosition = randomSpawn();
            _texture = texture;
            _entityVelocity = velocity;


        }

        

        private Vector2 randomSpawn()
        {
            score++ ; 
            return new Vector2(0, Game1.rand.Next(0, Game1.screenHeight));
        }

        public override void SetPosition(float x, float y)
        {
            if (_entityPosition.X >= 0 && _entityPosition.Y >= 0 && _entityPosition.X <= Game1.screenWidth && _entityPosition.Y <= Game1.screenHeight)
            {
                _entityPosition = new Vector2(x, y);
            }
            else
            {
                _entityPosition = randomSpawn();
            }
        }

        public override void Update(GameTime gameTime, SpriteBatch sprite)
        {
            this.SetPosition(_entityPosition.X + this.velocity * (float)gameTime.ElapsedGameTime.TotalSeconds, _entityPosition.Y);
        }



    }
}
