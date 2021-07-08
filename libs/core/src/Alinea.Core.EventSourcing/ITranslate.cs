namespace Alinea.Core.Abstraction.EventSourcing
{
    /// <summary>
    /// Cross Service Message Translation
    /// </summary>
    public interface ITranslate
    {
         void Translate(IEvent @event);
    }
}