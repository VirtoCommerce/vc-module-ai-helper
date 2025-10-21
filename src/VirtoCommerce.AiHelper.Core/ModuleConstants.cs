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

            public static SettingDescriptor AiHelperTextGenerationProvider { get; } = new()
            {
                Name = "AiHelper.TextGenerationProvider",
                GroupName = "AiHelper|General",
                ValueType = SettingValueType.ShortText,
                AllowedValues = [],
            };

            public static SettingDescriptor AiHelperImageRecognitionProvider { get; } = new()
            {
                Name = "AiHelper.ImageRecognitionProvider",
                GroupName = "AiHelper|General",
                ValueType = SettingValueType.ShortText,
                AllowedValues = [],
            };

            public static IEnumerable<SettingDescriptor> AllGeneralSettings
            {
                get
                {
                    yield return AiHelperEnabled;
                    yield return AiHelperTextGenerationProvider;
                    yield return AiHelperImageRecognitionProvider;
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

    public static class DefaultPrompts
    {
        public const string Translation = "Translate to {locale} the text, preserve HTML or Markdown markups: {text}";
        public const string ProductDescriptionGeneration = "Generate pretty seo-friendly description in {locale} language (maximum 1000 words, use only HTML tags if you need) for marketplace product: {product}";
        public const string ImageRecognition = "Generate pretty seo-friendly description in {locale} language (maximum 1000 words, use only HTML tags if you need) for marketplace product. Product name is {product.name}";
    }
}
