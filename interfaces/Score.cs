
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using learnmonogame.interfaces;
using learnmonogame.entities;
using learnmonogame.drawing;
using System;
using System.Collections.Generic;
using DeviceId  ;

namespace learnmonogame.interfaces
{
  public class Score : Component
  {
    
    public  struct data {
      public int Score ;
      public DateTime Date ;
      public DeviceIdBuilder deviceId ; 
    }

    private List<data> scoreLists = new List<data>(); 

    public void addScore(int score) {
      data result = new data() ;
      
      result.Score = score;
      result.Date = DateTime.Now;
      result.deviceId = new DeviceIdBuilder().AddMachineName().AddMacAddress();
      scoreLists.Add(result) ;
    }

    public void getFullScore() {
      int i=0 ; 
      foreach( data item in scoreLists) {
        i++; 
        Console.WriteLine( i+":"+item.Score) ; 
      }
    }

    public int getNumberOfDie() {
      return scoreLists.Count ; 
    }

    public int getHighScore() {
      int max = 0 ; 
      foreach( data item in scoreLists) {
        if (max < item.Score) {
          max = item.Score;
        }
      }
      return max ; 
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {

    }

    public override void Update(GameTime gameTime, SpriteBatch spriteBatch) {

    }
  }
}