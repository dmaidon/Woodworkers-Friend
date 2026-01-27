# Line Ending Configuration Guide

## Problem
Git warning: "Line endings in the following files are not consistent"

## Solution Applied

### 1. Updated .gitattributes
? Added VB.NET specific rules to enforce CRLF (Windows) line endings

### 2. How to Fix Existing Files

Run these commands in Git Bash or PowerShell:

```bash
# Navigate to repository
cd "C:\VB18\WwFriend"

# Remove the Git cache (doesn't delete files)
git rm --cached -r .

# Re-add all files with new line ending rules
git add .

# Commit the normalized line endings
git commit -m "Normalize line endings to CRLF for VB.NET files"
```

### 3. Visual Studio Configuration

Ensure Visual Studio uses CRLF:

1. **Tools ? Options ? Text Editor ? Advanced**
   - Set "Default line ending" to "Windows (CR LF)"

2. **File ? Advanced Save Options**
   - Line endings: Windows (CR LF)

### 4. EditorConfig (Optional)

Create `.editorconfig` in repository root:

```ini
root = true

[*]
charset = utf-8
insert_final_newline = true
trim_trailing_whitespace = true

[*.vb]
end_of_line = crlf
indent_style = space
indent_size = 4

[*.{vbproj,sln,config,resx}]
end_of_line = crlf
```

## Understanding Line Endings

- **LF** (`\n`) - Linux/Mac line ending
- **CRLF** (`\r\n`) - Windows line ending
- **CR** (`\r`) - Old Mac line ending (rarely used)

## What .gitattributes Does

```
*.vb text eol=crlf
```
- `text` - Tells Git it's a text file
- `eol=crlf` - Always convert to CRLF on checkout
- Git will store as LF in repository, convert to CRLF when you check out

## Common Commands

```bash
# Check current line endings
git ls-files --eol

# Show files with mixed line endings
git diff --check

# Normalize all files at once
git add --renormalize .
git commit -m "Normalize line endings"

# Reset a specific file's line endings
git rm --cached <filename>
git add <filename>
```

## Prevention

With `.gitattributes` in place, Git will automatically:
- ? Convert to CRLF when checking out VB files
- ? Convert to LF when committing (standard Git storage)
- ? Prevent mixed line endings
- ? Make collaboration consistent across different OS

## Troubleshooting

### Still seeing warnings?
1. Commit the .gitattributes changes first
2. Run: `git add --renormalize .`
3. Commit: `git commit -m "Normalize line endings"`

### Want to check a specific file?
```bash
file <filename>  # Shows "CRLF" or "LF"
# Or in PowerShell:
(Get-Content <filename> -Raw) -match "`r`n"  # True = CRLF, False = LF
```

### Git config (global setting)
```bash
# Windows users (recommended)
git config --global core.autocrlf true

# Linux/Mac users
git config --global core.autocrlf input
```

## Current Configuration

? `.gitattributes` updated with VB.NET rules
? All .vb files will use CRLF
? Solution/project files will use CRLF
? Markdown files will use CRLF

## Next Steps

1. Commit the .gitattributes file
2. Run `git add --renormalize .`
3. Commit the normalized files
4. Warning should be gone!

---

**Note:** Line endings are now managed automatically. Future checkouts will have consistent CRLF endings for all VB.NET files.
