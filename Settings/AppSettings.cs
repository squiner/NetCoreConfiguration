using System;

namespace NetCoreConfiguration.Settings
{
    public class AppSettings
    {
        public string Title { get; set; }
        public SettingsA SettingsA { get; set; }
        public SettingsB SettingsB { get; set; }
        public SettingsC SettingsC { get; set; }
    }

    public class SettingsA
    {
        public string TestA { get; set; }
    }

    public class SettingsB
    {
        public string TestA { get; set; }
        public string TestB { get; set; }
    }

    public interface ISettingsC
    {
        string TestA { get; set; }
        string TestB { get; set; }
        string TestC { get; set; }
    }

    public class SettingsC : ISettingsC
    {
        public string TestA { get; set; }
        public string TestB { get; set; }
        public string TestC { get; set; }

    }

    public class SettingsD
    {
        public Guid Guid { get; set; }

        public string TestD { get; set; }
    }

    public class SettingsE
    {
        public Guid Guid { get; set; }

        public string TestE { get; set; }
    }

    public class SettingsF
    {
        public Guid Guid { get; set; }

        public string TestF { get; set; }
    }

}
