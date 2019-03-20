using System;
using System.Text.RegularExpressions;
using UnityEngine;

namespace InControl
{
	// Token: 0x020000B9 RID: 185
	public struct VersionInfo : IComparable<VersionInfo>
	{
		// Token: 0x060003FA RID: 1018 RVA: 0x00020C0C File Offset: 0x0001F00C
		public VersionInfo(int major, int minor, int patch, int build)
		{
			this.Major = major;
			this.Minor = minor;
			this.Patch = patch;
			this.Build = build;
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00020C2C File Offset: 0x0001F02C
		public static VersionInfo InControlVersion()
		{
			return new VersionInfo
			{
				Major = 1,
				Minor = 5,
				Patch = 12,
				Build = 6556
			};
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00020C68 File Offset: 0x0001F068
		internal static VersionInfo UnityVersion()
		{
			Match match = Regex.Match(Application.unityVersion, "^(\\d+)\\.(\\d+)\\.(\\d+)");
			int build = 0;
			return new VersionInfo
			{
				Major = Convert.ToInt32(match.Groups[1].Value),
				Minor = Convert.ToInt32(match.Groups[2].Value),
				Patch = Convert.ToInt32(match.Groups[3].Value),
				Build = build
			};
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00020CF0 File Offset: 0x0001F0F0
		public int CompareTo(VersionInfo other)
		{
			if (this.Major < other.Major)
			{
				return -1;
			}
			if (this.Major > other.Major)
			{
				return 1;
			}
			if (this.Minor < other.Minor)
			{
				return -1;
			}
			if (this.Minor > other.Minor)
			{
				return 1;
			}
			if (this.Patch < other.Patch)
			{
				return -1;
			}
			if (this.Patch > other.Patch)
			{
				return 1;
			}
			if (this.Build < other.Build)
			{
				return -1;
			}
			if (this.Build > other.Build)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00020D9E File Offset: 0x0001F19E
		public static bool operator ==(VersionInfo a, VersionInfo b)
		{
			return a.CompareTo(b) == 0;
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00020DAB File Offset: 0x0001F1AB
		public static bool operator !=(VersionInfo a, VersionInfo b)
		{
			return a.CompareTo(b) != 0;
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00020DBB File Offset: 0x0001F1BB
		public static bool operator <=(VersionInfo a, VersionInfo b)
		{
			return a.CompareTo(b) <= 0;
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00020DCB File Offset: 0x0001F1CB
		public static bool operator >=(VersionInfo a, VersionInfo b)
		{
			return a.CompareTo(b) >= 0;
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x00020DDB File Offset: 0x0001F1DB
		public static bool operator <(VersionInfo a, VersionInfo b)
		{
			return a.CompareTo(b) < 0;
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x00020DE8 File Offset: 0x0001F1E8
		public static bool operator >(VersionInfo a, VersionInfo b)
		{
			return a.CompareTo(b) > 0;
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00020DF5 File Offset: 0x0001F1F5
		public override bool Equals(object other)
		{
			return other is VersionInfo && this == (VersionInfo)other;
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x00020E18 File Offset: 0x0001F218
		public override int GetHashCode()
		{
			return this.Major.GetHashCode() ^ this.Minor.GetHashCode() ^ this.Patch.GetHashCode() ^ this.Build.GetHashCode();
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00020E6C File Offset: 0x0001F26C
		public override string ToString()
		{
			if (this.Build == 0)
			{
				return string.Format("{0}.{1}.{2}", this.Major, this.Minor, this.Patch);
			}
			return string.Format("{0}.{1}.{2} build {3}", new object[]
			{
				this.Major,
				this.Minor,
				this.Patch,
				this.Build
			});
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x00020EF8 File Offset: 0x0001F2F8
		public string ToShortString()
		{
			if (this.Build == 0)
			{
				return string.Format("{0}.{1}.{2}", this.Major, this.Minor, this.Patch);
			}
			return string.Format("{0}.{1}.{2}b{3}", new object[]
			{
				this.Major,
				this.Minor,
				this.Patch,
				this.Build
			});
		}

		// Token: 0x040002F2 RID: 754
		public int Major;

		// Token: 0x040002F3 RID: 755
		public int Minor;

		// Token: 0x040002F4 RID: 756
		public int Patch;

		// Token: 0x040002F5 RID: 757
		public int Build;
	}
}
