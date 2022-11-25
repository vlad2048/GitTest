using CliWrap;
using LINQPad;
using LINQPad.Controls;
using LINQPadExtras.Utils;

namespace LINQPadExtras.CmdRunning.Panels;

static class RootPanel
{
	private static DumpContainer dc = null!;

	private static void OnRestart()
	{
		dc = new DumpContainer()
			.StyleRootPanel()
			.Dump();
	}

	public static CmdPanel MakeCmdPanel(string exeFile, string args, bool showCmdOnly)
	{
		RestartDetector.OnRestart(nameof(RootPanel), OnRestart);
		var cmdPanel = new CmdPanel(exeFile, args, showCmdOnly);
		dc.AppendContent(cmdPanel.Root);
		return cmdPanel;
	}

	public static LogPanel MakeLogPanel()
	{
		RestartDetector.OnRestart(nameof(RootPanel), OnRestart);
		var logPanel = new LogPanel();
		dc.AppendContent(logPanel.Root);
		return logPanel;
	}
}