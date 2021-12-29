using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PubSub;

namespace JackAreJunk.Web
{
    public class PlayerEntryWeb : IPlayerEntry
    {
        Hub hub = Hub.Default;
        PlayerEntryState state;
        public PlayerEntryWeb(PlayerEntryState state)
        {
            this.state = state;
        }

        public async Task<List<string>> GetPlayerNames()
        {
            state.TaskSource = new TaskCompletionSource<List<string>>();
            hub.Publish<PlayerEntryPubSubMessage>(new PlayerEntryPubSubMessage(state.TaskSource));
            var result = await state.TaskSource.Task;
            return result;
        }
    }
    public class PlayerEntryPubSubMessage
    {
        public PlayerEntryPubSubMessage(TaskCompletionSource<List<string>> taskSource)
        {
            TaskSource = taskSource;
        }
        public TaskCompletionSource<List<string>> TaskSource { get; set; }
    }
}
