using System;
using System.Collections.Generic;
using System.Text;
using Skyline.DataMiner.Automation;
using Skyline.DataMiner.Net.Messages;

namespace Skyline.DataMiner.Utils.TableHelper.Automation
{
    internal static class TableRetrieval
    {
        public static object[] GetTableColumns(IEngine engine, int dmaId, int elementId, int tablePid, int[] columnPids)
        {
            string[] filter = TableRetrieval.GetFilterString(columnPids);
            var message = new GetPartialTableMessage(dmaId, elementId, tablePid, filter);
            var requestedTable = (ParameterChangeEventMessage)engine.SendSLNetSingleResponseMessage(message);

            if (requestedTable.NewValue.ArrayValue == null || requestedTable.NewValue.ArrayValue[0].ArrayValue == null)
            {
                return null;
            }

            int nrOfColumns = requestedTable.NewValue.ArrayValue.Length - 1; // drop the first column because the call will always return the PK, even if you don't ask for it.
            int nrOfRows = requestedTable.NewValue.ArrayValue[0].ArrayValue.Length;

            object[] columns = new object[nrOfColumns];

            // start from 1 to skip the first column because the call will always return the PK, even if you don't ask for it.
            for (int i = 1; i < nrOfColumns; i++)
            {
                object[] tempColumns = new object[nrOfRows];
                for (int j = 0; j < nrOfRows; j++)
                {
                    tempColumns[j] = requestedTable.NewValue.ArrayValue[i].ArrayValue[j].ArrayValue[0].InteropValue;
                }

                columns[i] = tempColumns;
            }

            return columns;
        }

        public static string[] GetFilterString(int[] columnPids)
        {
            return new string[] { "columns=" + String.Join(",", columnPids) };
        }
    }
}
