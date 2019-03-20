using System;

namespace InControl
{
	// Token: 0x0200002A RID: 42
	public class Logger
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600014A RID: 330 RVA: 0x00006ECC File Offset: 0x000052CC
		// (remove) Token: 0x0600014B RID: 331 RVA: 0x00006F00 File Offset: 0x00005300
		public static event Logger.LogMessageHandler OnLogMessage;

		// Token: 0x0600014C RID: 332 RVA: 0x00006F34 File Offset: 0x00005334
		public static void LogInfo(string text)
		{
			if (Logger.OnLogMessage != null)
			{
				LogMessage message = new LogMessage
				{
					text = text,
					type = LogMessageType.Info
				};
				Logger.OnLogMessage(message);
			}
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00006F70 File Offset: 0x00005370
		public static void LogWarning(string text)
		{
			if (Logger.OnLogMessage != null)
			{
				LogMessage message = new LogMessage
				{
					text = text,
					type = LogMessageType.Warning
				};
				Logger.OnLogMessage(message);
			}
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00006FAC File Offset: 0x000053AC
		public static void LogError(string text)
		{
			if (Logger.OnLogMessage != null)
			{
				LogMessage message = new LogMessage
				{
					text = text,
					type = LogMessageType.Error
				};
				Logger.OnLogMessage(message);
			}
		}

		// Token: 0x0200002B RID: 43
		// (Invoke) Token: 0x06000150 RID: 336
		public delegate void LogMessageHandler(LogMessage message);
	}
}
