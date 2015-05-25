using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace EcoShoot
{
    public abstract class Button
    {
        //Atributos
        private String texturePath;
        public Vector2 position;
        public Texture2D texture;

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
            if (IsMouseIn() && Mouse.GetState().LeftButton == ButtonState.Pressed)
                OnClick();
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        public virtual Boolean IsMouseIn()
        {
            if (Mouse.GetState().X >= position.X && Mouse.GetState().Y >= position.Y &&
       Mouse.GetState().X <= position.X + texture.Width && Mouse.GetState().Y <= position.Y + texture.Height)
                return true;

            else
                return false;
        }

        public abstract void OnClick();
    }
}
