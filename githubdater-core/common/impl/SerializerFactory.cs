namespace NGitHubdater
{
    public class SerializerFactory
    {
        public static ISerializer GetInstance()
        {
            return new LocalXmlSerializer();
        }
    }
}
