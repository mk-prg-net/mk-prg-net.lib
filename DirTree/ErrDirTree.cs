using System;

namespace DMS {	

	// Liste aller Fehlernummern
	public enum ErrNo {EnterDir, ExitDir, TouchFile, Traverse};
	/// <summary>
	/// Zusammenfassung f�r CErrDirTree.
	/// </summary>
	/// 

	// Klasse f�r Ausnahmeobjekte von CDirTree
	public class ErrDirTree : ApplicationException
	{
		public ErrNo ErrorNo;
		public string file;
		
		public ErrDirTree(ErrNo errNo)
		{
			ErrorNo = errNo;
		}

		public ErrDirTree(ErrNo errNo, string pfile)
		{
			ErrorNo = errNo;
			file = pfile;
		}
	}
	
}
