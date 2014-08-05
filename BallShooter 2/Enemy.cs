using GameFramework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BallShooter_2
{
    internal class Enemy : SpriteObject
    {
        private Game1 _game;

        private float _rotateSpeed;
        private float _moveSpeed;
        private Vector2 _direction;
        private float _constructorSpeed;


    internal Enemy(Game1 game, Texture2D texture)
            : base(game, Vector2.Zero, texture)
        {
            //strong reference thingy
            _game = game;

            //set Position

           // PositionX = GameHelper.RandomNext(0, _game.GraphicsDevice.Viewport.Bounds.Width);
           // PositionY = GameHelper.RandomNext(0, _game.GraphicsDevice.Viewport.Bounds.Height);
            PositionX = GameHelper.RandomNext(0, _game.GraphicsDevice.Viewport.Bounds.Width - 60);
            PositionY = GameHelper.RandomNext(40, 450);

            //set the origin

            Origin = new Vector2(texture.Width, texture.Height) / 2;

            // set color
            SpriteColor = new Color(255, GameHelper.RandomNext(100, 201), 255);

           // set rotation
            _rotateSpeed = GameHelper.RandomNext(-5.0f, 5.0f);

            Scale = new Vector2(1.0f, 1.0f);
            _constructorSpeed = GameHelper.RandomNext(1.0f, 3.0f);
            _moveSpeed = GameHelper.RandomNext(_constructorSpeed) + _constructorSpeed;

            setDirection();    
        
             



        }

        public override void Update(GameTime gameTime)
        {
            //allow the base class to do any work it needs
            base.Update(gameTime);

            //Update Position
            Position += _direction * _moveSpeed;

            

            if (BoundingBox.Bottom < 40 && _direction.Y < 0)
            {
                PositionY = GameHelper.RandomNext(40, 450);
                setDirection();
            }

            if (BoundingBox.Top > 450 && _direction.Y > 0)
            {
                PositionY = GameHelper.RandomNext(40, 450);
                setDirection();
            }

            if (BoundingBox.Right < 20 && _direction.X < 0)
            {
                PositionX = GameHelper.RandomNext(0, _game.GraphicsDevice.Viewport.Bounds.Width - 60);
                setDirection();
            }

            if (BoundingBox.Left > _game.GraphicsDevice.Viewport.Bounds.Width - 60 && _direction.X > 0)
            {
                PositionX = GameHelper.RandomNext(0, _game.GraphicsDevice.Viewport.Bounds.Width - 60);
                setDirection();
            }
                       
            // Rotate the crystals
            Angle += MathHelper.ToRadians(_rotateSpeed);

            
            
            
            
       }

        private void InitializeRock(float size)
        {
            Scale = new Vector2(size, size);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }

        internal void DamageEnemy()
        {
            _game.SoundEffects["Speech Off"].Play();
           _game.GameObjects.Remove(this);
        }

        internal void setDirection()
        {
            do
            {
                _direction = new Vector2(GameHelper.RandomNext(-3.0f, 3.0f), GameHelper.RandomNext(-5.0f, 5.0f));
            }
            while (_direction == Vector2.Zero);
            // Normalize the movement vector so that it is exactly 1 unit in length             
            _direction.Normalize();
        }
    }
}
