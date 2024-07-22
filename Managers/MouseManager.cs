using CardGame.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CardGame.Managers
{
    public class MouseManager
    {
        //private string path = "";
        /**
         * 
         * The bool is whether the mouse cords should be relative to the screen og game map
         * if true the mouse cords + camera position is alerted back at the lisner
         * */
        private Dictionary<Clickable, bool> mouseLeftClickLisners = new Dictionary<Clickable, bool>();
        private Dictionary<LeftRelease, bool> mouseLeftReleaseLisners = new Dictionary<LeftRelease, bool>();
        private Dictionary<RightClickable, bool> mouseRightClickLisners = new Dictionary<RightClickable, bool>();
        private Dictionary<HoverLisner, bool> hoverLisners = new Dictionary<HoverLisner, bool>();
        public bool stopClick = false, stopLeftRelease = false;
        public Vector2 GetMousePos(Game1 g)
        {
            return g.gameCamera.ScreenToWorld(Mouse.GetState().Position.ToVector2());
        }
        private List<Clickable> sortedByDepth(List<Clickable> list)
        {
            //TODO: Cost effecitve remember sorted list
            List<Clickable> objects = list.FindAll(e => e is GameObject);

            objects = objects.OrderBy(o => (o as GameObject).depth).ToList();

            List<Clickable> newList = new List<Clickable>();
            newList.AddRange(objects);
            return newList;
        }
        private MouseState oldState = Mouse.GetState();
        public void Update(GameTime gt, Game1 g)
        {
            MouseState newState = Mouse.GetState();

            g.gameCamera.Zoom = 1f;
            Vector2 _worldPosition = g.gameCamera.ScreenToWorld(new Vector2(newState.X, newState.Y));
            g.gameCamera.Zoom = (g.gameBoard.cam.Zoom);

            Vector2 _worldPositionZoomed = g.gameCamera.ScreenToWorld(new Vector2(newState.X, newState.Y));
            try
            { 
                if (newState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
                {
                    stopClick = false;
                    List<Clickable> lisners = new List<Clickable>(mouseLeftClickLisners.Keys);
                    foreach (Clickable lisner in sortedByDepth(lisners))
                    {
                        if (stopClick)
                        {
                            break;
                        }
                        if (lisner == null)
                        {
                            break;
                        }
                        if (mouseLeftClickLisners.ContainsKey(lisner)){ 
                            if (mouseLeftClickLisners[lisner])
                            {
                                lisner.Clicked(_worldPosition.X, _worldPosition.Y, g);
                            }
                            else
                            {
                                lisner.Clicked(_worldPositionZoomed.X, _worldPositionZoomed.Y, g);
                            }
                        }

                    }
                }
                else if (newState.LeftButton == ButtonState.Released && oldState.LeftButton == ButtonState.Pressed)
                {
                    //Debug.WriteLine("normal:" + _worldPosition);
                    //Debug.WriteLine(_worldPositionZoomed);
                    List<LeftRelease> lisners = new List<LeftRelease>(mouseLeftReleaseLisners.Keys);
                    stopLeftRelease = false;
                    foreach (LeftRelease lisner in lisners)
                    {
                        if (stopLeftRelease)
                        {
                            break;
                        }
                        if (lisner == null)
                        {
                            break;
                        }
                        if (mouseLeftReleaseLisners[lisner])
                        {
                            lisner.LeftReleased(_worldPosition.X, _worldPosition.Y, g);
                        }
                        else
                        {
                            lisner.LeftReleased(_worldPositionZoomed.X, _worldPositionZoomed.Y, g);
                        }

                    }
                }
                if (newState.RightButton == ButtonState.Pressed && oldState.RightButton == ButtonState.Released)
                {
                    List<RightClickable> lisners = new List<RightClickable>(mouseRightClickLisners.Keys);
                    foreach (RightClickable lisner in lisners)
                    {
                        if (lisner == null)
                        {
                            break;
                        }
                        if (mouseRightClickLisners.ContainsKey(lisner)){ 
                            if (mouseRightClickLisners[lisner])
                            {
                                lisner.RightClicked(_worldPosition.X, _worldPosition.Y, g);
                            }
                            else
                            {
                                lisner.RightClicked(_worldPositionZoomed.X, _worldPositionZoomed.Y, g);
                            }
                        }

                    }
                }
                oldState = newState;

                List<HoverLisner> hoverLisnerList = new List<HoverLisner>(hoverLisners.Keys);
                foreach (HoverLisner lisner in hoverLisnerList)
                {
                    if (lisner == null)
                    {
                        break;
                    }
                    if (hoverLisners.ContainsKey(lisner))
                    {
                        if (hoverLisners[lisner])
                        {
                            lisner.Hover(_worldPosition.X, _worldPosition.Y, g);
                        }
                        else
                        {
                            lisner.Hover(_worldPositionZoomed.X, _worldPositionZoomed.Y, g);
                        }
                    }
                }
            }
            catch
            {
                //lol
            }
        }
        public static Vector2 GetMousePos(bool relative=false)
        {
            MouseState state = Mouse.GetState();
            return new Vector2(state.X, state.Y);
        }
     
        public void Add(Clickable obj, bool relative = false) { mouseLeftClickLisners.Add(obj, relative);}
        public void Remove(Clickable obj) { mouseLeftClickLisners.Remove(obj); }
        public void Clear() { mouseLeftClickLisners.Clear(); }
        public void AddRight(RightClickable obj, bool relative = false) { mouseRightClickLisners.Add(obj, relative); }
        public void RemoveRight(RightClickable obj) { mouseRightClickLisners.Remove(obj); }
        public void ClearRight() { mouseRightClickLisners.Clear(); }
        public void AddHover(HoverLisner obj, bool relative = false) { hoverLisners.Add(obj, relative); }
        public void RemoveHover(HoverLisner obj) { hoverLisners.Remove(obj); }
        public void ClearHover() { hoverLisners.Clear(); }
        public void AddLeftRelease(LeftRelease obj, bool relative = false) { mouseLeftReleaseLisners.Add(obj, relative); }
        public void RemoveLeftRelease(LeftRelease obj) { mouseLeftReleaseLisners.Remove(obj); }
        public void ClearLeftRelease() { mouseLeftReleaseLisners.Clear(); }
    }
}
