namespace TestTaskDatagile.Redis
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TestTaskDatagile.Ext;

    public class RedisUploader : IRedisUploader
    {
        public async Task Upload(string breed, DogeAPIResponse<IEnumerable<string>> response)
        {
            var cache = RedisConnectorHelper.Connection.GetDatabase();

            foreach(var imgurl in response.Result)
            {
                await cache.SetAddAsync(new StackExchange.Redis.RedisKey(breed),
                    new StackExchange.Redis.RedisValue(imgurl));
            }
            
        }
    }
}
