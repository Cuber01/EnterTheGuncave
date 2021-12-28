using System;
using System.Collections.Generic;
using RGM.Items;

namespace RGM.General.EventHandling
{
    // G stands for General. Or Game. Or Global. Choose your favourite.
    public static class GEventHandler
    {
        private static readonly List<dEvents> eventList = new List<dEvents>();
        private static readonly Dictionary<dEvents, List<Action<dEvents>>> subscribers = new Dictionary<dEvents, List<Action<dEvents>>>();

        public static void checkEvents()
        {
            foreach (dEvents e in eventList)
            {
                
                foreach (var subscriber in subscribers)
                {
                    if (subscriber.Key == e)
                    {

                        foreach (Action<dEvents> action in subscriber.Value)
                        {
                            action(e);
                        }

                    }
                }
                
            }
        }
        
        public static void subscribe(Action<dEvents> action, dEvents e)
        {
            if (!subscribers.TryGetValue(e, out var list))
            {
                list = new List<Action<dEvents>>();
                subscribers.Add(e, list);
            }

            list.Add(action);
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