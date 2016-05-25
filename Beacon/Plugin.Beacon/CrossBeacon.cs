using Plugin.Beacon.Abstractions;
using System;

namespace Plugin.Beacon
{
  /// <summary>
  /// Cross platform Beacon implemenations
  /// </summary>
  public class CrossBeacon
  {
    static Lazy<IBeacon> Implementation = new Lazy<IBeacon>(() => CreateBeacon(), System.Threading.LazyThreadSafetyMode.PublicationOnly);

    /// <summary>
    /// Current settings to use
    /// </summary>
    public static IBeacon Current
    {
      get
      {
        var ret = Implementation.Value;
        if (ret == null)
        {
          throw NotImplementedInReferenceAssembly();
        }
        return ret;
      }
    }

    static IBeacon CreateBeacon()
    {
#if PORTABLE
        return null;
#else
        return new BeaconImplementation();
#endif
    }

    internal static Exception NotImplementedInReferenceAssembly()
    {
      return new NotImplementedException("This functionality is not implemented in the portable version of this assembly.  You should reference the NuGet package from your main application project in order to reference the platform-specific implementation.");
    }
  }
}
