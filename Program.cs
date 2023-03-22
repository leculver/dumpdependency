// Example Usage:
//      mkdir dump-files
//      copy-dump-files /path/to/coredump dump-files/
//      tar cjf dump-files.tar.bz2 dump-files/

using Microsoft.Diagnostics.Runtime;

if (args.Length != 2 || !File.Exists(args[0]) || !Directory.Exists(args[1]))
{
    Console.WriteLine($"usage: copy-dump-files dump_file path_to_copy_to");
    Environment.Exit(1);
}

using DataTarget dt = DataTarget.LoadDump(args[0]);
foreach (ModuleInfo module in dt.EnumerateModules())
{
    string fileName = Path.GetFileName(module.FileName);
    string dest = Path.Combine(args[1], fileName);
    File.Copy(module.FileName, dest, true);
}
