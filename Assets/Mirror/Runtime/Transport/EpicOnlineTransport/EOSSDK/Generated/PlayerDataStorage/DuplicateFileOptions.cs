// Copyright Epic Games, Inc. All Rights Reserved.
// This file is automatically generated. Changes to this file may be overwritten.

namespace Epic.OnlineServices.PlayerDataStorage
{
	/// <summary>
	/// Input data for the <see cref="PlayerDataStorageInterface.DuplicateFile" /> function
	/// </summary>
	public class DuplicateFileOptions
	{
		/// <summary>
		/// The Product User ID of the local user who authorized the duplication of the requested file; must be the original file's owner
		/// </summary>
		public ProductUserId LocalUserId { get; set; }

		/// <summary>
		/// The name of the existing file to duplicate
		/// </summary>
		public string SourceFilename { get; set; }

		/// <summary>
		/// The name of the new file
		/// </summary>
		public string DestinationFilename { get; set; }
	}

	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 8)]
	internal struct DuplicateFileOptionsInternal : ISettable, System.IDisposable
	{
		private int m_ApiVersion;
		private System.IntPtr m_LocalUserId;
		private System.IntPtr m_SourceFilename;
		private System.IntPtr m_DestinationFilename;

		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref m_LocalUserId, value);
			}
		}

		public string SourceFilename
		{
			set
			{
				Helper.TryMarshalSet(ref m_SourceFilename, value);
			}
		}

		public string DestinationFilename
		{
			set
			{
				Helper.TryMarshalSet(ref m_DestinationFilename, value);
			}
		}

		public void Set(DuplicateFileOptions other)
		{
			if (other != null)
			{
				m_ApiVersion = PlayerDataStorageInterface.DuplicatefileoptionsApiLatest;
				LocalUserId = other.LocalUserId;
				SourceFilename = other.SourceFilename;
				DestinationFilename = other.DestinationFilename;
			}
		}

		public void Set(object other)
		{
			Set(other as DuplicateFileOptions);
		}

		public void Dispose()
		{
			Helper.TryMarshalDispose(ref m_LocalUserId);
			Helper.TryMarshalDispose(ref m_SourceFilename);
			Helper.TryMarshalDispose(ref m_DestinationFilename);
		}
	}
}