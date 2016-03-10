using System;
using System.Collections.Generic;
using System.Text;

using System.Xml;

namespace mko.db
{
    public class ReturnCodeXmlException<TEnumStatus, TEnumError> : SPAdapterTemplate<TEnumStatus, TEnumError>.ReturnCode
        where TEnumStatus : struct
        where TEnumError  : struct
    {
        public string SourceUri;
        public int LineNumber;
        public int LinePosition;
        public string Msg;

        public ReturnCodeXmlException(XmlException xex, TEnumError xmlExceptionReturnCode)
            : base(xmlExceptionReturnCode)
        {
            this.SourceUri = xex.SourceUri;
            this.LineNumber = xex.LineNumber;
            this.LinePosition = xex.LinePosition;
            this.Msg = xex.Message;
        }

        public override string Description
        {
            get
            {
                if (Success)
                    return base.Description;
                else
                {
                    return string.Format("{0}\nDetails: SourceUri= {1}, Zeile= {2:D}, Spalte= {3:D}, Beschreibung= {4}", base.Description, SourceUri, LineNumber, LinePosition, Msg);
                }
            }
        }

    }
}
