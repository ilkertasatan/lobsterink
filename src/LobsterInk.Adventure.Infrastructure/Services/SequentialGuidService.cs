using LobsterInk.Application.Abstraction.Services;

namespace LobsterInk.Adventure.Infrastructure.Services;

public sealed class SequentialGuidService : ISequentialGuid
{
    public Guid NewGuid()
    {
        var destinationArray = Guid.NewGuid().ToByteArray();
        var time = new DateTime(0x76c, 1, 1);
        var now = DateTime.Now;
        
        var span = new TimeSpan(now.Ticks - time.Ticks);
        var timeOfDay = now.TimeOfDay;

        var bytes = BitConverter.GetBytes(span.Days);
        var array = BitConverter.GetBytes((long) (timeOfDay.TotalMilliseconds / 3.333333));
        
        Array.Reverse(bytes);
        Array.Reverse(array);
        
        Array.Copy(bytes, bytes.Length - 2, destinationArray, destinationArray.Length - 6, 2);
        Array.Copy(array, array.Length - 4, destinationArray, destinationArray.Length - 4, 4);

        return new Guid(destinationArray);
    }
}