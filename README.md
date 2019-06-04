Hitai
===
[![Release](https://img.shields.io/github/release/sorashi/hitai.svg)](http://github.com/sorashi/hitai/releases/latest)
[![AppVeyor](https://ci.appveyor.com/api/projects/status/4n7f7h8v5gdnmw4v?svg=true)](https://ci.appveyor.com/project/Sorashi/hitai)

Desktop application from the practical part of [my senior thesis](thesis.pdf)
for client end-to-end encryption using a custom RSA implementation
with the ability to encrypt, decrypt, sign and verify text data, with an
automatic update mechanism and with an insight tab that should give the user
an idea of how the RSA concept functions.

# Disclaimer
This RSA implementation does not ensure privacy nor security. It also does not
declare any format specification. There exist time tested specifications
for the RSA like the [OpenPGP standard](https://tools.ietf.org/html/rfc4880),
which is implemented by time tested programs like [GPG](https://gnupg.org/).

Hitai does not use any of those, but rather implements its own experimental
way of using and practicing the ideas from the [original RSA paper](https://people.csail.mit.edu/rivest/Rsapaper.pdf).

Please do not use this program for any confidential communication as you have *no
guarantee of security nor privacy*.

# Links

- [Download](http://github.com/sorashi/hitai/releases/latest)
- [Changelog](changelog.md)