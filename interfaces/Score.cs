
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using learnmonogame.interfaces;
using learnmonogame.entities;
using learnmonogame.drawing;
using System;
using System.Collections.Generic;

namespace learnmonogame.interfaces
{
  public class Score : Component
  {
    
    private struct data {
      uint Score ;
      DateTime Date ;
      string deviceId ; //new DeviceIdBuilder().AddMachineName().AddMacAddress().AddProcessorId().AddMotherboardSerialNumber().ToString();
    }

    private List<data> scoreLists = new List<data>(); 

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {

    }

    public override void Update(GameTime gameTime, SpriteBatch spriteBatch) {

    }
  }
}