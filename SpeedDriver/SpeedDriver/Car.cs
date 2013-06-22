using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace SpeedDriver
{
    class Car
    {
        Vector2 position;
        Rectangle ScreenBounds;
        KeyboardState keyBoardState;

        Texture2D carTexture;
        Vector2 textureRotatePoint;
        Rectangle sourceRectangle;

        private float carSpeed;
        private float carSpeedY;
        private float carSpeedX;
        private float carAccel;
        private float carDecel;
        private float carAngle;
        private float carSpeedMax;

        public Car(Texture2D carGraphic, Rectangle Screen, float s)
        {
            position = new Vector2(0, 0);
            carTexture = carGraphic;
            textureRotatePoint = new Vector2(carTexture.Width / 2, carTexture.Height / 2);
            sourceRectangle = new Rectangle(0, 0, carTexture.Width, carTexture.Height);
            ScreenBounds = Screen;
            carSpeed = 0f;
            carSpeedY = 0f;
            carSpeedX = 0f;
            carAccel = 0.2f;
            carDecel = 0.05f;
            carAngle = 0;
            carSpeedMax = s;
        }

        public void StartPosition(int x, int y)
        {
            position.X = x;
            position.Y = y;
        }

        public Rectangle GetBounds()
        {
            return new Rectangle(
                (int)position.X,
                (int)position.Y,
                carTexture.Width,
                carTexture.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(carTexture, position, sourceRectangle, Color.White, (float)Math.PI / 2 - carAngle, textureRotatePoint, 1.0f, SpriteEffects.None, 1);
        }

        public void Update()
        {
            keyBoardState = Keyboard.GetState();

            if (keyBoardState.IsKeyDown(Keys.Up))
            {
                UpdateSpeed(0);
            }
            if (keyBoardState.IsKeyDown(Keys.Down))
            {
                UpdateSpeed(1);
            }
            else
                UpdateDeceleration();
            if (keyBoardState.IsKeyDown(Keys.Right))
            {
                if (carSpeed != 0)
                    UpdateTurn(0);
            }
            if (keyBoardState.IsKeyDown(Keys.Left))
            {
                if (carSpeed != 0)
                    UpdateTurn(1);
            }

            UpdateVelocity();
            UpdatePosition();
            LockCar();
        }

        private void UpdateVelocity()
        {
            if (carAngle >= 0 && carAngle < Math.PI / 2)
            {
                carSpeedX = carSpeed * (float)(Math.Cos((double)carAngle));
                carSpeedY = carSpeed * (float)(Math.Sin((double)carAngle));
                carSpeedY = -carSpeedY;
            }
            if (carAngle >= Math.PI / 2 && carAngle < Math.PI)
            {
                carSpeedX = carSpeed * (float)(Math.Cos((double)carAngle));
                carSpeedY = carSpeed * (float)(Math.Sin((double)carAngle));
                carSpeedY = -carSpeedY;
            }
            if (carAngle >= Math.PI && carAngle < (3 / 2) * Math.PI)
            {
                carSpeedX = carSpeed * (float)(Math.Cos((double)carAngle));
                carSpeedY = carSpeed * (float)(Math.Sin((double)carAngle));
                carSpeedY = -carSpeedY;
            }
            if (carAngle >= (3 / 2) * Math.PI && carAngle < 2 * Math.PI)
            {
                carSpeedX = carSpeed * (float)(Math.Cos((double)carAngle));
                carSpeedY = carSpeed * (float)(Math.Sin((double)carAngle));
                carSpeedY = -carSpeedY;
            }
        }

        private void UpdateSpeed(int num)
        {
            if (num == 0)
            {
                if (carSpeed < carSpeedMax)
                    carSpeed += carAccel;
                else
                    carSpeed = carSpeedMax;
            }
            if (num == 1)
            {
                if (carSpeed > 0.1)
                    carSpeed -= carAccel;
                else
                    carSpeed = 0;
            }
            if (carSpeed > carSpeedMax)
                carSpeed = carSpeedMax;
        }

        private void UpdateDeceleration()
        {
            if (carSpeed > carDecel)
                carSpeed -= carDecel;
            else
                carSpeed = 0;
        }

        private void UpdatePosition()
        {
            position.Y += carSpeedY;
            position.X += carSpeedX;
        }

        private void LockCar()
        {
            if (position.Y - carTexture.Height / 2 < 0)
            {
                position.Y = carTexture.Height / 2;
                carSpeed = 0;
            }
            if (position.Y + carTexture.Height / 2 > ScreenBounds.Height)
            {
                position.Y = ScreenBounds.Height - carTexture.Height / 2;
                carSpeed = 0;
            }
            if (position.X - carTexture.Height / 2 < 0)
            {
                position.X = carTexture.Height / 2;
                carSpeed = 0;
            }
            if (position.X + carTexture.Height / 2 > ScreenBounds.Width)
            {
                position.X = ScreenBounds.Width - carTexture.Height / 2;
                carSpeed = 0;
            }
        }

        private void UpdateTurn(int num)
        {
            if (num == 0)
            {
                if (carAngle > 0)
                    carAngle -= (float)Math.PI / 48;
                else
                    carAngle = 2 * (float)Math.PI;
            }
            if (num == 1)
            {
                if (carAngle < 2 * Math.PI)
                    carAngle += (float)Math.PI / 48;
                else
                    carAngle = 0;
            }
        }
    }
}
