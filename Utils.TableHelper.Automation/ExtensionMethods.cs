namespace Skyline.DataMiner.Utils.TableHelper.Automation
{
    using DataMiner.Automation;
    using Net.Messages;
    using System;

    /// <summary>
    /// Helper functions of the MCR Swimlane solution.
    /// </summary>
    public static class ExtensionMethods
    {
        public static object[] GetTableColumns(this IEngine engine, int dmaId, int elementId, int tablePid, int[] columnPids)
        {
            return TableRetrieval.GetTableColumns(engine, dmaId, elementId, tablePid, columnPids);
        }

        public static object[] GetTableColumns(this Element element, IEngine engine, int tablePid, int[] columnPids)
        {
            return TableRetrieval.GetTableColumns(engine, element.DmaId, element.ElementId, tablePid, columnPids);
        }
    }
}