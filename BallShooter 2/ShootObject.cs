using GameFramework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BallShooter_2
{
    class ShootObject : GameFramework.SpriteObject
    {
        private Game1 _game;

        private Vector2 _velocity;
        private int _updates;


        internal ShootObject(Game1 game, Texture2D texture)
            : base(game, Vector2.Zero, texture)
        {
            // Store a strongly-typed reference to the game
            _game = game;

            // Set the origin to the top-center of the sprite
            Origin = new Vector2(texture.Width / 2, 0);

            Scale = new Vector2(0.5f, 0.5f);

            // set color
            //SpriteColor = new Color(204, 255, 153);
            //SpriteColor = new Color(GameHelper.RandomNext(0, 256), GameHelper.RandomNext(0, 256), GameHelper.RandomNext(0, 256));

        }

        //reset the properties of the bullet ready to move from the specified position in the specified direction
        internal void InitializeBullet(Vector2 Position, float Angle)
        {
            //initalize the bullet properties
            this.Position = Position;
            this.Angle = Angle;

            //calculate the velocity vector for the bullet
            _velocity = new Vector2((float)Math.Sin(Angle), -(float)Math.Cos(Angle));
            

            //Mark the bullet as active
            IsActive = true;

            //reset it's update count
            _updates = 0;
        }

        //object properties
        
        public bool IsActive { get; set; }

        //game functions

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            //only update if we are active
            if (!IsActive) return;

            //add the velocity to the position
            Position += _velocity * 13;

            //have we hit a rock?
            CheckForCollision();

            //see if we have updated enough times to run out of energy and disappear
            _updates += 1;
            if (_updates > 40) IsActive = false;

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //only draw it we are active
            if (!IsActive) return;

            //Draw the sprite
 	        base.Draw(gameTime, spriteBatch);
        }

        //bullets function
        private void CheckForCollision()
        {
            int objectCount;
            GameObjectBase gameObj;
            Enemy enemObj;
            float enemSize;
            float enemDistance;

            //Loop backwards through the enemy as we may modify the collection when an enemy is destroyed
            objectCount = _game.GameObjects.Count;
            for (int i = objectCount - 1; i >= 0; i--)
            {
                //get a reference to the object at this position
                gameObj = _game.GameObjects[i];
                //is this enemy?
                if (gameObj is Enemy)
                {
                    //does the bounding rectangle contain the bullet position?
                    enemObj = (Enemy)gameObj;
                    if (enemObj.BoundingBox.Contains((int)PositionX, (int)PositionY))
                    {
                        enemSize = enemObj.SpriteTexture.Width / 2.0f * enemObj.ScaleX;
                        enemDistance = Vector2.Distance(Position, enemObj.Position);
                        if (enemDistance < enemSize)
                        {
                            enemObj.DamageEnemy();
                            IsActive = false;
                        }
                    }
                }
            }
        }
    }
}
   
