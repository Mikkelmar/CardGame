using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CardGame.Client
{
    public class ServerActionQueue
    {
        private readonly ConcurrentQueue<Action> actionQueue = new ConcurrentQueue<Action>();

        public void EnqueueAction(Action action)
        {
            actionQueue.Enqueue(action);
        }

        public void ExecuteAll()
        {
            if (actionQueue.TryDequeue(out var action))
            {
                try
                {
                    action();
                }
                catch (Exception ex)
                {
                    // Handle exception, log error, etc.
                    Console.WriteLine($"Error processing action: {ex.Message}");
                }
            }
        }
    }
}
