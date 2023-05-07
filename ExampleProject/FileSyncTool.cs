using System.Security.Cryptography;

namespace ExampleProject;

public class FileSyncTool
{
    private readonly string _sourceDir;
    private readonly string _targetDir;
    private readonly bool _overwrite;

    public FileSyncTool(string sourceDir, string targetDir, bool overwrite)
    {
        _sourceDir = sourceDir;
        _targetDir = targetDir;
        _overwrite = overwrite;
    }

    public void Sync()
    {
        Console.WriteLine($"Synchronizing files from {_sourceDir} to {_targetDir}");

        // Get the list of files in the source directory
        var sourceFiles = Directory.GetFiles(_sourceDir, "*", SearchOption.AllDirectories);

        foreach (var sourceFile in sourceFiles)
        {
            // Compute the relative path of the file in the source directory
            var relativePath = GetRelativePath(sourceFile, _sourceDir).Split('/')[1];

            // Compute the target path of the file in the target directory
            var targetFile = Path.Combine(_targetDir, relativePath);

            // Check if the file already exists in the target directory
            if (File.Exists(targetFile))
            {
                // Compute the hash of the source and target files
                var sourceHash = ComputeFileHash(sourceFile);
                var targetHash = ComputeFileHash(targetFile);

                // If the hashes match, skip the file
                if (sourceHash == targetHash)
                {
                    Console.WriteLine($"Skipping '{relativePath}' (already up-to-date)");
                    continue;
                }

                // If the hashes don't match, ask the user if they want to overwrite the file
                Console.WriteLine($"The file '{relativePath}' already exists in the target directory.");
                if (_overwrite)
                {
                    Console.WriteLine($"Overwriting {relativePath}...");    
                }
                else {
                    Console.WriteLine($"Skipping '{relativePath}' (user cancelled overwrite)");
                }
            }

            // Copy the file to the target directory
            Console.WriteLine($"Copying '{relativePath}' to {_targetDir}");
            Directory.CreateDirectory(Path.GetDirectoryName(targetFile));
            File.Copy(sourceFile, targetFile, _overwrite);
        }
    }

    private static string GetRelativePath(string fullPath, string rootPath)
    {
        var rootUri = new Uri(rootPath);
        var fileUri = new Uri(fullPath);
        return rootUri.MakeRelativeUri(fileUri).ToString();
    }

    private static string ComputeFileHash(string filePath)
    {
        using var sha256 = SHA256.Create();
        using var stream = File.OpenRead(filePath);
        var hashBytes = sha256.ComputeHash(stream);
        return Convert.ToBase64String(hashBytes);
    }
}