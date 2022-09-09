
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using kuiperbeltgame.interfaces;
using kuiperbeltgame.entities;
using kuiperbeltgame.drawing;
using kuiperbeltgame.utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization; 
using DeviceId  ;

namespace kuiperbeltgame.interfaces
{


[XmlRoot("scorelists")]
  public class ListOfScores {

    [XmlElement("scores")]
    public List<ScoreData> scores = new List<ScoreData>{} ;

    public ListOfScores() {}


    
  }
  public class ScoreData {

    [XmlElement("score")]
    private int _score ;

    [XmlElement("date")]
    private DateTime _date;

    public ScoreData() {}

    public int Score {
      get {return this._score; }
      set {this._score = value; }
    }

    public DateTime Date {
      get {return this._date; }
      set {this._date = value; }
     }
  }
  
  public class Score : Component
  {

    private SpriteFont _font ;
    
    public ListOfScores scoreLists = new ListOfScores();


    public Backup b = new Backup(); 

    private List<Component> _scoreboardComponents = new List<Component>() ;


    #region scoreboardComponents
    private Button returnButton ;
    private Label textOfScoreboard ; 
    #endregion



   
    public void addScore(int score) {
      ScoreData result = new ScoreData() { Score = score, Date = DateTime.Now} ;

      //result.deviceId = new DeviceIdBuilder().AddMachineName().AddMacAddress();
      scoreLists.scores.Add(result) ;
    }

    public void tidyList() {
      
    ScoreData permutation ;
    int iteration = scoreLists.scores.Count;
      for (int i = 0; i < iteration; i++) {
        for (int j = 0; j < iteration; j++) {
          if ( scoreLists.scores[i].Score > scoreLists.scores[j].Score) {
            permutation = scoreLists.scores[i] ;
            scoreLists.scores[i] = scoreLists.scores[j] ;
            scoreLists.scores[j] = permutation ;
          }
        }
      }
    } 


    public int getNumberOfDie() {
      return scoreLists.scores.Count ; 
    }

    private string getScoreboardString() {


      /* 
      CHANTIER PAS FINI : D
      */
      string[] nameOfColumns = new string[] { "RANK", "SCORE", "DATE" };
       
      string resultat = "TOTAL SCORE RANKING"+Environment.NewLine ;
      string[] obj ; 
      int i = 0 ;

      for (int x = 0; x < nameOfColumns.Count(); x++) {
        resultat += nameOfColumns[x] ; 
        for ( int j = 10 ; j < (Game1.screenWidth-10)/(nameOfColumns.Count()*10); j++ ) {
          resultat += " " ; 
        }
      }

      foreach( var item in scoreLists.scores) {
        resultat += Environment.NewLine ;
        obj = new string[] {(i+1).ToString(),item.Score.ToString(),item.Date.ToString("MM/dd/yyyy (h:mm pp)") } ; 


        for (int x = 0; x < obj.Count(); x++) {
        resultat += obj[x] ; 
        for ( int j = 10 ; j < (Game1.screenWidth-10)/(nameOfColumns.Count()*10); j++ ) {
          resultat += " " ; 
        }
      }

      i++; 
      }

      return resultat; 
    }


    public int getHighScore() {
      int max = 0 ; 
      foreach( ScoreData item in scoreLists.scores) {
        if (max < item.Score) {
          max = item.Score;
        }
      }
      return max ; 
    }

    public void LoadContent(SpriteFont font,SpriteBatch spriteBatch)  {
      _font = font ;
        textOfScoreboard = new Label( getScoreboardString(), _font, Color.Black, new Vector2(10,50)) ;
        
        if ( b.IsBackupFileExists()) {
          scoreLists = b.GetPreviousSave();
        }
        
        }

    public void SaveInFile() {
      b.SaveBackupFile(this.scoreLists); 
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
      this.tidyList() ;
      textOfScoreboard.DrawScore(getScoreboardString(),gameTime, spriteBatch);
    }

    public override void Update(GameTime gameTime, SpriteBatch spriteBatch) {
    
    }
  }
}