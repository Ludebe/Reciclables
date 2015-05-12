using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameBaseProject.Buttons
{
    class ButtonSalir : Button
    {
        private String texturePath;

        public ButtonSalir(String texturePath)
            : base(texturePath)
        {
            this.texturePath = texturePath;
            this.position = Vector2.Zero;
        }

        public ButtonSalir(String texturePath, Vector2 position)
            : base(texturePath, position)
        {
            this.texturePath = texturePath;
            this.position = position;
        }


        // |-------------Mis métodos------------|

        public override void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>(texturePath);
        }

        public override void Update(GameTime gameTime)
        {
            //Evento click
            if (IsMouseIn() && Mouse.GetState().LeftButton == ButtonState.Pressed)
                OnClick();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        public override Boolean IsMouseIn()
        {
            if (Mouse.GetState().X >= position.X && Mouse.GetState().Y >= position.Y &&
        Mouse.GetState().X <= position.X + texture.Width && Mouse.GetState().Y <= position.Y + texture.Height)
                return true;

            else
                return false;
        }

        public override void OnClick()
        {
            Game1.Instance.Exit();
        }
    }
}
