using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using kuiperbeltgame.interfaces;
using kuiperbeltgame.entities;
using kuiperbeltgame.drawing;
using kuiperbeltgame.utilities; 
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Media;

namespace kuiperbeltgame;

public class Game1 : Game
{
    // Ele
    #region ConfigurationPartieDebut
    private int _minSpeed = 300;
    private int _maxSpeed = 800;
    private int _nbOfProjectiles = 5;
    private float tempsEntreRockets = 1;

    #endregion

    #region entities
    private Player _localPlayer1;
    public static List<Projectile> projectiles = new List<Projectile>();
    public static List<Rocket> rockets = new List<Rocket>();
    #endregion

    #region gamecomponents

    #region hudcomponents
    private Label _scoreOfGame;
    private Label _numberOfDeaths ;
    private Label _highScore;
    #endregion 

    #region mainMenu
    private List<Component> _mainMenuComponent;
    private List<Component> _scoreboardMenuComponent ; 
    private List<Component> _topHud ;
    #endregion

    #endregion

    #region trucutile

    public Song music ; 
    public Score scoreboard = new Score() ; 
    public static int screenWidth, screenHeight;
    public int _idMenu = 0;
    public static Random rand = new Random();
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    public static SpriteFont debugFont;
    private float _cooldownBeforeRocket = 0;
    #endregion
    
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        base.Initialize();
    }


    protected override void LoadContent()
    {

        screenWidth = GraphicsDevice.Viewport.Width;
        screenHeight = GraphicsDevice.Viewport.Height;

        this.music = Content.Load<Song>("stage1");

        



        _spriteBatch = new SpriteBatch(GraphicsDevice);
        
        debugFont = Content.Load<SpriteFont>("debug");
        scoreboard.LoadContent(debugFont, _spriteBatch);
    
        _scoreOfGame = new Label("Score Label",debugFont, Color.White, new Vector2(0f, 0f)) ;
        _numberOfDeaths = new Label("Death Label", debugFont, Color.White, new Vector2(screenWidth-250, 0f)) ;
        _highScore = new Label("High Score Label", debugFont, Color.White, new Vector2(screenWidth/2-100, 0f)) ;


        #region definebutton

        var titleScreen = new Button(Surface.DrawRect(ref _spriteBatch, screenWidth, 32, Color.White), debugFont,0)
        {
            Position = new Vector2(0, 0),
            Text = "Kuiper Belt Game A0.2.1",
        };

        var newGameButton = new Button(Surface.DrawRect(ref _spriteBatch, 180, 32, Color.White), debugFont,1)
        {
            Position = new Vector2(0, 100),
            Text = "New Game",
        };

        var scoreboardButton = new Button(Surface.DrawRect(ref _spriteBatch, 180, 32, Color.White), debugFont,1) {
            Position = new Vector2(0,150),
            Text = "Scoreboard",
        };

        var settingButton = new Button(Surface.DrawRect(ref _spriteBatch, 180, 32, Color.White), debugFont,1) {
            Position = new Vector2(0,200),
            Text = "Setting",
        };

        var quitButton = new Button(Surface.DrawRect(ref _spriteBatch, 180, 32, Color.White), debugFont,1)
        {
            Position = new Vector2(0, 250),
            Text = "Quit",
        };

        var creditButton = new Button(Surface.DrawRect(ref _spriteBatch, screenWidth, 32, Color.White), debugFont,0)
        {
            Position = new Vector2(0, screenHeight-32),
            Text = "Written by Lombres",
        };

        var returnButton = new Button(Surface.DrawRect(ref _spriteBatch, Game1.screenWidth, 32, Color.White), debugFont,0)
        {
            Position = new Vector2(0, Game1.screenHeight-32),
            Text = "Back to main menu",
        };

        // Les événements



        _mainMenuComponent = new List<Component>()
      {
        titleScreen,
        newGameButton,
        settingButton,
        scoreboardButton,
        quitButton,
        creditButton
      };

      _scoreboardMenuComponent = new List<Component>() {
        titleScreen,
        returnButton,
      };

        newGameButton.Click += newGameButton_Click;
        quitButton.Click += QuitButton_Click;
        scoreboardButton.Click += scoreboardButton_Click;
        returnButton.Click += returnButton_Click;


        #endregion



        #region loadentities
        _localPlayer1 = new Player("player 1", Surface.DrawRect(ref _spriteBatch, 32, 32, Color.White), new Vector2(screenWidth / 2, 0), 500);

        for (int i = 0; i < _nbOfProjectiles; i++)
        {
            projectiles.Add(new Projectile("projectile " + i, Surface.DrawRect(ref _spriteBatch, 32, 32, Color.White), rand.Next(_minSpeed, _maxSpeed)));
        }
        #endregion


    }


    private void scoreboardButton_Click(object sender, System.EventArgs e) {
        _idMenu = 2 ;
    }

    private void returnButton_Click(object sender, System.EventArgs e) {
        _idMenu = 0 ;

    }
    private void QuitButton_Click(object sender, System.EventArgs e)
    { 
        scoreboard.SaveInFile();
        Exit();
    }

    private void newGameButton_Click(object sender, System.EventArgs e)
    {
        _idMenu = 1;
        MediaPlayer.Play(music);
        
        Projectile.score = 0;
    }

    protected override void Update(GameTime gameTime)
    {

        if (_idMenu == 1)
        {
            var kstate = Keyboard.GetState();

            _cooldownBeforeRocket += gameTime.ElapsedGameTime.Milliseconds; ;

            if (kstate.IsKeyDown(Keys.W) && _cooldownBeforeRocket >= 500)
            {
                _cooldownBeforeRocket = 0;
                rockets.Add(new Rocket("rocket " + rockets.Count, Surface.DrawRect(ref _spriteBatch, 16, 32, Color.Green), 800, _localPlayer1.position));
            }


            foreach (Projectile projectile in new List<Projectile>(projectiles))
            {
                if (projectiles.Contains(projectile))
                {
                    if (_localPlayer1.Collision(projectile))
                    {
                        MediaPlayer.Stop();
                        scoreboard.addScore(Projectile.score);
 

                        _idMenu = 0;

                        projectiles.Clear();
                        for (int i = 0; i < _nbOfProjectiles; i++)
                        {
                            projectiles.Add(new Projectile("projectile " + i, Surface.DrawRect(ref _spriteBatch, 32, 32, Color.White), rand.Next(_minSpeed, _maxSpeed)));
                        }
                    }
                    else
                    {
                        foreach (Rocket rocket in new List<Rocket>(rockets))
                        {
                            if (rockets.Contains(rocket))
                            {
                                if (rocket.Collision(projectile))
                                {
                                    Projectile.score += 5;
                                    rockets.Remove(rocket);
                                    projectiles.Remove(projectile);

                                    for (int i = 0; i < 2; i++)
                                    {
                                        projectiles.Add(new Projectile("projectile " + i, Surface.DrawRect(ref _spriteBatch, 32, 32, Color.White), rand.Next(_minSpeed, _maxSpeed)));
                                    }
                                }
                            }
                        }
                    }
                }
            }
            _localPlayer1.Update(gameTime, _spriteBatch);

            foreach (Projectile projectile in new List<Projectile>(projectiles))
            {
                if (projectiles.Contains(projectile))
                {
                    projectile.Update(gameTime, _spriteBatch);
                }
            }


            foreach (Rocket rocket in new List<Rocket>(rockets))
            {
                if (rockets.Contains(rocket))
                {
                    rocket.Update(gameTime, _spriteBatch);
                }
            }
        }
        else if(_idMenu == 2) {
           scoreboard.Update(gameTime, _spriteBatch);

           foreach (var component in _scoreboardMenuComponent) component.Update(gameTime, _spriteBatch) ;
        } 
        else
        {
            foreach (var component in _mainMenuComponent) component.Update(gameTime, _spriteBatch);
        }



        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {

        switch (_idMenu) {
            case 0:
           // MediaPlayer.Stop(music);
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
            
            foreach (var component in _mainMenuComponent) component.Draw(gameTime, _spriteBatch);

            _spriteBatch.End();

            break;

            case 1:
            

            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();

            // Draw HUD 
            _scoreOfGame.DrawScore("Score : "+ Projectile.score,gameTime, _spriteBatch);
            _numberOfDeaths.DrawScore("Number of deaths  : " + scoreboard.getNumberOfDie(),gameTime, _spriteBatch);
            _highScore.DrawScore("High Score : "+ scoreboard.getHighScore(),gameTime, _spriteBatch);
        
            _spriteBatch.Draw(_localPlayer1.Texture, _localPlayer1.position, Color.White);



            foreach (Projectile projectile in new List<Projectile>(projectiles))
            {
                if (projectiles.Contains(projectile))
                {
                    _spriteBatch.Draw(projectile.Texture, projectile.position, Color.White);
                }
            }


            foreach (Rocket rocket in new List<Rocket>(rockets))
            {
                if (rockets.Contains(rocket))
                {
                    _spriteBatch.Draw(rocket.Texture, rocket.position, Color.Green);
                }

            }


            /* 
            For show debug :
            Entity.DebugEntity(font, sprite Batch, Vector2, Color)            
            */

            foreach (Rocket rocket in rockets) rocket.DebugEntity(debugFont, _spriteBatch, new Vector2(250, 0), Color.Green);

            _spriteBatch.End();
            break;
            case 2 :
            _spriteBatch.Begin();
            
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Draw(Surface.DrawRect( ref _spriteBatch, screenWidth-100, screenHeight-100, Color.White), new Vector2(10,50), Color.White) ;
            
            foreach (var component in _scoreboardMenuComponent) component.Draw(gameTime, _spriteBatch);


            scoreboard.Draw(gameTime, _spriteBatch);


        
            _spriteBatch.End();

            break ; 
            default:
            GraphicsDevice.Clear(Color.Red);
            break; 

        }





        base.Draw(gameTime);
    }



}
