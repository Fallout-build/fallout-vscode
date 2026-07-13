# Fallout for VS Code

Explore, run, and visualize your [Fallout](https://github.com/Fallout-build/Fallout) (the NUKE successor) build targets without leaving the editor.

## Features

- **Targets view** — a dedicated Fallout container in the activity bar lists every build target, with the default target and each target's relations (`depends on`, `runs after`, `triggered by`, `triggers`) as expandable children.
- **Run a target** — inline ▶ on any target runs it in an integrated terminal (`./build.ps1` on Windows, `./build.sh` elsewhere).
- **Go to definition** — jump straight to the `Target X => ...` C# declaration; disambiguated by declaring type when several components declare the same name.
- **Build graph** — a Mermaid diagram of the whole dependency graph; click a node to run that target.
- Auto-refreshes as the build graph changes.

## Requirements

The extension reads a `build-graph.json` emitted by the Fallout build into `.fallout/temp/` (or the legacy `.nuke/temp/`). Run the build once (e.g. `./build.ps1 --plan`) to generate it.

## Versioning

The extension's `major.minor` track the Fallout framework version it targets; the patch moves independently. A mismatch between the extension and the framework your workspace builds with surfaces as a non-blocking warning.

## License

[MIT](LICENSE)
