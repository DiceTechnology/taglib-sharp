using System;
using TagLib;
using TagLib.Id3v2;

namespace AacTagModifier
{
	/// <summary>
	///		Class that contains the main method for adding ID3 tags to AAC files.
	/// </summary>
	public class AacId3TagModifier
	{
		/// <summary>
		///		Function to add ID3 transportStreamTimestamp to an AAC file.
		/// </summary>
		/// <param name="args">
		///		Array of input parameters. Should have 2 elements in it.
		///		args[0] = file location.
		///		args[1] = timestamp to be added to the file.
		/// </param>
		public static void Main (string[] args)
		{
			if (args.Length != 2) {
				Console.WriteLine ("There must be 2 command line arguments [file, timestampPts]. Only passed: [" + string.Join (",", args) + "]");
				return;
			}

			var fileLocation = args[0];
			var timestampString = args[1];

			var file = File.Create (fileLocation);
			var offsetPts = Convert.ToInt64 (timestampString);
			var bytes = BitConverter.GetBytes (offsetPts);

			if (BitConverter.IsLittleEndian)
				Array.Reverse (bytes);

			var id3v2_tag = (TagLib.Id3v2.Tag)file.GetTag (TagTypes.Id3v2, true);

			if (id3v2_tag != null) {
				Console.WriteLine ("Updating private frame with timestamp: [" + string.Join (",", bytes) + "]");
				// Get the private frame, create if necessary.
				PrivateFrame frame = PrivateFrame.Get (id3v2_tag, "com.apple.streaming.transportStreamTimestamp", true);
				frame.PrivateData = bytes;
				file.Save ();
				Console.WriteLine ("Updated ID3 tag of file: " + fileLocation);
			}
		}
	}
}
