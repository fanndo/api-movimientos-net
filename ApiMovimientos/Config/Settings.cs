namespace ApiMovimientos.Config
{
    public class DbSettings
    {
        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }

        public string ProductCollectionName { get; set; }
    }

    public class RedisSettings
    {
        public string Host { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public bool Ssl { get; set; }
        public bool AllowAdmin { get; set; }
        public bool AbortConnect { get; set; }
        public int ConnectRetry { get; set; }
        public int ConnectTimeout { get; set; }
        public int SyncTimeout { get; set; }
        public int DefaultDatabase { get; set; }

        public string ToConnectionString()
        {
            var connectionString =
                $"{Host}, " +
                $"Ssl= {Ssl}, " +
                $"Password= {Password}, " +
                $"name= {Name}, " +
                $"AllowAdmin= {AllowAdmin}, " +
                $"abortConnect= {AbortConnect},  " +
                $"ConnectRetry= {ConnectRetry}, " +
                $"ConnectTimeout= {ConnectTimeout}, " +
                $"SyncTimeout= {SyncTimeout}, " +
                $"DefaultDatabase = {DefaultDatabase}";
            return connectionString;
        }


    }
}
