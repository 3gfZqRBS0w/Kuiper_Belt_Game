
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using kuiperbeltgame.interfaces;
using kuiperbeltgame.entities;
using kuiperbeltgame.drawing;
using System;
using System.Collections.Generic;

namespace kuiperbeltgame.interfaces
{
  public class Label : Component 
  {

    private Vector2 _position;
    private string _text ; 
    private SpriteFont _font;
    private  Color _col ;

    public Label(string text, SpriteFont font, Color col, Vector2 pos) {
      _text = text ; 
      _position = pos;
      _col = col;
      _font = font ;
    }

    // for static text 
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
      spriteBatch.DrawString(_font, _text, _position, _col) ;    
    }

    // for dynamic text
    public void DrawScore(string str, GameTime gameTime, SpriteBatch spriteBatch) {
      spriteBatch.DrawString(_font, str, _position, _col) ;
    }
    public override void Update(GameTime gameTime, SpriteBatch spriteBatch) {

    }


  }
}