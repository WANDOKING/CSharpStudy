namespace Common;

public class PluginAttribute : Attribute
{
}

public class StartupAttribute : Attribute
{
}

[Plugin]
public class SystemInfo
{
    bool _is64Bit;

    public SystemInfo()
    {
        _is64Bit = Environment.Is64BitOperatingSystem;
        Console.WriteLine("SystemInfo created.");
    }

    [Startup]
    public void WriteInfo()
    {
        Console.WriteLine("OS == {0}bits", (_is64Bit == true) ? "64" : "32");
    }
}