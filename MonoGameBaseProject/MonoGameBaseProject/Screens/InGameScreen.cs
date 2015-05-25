using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using EcoShoot.Managers;

namespace EcoShoot.Screens
{
    public class InGameScreen : Screen
    {
        //Atributos
        SpriteFont myFont;
        Basura[] basuras;
        Basura basuraOnScreen;
        Int32 reciclablesAcertados, noReciclablesAcertados, reciclablesFallados, noReciclablesFallados, total, totalMax;
        Boolean fin, gana;

        public InGameScreen()
        {
            reciclablesAcertados = noReciclablesAcertados =
                reciclablesFallados = noReciclablesFallados = total = 0;
            totalMax = 30;
            fin = false;
            basuras = new Basura[8];
            String texturePath;

            //Primero se inicializa el objeto y después en LoadContent() se le setea la posición
            texturePath = "Basura//Banana";
            basuras[0] = new Basura(texturePath, false);

            texturePath = "Basura//Botella";
            basuras[1] = new Basura(texturePath, true);

            texturePath = "Basura//Botella_Vidrio";
            basuras[2] = new Basura(texturePath, true);

            texturePath = "Basura//Carne";
            basuras[3] = new Basura(texturePath, false);

            texturePath = "Basura//Carton";
            basuras[4] = new Basura(texturePath, true);

            texturePath = "Basura//Huevo";
            basuras[5] = new Basura(texturePath, false);

            texturePath = "Basura//Lata";
            basuras[6] = new Basura(texturePath, true);

            texturePath = "Basura//Manzana";
            basuras[7] = new Basura(texturePath, false);
        }

        public override void LoadContent(ContentManager Content)
        {
            myFont = Content.Load<SpriteFont>("Fonts//MyFont");

            Random r = new Random();
            Vector2 position = new Vector2();

            //Se carga el contenido de todas las basuras
            for (int i = 0; i < basuras.Length; i++)
            {
                basuras[i].LoadContent(Content);
                position.X = r.Next(0, Convert.ToInt32(ScreenManager.Instance.dimensions.X - basuras[i].Texture.Width - 1));
                position.Y = ScreenManager.Instance.dimensions.Y + basuras[i].Texture.Height + 1;

                basuras[i].Position = position;
            }

            //Se asigna una basura random
            basuraOnScreen = GetRandomBasura();
        }

        public override void Update(GameTime gameTime)
        {
            if (!fin)
            {
                basuraOnScreen.Update(gameTime);

                if (basuraOnScreen.CanDelete)
                {
                    //Si era reciclable
                    if (basuraOnScreen.Reciclable)
                    {
                        //y acertó
                        if (basuraOnScreen.Acertado)
                            reciclablesAcertados += 1;

                        //y falló
                        else
                            reciclablesFallados += 1;
                    }

                    //Si no era reciclable
                    else
                    {
                        //y acertó
                        if (basuraOnScreen.Acertado)
                            noReciclablesAcertados += 1;

                            //y falló
                        else
                            noReciclablesFallados += 1;
                    }

                    //Crea una nueva basura para la pantalla
                    basuraOnScreen = GetRandomBasura();
                    total += 1;
                    if (total == totalMax)
                        fin = true;
                }
            }

            if (fin)
                GanaOPierde();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(myFont, reciclablesAcertados + "/" + (reciclablesAcertados + reciclablesFallados).ToString(),
                new Vector2(20, 20), Color.DarkGreen);

            spriteBatch.DrawString(myFont, noReciclablesAcertados + "/" + (noReciclablesAcertados + noReciclablesFallados).ToString(),
                new Vector2(ScreenManager.Instance.dimensions.X - 60, 20), Color.DarkGray);

            spriteBatch.DrawString(myFont, total.ToString(), new Vector2(ScreenManager.Instance.dimensions.X/2, 20), Color.WhiteSmoke);

            if (fin)
            {
                if (gana)
                    spriteBatch.DrawString(myFont, "GANASTE!", new Vector2(ScreenManager.Instance.dimensions.X / 2 - 50, ScreenManager.Instance.dimensions.Y / 2), Color.DarkGreen);

                else
                    spriteBatch.DrawString(myFont, "PERDISTE...", new Vector2(ScreenManager.Instance.dimensions.X / 2 - 50, ScreenManager.Instance.dimensions.Y/2), Color.DarkRed);
            }

            basuraOnScreen.Draw(spriteBatch);
        }

        /* Devuelve una basura random desde el array basuras
         * */
        Basura GetRandomBasura()
        {
            if (basuras.Length > 0)
            {
                Random r = new Random();
                Vector2 speed = new Vector2();
                Vector2 position = new Vector2();
                Basura b;

                Int16 index = Convert.ToInt16(r.Next(basuras.Length));

                //Primero se inicializa el objeto y después en LoadContent() se le setea la posición
                speed.Y = speed.X = 1 + total / 3;
                speed.X *= -1;
                speed.Y *= -1;

                position.X = r.Next(0, Convert.ToInt32(ScreenManager.Instance.dimensions.X - basuras[index].Texture.Width - 1));
                position.Y = ScreenManager.Instance.dimensions.Y + basuras[index].Texture.Height + 1;

                b = new Basura(position, speed, basuras[index].Texture, basuras[index].Reciclable);
                return b;
            }

            else
                return null;
        }

        /* Determina si el jugador gana o pierde
         * */
        void GanaOPierde()
        {
            Int32 totalAcertado, totalFallado;
            totalAcertado = reciclablesAcertados + noReciclablesAcertados;
            totalFallado = reciclablesFallados + noReciclablesFallados;

            if(totalAcertado * 3 > totalFallado)
                gana = true;

            else
                gana = false;
        }
    }
}
