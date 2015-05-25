using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace EcoShoot.Buttons
{
    class ButtonJugar : Button
    {
        private String texturePath;

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
            if (IsMouseIn() && Mouse.GetState().LeftButton == ButtonState.Pressed)
                OnClick();
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
            Managers.ScreenManager.Instance.ChangeScreen("InGameScreen");
        }
    }
}
