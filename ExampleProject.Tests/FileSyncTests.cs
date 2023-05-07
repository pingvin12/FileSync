namespace ExampleProject.Tests;

public class FileSyncTests
{
    private readonly string _sourceDir =
        ".\\source";

    private readonly string _targetDir =
        ".\\target";

    private readonly string _testFile = "Hello.txt";

    public FileSyncTests()
    {
        // Create temporary directories for testing
        _sourceDir = Path.Combine(Path.GetTempPath(), "FileSyncToolTests", "source");
        _targetDir = Path.Combine(Path.GetTempPath(), "FileSyncToolTests", "target");
        Directory.CreateDirectory(_sourceDir);
        Directory.CreateDirectory(_targetDir);

        // Create a test file in the source directory
        _testFile = Path.Combine(_sourceDir, "Hello.txt");
        File.WriteAllText(_testFile, "Hello, world!");
    }

    [Fact]
    public void TestFileSyncToolSync()
    {
        // Create an instance of the FileSyncTool class
        var syncTool = new FileSyncTool(_sourceDir, _targetDir, true);

        // Call the Sync method
        syncTool.Sync();

        // Verify that the file was copied to the target directory
        var expectedFile = Path.Combine(_targetDir, "Hello.txt");
        Assert.True(File.Exists(expectedFile));
        Assert.Equal("Hello, world!", File.ReadAllText(expectedFile));
        Dispose();
    }

    [Fact]
    public void TestFileSyncToolSync_WhenFileExistsInTargetDirectory_ShouldOverwrite()
    {
        // Create a test file in the target directory
        var existingFile = Path.Combine(_targetDir, "Hello.txt");
        File.WriteAllText(existingFile, "This file should be overwritten.");

        // Create an instance of the FileSyncTool class
        var syncTool = new FileSyncTool(_sourceDir, _targetDir, true);
        // Call the Sync method
        syncTool.Sync();

        // Verify that the file was overwritten in the target directory
        Assert.Equal("Hello, world!", File.ReadAllText(existingFile));
        Dispose();
    }

    [Fact]
    public void TestFileSyncToolSync_WhenSourceDirectoryDoesNotExist_ShouldThrowDirectoryNotFoundException()
    {
        // Delete the source directory to simulate it not existing
        Directory.Delete(_sourceDir, true);

        // Create an instance of the FileSyncTool class
        var syncTool = new FileSyncTool(_sourceDir, _targetDir, false);

        // Call the Sync method and verify that it throws an exception
        var ex = Assert.Throws<DirectoryNotFoundException>(() => syncTool.Sync());
        Assert.Equal($"Could not find a part of the path \'{_sourceDir}\'.", ex.Message);
    }

    [Fact]
    public void TestFileSyncToolSync_WhenTargetDirectoryDoesNotExist_ShouldCreateIt()
    {
        // Delete the target directory to simulate it not existing
        Directory.Delete(_targetDir, true);

        // Create an instance of the FileSyncTool class
        var syncTool = new FileSyncTool(_sourceDir, _targetDir, true);

        // Call the Sync method
        syncTool.Sync();

        // Verify that the target directory was created
        Assert.True(Directory.Exists(_targetDir));
        Dispose();
    }

    public void Dispose()
    {
        // Delete the temporary directories and files
        Directory.Delete(_sourceDir, true);
        Directory.Delete(_targetDir, true);
    }
}
