using System;
using VirtoCommerce.Platform.Core.Common;

namespace VirtoCommerce.AiHelper.Core.Common;
public static class ExType<T>
{
    public static T New()
    {
        return AbstractTypeFactory<T>.TryCreateInstance();
    }
    public static T New(Type type)
    {
        return AbstractTypeFactory<T>.TryCreateInstance(type.Name);
    }
}
