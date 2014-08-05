using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameFramework;



namespace BallShooter_2
{
    internal class Texte_Timer : TextObject
    {

        private Game1 _game;
    
        //-------------------------------------------------------------------------------------
        // Class variables
        int _lastUpdateMilliseconds;
        public int newElapsedTime;


        int limit;
       
        // Create a single StringBuilder instance to avoid creating objects each update
        private StringBuilder _strBuilder = new StringBuilder();

        //-------------------------------------------------------------------------------------
        // Class constructors

        public Texte_Timer(Game1 game, SpriteFont font, Vector2 position, Color textColor)
            : base(game, font, position)
        {
            _game = game;

            SpriteColor = textColor;


        }


        public override void Update(GameTime gameTime)
        {
            // Allow the base class to do its stuff
            base.Update(gameTime);


                // Find out exactly how much time has passed
                newElapsedTime = (int)gameTime.TotalGameTime.Seconds - _lastUpdateMilliseconds;


                if (newElapsedTime > 20) {
                    // Build a message to display the details and set it into the Text property
                    _strBuilder.Length = 0;

                    //currentTime = (int)gameTime.TotalGameTime.Seconds;
                    limit = 20;

                    _strBuilder.AppendLine("All is dust");
                    _strBuilder.AppendLine(limit.ToString());
                    _strBuilder.AppendLine(_lastUpdateMilliseconds.ToString());
                    Text = _strBuilder.ToString();

                    _strBuilder.AppendLine("Game over");
                    _lastUpdateMilliseconds = (int)gameTime.TotalGameTime.Seconds; 
                    _game.ResetGame();
                }


            }


        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Update the number of times draw has been called
            


            // Draw the text
            base.Draw(gameTime, spriteBatch);
        }
    }
}
