using System;
using System.Runtime.InteropServices;
using stdole;

// suppress SonarAnalyzer warnings
#pragma warning disable S101 // Types should be named in PascalCase
#pragma warning disable S1144 // Unused private types or members should be removed
#pragma warning disable S3453 // Classes should not have only "private" constructors

namespace Hjalte.InventorApiExtensions.Common
{
	// https://github.com/frankfralick/InventorAddIns/blob/master/SimpleAddIn/PictureDispConverter.cs
	// https://adndevblog.typepad.com/manufacturing/2012/06/how-to-convert-iconbitmap-to-ipicturedisp-without-visualbasiccompatibilityvb6supporticontoipicture.html
	// C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\Common7\IDE\PublicAssemblies\stdole.dll

	public sealed class PictureDispConverter
	{
		[DllImport("OleAut32.dll", EntryPoint = "OleCreatePictureIndirect", ExactSpelling = true, PreserveSig = false)]
		private static extern IPictureDisp OleCreatePictureIndirect([MarshalAs(UnmanagedType.AsAny)]
			object picdesc, ref Guid iid, [MarshalAs(UnmanagedType.Bool)]
			bool fOwn);

		static Guid iPictureDispGuid = typeof(IPictureDisp).GUID;

        private sealed class PICTDESC
        {
			private PICTDESC() { }

            public const short PICTYPE_UNINITIALIZED = -1;
            public const short PICTYPE_NONE = 0;
			public const short PICTYPE_BITMAP = 1;
			public const short PICTYPE_METAFILE = 2;
			public const short PICTYPE_ICON = 3;
			public const short PICTYPE_ENHMETAFILE = 4;

			[StructLayout(LayoutKind.Sequential)]
			public class Icon
			{
				internal int cbSizeOfStruct = Marshal.SizeOf(typeof(PICTDESC.Icon));
				internal int picType = PICTDESC.PICTYPE_ICON;
				internal IntPtr hicon = IntPtr.Zero;
				internal int unused1;
				internal int unused2;

				internal Icon(System.Drawing.Icon icon)
				{
					this.hicon = icon.ToBitmap().GetHicon();
				}
			}


			[StructLayout(LayoutKind.Sequential)]
			public class Bitmap
			{
				internal int cbSizeOfStruct = Marshal.SizeOf(typeof(PICTDESC.Bitmap));
				internal int picType = PICTDESC.PICTYPE_BITMAP;
				internal IntPtr hbitmap = IntPtr.Zero;
				internal IntPtr hpal = IntPtr.Zero;
				internal int unused;

				internal Bitmap(System.Drawing.Bitmap bitmap)
				{
					this.hbitmap = bitmap.GetHbitmap();
				}
			}
		}


		public static IPictureDisp ToIPictureDisp(System.Drawing.Icon icon)
		{
			PICTDESC.Icon pictIcon = new PICTDESC.Icon(icon);
			return OleCreatePictureIndirect(pictIcon, ref iPictureDispGuid, true);
		}


		public static IPictureDisp ToIPictureDisp(System.Drawing.Bitmap bmp)
		{
			PICTDESC.Bitmap pictBmp = new PICTDESC.Bitmap(bmp);
			return OleCreatePictureIndirect(pictBmp, ref iPictureDispGuid, true);
		}

	}
}
#pragma warning restore S3453 // Classes should not have only "private" constructors
#pragma warning restore S1144 // Unused private types or members should be removed
#pragma warning restore S101 // Types should be named in PascalCase
