using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JackAreJunk.Web
{
    public class PlayerEntryState
    {
        public TaskCompletionSource<List<string>> TaskSource { get; set; }
        public PlayerEntryState()
        {
        }

    }
}
