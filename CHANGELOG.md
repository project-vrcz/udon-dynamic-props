# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [0.1.4] - 2026-05-07

### Fixed

- `AdaptableStationHeight` teleport player to random position.

## [0.1.3] - 2026-05-05

### Added

- New `AdaptableStationHeight` component to adjust `VRCStation` player enter position to fit player.

## [0.1.2] - 2026-05-05

### Fixed

- Fix compile error during build. [`#2`](https://github.com/project-vrcz/udon-dynamic-props/pull/2)

## [0.1.1] - 2026-05-02

### Added

- Add setup prefab.

### Changed

- Force `UdonDynamicPropsManager` sync mode to `None`.

## [0.1.0] - 2026-05-02

### Added

- Assign `PhysBoneCollider` to all player.
  - `UdonDynamicPropsSetColliders` component to add players's `PhysBoneColliders` to marked PhysBone `colliders` list during build.
- `UdonDynamicPropsToggleCollidersStationBinding` to disable player's `PhysBoneCollider` when player enter station.

[unreleased]: https://github.com/project-vrcz/udon-dynamic-props/compare/base-v0.1.4...HEAD
[0.1.4]: https://github.com/project-vrcz/udon-dynamic-props/compare/base-v0.1.3...base-v0.1.4
[0.1.3]: https://github.com/project-vrcz/udon-dynamic-props/compare/base-v0.1.2...base-v0.1.3
[0.1.2]: https://github.com/project-vrcz/udon-dynamic-props/compare/base-v0.1.1...base-v0.1.2
[0.1.1]: https://github.com/project-vrcz/udon-dynamic-props/compare/base-v0.1.0...base-v0.1.1
[0.1.0]: https://github.com/project-vrcz/udon-dynamic-props/releases/tag/base-v0.1.0
