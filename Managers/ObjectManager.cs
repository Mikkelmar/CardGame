using CardGame.Objects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame.Managers
{
    public class ObjectManager
    {
        public List<GameObject> gameObjects = new List<GameObject>();

        public void Update(GameTime gt, Game1 g)
        {
            for(var i = 0; i < gameObjects.Count;i++)
            {
                GameObject obj = gameObjects[i];
                if (obj.rendered)
                {
                    obj.SetBounds(obj.X, obj.Y, (int) obj.Width, (int) obj.Height);
                    obj.Update(gt, g);
                }
            }
        }
        public void Draw(Game1 g)
        {
            //gameObjects.Sort((o1, o2) => o2.Y.CompareTo(o1.Y));
            List<GameObject> objects = new List<GameObject>(gameObjects);
            foreach (GameObject obj in objects)
            {
                if(obj.rendered && obj.visiable)
                {
                    obj.Draw(g);
                }
                
            }
        }
        
        public bool Intersect(Rectangle newPos)
        {
            foreach (GameObject obj in gameObjects)
            {
                if (obj.Intersect(newPos)){ return true; }
            }
            return false;
        }

 
        public List<GameObject> GetAllObjectsWith(Predicate<GameObject> filter)
        {
            List<GameObject> allSelected = new List<GameObject>();
            foreach (GameObject obj in gameObjects)
            {
                if (filter(obj))
                {
                    allSelected.Add(obj);
                }

            }
            return allSelected;
        }
 
        public Vector2 FromToDir(GameObject from, GameObject to)
        {
            return new Vector2(from.GetPosCenter().X - to.GetPosCenter().X, from.GetPosCenter().Y - to.GetPosCenter().Y);
        }
        public void Add(GameObject obj, Game1 g) { gameObjects.Add(obj); obj.Init(g); }
        public void Add(GameObject obj) { gameObjects.Add(obj);}
        public void Remove(GameObject obj, Game1 g) { 
            if(obj != null)
            {
                obj.Destroy(g); gameObjects.Remove(obj);
            }
              }
        public void Remove(int index, Game1 g) { gameObjects[index].Destroy(g); gameObjects.Remove(gameObjects[index]);  }
        public void Clear(Game1 g) {
            List<GameObject> obs = new List<GameObject>(gameObjects);
            foreach (GameObject o in obs)
            {
                o.Destroy(g);
            }
            gameObjects.Clear(); 
        }
    }
}
