using System;

namespace Context
{
    public static class ContextProvider
    {
        public static IContext Context { get; private set; }

        public static void Subscribe(IContext context)
        {
          /*if (Context != null)
            {
                return;
            }
            */
            Context = context;
        }
        

    }
}
