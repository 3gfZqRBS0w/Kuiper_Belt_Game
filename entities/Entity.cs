// System : C

using System;

// MonoGame : D

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
// les classes 
using kuiperbeltgame.entities;
using kuiperbeltgame.drawing;

namespace kuiperbeltgame.entities
{
    public abstract class Entity
    {
        protected string _entityName {get; set; }
        protected Texture2D _texture {get; set; }
        protected Vector2 _entityPosition { get; set; }

        protected float _entityVelocity {get; set;}
       
        public abstract void SetPosition(float x, float y) ;
        public abstract void Update(GameTime gameTime, SpriteBatch sprite) ;

        
             public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)_entityPosition.X, (int)_entityPosition.Y, _texture.Width, _texture.Height);
            }
        } 


        public void DebugEntity(SpriteFont font, SpriteBatch a, Vector2 pos, Color col)
        {

           a.DrawString(font, @"
        ENTITY NAME = + " + _entityName + @"  
        X = " + Math.Round(this.position.X) + @"
        Y = " + Math.Round(this.position.Y) + @"
        COLLISION 
        Top/Bottom = " + this.Rectangle.Top + "/" + this.Rectangle.Bottom + @"
        Left/Right = " + this.Rectangle.Left + "/" +  this.Rectangle.Right, pos, col);


        }

        
        



        // getters/setters 

        public Texture2D Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }

        public float velocity
        {
            get { return _entityVelocity; }
        }

        public Vector2 position
        {
            get { return _entityPosition; }
        }
        public string Name {
            get { return _entityName; }
        }








    }
}
