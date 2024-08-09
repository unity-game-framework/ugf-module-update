# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [5.0.0-preview](https://github.com/unity-game-framework/ugf-module-update/releases/tag/5.0.0-preview) - 2024-08-09  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-update/milestone/12?closed=1)  
    

### Changed

- Update package ([#42](https://github.com/unity-game-framework/ugf-module-update/issues/42))  
    - Update dependencies: `com.ugf.application` to `9.0.0-preview` version.
    - Update package _Unity_ version to `2023.2`.
    - Update package registry to _UPM Hub_.

## [4.0.0](https://github.com/unity-game-framework/ugf-module-update/releases/tag/4.0.0) - 2023-01-04  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-update/milestone/11?closed=1)  
    

### Changed

- Update project ([#40](https://github.com/unity-game-framework/ugf-module-update/issues/40))  
    - Update dependencies: `com.ugf.application` to `8.4.0` and `com.ugf.editortools` to `2.15.0` versions.
    - Update package _Unity_ version to `2022.2`.
    - Add `UpdateModuleAsset` class inspector selection preview support.

## [4.0.0-preview](https://github.com/unity-game-framework/ugf-module-update/releases/tag/4.0.0-preview) - 2022-07-14  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-update/milestone/10?closed=1)  
    

### Changed

- Change string ids to global id data ([#38](https://github.com/unity-game-framework/ugf-module-update/issues/38))  
    - Update dependencies: `com.ugf.application` to `8.3.0`, `com.ugf.update` to `6.0.0` and `com.ugf.editortools` to `2.8.1` versions.
    - Update package _Unity_ version to `2022.1`.
    - Update package _API Compatibility_ to `.NET Standard 2.1`.
    - Change usage of ids as `GlobalId` structure instead of `string`.

## [3.0.0-preview.2](https://github.com/unity-game-framework/ugf-module-update/releases/tag/3.0.0-preview.2) - 2021-06-23  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-update/milestone/9?closed=1)  
    

### Changed

- Rework IUpdateSystemDescription with struct ([#36](https://github.com/unity-game-framework/ugf-module-update/pull/36))  
    - Update dependencies: `com.ugf.update` to `6.0.0-preview.1` version.
    - Change `IUpdateModuleDescription.Systems` from dictionary to list collection.
    - Change `UpdateModule` to register _PlayerLoop_ systems from description systems.
    - Remove `IUpdateSystemDescription` interface and all related classes, replaced by `UpdateSystemDescription` structure.
    - Remove `IUpdateModule.Systems` property, all required data can be accessed from description.

### Removed

- Remove IUpdateGroupDescription.SystemType ([#35](https://github.com/unity-game-framework/ugf-module-update/pull/35))  
    - Add `UpdateGroupSystemDescription` to describe update group required to add to the specified update subsystem.
    - Add `UpdateGroupListAsset` to build `UpdateGroup` with collection of items as list.
    - Change `UpdateModule` to contains regular provider for groups and register groups from description to _UpdateProvider_.
    - Change `UpdateModule` to clear _UpdateProvider_ on uninitialize.
    - Change `IUpdateModuleDescription` to contains `Groups` as `UpdateGroupSystemDescription` items.
    - Change `UpdateGroupAsset` to be `UpdateGroup` builder without description.
    - Change `UpdateGroupDescribed` to contains description of any type.
    - Remove `IUpdateGroupDescription` interface and all related classes.
    - Remove `UpdateGroupAsset<TItem, TDescription>` class and replaced by `UpdateGroupAsset<TDescription>` with additional method to define and build description of any type.
    - Remove `UpdateGroupProvider` class, `UpdateModule` register groups instead.

## [3.0.0-preview.1](https://github.com/unity-game-framework/ugf-module-update/releases/tag/3.0.0-preview.1) - 2021-06-18  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-update/milestone/8?closed=1)  
    

### Added

- Add module entries collection ([#32](https://github.com/unity-game-framework/ugf-module-update/pull/32))  
    - Add `IUpdateModuleDescription.SubGroups` collection to describe groups which required to add at specified update group as subgroup.
    - Add `IUpdateModuleDescription.Entries` collection to describe entries which required to add at specified update group.
    - Add `IUpdateModule.Entries` to provide registered entries.

### Changed

- Update to latest update package ([#30](https://github.com/unity-game-framework/ugf-module-update/pull/30))  
    - Change `UpdateGroupProvider` to work with updated `IUpdateProvider`.
    - Change `UpdateGroupDescribed` to implement update `UpdateGroup`.
    - Change `UpdateGroupAssetBase.OnBuild` method to receive `IApplication` as argument.
    - Change `UpdateSystemDescriptionProvider` to receive `IUpdateLoop` in constructor directly.
    - Change name of `UpdateGroupAssetBase` and `UpdateSystemDescriptionAssetBase` to `UpdateGroupAsset` and `UpdateSystemDescriptionAsset`.
    - Change name of `UpdateGroupAsset` to `UpdateGroupSetAsset`.
    - Change name of `UpdateSystemDescriptionAsset` to `UpdateSystemDescriptionDefaultAsset`.
    - Remove `Name` property from `UpdateGroupAsset` asset class.
- Add application argument for IUpdateGroupBuilder  ([#28](https://github.com/unity-game-framework/ugf-module-update/pull/28))  
    - Change `IUpdateGroupBuilder` to have `IApplication` as argument in build method.

## [3.0.0-preview](https://github.com/unity-game-framework/ugf-module-update/releases/tag/3.0.0-preview) - 2021-03-02  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-update/milestone/7?closed=1)  
    

### Changed

- Rework to use providers for systems and groups in module ([#24](https://github.com/unity-game-framework/ugf-module-update/pull/24))  
    - Update `IUpdateModule` to replace `Systems` and `Groups` dictionaries with `IProvider` interfaces.
    - Add `UpdateGroupProvider` and `UpdateSystemDescriptionProvider` provider classes with update system registration.
    - Remove `Add`, `Remove`, `Get`, `TryGet` and etc manage methods from `IUpdateModule` and `UpdateModule` classes.
- Update to Unity 2021 and Application dependency ([#22](https://github.com/unity-game-framework/ugf-module-update/pull/22))  
    - Update project to _Unity_ of `2021.1` version.
    - Update dependencies: `com.ugf.application` to `8.0.0-preview.4` version.
    - Update package publish registry.
    - Remove deprecated code.

### Removed

- Remove IUpdateGroupDescribed interface ([#23](https://github.com/unity-game-framework/ugf-module-update/pull/23))  
    - Remove `IUpdateGroupDescribed` interface.
    - Change `IUpdateModule`, `UpdateGroupAssetBase` and `UpdateGroupDescribed<TItem, TDescription>` classes to work with `IUpdateGroup` directly.

## [2.1.0](https://github.com/unity-game-framework/ugf-module-update/releases/tag/2.1.0) - 2021-01-16  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-update/milestone/6?closed=1)  
    

### Changed

- Update application dependency ([#17](https://github.com/unity-game-framework/ugf-module-update/pull/17))  
    - Update dependencies: `com.ugf.application` to `7.1.0` version.
    - Deprecate `UpdateModuleDescription` constructor with `registerType` argument, use properties initialization instead.

## [2.0.0](https://github.com/unity-game-framework/ugf-module-update/releases/tag/2.0.0) - 2020-12-05  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-update/milestone/5?closed=1)  
    

### Changed

- Rework to support updated application package ([#14](https://github.com/unity-game-framework/ugf-module-update/pull/14))  
    - Update to use `UGF.Builder` and `UGF.Description` packages from the latest version of `UGF.Application` package.
    - Add `IUpdateGroupBuilder` and `IUpdateGroupDescribed` interfaces to implement update groups with descriptions.
    - Add `UpdateGroupAssetBase` and `UpdateGroupAsset<TItem, TDescription>` builder assets to implement building of `IUpdateGroupDescribed` instances.
    - Add `UpdateGroupAsset` asset to build default `UpdateGroup` with `UpdateSet<IUpdateHandler>` update collection.
    - Add dependencies: `com.ugf.logs` of `4.1.0` version.
    - Add `UpdateModule` systems and groups logging.
    - Change dependencies: `com.ugf.application` to `6.0.0` and `com.ugf.update` to `5.2.1`.
    - Change `IUpdateGroupDescription` to contains only information about type of the target player loop system.
    - Change `IUpdateModule` to manage registered `IUpdateSystemDescription` and `IUpdateGroupDescribed` in player loop, and add ability to add or remove systems and groups at runtime after module initialization.
    - Change `IUpdateModuleDescription` to contains collection of `IUpdateGroupBuilder` instead of `IUpdateGroupDescription`.
    - Change name of the root of create asset menu, from `UGF` to `Unity Game Framework`.
    - Remove `UpdateGroupDescriptionAsset` and `UpdateGroupDescriptionAssetBase`, all functionality moved to `UpdateGroupAssetBase` and related classes.
    - Remove `UpdateGroupDescriptionBase` and `UpdateGroupDescription`, and replaced by reworked `UpdateGroupDescription`.

## [1.0.0](https://github.com/unity-game-framework/ugf-module-update/releases/tag/1.0.0) - 2020-11-22  

### Release Notes

- [Milestone](https://github.com/unity-game-framework/ugf-module-update/milestone/4?closed=1)  
    

### Changed

- Rework and update to use latest UGF.Application package ([#11](https://github.com/unity-game-framework/ugf-module-update/pull/11))  
    - Add `UpdateGroupDescription` to define update group creation on module initialization.
    - Add `UpdateSystemDescription` to define player loop system creation on module initialization.
    - Change `UpdateModule` to create player loop systems and update groups on initialization.
- Update to Unity 2020.2 ([#9](https://github.com/unity-game-framework/ugf-module-update/pull/9))

## [0.3.0-preview](https://github.com/unity-game-framework/ugf-module-update/releases/tag/0.3.0-preview) - 2019-12-09  

- [Commits](https://github.com/unity-game-framework/ugf-module-update/compare/0.2.0-preview...0.3.0-preview)
- [Milestone](https://github.com/unity-game-framework/ugf-module-update/milestone/3?closed=1)

### Added
- Package dependencies:
    - `com.ugf.application`: `3.0.0-preview`.

### Changed
- Update `UGF.Application` package.

### Removed
- Package dependencies:
    - `com.ugf.module`: `0.2.0-preview`.

## [0.2.0-preview](https://github.com/unity-game-framework/ugf-module-update/releases/tag/0.2.0-preview) - 2019-11-09  

- [Commits](https://github.com/unity-game-framework/ugf-module-update/compare/0.1.0-preview...0.2.0-preview)
- [Milestone](https://github.com/unity-game-framework/ugf-module-update/milestone/2?closed=1)

### Changed
- Package dependencies:
    - `com.ugf.update`: from `3.2.0-preview` to `3.3.0-preview`.

## [0.1.0-preview](https://github.com/unity-game-framework/ugf-module-update/releases/tag/0.1.0-preview) - 2019-10-12  

- [Commits](https://github.com/unity-game-framework/ugf-module-update/compare/db413d0...0.1.0-preview)
- [Milestone](https://github.com/unity-game-framework/ugf-module-update/milestone/1?closed=1)

### Added
- This is a initial release.


