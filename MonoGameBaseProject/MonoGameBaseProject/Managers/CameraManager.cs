using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace EcoShoot.Managers
{
    public class CameraManager
    {
        private static CameraManager instance;
        Matrix viewMatrix;
        public Matrix ViewMatrix { get { return viewMatrix; } }
        private Vector2 position;

        public static CameraManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new CameraManager();

                return instance;
            }
        }

        //Setea la posición de la camara
        public void SetFocalPoint(Vector2 focalPoint)
        {
            position = new Vector2(focalPoint.X - ScreenManager.Instance.dimensions.X / 2,
                focalPoint.Y - ScreenManager.Instance.dimensions.Y / 2);

            if (position.X < 0)
                position.X = 0;
            if (position.Y < 0)
                position.Y = 0;
        }

        public void Update(GameTime gameTime)
        {
            viewMatrix = Matrix.CreateTranslation(new Vector3(-position, 0));
        }

    }
}
