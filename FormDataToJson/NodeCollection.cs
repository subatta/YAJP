using System.Collections.Generic;

namespace FormDataToJson
{
    public class NodeCollection : Dictionary<string, Node>
    {
        const char TypeDelimiter = '.';

        public void AddEntry(string sEntry, int wBegIndex, string sValue)
        {
            if (wBegIndex < sEntry.Length)
            {
                string sKey;
                int wEndIndex;

                wEndIndex = sEntry.IndexOf(TypeDelimiter, wBegIndex);
                if (wEndIndex == -1)
                {
                    wEndIndex = sEntry.Length;
                }
                sKey = sEntry.Substring(wBegIndex, wEndIndex - wBegIndex);
                Node oItem;
                if (!string.IsNullOrEmpty(sKey))
                {
                    if (this.ContainsKey(sKey))
                    {
                        oItem = this[sKey];
                    }
                    else
                    {
                        oItem = new Node();
                        oItem.Key = sKey;
                        oItem.Data = sValue;
                        this.Add(sKey, oItem);
                    }
                    // Now add the rest to the new item's children
                    oItem.Children.AddEntry(sEntry, wEndIndex + 1, sValue);
                }
            }
        }
    }

}
