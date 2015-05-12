using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

using MonoGameBaseProject.Buttons;

namespace MonoGameBaseProject.Screens
{
    public class MenuScreen : Screen
    {

        //Imágenes del menú
        private String imagesPath;
        private Image logoImage;
        private Image backgroundImage;
        //Lista de drawings
        private List<Image> drawings;

        //Botones del menú
        private ButtonJugar buttonJugar;
        private ButtonSalir buttonSalir;
        private List<Button> buttons;

        //Sonidos
        private String soundsPath;
        private SoundEffect selectionEffect;
        private bool playOnce = false;
        public MenuScreen()
        {
            base.Type = this.GetType();
            drawings = new List<Image>();
            buttons = new List<Button>();
            imagesPath = "MenuScreen/";
            soundsPath = "Sounds/";
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
            buttonJugar = new ButtonJugar(imagesPath + "Jugar", new Vector2(Managers.ScreenManager.Instance.dimensions.X / 3 + 15, Managers.ScreenManager.Instance.dimensions.Y / 2));
            buttonJugar.LoadContent(Content);
            buttonSalir = new ButtonSalir(imagesPath + "Salir", new Vector2(Managers.ScreenManager.Instance.dimensions.X / 3 + 15, buttonJugar.position.Y + buttonJugar.texture.Height + espacioEntreBotones));
            buttonSalir.LoadContent(Content);
            buttons.Add(buttonJugar);
            buttons.Add(buttonSalir);
            #endregion

            #region Sonidos
            selectionEffect = Content.Load<SoundEffect>(soundsPath + "sound1");
            #endregion

        }

        public override void Update(GameTime gameTime)
        {

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
            //Sonido
            MouseControl();

        }

        private void MouseControl()
        {
            //Hace sonido al pasar el mouse por encima
            foreach (Button button in buttons)
            {
                if (button.IsMouseIn())
                    selectionEffect.Play();
            }
            
        }

    }
}
