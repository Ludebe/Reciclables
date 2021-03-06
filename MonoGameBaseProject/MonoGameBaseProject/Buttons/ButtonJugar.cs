﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using EcoShoot.Managers;

namespace EcoShoot.Buttons
{
    class ButtonJugar : Button
    {
        String texturePath;
        MouseState oldMouseState;
        MouseState newMouseState;

        public ButtonJugar(String texturePath)
            : base(texturePath)
        {
            this.texturePath = texturePath;
            this.position = Vector2.Zero;
        }

        public ButtonJugar(String texturePath, Vector2 position)
            : base(texturePath, position)
        {
            this.texturePath = texturePath;
            this.position = position;
        }

        public override void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>(texturePath);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            oldMouseState = newMouseState;
            newMouseState = Mouse.GetState();

            //Evento click
            if (IsMouseIn() && InputManager.Instance.LeftMouseButtonPressed())
                OnClick();
        }

        public override Boolean IsMouseIn()
        {
            if (newMouseState.X >= position.X && newMouseState.Y >= position.Y &&
       newMouseState.X <= position.X + texture.Width && newMouseState.Y <= position.Y + texture.Height)
                return true;

            else
                return false;
        }

        public override Boolean MouseEntered()
        {
            //Si el mouse antes no estaba sobre el botón y ahora sí
            if (!(oldMouseState.X >= position.X && oldMouseState.Y >= position.Y &&
                    oldMouseState.X <= position.X + texture.Width && oldMouseState.Y <= position.Y + texture.Height)
                    &&
                    newMouseState.X >= position.X && newMouseState.Y >= position.Y &&
                    newMouseState.X <= position.X + texture.Width && newMouseState.Y <= position.Y + texture.Height)
                return true;

            else
                return false;
        }

        public override void OnClick()
        {
            Managers.ScreenManager.Instance.ChangeScreen("InstructionsScreen");
        }
    }
}
