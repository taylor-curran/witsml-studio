using System;

namespace PDS.WITSMLstudio.Query
{
    public class GrowingObjectQueryProvider<T>
    {
        public object Context { get; set; } = new object();
        public bool IsCancelled { get; set; } = false;
        
        public GrowingObjectQueryProvider()
        {
        }
        
        public GrowingObjectQueryProvider(object connection, object settings, object context)
        {
            Context = context;
        }
        
        public void Initialize()
        {
        }
        
        public void UpdateDataQuery(object query)
        {
        }
    }
}
