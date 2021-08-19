using System;

namespace ServerProj.Base.Enum
{
    [Flags]
    public enum ServiceType
    {
        All = 0,
        Registry = 1,
        Gateway = 2,
        Auth = 4,
        Business = 8,
    }
}
