# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

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


