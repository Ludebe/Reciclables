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
    public class Basura
    {
        //Atributos de constructor
        public Vector2 Position;
        public Vector2 Speed;
        public String TexturePath;
        public Boolean Reciclable;

        //Otros atributos
        public Texture2D Texture;
        public Boolean CanDelete;
        public Boolean Acertado;

        /* Primero se inicializa y después
         * se le setea la posición para poder
         * calcular con las medidas de la textura.
         * */

        //Constructor 1
        public Basura(Vector2 Speed, String TexturePath, Boolean IsReciclable)
        {
            this.Speed = Speed;
            this.TexturePath = TexturePath;
            this.Reciclable = IsReciclable;

            CanDelete = false;
        }

        //Constructor 2
        public Basura(String TexturePath, Boolean IsReciclable)
        {
            this.TexturePath = TexturePath;
            this.Reciclable = IsReciclable;

            CanDelete = false;
        }

        //Constructor 3
        public Basura(Vector2 Position, Vector2 Speed, String TexturePath, Boolean IsReciclable)
        {
            this.Position = Position;
            this.Speed = Speed;
            this.TexturePath = TexturePath;
            this.Reciclable = IsReciclable;

            CanDelete = false;
        }

        //Constructor 4
        public Basura(Vector2 Position, Vector2 Speed, Texture2D Texture, Boolean IsReciclable)
        {
            this.Position = Position;
            this.Speed = Speed;
            this.Texture = Texture;
            this.Reciclable = IsReciclable;

            CanDelete = false;
        }

        public void LoadContent(ContentManager Content)
        {
            Texture = Content.Load<Texture2D>(TexturePath);
        }

        public void Update(GameTime gameTime)
        {
            /* Si se está presionando el click, comprueba que esté sobre el objeto
             * */
            if (Mouse.GetState().LeftButton == ButtonState.Pressed ||
                Mouse.GetState().RightButton == ButtonState.Pressed)
            {
                //Si se preisonó sobre el objeto, ejecuta la operación.
                if (ObjectClicked())
                {

                }

                //Sino, lo mueve normalmente.
                else 
                {
                    Move();
                }
            }

            else
            {
                Move();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }

        /* Si se clickeó sobre la basura
         * */
        public Boolean ObjectClicked() 
        {
            MouseState mouseState = Mouse.GetState();

            //Comprueba que el mouse esté sobre el objeto
            if (mouseState.X > Position.X && mouseState.X <= Position.X + Texture.Width &&
                mouseState.Y > Position.Y && mouseState.Y <= Position.Y + Texture.Height)
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                    OnLeftClick();

                else if (mouseState.RightButton == ButtonState.Pressed)
                    OnRightClick();

                return true;
            }

            else
                return false;
        }


        void Move() 
        {
            if (Position.X + Speed.X + Texture.Width > ScreenManager.Instance.dimensions.X ||
                    Position.X <= 0)
                Speed.X *= -1;

            if (Position.Y <= 0)
                Speed.Y *= -1;

            //Si salió de la pantalla, se puede borrar.
            if (Position.Y > ScreenManager.Instance.dimensions.Y &&
                Speed.Y > 0)
                CanDelete = true;

            else
            {
                //Mueve
                Position.X += Speed.X;
                Position.Y += Speed.Y;
            }
        }

        void OnLeftClick()
        {
            if (Reciclable)
                Acertado = true;
            else
                Acertado = false;

            CanDelete = true;
        }

        void OnRightClick()
        {
            if (Reciclable)
                Acertado = false;
            else
                Acertado = true;

            CanDelete = true;
        }
    }
}
