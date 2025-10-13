using System.Collections.Generic;
using VirtoCommerce.Platform.Core.Settings;

namespace VirtoCommerce.AiHelper.Core;

public static class ModuleConstants
{
    public static class Security
    {
        public static class Permissions
        {
            public const string Access = "ai-helper:access";
            public const string Create = "ai-helper:create";
            public const string Read = "ai-helper:read";
            public const string Update = "ai-helper:update";
            public const string Delete = "ai-helper:delete";

            public static string[] AllPermissions { get; } =
            [
                Access,
                Create,
                Read,
                Update,
                Delete,
            ];
        }
    }

    public static class Settings
    {
        public static class General
        {
            public static SettingDescriptor AiHelperEnabled { get; } = new()
            {
                Name = "AiHelper.Enabled",
                GroupName = "AiHelper|General",
                ValueType = SettingValueType.Boolean,
                DefaultValue = true,
            };

            public static SettingDescriptor AiHelperTranslationProvider { get; } = new()
            {
                Name = "AiHelper.TranslationProvider",
                GroupName = "AiHelper|General",
                ValueType = SettingValueType.ShortText,
                AllowedValues = [],//["OpenAI"],
                //DefaultValue = "OpenAI",
            };


            public static IEnumerable<SettingDescriptor> AllGeneralSettings
            {
                get
                {
                    yield return AiHelperEnabled;
                    yield return AiHelperTranslationProvider;
                }
            }
        }

        public static IEnumerable<SettingDescriptor> AllSettings
        {
            get
            {
                return General.AllGeneralSettings;
            }
        }
    }
}
