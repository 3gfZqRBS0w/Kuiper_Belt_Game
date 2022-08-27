
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using kuiperbeltgame.interfaces;
using kuiperbeltgame.entities;
using kuiperbeltgame.drawing;
using System;
using System.Collections.Generic;


namespace kuiperbeltgame.entities
{
    public class Rocket : Entity
    {


        public Rocket(string name, Texture2D texture, float velocity, Vector2 position)
        {

            _entityName = name;
            _texture = texture;
            _entityPosition = position;
            _entityVelocity = velocity;


        }

        public override void SetPosition(float x, float y)
        {
            if (_entityPosition.X >= 0 && _entityPosition.Y >= 0 && _entityPosition.X <= Game1.screenWidth && _entityPosition.Y <= Game1.screenHeight)
            {
                _entityPosition = new Vector2(x, y);
            }
            else
            {
                Game1.rockets.RemoveAt(0);
            }
        }

        public bool Collision(Projectile proj)
        {
             
            return (((this.Rectangle.Top > proj.Rectangle.Top) && (this.Rectangle.Top < proj.Rectangle.Bottom)) || ((this.Rectangle.Bottom > proj.Rectangle.Top) && (this.Rectangle.Bottom < proj.Rectangle.Bottom))) && (this.Rectangle.Left > proj.Rectangle.Left && this.Rectangle.Left < proj.Rectangle.Right);
        }


        public override void Update(GameTime gameTime, SpriteBatch sprite)
        {
            this.SetPosition(_entityPosition.X - this.velocity * (float)gameTime.ElapsedGameTime.TotalSeconds, _entityPosition.Y);
        }
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)_entityPosition.X, (int)_entityPosition.Y, _texture.Width, _texture.Height);
            }
        }
    }


}
