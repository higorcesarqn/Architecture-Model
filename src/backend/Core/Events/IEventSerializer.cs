namespace Core.Events
{
    public interface IEventSerializer
    {
        T Deserialize<T>(string serialization) where T : Event;

        string Serialize<T>(T theEvent, bool indented = false) where T : Event;
    }
}