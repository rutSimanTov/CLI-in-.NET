
# File Bundler CLI 🚀

A powerful and easy-to-use Command Line Interface (CLI) tool built with .NET 8, designed to bundle multiple source code files into a single output file. This tool is perfect for creating consolidated code reviews, backups, or sharing entire projects in one document.

## Features 💎

* **Smart Bundling**: Combine files from your current directory based on specific programming languages or file extensions.
* **Interactive Response Files**: Use the `rsp` command to generate response files through an interactive prompt, making complex commands easier to run.
* **Custom Sorting**: Sort files alphabetically or by file type.
* **Code Cleanup**: Option to remove empty lines to keep the bundled file concise.
* **Metadata Support**: Automatically add the author's name and source file paths as comments within the bundled file.

## Installation ⚙️

1. Clone the repository to your local machine.
2. Navigate to the project directory.
3. Build the project using the .NET CLI:
```bash
dotnet build

```



## Commands ⌨️

### 1. `bundle` 🟢

Bundles files into a single output.

**Options:**

* `-o, --output  <path>`📥: The path and name of the resulting bundle file.
* `-l, --language <ext>` 🌐: (Required) Comma-separated list of file extensions to include (e.g., `cs,js,txt`) or `all`.
* `-n, --note` 📍: Include the original file path as a comment before its content.
* `-s, --sort  <type>`📊: Sort order: `ab` (alphabetical) or `type` (by extension). Defaults to `ab`.
* `-e, --empty`✂️: Remove empty lines from the source files.
* `-a, --author <name>`👤: Adds the developer's name at the top of the file.

### 2. `rsp` 🔵

An interactive helper that asks you questions and generates a response file (`rsp.rsp`). This allows you to run the bundle command without typing long strings of arguments.

**How to use it:**

1. Run: `prj rsp`
2. Follow the on-screen prompts.
3. Execute the generated file:
```bash
prj bundle @rsp.rsp

```



## Usage Example 💡

To bundle all C# files into a file named `myBundle.txt`, sorted by type, including file paths:

```bash
prj bundle --o myBundle.txt --l cs --s type --n

```

## Technologies Used 🛠️

* **C# / .NET 8**
* **System.CommandLine**: For robust CLI argument parsing and help generation.

