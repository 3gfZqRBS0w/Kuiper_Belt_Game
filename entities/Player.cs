
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
    public class Player : Entity
    {

        
        public Player(string name, Texture2D texture, Vector2 position, float velocity)
        {
            _entityName = name;
            _texture = texture;
            _entityPosition = position;
            _entityVelocity = velocity;

        }



        public bool Collision(Projectile proj)
        {
            
            return  ( ( (this.Rectangle.Top > proj.Rectangle.Top) && (this.Rectangle.Top < proj.Rectangle.Bottom) ) ||  ( (this.Rectangle.Bottom > proj.Rectangle.Top)  && (this.Rectangle.Bottom < proj.Rectangle.Bottom) )  ) && (this.Rectangle.Left > proj.Rectangle.Left && this.Rectangle.Left < proj.Rectangle.Right)     ;
        }

//


        public override void Update(GameTime gameTime, SpriteBatch sprite)
        {


   
       

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) Environment.Exit(0);


            var kstate = Keyboard.GetState();


                
            if (kstate.IsKeyDown(Keys.Left))
            {

                this.SetPosition(this.position.X - this.velocity * (float)gameTime.ElapsedGameTime.TotalSeconds, this.position.Y);
            }

            if (kstate.IsKeyDown(Keys.Right))
            {
                this.SetPosition(this.position.X + this.velocity * (float)gameTime.ElapsedGameTime.TotalSeconds, this.position.Y);
            }



            if (kstate.IsKeyDown(Keys.Up))
            {
                this.SetPosition(this.position.X, this.position.Y - this.velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
            }

            if (kstate.IsKeyDown(Keys.Down))
            {
                this.SetPosition(this.position.X, this.position.Y + this.velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);
            }

            
            


        }



        public override void SetPosition(float x, float y)
        {
            if (_entityPosition.X >= 0 && _entityPosition.Y >= 0 && _entityPosition.X <= Game1.screenWidth && _entityPosition.Y <= Game1.screenHeight)
            {
                _entityPosition = new Vector2(x, y);
            }
            else
            {
                _entityPosition = new Vector2(Game1.screenWidth / 2, Game1.screenHeight / 2);
            }
        }

    }
}
