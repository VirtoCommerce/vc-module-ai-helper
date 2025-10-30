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

            public static SettingDescriptor AiHelperImageGenerationProvider { get; } = new()
            {
                Name = "AiHelper.ImageGenerationProvider",
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
                    yield return AiHelperImageGenerationProvider;
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
        public const string FillProperties = "Using proposed images you need fill the properties of product {product.name}. Look at json template and fill field 'value' in every paragraph. You should use only values from list 'availableValues' if it fill for the property, otherwise use the most suitable in you opinion. Any property may have more than one value from different images, if property has 'isMultivalue' you may fill multiple answer comma separated, otherwise single only. Return answer in json format with keys and values in order as template. Answer in English. Json template is: {jsonTemplate}";
        public const string ProductImageGeneration = "Generate pretty photo-realistic image for marketplace product card. Product name is {product.name}, product description is {product.description}";
    }
}
