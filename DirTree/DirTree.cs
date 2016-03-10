using System;
using System.IO;
using System.Threading;
using System.Security;
using System.Security.Permissions;
using System.Diagnostics;

namespace DMS {	
	// Basisklasse für alle Klassen, die einen rekursiven Durchlauf durch einen Dateibaum
	// durchführen
	public class DirTree 
	{
        // Ablaufverfolgung konfigurieren        
        TraceSwitch DirTreeTraceSwitch = new TraceSwitch("DirTreeTraceSwitch", "Grad der Trace- Informationen von DirTree beeinflussen");       


        //-----------------------------------------------------------------------------
        // Member zur Ausgabe des Arbeitsfortschrittes

        // Klasse mit Informationen über den Arbeitsfortschritt. 
        // Detailiertere Arbeitsfortschrittmeldungen müssen von dieser Klasse 
        // abgeleitet werden.
        public class DirTreeProgressInfo : mko.ProgressInfo
        {
            int _DirCount;
            public int DirCount
            {
                get
                {
                    return _DirCount;
                }

            }
            int _FileCount;
            public int FileCount
            {
                get
                {
                    return _FileCount;
                }
            }

            [DebuggerStepThrough]
            public DirTreeProgressInfo(int actDirCount, int actFileCount)
            {
                _DirCount = actDirCount;
                _FileCount = actFileCount;
            }
        }

        // Funktionszeigertyp von Handlern zur Behandlung des Arbeitsfortschritts- Event
		public delegate void DGEventProgress(DirTreeProgressInfo info);        

		// Ereignis: Arbeitsfortschritt
		public event DGEventProgress EventProgress;

        // Generator für Arbeitsfortschrittmeldungen: Kann in abgeleiteten Klassen 
        // überschrieben werden, um detailiertere Arbeitsfortschrittmeldungen, die von
        // DirTreeProgressInfo abgeleitet sind, zu erzeugen
        protected virtual DirTreeProgressInfo MakeProgressInfo()
        {
            return new DirTreeProgressInfo(m_dir_count, m_file_count);

        }

		//Ereignis: Scan beendet
		public event DGEventProgress EventEndScanDir;

        //-----------------------------------------------------------------------------------
        // Konstruktoren
		public DirTree()
		{              
            Debug.WriteLine("Instanz von DirTree angelegt um " + DateTime.Now.ToShortTimeString() + " Uhr");
  		}

		protected int m_file_count;
		public int FileCount 
		{
			get 
			{
				return m_file_count;
			}
		}
		protected int m_dir_count;
		public int DirCount
		{
			get 
			{
				return m_dir_count;
			}

            set //(int value)
            {
                m_dir_count = value;
            }
		}

        //---------------------------------------------------------------------------------------
		// Verarbeitungsroutinen für die einzelnen Phasen beim scannen eines Verzeichnisbaumes
		protected virtual bool BeginScanDir(string path) 
		{            
            Trace.WriteLineIf(DirTreeTraceSwitch.TraceInfo, "BeginScanDir(" + path + ")");
			return true;
		}

		protected virtual bool EndScanDir(string path)
		{
            if(DirTreeTraceSwitch.TraceInfo)
                Trace.WriteLine("EndScanDir(" + path + ")");
			return true;
		}

		protected virtual bool EnterDir(string path) 
		{
            if(DirTreeTraceSwitch.TraceInfo)
                Trace.WriteLine("EnterDir(" + path + ")");
			return true;
		}

		protected virtual bool ExitDir(string path) 
		{
            if(DirTreeTraceSwitch.TraceInfo)
                Trace.WriteLine("ExitDir(" + path + ")");
			return true;
		}

		protected virtual bool TouchFile(string path)
		{
            if(DirTreeTraceSwitch.TraceInfo)
                Trace.WriteLine("TouchFile(" + path + ")");
			return true;
		}		

