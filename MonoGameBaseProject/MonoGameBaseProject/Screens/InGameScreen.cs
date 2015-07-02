using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using EcoShoot.Managers;

namespace EcoShoot.Screens
{
    public class InGameScreen : Screen
    {
        //Atributos
        SpriteFont myFont, nivelFont, resultadoFont;
        Texture2D backgroundImage;
        Basura[] basuras;
        Basura basuraOnScreen;
        Int32 reciclablesAcertados, noReciclablesAcertados, reciclablesFallados, noReciclablesFallados, total, totalMax;
        SoundEffect failSound;
        SoundEffect winSound;
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
            nivelFont = Content.Load<SpriteFont>("Fonts//NivelFont");
            resultadoFont = Content.Load<SpriteFont>("Fonts//ResultadoFont");

            backgroundImage = Content.Load<Texture2D>("InGameScreen//Background");

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

            //Se cargan los sonidos
            failSound = Content.Load<SoundEffect>("Sounds//FailSound");
            winSound = Content.Load<SoundEffect>("Sounds//WinSound");

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
                        {
                            reciclablesAcertados += 1;
                            AudioManager.Instance.PlaySound(winSound);
                        }

                        //y falló
                        else
                        {
                            reciclablesFallados += 1;
                            AudioManager.Instance.PlaySound(failSound);
                        }
                    }

                    //Si no era reciclable
                    else
                    {
                        //y acertó
                        if (basuraOnScreen.Acertado)
                        {
                            noReciclablesAcertados += 1;
                            AudioManager.Instance.PlaySound(winSound);
                        }

                            //y falló
                        else
                        {
                            noReciclablesFallados += 1;
                            AudioManager.Instance.PlaySound(failSound);
                        }
                    }

                    //Crea una nueva basura para la pantalla
                    basuraOnScreen = GetRandomBasura();
                    total += 1;
                    if (total == totalMax)
                        fin = true;
                }
            }

            if (fin)
            {
                Gana();

                //Para volver a jugar
                if (InputManager.Instance.KeyPressed(Microsoft.Xna.Framework.Input.Keys.R))
                    VolverAJugar();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backgroundImage, Vector2.Zero, Color.White);

            basuraOnScreen.Draw(spriteBatch);

            spriteBatch.DrawString(myFont, reciclablesAcertados + "/" + (reciclablesAcertados + reciclablesFallados).ToString(),
                new Vector2(20, 45), Color.White);

            spriteBatch.DrawString(myFont, noReciclablesAcertados + "/" + (noReciclablesAcertados + noReciclablesFallados).ToString(),
                new Vector2(ScreenManager.Instance.dimensions.X - 60, 45), Color.White);

            spriteBatch.DrawString(nivelFont, total.ToString(), new Vector2(ScreenManager.Instance.dimensions.X / 2 - 30, 20), Color.WhiteSmoke);

            if (fin)
            {
                if (gana)
                {
                    String a = "GANASTE!";
                    spriteBatch.DrawString(resultadoFont, a, new Vector2(ScreenManager.Instance.dimensions.X/2 - resultadoFont.MeasureString(a).X/2, ScreenManager.Instance.dimensions.Y / 4), Color.DarkGreen);
                    a = ((noReciclablesAcertados + reciclablesAcertados) * 100) / (noReciclablesAcertados + reciclablesAcertados + noReciclablesFallados + reciclablesFallados) + "% Acertado";
                    spriteBatch.DrawString(resultadoFont, a,
                    new Vector2(ScreenManager.Instance.dimensions.X / 2 - resultadoFont.MeasureString(a).X / 2, ScreenManager.Instance.dimensions.Y / 4 + resultadoFont.MeasureString("GANASTE!").Y), Color.DarkGreen);
                }

                else
                {
                    String a = "PERDISTE!";
                    spriteBatch.DrawString(resultadoFont, a, new Vector2(ScreenManager.Instance.dimensions.X / 2 - resultadoFont.MeasureString(a).X / 2, ScreenManager.Instance.dimensions.Y / 4), Color.DarkRed);
                    a = ((noReciclablesAcertados + reciclablesAcertados) * 100) / (noReciclablesAcertados + reciclablesAcertados + noReciclablesFallados + reciclablesFallados) + "% Acertado";
                    spriteBatch.DrawString(resultadoFont, a,
                    new Vector2(ScreenManager.Instance.dimensions.X / 2 - resultadoFont.MeasureString(a).X / 2, ScreenManager.Instance.dimensions.Y / 4 + resultadoFont.MeasureString("PERDISTE!").Y), Color.DarkRed);
                }

                //Jugar de nuevo
                String b = "Para jugar de nuevo presiona R";
                spriteBatch.DrawString(myFont, b, new Vector2(5, ScreenManager.Instance.dimensions.Y - myFont.MeasureString(b).Y), Color.White);
                
            }
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
        void Gana()
        {
            int porcentajeAcertado = ((noReciclablesAcertados + reciclablesAcertados) * 100) / 
                (noReciclablesAcertados + reciclablesAcertados + noReciclablesFallados + reciclablesFallados);

            if (porcentajeAcertado >= 50)
                gana = true;

            else
                gana = false;
        }

        void VolverAJugar()
        {
            reciclablesAcertados = noReciclablesAcertados = reciclablesFallados = 
                noReciclablesFallados = total = 0;
            gana = false;
            fin = false;
        }
    }
}
        
        