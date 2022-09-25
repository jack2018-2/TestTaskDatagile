namespace TestTaskDatagile.Redis
{
    using System.Collections.Generic;
    using TestTaskDatagile.Ext;

    public class RedisUploader : IRedisUploader
    {
        public void Upload(string breed, DogeAPIResponse<IEnumerable<string>> response)
        {
            var cache = RedisConnectorHelper.Connection.GetDatabase();

            foreach(var imgurl in response.Result)
            {
                cache.SetAdd(new StackExchange.Redis.RedisKey(breed),
                    new StackExchange.Redis.RedisValue(imgurl));
            }
            
        }
    }
}
