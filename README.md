# AiHelper

## Overview

`vc-module-ai-helper` provides a provider-agnostic API for AI-assisted features in Virto Commerce, such as translation and product description generation. The core defines interfaces and base abstractions; concrete AI providers (e.g., OpenAI, Grok) are delivered as separate modules.

- Core module (this repo): contracts, models, base classes, Web API, and wiring.
- Provider modules: implement the contracts and register themselves (e.g., `vc-module-ai-helper-open-ai`, `vc-module-ai-helper-grok-ai`).
- Selection at runtime: the active provider is chosen via settings and a factory/registrar.

## Architecture

- Core abstractions (in `VirtoCommerce.AiHelper.Core`):
  - Interfaces: `IAiProvider`, `IAiTask`, `IAiTextGenerationService`, `IAiImageRecognitionService`, `IAiImageGenerationService`.
  - Base class: `AbstractAiProvider` (holds `AvailableServices`, exposes `ProviderName`, `ProviderType`, and `GetService<T>()`).
  - Builder: `AiProviderBuilder` (compose an `IAiProvider` with services and produce cloneable instances).
  - Settings and defaults: `ModuleConstants.Settings.*`, `ModuleConstants.DefaultPrompts.*`.
- Runtime wiring (in `VirtoCommerce.AiHelper.Web.Module`):
  - Registers `AiProviderRegistrar` as both `IAiProviderRegistrar` and `IAiProviderFactory`.
  - Registers module settings and permissions.
  - Registers at least one provider (sample `DummyAiProvider`) and populates allowed values for `AiHelper.TextGenerationProvider` from available providers supporting `IAiTextGenerationService`.
- Provider registration (in `VirtoCommerce.AiHelper.Data.Services.AiProviderRegistrar`):
  - `Register<TAiProvider>()` wires a builder and factory into `AbstractTypeFactory<IAiProvider>`.
  - `GetAllAiProviders()` and `GetAiProvidersByService<T>()` discover providers and their capabilities.

See the diagram: [docs/media/diagram-architecture-request.png](./docs/media/diagram-architecture-request.png)

## Quickstart

1. Install the core module (`vc-module-ai-helper`) into your Virto Commerce platform instance.
2. Install at least one provider module (e.g., `vc-module-ai-helper-open-ai` or `vc-module-ai-helper-grok-ai`).
3. In Platform settings, ensure AiHelper is enabled and select the active text generation provider:
   - `AiHelper.Enabled` = `true`
   - `AiHelper.TextGenerationProvider` = `<ProviderType from your provider>`
4. Call the Web API endpoints (below) or use UI integrations that leverage MediatR commands.

## Configuration

Settings (group `AiHelper|General`):

- `AiHelper.Enabled` (Boolean, default `true`): turns the module on/off.
- `AiHelper.TextGenerationProvider` (ShortText): active provider type for text generation. Allowed values are populated from registered providers that implement `IAiTextGenerationService`.
- `AiHelper.ImageRecognitionProvider` (ShortText): active provider type for image recognition (populated by providers implementing `IAiImageRecognitionService`).

## Extensibility (writing a provider)

1. Implement `IAiProvider` (typically inherit from `AbstractAiProvider`).
2. Implement one or more service interfaces (e.g., `IAiTextGenerationService`) and add them to `AvailableServices` via `AiProviderBuilder.WithService(...)`.
3. In your module's `PostInitialize`, call `IAiProviderRegistrar.Register<YourProvider>(() => serviceProvider.GetService<YourProvider>())`.
4. Add your `ProviderType` to allowed values of relevant settings.

Default prompts under `ModuleConstants.DefaultPrompts` (e.g., `Translation`, `ProductDescriptionGeneration`) can be reused.

## License

Copyright (c) Virto Solutions LTD.  All rights reserved.

Licensed under the Virto Commerce Open Software License (the "License"); you
may not use this file except in compliance with the License. You may
obtain a copy of the License at

<https://virtocommerce.com/open-source-license>

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or
implied.
