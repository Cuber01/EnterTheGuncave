using System.Collections.Generic;
using RGM.Items;

namespace RGM.General.EventsHandling
{
    public class EventHandler
    {
        public static List<dEvents> eventList = new List<dEvents>();
        
        private static Dictionary<BaseItem, dEvents> subscribers = new Dictionary<BaseItem, dEvents>();

        public static void checkEvents()
        {
            foreach (dEvents e in eventList)
            {
                
                foreach (var subscriber in subscribers)
                {
                    if (subscriber.Value == e)
                    {
                        subscriber.Key.activate();
                    }
                }
                
            }
        }
        
        public static void subscribe(BaseItem item, dEvents e)
        {
            subscribers.Add(item, e);
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