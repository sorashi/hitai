# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/).

## [Unreleased]
### Added
- readme
- insight now shows a formula/hint when hovering over a field label
### Fixed
- `EnsurePositiveModulus` is now called when generating a new keypair using
`SystemAsymmetricEncryptionProvider`
- `Keypair` now holds all the private information essential for
`SystemAsymmetricEncryptionProvider`, i.e. `P`, `Q`, `DP`, `DQ` and `InverseQ`
were added. `SystemAsymmetricEncryptionProvider` was marked `PublicOnly` without
these properties and so was unable to decipher data.

## [0.1.1] - 2019-03-21
### Fixed
- cancellation of Hitai keypair generation now aborts background threads
- adjusted controls' position

## 0.1.0 - 2019-02-02
- First release

[Unreleased]: https://github.com/sorashi/hitai/compare/v0.1.1...HEAD
[0.1.1]: https://github.com/sorashi/hitai/compare/v0.1.0...v0.1.1
