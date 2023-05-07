namespace ExampleProject;

internal class Program
{
    private static void Main(string[] args)
    {
        // Check that the correct number of arguments were passed in
        if (args.Length != 2)
        {
            Console.WriteLine("Usage: FileSyncTool <source_directory> <target_directory> <overwrite_target>");
            Console.WriteLine("Example: FileSyncTool ./source ./target true");
            return;
        }

        var sourceDir = args[0];
        var targetDir = args[1];
	var overwriteFiles = args[2];

        // Check that the source directory exists
        if (!Directory.Exists(sourceDir))
        {
            Console.WriteLine($"Source directory '{sourceDir}' does not exist.");
            return;
        }

        // Check that the target directory exists
        if (!Directory.Exists(targetDir))
        {
            Console.WriteLine($"Target directory '{targetDir}' does not exist.");
            return;
        }

	    bool overwrite = bool.TryParse(args[2], out overwrite);

        // Initialize the synchronization tool and perform the sync
        var syncTool = new FileSyncTool(sourceDir, targetDir, overwrite);
        syncTool.Sync();

        Console.WriteLine("File synchronization complete.");
    }
}
