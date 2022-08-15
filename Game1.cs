
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using learnmonogame.interfaces;
using learnmonogame.entities;
using learnmonogame.drawing;

using System;
using System.Collections.Generic;

namespace learnmonogame;

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
    private Label _titleOfGame;

    private Label _scoreOfGame;
    private List<Component> _gameComponents;
    #endregion

    #region trucutile 
    public static int screenWidth, screenHeight;
    public bool _OnMenu = true;
    public static Random rand = new Random();
    public static int score = 0;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private static SpriteFont _debugFont;
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
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _debugFont = Content.Load<SpriteFont>("debug");


        _titleOfGame = new Label("Kuiper Belt Game", _debugFont, Color.White, new Vector2(10.0f, 10.0f)) ;
        _scoreOfGame = new Label("Score : " + Projectile.score, _debugFont, Color.White, new Vector2(0f, 0f)) ;

        screenWidth = GraphicsDevice.Viewport.Width;
        screenHeight = GraphicsDevice.Viewport.Height;

        #region definebutton
        var newGameButton = new Button(Surface.DrawRect(ref _spriteBatch, 128, 32, Color.Blue), Content.Load<SpriteFont>("debug"))
        {
            Position = new Vector2(0, 100),
            Text = "New Game",
        };

        newGameButton.Click += newGameButton_Click;

        var quitButton = new Button(Surface.DrawRect(ref _spriteBatch, 128, 32, Color.Blue), Content.Load<SpriteFont>("debug"))
        {
            Position = new Vector2(0, 150),
            Text = "Quit",
        };

        quitButton.Click += QuitButton_Click;

        _gameComponents = new List<Component>()
      {
        newGameButton,
        quitButton,
      };
        #endregion



        #region loadentities
        _localPlayer1 = new Player("player 1", Surface.DrawRect(ref _spriteBatch, 32, 32, Color.Blue), new Vector2(screenWidth / 2, 0), 500);

        for (int i = 0; i < _nbOfProjectiles; i++)
        {
            projectiles.Add(new Projectile("projectile " + i, Surface.DrawRect(ref _spriteBatch, 32, 32, Color.Red), rand.Next(_minSpeed, _maxSpeed)));
        }
        #endregion


    }


    private void QuitButton_Click(object sender, System.EventArgs e)
    {
        Exit();
    }

    private void newGameButton_Click(object sender, System.EventArgs e)
    {
        _OnMenu = false;
        Projectile.score = 0;
    }

    protected override void Update(GameTime gameTime)
    {
        Console.WriteLine("le score est "+Projectile.score) ; 


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
                    _OnMenu = true;
                    projectiles.Clear();
                    for (int i = 0; i < _nbOfProjectiles; i++)
                    {
                        projectiles.Add(new Projectile("projectile " + i, Surface.DrawRect(ref _spriteBatch, 32, 32, Color.Red), rand.Next(_minSpeed, _maxSpeed)));
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
                                Projectile.score += 5 ;
                                rockets.Remove(rocket);
                                projectiles.Remove(projectile);

                                for (int i = 0; i < 2; i++)
                                {
                                    projectiles.Add(new Projectile("projectile "+i, Surface.DrawRect(ref _spriteBatch, 32, 32, Color.Red), rand.Next(_minSpeed, _maxSpeed)));
                                }
                            }
                        }
                    }
                }
            }

        }



        if (!_OnMenu)
        {
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
        else
        {
            foreach (var component in _gameComponents)
                component.Update(gameTime, _spriteBatch);
        }


        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        if (_OnMenu)
        {
            GraphicsDevice.Clear(Color.Red);
            _spriteBatch.Begin();
            //_spriteBatch.DrawString(_debugFont, "Kuiper Belt Game A0.1", new Vector2(screenWidth / 2 - 100, 0), Color.White);

            _titleOfGame.Draw(gameTime, _spriteBatch);
            
            foreach (var component in _gameComponents)
                component.Draw(gameTime, _spriteBatch);

            _spriteBatch.End();
        }
        else
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
            //_scoreOfGame.Draw(gameTime, _spriteBatch);
            _spriteBatch.DrawString(_debugFont,"Score : "+Projectile.score , new Vector2(0f, 0f), Color.White) ; 
        
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




            //_localPlayer1.DebugEntity(_debugFont, _spriteBatch, new Vector2(0, 0), Color.Blue);

            foreach (Rocket rocket in rockets) rocket.DebugEntity(_debugFont, _spriteBatch, new Vector2(250, 0), Color.Green);

            //  foreach(Projectile projectile in projectiles) projectile.DebugEntity(_debugFont, _spriteBatch, new Vector2(0,0), Color.Red) ;

            /*
                    _localPlayer1.DebugEntity(_debugFont, _spriteBatch, new Vector2(0,0), Color.Blue) ;
                    projectile1.DebugEntity(_debugFont, _spriteBatch, new Vector2(screenWidth/2,0), Color.Red) ;
                    projectile2.DebugEntity(_debugFont, _spriteBatch, new Vector2(screenWidth/3,0), Color.Green) ;*/

            _spriteBatch.End();
        }

        base.Draw(gameTime);
    }



}
