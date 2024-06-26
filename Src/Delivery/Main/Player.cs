﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace Delivery
{
    public class Player : Entity
    {       

        public Vector2 velocity;
        public Rectangle playerFallRect;
        public SpriteEffects effects;

        public float playerSpeed = 0.5f;
        public float fallSpeed = 0.7f;
        public float jumpSpeed = -3;
        public float startY;

        public bool isFalling = true;
        public bool isJumping;
        public bool isMovingLeft;
        public bool jumpedFirst;

        public Animation[] playerAnimation;
        public currentAnimation playerAnimationController;
        public Player(Vector2 position, Texture2D idleSprite, 
            Texture2D runSprite,Texture2D jumpSprite)
        {
            playerAnimation = new Animation[3];

            this.position = position;
            velocity = position;
            effects = SpriteEffects.None;

            playerAnimation[0] = new Animation(idleSprite,8,8);
            playerAnimation[1] = new Animation(runSprite,8,8);
            playerAnimation[2] = new Animation(jumpSprite,8,8);
            hitbox = new Rectangle((int)position.X, (int)position.Y,8, 5);
            playerFallRect = new Rectangle((int)position.X, (int)position.Y, 6, 6);
            isMovingLeft = true;
        }


        public override void Update()
        {
            KeyboardState keyboard = Keyboard.GetState();
            TouchCollection touchpanel = TouchPanel.GetState();


            playerAnimationController = currentAnimation.Idle;
            position = velocity;

            startY = position.Y;

            if (true)//(!Game1.isBlocked)
            {
                Move(keyboard, touchpanel);
                Jump(keyboard, touchpanel);
            }
            
            if (isFalling)
            {
                velocity.Y += fallSpeed;
                playerAnimationController = currentAnimation.Jumping;
            }
            
            hitbox.X = (int)position.X;
            hitbox.Y = (int)position.Y;
            playerFallRect.X = (int)position.X;
            playerFallRect.Y = (int)velocity.Y+2;
        }

        private void Move(KeyboardState keyboard, TouchCollection touchpanel)
        {
            if (jumpedFirst)
            {
                if (isMovingLeft)
                {
                    velocity.X -= playerSpeed;
                    playerAnimationController = currentAnimation.Run;
                    effects = SpriteEffects.None;
                }
                else
                {
                    velocity.X += playerSpeed;
                    playerAnimationController = currentAnimation.Run;
                    effects = SpriteEffects.FlipHorizontally;
                }
            }
           
            // ***************************************************************
            //Kbd cheats :) 
            if (keyboard.IsKeyDown(Keys.A))
            {
                velocity.X -= playerSpeed;
                playerAnimationController = currentAnimation.Run;
                effects = SpriteEffects.None;
            }
            if (keyboard.IsKeyDown(Keys.D))
            {
                velocity.X += playerSpeed;
                playerAnimationController = currentAnimation.Run;
                effects = SpriteEffects.FlipHorizontally;
            }
            // ***************************************************************
        }

        private void Jump(KeyboardState keyboard, TouchCollection touchpanel)
        {
            if (isJumping)
            {
                jumpedFirst = true;
                velocity.Y += jumpSpeed;//Making it go up
                jumpSpeed += 1;//Some math (explained later)

                Move(keyboard, touchpanel);
                
                playerAnimationController = currentAnimation.Jumping;

                if (velocity.Y >= startY)
                //If it's farther than ground
                {
                    velocity.Y = startY;//Then set it on
                    isJumping = false;

                }
            }
            else
            {
                // if space keypressed or touchscreen (panel) "get your sense"... ;)
                if ( ( keyboard.IsKeyDown(Keys.Space) || touchpanel.Count > 0) && !isFalling)
                {
                    //...then jump :)

                    isJumping = true;
                    isFalling = false;
                    jumpSpeed = -6;//Give it upward thrust
                }
            }

        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            
            switch (playerAnimationController)
            {
                case currentAnimation.Idle:
                    playerAnimation[0].Draw(spriteBatch, position, gameTime, 500, effects);
                    break;
                case currentAnimation.Run:
                    playerAnimation[1].Draw(spriteBatch, position, gameTime, 100, effects);
                    break;
                case currentAnimation.Jumping:
                    playerAnimation[2].Draw(spriteBatch, position, gameTime, 100, effects);
            
                    break;
                
            }

        }
    }
}
