using System;
using VirtoCommerce.Platform.Core.Exceptions;

namespace VirtoCommerce.AiHelper.Core.Exceptions;
[Serializable]
public class AiException : PlatformException
{
    public AiException(string message)
        : base(message)
    {
    }

    public AiException(string message, Exception innerException)
    : base(message, innerException)
    {
    }
}
