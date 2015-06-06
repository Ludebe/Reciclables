using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using EcoShoot.Managers;

namespace EcoShoot
{
    public abstract class Button
    {
        //Atributos
        private String texturePath;
        public Vector2 position;
        public Texture2D texture;
        MouseState oldMouseState;
        MouseState newMouseState;

        public Button(String texturePath)
        {
            this.texturePath = texturePath;
            this.position = Vector2.Zero;
        }

        public Button(String texturePath, Vector2 position)
        {
            this.texturePath = texturePath;
            this.position = position;
        }


        // |-------------Mis métodos------------|

        public virtual void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>(texturePath);
        }

        public virtual void Update(GameTime gameTime)
        {
            //Evento click
            if (IsMouseIn() && InputManager.Instance.LeftMouseButtonPressed())
                OnClick();
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        public virtual Boolean IsMouseIn()
        {
            if (newMouseState.X >= position.X && newMouseState.Y >= position.Y &&
       newMouseState.X <= position.X + texture.Width && newMouseState.Y <= position.Y + texture.Height)
                return true;

            else
                return false;
        }

        public virtual Boolean MouseEntered()
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

        public abstract void OnClick();
    }
}
