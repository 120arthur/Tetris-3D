using System;

namespace Context
{
    public static class ContextProvider
    {
        public static Context Context { get; private set; }

        public static void Subscribe(Context context)
        {
            if (context != null)
            {
                return;
            }

            Context = context;
        }
        
    }
}
