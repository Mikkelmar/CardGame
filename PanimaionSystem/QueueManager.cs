using CardGame.Cards.PanimaionSystem;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame.PanimaionSystem
{
    public class QueueManager
    {
        private static Queue<IQueueable> queue;
        private Queue<GameAction> whenDoneQueueActions;
        public bool inProgress() { return queue.Count != 0; }
        private TimeSpan waitTime = TimeSpan.FromMilliseconds(500);
        private TimeSpan currentWaitTime = TimeSpan.FromMilliseconds(0);
        public QueueManager()
        {
            queue = new Queue<IQueueable>();
            whenDoneQueueActions = new Queue<GameAction>();
        }
        public void addWhenDoneQueueActions(GameAction item)
        {
            whenDoneQueueActions.Enqueue(item);
        }
        public static void Enqueue(IQueueable item)
        {
            queue.Enqueue(item);
        }
        public void EnqueueItem(IQueueable item)
        {
            queue.Enqueue(item);
        }

        public void Update(GameTime gameTime, Game1 g)
        {
            if (queue.Count > 0)
            {
                var current = queue.Peek();
                if (!current.haveStarted)
                {
                    current.Start(g);
                }
                current.Update(gameTime, g);

                if (current.IsComplete)
                {
                    queue.Dequeue();
                    if (queue.Count > 0)
                    {
                        queue.Peek().Start(g);
                    }
                    else
                    {
                        currentWaitTime = TimeSpan.FromMilliseconds(0);
                    }
                }
            }
            else
            {
                if(currentWaitTime > waitTime)
                {
                    executeFinishedActions(gameTime, g);
                }
                else
                {
                    currentWaitTime += gameTime.ElapsedGameTime;
                }
                
            }
        }
        private void executeFinishedActions(GameTime gameTime, Game1 g)
        {

            
            if (whenDoneQueueActions.Count > 0)
            {
                System.Diagnostics.Debug.WriteLine("done with all animations!" + whenDoneQueueActions.Count);
                var current = whenDoneQueueActions.Peek();
                current.Update(gameTime, g);

                if (current.IsComplete)
                {
                    whenDoneQueueActions.Dequeue();
                    if (whenDoneQueueActions.Count > 0)
                    {
                        whenDoneQueueActions.Peek().Start(g);
                    }
                    executeFinishedActions(gameTime, g);
                }
            }
        }
        public void Draw(Game1 g)
        {
            if (queue.Count > 0)
            {
                var current = queue.Peek();

                if (current is ObjectAnimation)
                {
                    ((ObjectAnimation)current).Draw(g);
                }
            }
        }

        public void Start(Game1 g)
        {
            if (queue.Count > 0)
            {
                queue.Peek().Start(g);
            }
        }
    }

}
