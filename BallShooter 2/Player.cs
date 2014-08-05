using GameFramework;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Text;
using Microsoft.Xna.Framework.Input.Touch;

namespace BallShooter_2
{
    internal class Player : SpriteObject
    {
        private Game1 _game;

        TouchCollection touchCollection;

        private const int MaxBullets = 4;


        internal Player(Game1 game, Texture2D texture)
            : base(game, Vector2.Zero, texture)
        {
            //strong reference thingy
            _game = game;

            //set Position

            PositionX = 1000;
            PositionY = 600;

            //set the origin

            Origin = new Vector2(texture.Width, texture.Height) / 2;

            // set color
            SpriteColor = Color.White;

            //set movement

            // set rotation

            //set scale
            Scale = new Vector2(1.3f, 1.3f);
        }

        public override void Update(GameTime gameTime)
        {
            //allow the base class to do any work it needs
            base.Update(gameTime);

            Update_ProcessTouchInput();

        }

        private void Update_ProcessTouchInput()
        {

            //Update Position -> Touchthingy
            touchCollection = TouchPanel.GetState();
            foreach (TouchLocation tl in touchCollection)
            {

                if ((tl.State == TouchLocationState.Pressed)
                        || (tl.State == TouchLocationState.Moved))
                {

                    PositionY = 600;
                    PositionX = tl.Position.X;

                    FireBullet();
                }
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }

        private void FireBullet()
        {

            ShootObject enemObj;

            //try to obtain a bullet object to shoot
            enemObj = GetShootObject();
            //did we find one?

            if (enemObj == null)
            {
                //no, so we can't shoot at the moment
                return;
            }

            //initalizie the bullet with our own position and angle
            enemObj.InitializeBullet(Position, Angle);
        }

        //find (or create) a bullet object to fire
        private ShootObject GetShootObject()
        {
            int objectCount;
            int shootCount = 0;
            GameObjectBase gameObj;
            ShootObject shootObj = null;

            //Look for an inactiv bullet
            objectCount = _game.GameObjects.Count;
            for (int i = 0; i < objectCount; i++)
            {
                gameObj = _game.GameObjects[i];
                //is this a bullet
                if (gameObj is ShootObject)
                {
                    shootCount += 1;

                    //PAAAUUUSE 

                    //is it inactiv
                        if (((ShootObject)gameObj).IsActive == false)
                        {
                            return (ShootObject)gameObj;
                        }
                    }
                }

                //did we find a bullet
                if (shootCount < MaxBullets)
                {
                    shootObj = new ShootObject(_game, _game.Textures["ball1"]);
                    //foreach soviele Bullets dann schiessen
                    
                   _game.GameObjects.Add(shootObj);
                   return shootObj;

                }
                return null;
            }
        }
    }



