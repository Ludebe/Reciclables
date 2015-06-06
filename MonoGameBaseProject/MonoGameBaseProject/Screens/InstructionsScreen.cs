using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using EcoShoot.Managers;

namespace EcoShoot.Screens
{
    public class InstructionsScreen : Screen
    {
        //Atributos
        Texture2D backgroundImage;

        public InstructionsScreen()
        {
            
        }

        public override void LoadContent(ContentManager Content)
        {
            backgroundImage = Content.Load<Texture2D>("InstructionsScreen//Background");
        }

        public override void Update(GameTime gameTime)
        {
            if (InputManager.Instance.LeftMouseButtonPressed() || InputManager.Instance.RightMouseButtonPressed())
                ScreenManager.Instance.ChangeScreen("InGameScreen");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backgroundImage, Vector2.Zero, Color.White);
        }
    }
}
