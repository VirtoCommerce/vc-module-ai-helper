using System;

namespace VirtoCommerce.AiHelper.Core.Models;
[Flags]
public enum AiRequestLogResponseGroup
{
    None = 0,
    Minimal = 1,
    Normal = 2,
    Verbose = 4,
}
