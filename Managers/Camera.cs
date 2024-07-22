using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Engine;

namespace CardGame.Managers
{
    public class Camera
    {
        //private OrthographicCamera cam;
        public Vector2 position;
        //public Matrix transform { get; private set; }
        public float delay { get; set; } = 3.0f;
        public bool instantCamera = true;
        public float Zoom = 1f;
        public Camera(Vector2 position)
        {
            this.position = position;
        }
        public void setZoom(Game1 g, float newZoom)
        {
            this.Zoom = newZoom;
            g.gameCamera.Zoom = newZoom;
        }
        //update
        public void Update(Vector2 pos, Game1 g)
        {
            float d = delay * Drawing.delta;

            //move
            if (instantCamera)
            {
                position.X = pos.X;// - ((Drawing.WINDOW_WIDTH / zoomScale) / 4);
                position.Y = pos.Y;// - ((Drawing.WINDOW_HEIGHT / zoomScale) / 4);

            }
            else
            {
                position.X += ((pos.X - position.X) - (Drawing.WINDOW_WIDTH) / 2) * d;
                position.Y += ((pos.Y - position.Y) - (Drawing.WINDOW_HEIGHT) / 2) * d;
            }

            g.gameCamera.LookAt(position);
            //transform = Matrix.CreateTranslation(-position.X, -position.Y, 0);
            //*Matrix.CreateScale(zoomScale)


        }

    }
}
