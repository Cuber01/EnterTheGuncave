using System;
using System.Collections.Generic;
using RGM.Items;

namespace RGM.General.EventHandling
{
    // G stands for General. Or Game. Or Global. Choose your favourite.
    public static class GEventHandler
    {
        private static readonly List<dEvents> eventList = new List<dEvents>();
        private static readonly Dictionary<dEvents, Action<dEvents>> subscribers = new Dictionary<dEvents, Action<dEvents>>();

        public static void checkEvents()
        {
            foreach (dEvents e in eventList)
            {
                
                foreach (var subscriber in subscribers)
                {
                    if (subscriber.Key == e)
                    {
                        subscriber.Value(e);
                    }
                }
                
            }
        }
        
        public static void subscribe(Action<dEvents> action, dEvents e)
        {
            subscribers.Add(e, action);
        }
        
        public static void clearEvents()
        {
            eventList.Clear();
        }
        
        public static void fireEvent(dEvents e)
        {
            eventList.Add(e);
        }
    }
}