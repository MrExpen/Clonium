using System.Runtime.Serialization;

namespace GameLib.Exceptions;

[Serializable]
public class InvalidTurnException : Exception
{
    //
    // For guidelines regarding the creation of new exception types, see
    //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
    // and
    //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
    //

    public InvalidTurnException()
    {
    }

    public InvalidTurnException(string message) : base(message)
    {
    }

    public InvalidTurnException(string message, Exception inner) : base(message, inner)
    {
    }

    protected InvalidTurnException(
        SerializationInfo info,
        StreamingContext context) : base(info, context)
    {
    }
}