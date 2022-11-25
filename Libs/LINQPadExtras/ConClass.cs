using CliWrap;
using LINQPadExtras.CmdRunning;
using LINQPadExtras.CmdRunning.Panels;
using LINQPadExtras.Utils;
using LINQPadExtras.Utils.Exts;

namespace LINQPadExtras;

public static class Con
{
	public static string Run(string exeFile, params string[] args) => RunIn(exeFile, Path.GetDirectoryName(exeFile)!, args);

	public static string RunIn(string exeFile, string workingDirectory, params string[] args)
	{
		return Cli.Wrap(exeFile)
			.WithWorkingDirectory(workingDirectory)
			.WithArguments(args)
			.WithValidation(CommandResultValidation.None)
			.Run();
	}

	public static void DeleteFile(string file)
	{
		if (!File.Exists(file)) return;
		ShowCmd("del", $"/q {file.QuoteIFN()}");
		ProcessKiller.RunWithKillProcessRetry(
			() => File.Delete(file),
			$"delete file: '{file}'",
			file,
			false
		);
	}

	public static void DeleteFolder(string folder)
	{
		if (!Directory.Exists(folder)) return;
		ShowCmd("rmdir", $"/s /q {folder.QuoteIFN()}");
		ProcessKiller.RunWithKillProcessRetry(
			() => Directory.Delete(folder, true),
			$"delete folder: '{folder}'",
			folder,
			true
		);
	}

	public static void MakeFolder(string folder)
	{
		if (Directory.Exists(folder)) return;
		ShowCmd("mkdir", folder.QuoteIFN());
		Directory.CreateDirectory(folder);
	}

	public static void EmptyFolder(string folder)
	{
		if (Directory.Exists(folder))
		{
			if (Directory.GetFileSystemEntries(folder).Length == 0) return;
			DeleteFolder(folder);
		}

	}

	internal static void ShowCmd(string exeFile, string args) => RootPanel.MakeCmdPanel(exeFile, args, true);
}