		// Zurücksetzen der Zählerstände 
		public void reset() 
		{
			m_file_count = 0;
			m_dir_count = 0;
		}

        bool stop = false;

        // beendet die Ausführung 
        public void StopDirTree()
        {
            if (DirTreeTraceSwitch.TraceInfo)
                Trace.WriteLine("Dateiscann wurde vorzeitig gestoppt");
            stop = true;
        }
       
        //------------------------------------------------------------------------------------
        // Implementierung des Verzeichnisscanners

		// Routine, welche den Dateibaum rekursiv durchläuft
		protected bool traverse_exe(string root_path)
		{
			try 
			{
                if (!stop)
                {
                    // Aufrufen der Ereignisse, wenn Eventhandler registriert wurden
                    if (!EnterDir(root_path))
                        throw new ErrDirTree(ErrNo.EnterDir);

                    m_dir_count++;

                    string[] files = Directory.GetFiles(root_path);
                    m_file_count += files.Length;

                    foreach (string file in files)
                    {
                        if (!TouchFile(file))
                            throw new ErrDirTree(ErrNo.TouchFile, file);
                    }

                    string[] dirs = Directory.GetDirectories(root_path);

                    foreach (string dir in dirs)
                    {
                        if (!traverse_exe(dir))
                            return false;
                    }

                    // Arbeitsforschritt anzeigen: CallBack aufrufen

                    if (EventProgress != null)
                        EventProgress.Invoke(MakeProgressInfo());

                    if (!ExitDir(root_path))
                        throw new ErrDirTree(ErrNo.ExitDir);
                }
                else
                    return false;
			} 
			catch (ErrDirTree ex) 
			{
				switch (ex.ErrorNo) 
				{
					case ErrNo.EnterDir:
                        if (DirTreeTraceSwitch.TraceError)
                            Trace.WriteLine("Err DirTree: EnterDir im Pfad " + root_path + " abgebrochen");
                        throw;
					case ErrNo.ExitDir:
                        if (DirTreeTraceSwitch.TraceError)
						    Trace.WriteLine("Err DirTree: ExitDir im Pfad " + root_path + " abgebrochen");
                        throw;
					case ErrNo.TouchFile:
                        if (DirTreeTraceSwitch.TraceError)
						    Trace.WriteLine("Err DirTree: TouchFile im Pfad " + root_path + "\\" + ex.file + " abgebrochen");
                        throw;
				}
                throw;
			} 
			catch (Exception ex) 
			{
				Trace.WriteLine("Err DirTree: Es ist ein allgemeiner Fehler in aufgetreten: " + ex.Message);
                throw;
			}

			return true;

		}

		
		// Routine, die den rekursiven Dateibaumdurchlauf in einem gesicherten Kontext startet
		public bool scanDir(string rootPath) 
		{
                        
            // Prüfen, ob der Rufer die benötigten FileIOPermission hat
            FileIOPermissionAccess access = FileIOPermissionAccess.PathDiscovery;
            FileIOPermission fileIoPerm = new FileIOPermission(access, rootPath);
            fileIoPerm.Demand();

            Debug.Assert(Directory.Exists(rootPath), "Der Pfad " + rootPath + " existiert nicht !");

            stop = false;
			bool erfolg = false;
			try 
			{
				if(!BeginScanDir(rootPath))
					throw new Exception("ErrBeginTraverse");

				traverse_exe(rootPath);
				
				erfolg = true;
				
			} 
			catch(Exception ex) 
			{
                if(DirTreeTraceSwitch.TraceError)
                    Trace.WriteLine("Err DirTree: Es ist ein allgemeiner Fehler aufgetreten in scanDir: " + ex.Message);
                throw;
			}

			if(!EndScanDir(rootPath))
				erfolg = false;

			if (EventEndScanDir != null)
				EventEndScanDir(MakeProgressInfo());

			return erfolg;
		}  
    }	
}