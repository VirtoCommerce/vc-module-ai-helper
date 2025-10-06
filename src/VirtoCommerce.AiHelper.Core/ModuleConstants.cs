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

            public static SettingDescriptor AiHelperProvider { get; } = new()
            {
                Name = "AiHelper.Provider",
                GroupName = "AiHelper|General",
                ValueType = SettingValueType.ShortText,
                AllowedValues = ["OpenAI"],
                DefaultValue = "OpenAI",
            };

            public static SettingDescriptor AiHelperOpenAiModel { get; } = new()
            {
                Name = "AiHelper.OpenAiModel",
                GroupName = "AiHelper|OpenAI",
                ValueType = SettingValueType.ShortText,
                AllowedValues = ["gpt-5", "gpt-5-mini", "gpt-5-nano"],
                DefaultValue = "gpt-5-nano",
            };

            public static SettingDescriptor AiHelperOpenAiKey { get; } = new()
            {
                Name = "AiHelper.OpenAiKey",
                GroupName = "AiHelper|OpenAI",
                ValueType = SettingValueType.SecureString,
            };

            public static SettingDescriptor AiHelperOpenAiPromptTranslate { get; } = new()
            {
                Name = "AiHelper.OpenAiPromptTranslate",
                GroupName = "AiHelper|Prompts",
                ValueType = SettingValueType.LongText,
                DefaultValue = "Translate to {locale} the text, preserve HTML or Markdown markups: {text}",
            };

            public static IEnumerable<SettingDescriptor> AllGeneralSettings
            {
                get
                {
                    yield return AiHelperEnabled;
                    yield return AiHelperProvider;
                    yield return AiHelperOpenAiModel;
                    yield return AiHelperOpenAiKey;
                    yield return AiHelperOpenAiPromptTranslate;
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
