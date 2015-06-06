using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

using EcoShoot.Buttons;

namespace EcoShoot.Screens
{
    public class MenuScreen : Screen
    {

        //Imágenes del menú
        String imagesPath;
        Image logoImage;
        Image backgroundImage;
        //Lista de drawings
        List<Image> drawings;

        //Botones del menú
        ButtonJugar buttonJugar;
        ButtonOpciones buttonOpciones;
        ButtonSalir buttonSalir;
        List<Button> buttons;

        //Sonidos
        SoundEffect mouseOnSound;

        public MenuScreen()
        {
            base.Type = this.GetType();
            drawings = new List<Image>();
            buttons = new List<Button>();
            imagesPath = "MenuScreen/";
            //Inicializo las imágenes
            logoImage = new Image();
            backgroundImage = new Image();

        }

        public override void LoadContent(ContentManager Content)
        {
            #region imágenes
            //Cargo los paths de las imágenes
            logoImage.path = this.imagesPath + "Logo";
            backgroundImage.path = this.imagesPath + "FondoV2";
            //Carga las imágenes
            logoImage.Loadcontent();
            backgroundImage.Loadcontent();
            //Finalmente carga lo que haya en el drawings
            foreach (Image image in drawings)
                image.Loadcontent();

            //Asigno la posición de las imágenes
            logoImage.position = new Vector2(Managers.ScreenManager.Instance.dimensions.X / 2 - logoImage.texture.Width / 2, 10);
            backgroundImage.position = new Vector2(0, 0);
            #endregion

            #region Botones
            int espacioEntreBotones = 15;
            buttonJugar = new ButtonJugar(imagesPath + "Jugar", new Vector2(Managers.ScreenManager.Instance.dimensions.X / 3, Managers.ScreenManager.Instance.dimensions.Y / 2));
            buttonJugar.LoadContent(Content);
            buttonOpciones = new ButtonOpciones(imagesPath + "Opciones", new Vector2(Managers.ScreenManager.Instance.dimensions.X / 3, buttonJugar.position.Y + buttonJugar.texture.Height + espacioEntreBotones));
            buttonOpciones.LoadContent(Content);
            buttonSalir = new ButtonSalir(imagesPath + "Salir", new Vector2(Managers.ScreenManager.Instance.dimensions.X / 3, buttonOpciones.position.Y + buttonOpciones.texture.Height + espacioEntreBotones));
            buttonSalir.LoadContent(Content);
            buttons.Add(buttonJugar);
            buttons.Add(buttonOpciones);
            buttons.Add(buttonSalir);
            #endregion

            #region Sonidos
            mouseOnSound = Content.Load<SoundEffect>("Sounds//MouseInSound");
            #endregion

        }

        public override void Update(GameTime gameTime)
        {
            MouseControl(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            backgroundImage.Draw(spriteBatch);
            logoImage.Draw(spriteBatch);
            //Dibujo imágenes
            foreach (Image image in drawings)
                image.Draw(spriteBatch);
            //Dibujo botones
            foreach (Button button in buttons)
                button.Draw(spriteBatch);
        }

        private void MouseControl(GameTime gameTime)
        {
            ////Hace sonido al pasar el mouse por encima
            foreach (Button button in buttons)
            {
                button.Update(gameTime);
                if(button.MouseEntered())
                    EcoShoot.Managers.AudioManager.Instance.PlaySound(mouseOnSound);

                if (button.IsMouseIn() && Mouse.GetState().LeftButton == ButtonState.Pressed)
                    button.OnClick();
            }
        }
    }
}
