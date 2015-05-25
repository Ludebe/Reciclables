using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace EcoShoot.Screens
{
    class Objeto
    {
        private bool isReciclable; //Si el objeto es reciclable o no
        private string texturePath; //La dirección de la textura
        private Vector2 position; //Posición del objeto
        private Rectangle sourceRect; //Rectangulo del tamaño del objeto

        //Encapsulamiento de posición y rectángulo

        public Vector2 Position
        {
            get { return position; }
        }

        public Rectangle SourceRect
        {
            get { return sourceRect; }
        }

        public void LoadContent(Vector2 position, Rectangle sourceRect, bool isReciclable, string texturePath)
        {
            this.position = position;
            this.sourceRect = sourceRect;
            this.isReciclable = isReciclable;
            this.texturePath = texturePath;
        }

        public void Update(GameTime gameTime)
        {
            //TODO: Checkear si es clickeado y lo que desencadenaria eso
        }



    }
}
