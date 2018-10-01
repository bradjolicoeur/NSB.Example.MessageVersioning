namespace NSB.Example.Contracts.Events
{
    public interface IExampleEventV2 : IExampleEvent
    {
        string NewProperty { get; set; }
    }
}