using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameFramework;
using Microsoft.Xna.Framework.Input;
using System;
using Microsoft.Xna.Framework.Audio;


namespace BallShooter_2
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : GameFramework.GameHost
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D bgTexture;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //load the object textures into the texture dictionary
            Textures.Add("cat2", this.Content.Load<Texture2D>("cat2"));
            bgTexture = Content.Load<Texture2D>("pink_blue");
            Textures.Add("crystal2", this.Content.Load<Texture2D>("crystal2"));
            Textures.Add("ball1", this.Content.Load<Texture2D>("ball1"));

            SoundEffects.Add("Speech Off", Content.Load<SoundEffect>("Speech Off"));
            
            Fonts.Add("Miramonte", this.Content.Load<SpriteFont>("Miramonte"));
            // TODO: use this.Content to load your game content here
            ResetGame();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) this.Exit();

            // TODO: Add your update logic here
            UpdateAll(gameTime);
            

            if (GameObjects.Count == 6)
            {                
                ResetGame();
            }
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Pink);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(bgTexture, GraphicsDevice.Viewport.Bounds, Color.White);

            DrawSprites(gameTime, spriteBatch, Textures["cat2"]); 
            DrawSprites(gameTime, spriteBatch, Textures["crystal2"]);
            DrawSprites(gameTime, spriteBatch, Textures["ball1"]);

            DrawText(gameTime, spriteBatch);

            
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void ResetGame()
        {

            // Remove any existing objects
            GameObjects.Clear();

          //  GameObjects.Add(new BenchmarkObject(this, Fonts["Miramonte"], new Vector2(0, 40), Color.Black));
            
            GameObjects.Add(new Texte_Timer(this, Fonts["Miramonte"], new Vector2(0, 40), Color.Black));            

            // Add 10 boxes and 10 balls
            GameObjects.Add(new Player(this, Textures["cat2"]));

            for (int i = 0; i < 20; i++)
            {

                GameObjects.Add(new Enemy(this, Textures["crystal2"]));
            }

                   
           
        }
    }
}
